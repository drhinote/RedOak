
using ClosedXML.Excel;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.IO.Packaging;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;

namespace Roi.Analysis
{

    public enum TestType
    {
        TwoSymbol = 2,
        ThreeSymbol = 3
    }

    public class LogTo
    {
        public static void Error(string message)
        {

        }

        public static void Error(Exception message)
        {

           
        }

        public static void Info(string message)
        {

        }
    }

    public static class Server
    {
        static TcpListener TcpListener = new TcpListener(IPAddress.Any, 1111);
        static HttpListener PingListener = new HttpListener();
        static Thread pingThread = new Thread(() => ListenForPing(PingListener));
        static Thread listenThread = new Thread(() => ListenForClients(TcpListener));

        static Encoding Encoding = Encoding.UTF8;
        static string dbConnectionString = "Server=tcp:redoak.database.windows.net,1433;Initial Catalog=roi;Persist Security Info=False;User ID=redoak;Password=qazwsx@1701;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
        const string templateURL = @"C:\Roi\";
        const string baseURL = @"C:\Reports\";
        static bool running = false;

        static Server()
        {
            Environment.CurrentDirectory = templateURL;
            PingListener.Prefixes.Add("http://*:1128/");
        }

        public static void Start()
        {
            Directory.CreateDirectory(baseURL + "Tests");
            Directory.CreateDirectory(baseURL + "Results");

            if (!File.Exists("connection-string.txt"))
            {
                File.WriteAllText("connection-string.txt", dbConnectionString);
                Environment.Exit(12);
            }
            else
            {
                dbConnectionString = File.ReadAllText("connection-string.txt");
            }

            if (!running)
            {
                running = true;
                pingThread.Start();
                listenThread.Start();
            }
        }

        public static void Stop()
        {
            if (running)
            {
                running = false;
                PingListener.Stop();
                TcpListener.Stop();
            }
        }

        static void ListenForPing(HttpListener listener)
        {
            listener.Prefixes.Add("http://*:1128/");
            listener.Start();
            using (listener)
            {
                while (running)
                {
                    try
                    {
                        var client = listener.GetContext();
                        if (client != null)
                            new Thread(() => HandlePing(client)).Start();
                    }
                    catch (Exception e)
                    {

                        LogTo.Error("Ping Failed " + e);
                    }
                }
            }
        }

        static void HandlePing(HttpListenerContext client)
        {
            try
            {
                var req = new StreamReader(client.Request.InputStream).ReadToEnd();
                client.Response.Close(Encoding.ASCII.GetBytes("Pong"), true);
            }
            catch (Exception e)
            {
                LogTo.Error("Ping Failed " + e);
            }
        }

        static void ListenForClients(TcpListener listener)
        {
            listener.Start();
            while (running)

            {
                try
                {
                    var client = listener.AcceptTcpClient();
                    if (client != null)
                        new Thread(c => { using ((TcpClient)c) HandleClientComm((TcpClient)c); }).Start(client);
                }
                catch (Exception e)
                {
                    LogTo.Info(e.ToString());
                }
            }
        }

        static void HandleClientComm(TcpClient tcpClient)
        {

            tcpClient.NoDelay = true;

            var clientStream = tcpClient.GetStream();
            var reader = new BinaryReader(clientStream, Encoding);
            using (var writer = new BinaryWriter(clientStream, Encoding, false))
            {
                try
                {
                    LogTo.Info(tcpClient.Client.RemoteEndPoint + " Connected.");
                    string read, company = "";
                    read = reader.ReadString();
                    if (read.Length > 30) LogTo.Info(read.Remove(30));
                    else LogTo.Info(read);
                    if (read.StartsWith("@0*^*", StringComparison.OrdinalIgnoreCase))  // Check for duplicate machines
                    {
                        company = checkMachine(read);
                        writer.Write(company);
                    }

                    if (!company.Equals("Duplicate!!!\r\n") && !company.Equals(""))
                    {
                        if (read.StartsWith("@1*^*", StringComparison.OrdinalIgnoreCase)) // Send & receive subjects
                        {
                            read = read.Replace("@1*^*", "");
                            long max = 0;
                            LogTo.Info("** Subjects");
                            using (SqlConnection myConnection = new SqlConnection(dbConnectionString))
                            {
                                using (SqlCommand myCommand = new SqlCommand())
                                {
                                    myCommand.Connection = myConnection;
                                    myConnection.Open();
                                    myCommand.CommandText = "SELECT name, dob, social, opid, uuid, num FROM subjects WHERE num > " + read + " AND company LIKE '" + company + "%'";
                                    using (SqlDataReader fetch = myCommand.ExecuteReader())
                                    {
                                        while (fetch.Read())
                                        {
                                            long num = fetch["num"] as long? ?? -1;
                                            var write = fetch["name"] + "*$^$*" +
                                                fetch["dob"] + "*$^$*" +
                                                fetch["social"] + "*$^$*" +
                                                fetch["opid"] + "*$^$*" +
                                                fetch["uuid"] + "*$^$*" +
                                                num + "\r\n";
                                            clientStream.Write(Encoding.GetBytes(write), 0, Encoding.GetByteCount(write));
                                            if (max < num)
                                            {
                                                max = num;
                                            }
                                        }
                                    }
                                }
                            }
                            writer.Write("*^*Done" + max);
                            read = reader.ReadString();
                            while (!read.Equals("*^*Done"))
                            {
                                if (!string.IsNullOrEmpty(read.Trim()))
                                {
                                    var sp = read.Split('|');
                                    AddSubject(sp, company);
                                }
                                writer.Write("D");
                            }
                        }

                        if (read.StartsWith("@2*^*", StringComparison.OrdinalIgnoreCase))  // Send and receive testers
                        {
                            using (SqlConnection myConnection = new SqlConnection(dbConnectionString))
                            {
                                using (SqlCommand myCommand = new SqlCommand())
                                {
                                    myCommand.Connection = myConnection;
                                    myConnection.Open();
                                    myCommand.CommandText = "SELECT userid, paw FROM authorized_ids WHERE company LIKE '" + company + "%' AND IsDeleted = 0";
                                    using (SqlDataReader fetch = myCommand.ExecuteReader())
                                    {
                                        while (fetch.Read())
                                        {
                                            string enc = "";
                                            foreach (char c in fetch.GetString(1).ToCharArray())
                                            {
                                                enc += (char)(((byte)c) + 1);  // very weak encoding, need to fix
                                            }
                                            var write = fetch.GetString(0) + "$" +
                                                enc + "\r\n";
                                            clientStream.Write(Encoding.GetBytes(write), 0, Encoding.GetByteCount(write));
                                        }
                                    }
                                }
                            }
                            clientStream.Write(Encoding.GetBytes("*^*Done\r\n"), 0, Encoding.GetByteCount("*^*Done\r\n"));
                        }

                        if (read.StartsWith("@3*^*", StringComparison.OrdinalIgnoreCase))  // Receive tests from the machine
                        {
                            while (!read.Contains("*^*Done"))
                            {
                                LogTo.Info("Start");
                                string dob = reader.ReadString();
                                string opid = reader.ReadString();
                                string uuid = reader.ReadString();
                                long time = long.Parse(reader.ReadString());
                                LogTo.Info("time ");

                                string history = null;
                                byte[] header = null;
                                byte[] r2 = null;
                                byte[] l2 = null;
                                byte[] r3 = null;
                                byte[] l3 = null;
                                string tester = "";
                                if (!CheckForTest(uuid, time))
                                {
                                    writer.Write(true);
                                    ReadTest(reader, company, uuid, ref opid, ref dob,
                                        time, ref header, ref history, ref r2, ref l2, ref r3, ref l3, ref tester);
                                }
                                else
                                {
                                    writer.Write(false);
                                }

                                read = reader.ReadString();
                            }
                        }

                        if (read.StartsWith("@4*^*", StringComparison.OrdinalIgnoreCase))  // Check and alter web IDs
                        {
                            read = read.Replace("@4*^*", "");
                            string[] sp = read.Split('|');
                            string newID = sp[0];
                            string oldID = sp[1];
                            string paw = sp[2];
                            bool idFound = false;

                            using (SqlConnection myConnection = new SqlConnection(dbConnectionString))
                            {
                                using (SqlCommand myCommand = new SqlCommand())
                                {
                                    myCommand.Connection = myConnection;
                                    myConnection.Open();
                                    myCommand.CommandText = "SELECT userid,company FROM authorized_ids WHERE userid='" + newID + "'";
                                    using (SqlDataReader fetch = myCommand.ExecuteReader())
                                    {
                                        if (fetch.Read())
                                        {
                                            idFound = true;
                                        }
                                    }
                                }
                                if (oldID.Equals("") && idFound)
                                {
                                    clientStream.Write(Encoding.GetBytes("Nope\r\n"), 0, Encoding.GetByteCount("Nope\r\n"));
                                }
                                else if (oldID.Equals(""))
                                {
                                    using (SqlCommand myCommand = new SqlCommand())
                                    {
                                        myCommand.Connection = myConnection;
                                        myCommand.CommandText = "IF EXISTS (SELECT * FROM authorized_ids WHERE userid='" + newID +
                                            "') UPDATE authorized_ids SET IsDeleted = 0 WHERE userid='" + newID
                                            + "' ELSE INSERT INTO authorized_ids VALUES ('" +
                                                              newID + "', '" + company + "', '" + paw + "', 0)";
                                        myCommand.ExecuteNonQuery();
                                    }
                                    clientStream.Write(Encoding.GetBytes("Ok\r\n"), 0, Encoding.GetByteCount("Ok\r\n"));
                                }
                                else if (newID.Equals("DELETE_USER"))
                                {
                                    using (SqlCommand myCommand = new SqlCommand())
                                    {
                                        myCommand.Connection = myConnection;
                                        myCommand.CommandText = "UPDATE authorized_ids SET IsDeleted = 1 WHERE userid='" + oldID + "'";
                                        myCommand.ExecuteNonQuery();
                                    }
                                    clientStream.Write(Encoding.GetBytes("Ok\r\n"), 0, Encoding.GetByteCount("Ok\r\n"));
                                }
                                else
                                {
                                    using (SqlCommand myCommand = new SqlCommand())
                                    {
                                        myCommand.Connection = myConnection;
                                        myCommand.CommandText = "UPDATE authorized_ids SET paw='" + paw + "' WHERE userid='" + newID + "'";
                                        myCommand.ExecuteNonQuery();
                                    }
                                    clientStream.Write(Encoding.GetBytes("Ok\r\n"), 0, Encoding.GetByteCount("Ok\r\n"));
                                }
                            }
                        }
                        if (read.StartsWith("@5*^*", StringComparison.OrdinalIgnoreCase))  // Send Report to client
                        {
                            read = read.Replace("@5*^*", "");
                            string[] sp = read.Split('|');
                            string uuid = sp[0];
                            long time = long.Parse(sp[1]);

                            PrepareReport(reader, writer, uuid, time, company, ".pdf", true, tcpClient);
                            return;
                        }
                    }
                    if (read.StartsWith("@6*^*", StringComparison.OrdinalIgnoreCase))  // Create report for the web
                    {
                        LogTo.Info("#6");
                        string[] sp = read.Substring(5).Split('|');
                        string user = sp[0];
                        string uuid = sp[1];
                        long time = long.Parse(sp[2]);
                        string type = sp[3];
                        string extension = ".pdf";
                        if (sp.Length > 4) extension = sp[4];

                        using (SqlConnection myConnection = new SqlConnection(dbConnectionString))
                        {
                            company = ValidateUser(user, myConnection);
                        }

                        if (string.IsNullOrWhiteSpace(company))
                        {
                            LogTo.Info("#6B");
                            var res = "-1";
                            LogTo.Info(user + " is not authorized to view reports.");
                            clientStream.Write(Encoding.GetBytes(res), 0, Encoding.GetByteCount(res));
                        }
                        else
                        {
                            LogTo.Info("#6G");
                            var name = PrepareReport(reader, writer, uuid, time, company, extension, false, tcpClient);
                            LogTo.Info("#6F");

                            using (var file = File.OpenRead(name + extension))
                            {
                                file.CopyTo(clientStream);
                            }
                        }
                        return;
                    }
                    if (read.StartsWith("@7*^*", StringComparison.OrdinalIgnoreCase))
                    {  // Web search for tests
                        read = read.Replace("@7*^*", "");
                        string[] sp = read.Split('|');

                        var user = sp[0];
                        var name = sp[1];
                        var dob = sp[2];
                        var social = sp[3];
                        var opid = sp[4];
                        var uuid = sp[5];
                        var startTime = sp[6];
                        var endTime = sp[7];

                        using (var myConnection = new SqlConnection(dbConnectionString))
                        {
                            company = ValidateUser(user, myConnection);

                            if (company.Equals(""))
                            {
                                var res = "-1";
                                LogTo.Info(user + " is not authorized to view reports.");

                                clientStream.Write(Encoding.GetBytes(res), 0, Encoding.GetByteCount(res));
                            }
                            else
                            {
                                string query =
@"SELECT ISNULL(s.name,''),ISNULL(s.dob,''),ISNULL(s.social,''),ISNULL(s.opid,''),ISNULL(s.uuid,''),ISNULL(t.time,0),ISNULL(t.tester,''),ISNULL(t.company,'')
FROM tests t LEFT JOIN subjects s ON t.uuid = s.uuid
WHERE t.company LIKE '" + company + "%'";
                                if (!string.IsNullOrWhiteSpace(name))
                                {
                                    query += " AND LOWER(s.name) LIKE LOWER('" + name + "%')";
                                }
                                if (!string.IsNullOrWhiteSpace(dob))
                                {
                                    query += " AND LOWER(s.dob) LIKE LOWER('" + dob + "%')";
                                }
                                if (!string.IsNullOrWhiteSpace(social))
                                {
                                    query += " AND LOWER(s.social) LIKE LOWER('" + social + "%')";
                                }
                                if (!string.IsNullOrWhiteSpace(opid))
                                {
                                    query += " AND LOWER(s.opid) LIKE LOWER('" + opid + "%')";
                                }
                                if (!string.IsNullOrWhiteSpace(uuid))
                                {
                                    query += " AND LOWER(s.uuid) LIKE LOWER('" + uuid + "%')";
                                }
                                if (string.IsNullOrWhiteSpace(startTime) && startTime != "0")
                                {
                                    query += " AND tests.time > " + startTime;
                                }
                                if (string.IsNullOrWhiteSpace(endTime) && endTime != "0")
                                {
                                    query += " AND tests.time < " + endTime;
                                }
                                query += " ORDER BY time DESC";
                                writer.Write(string.Join("~", GetResults(myConnection, query)));
                                return;
                            }

                        }
                    }
                    if (read.StartsWith("@8*^*", StringComparison.OrdinalIgnoreCase))
                    { // Check user for ability to sign up for an account
                        read = read.Replace("@8*^*", "");
                        using (var c = new SqlConnection(dbConnectionString))
                        {
                            writer.Write(ValidateUser(read, c));
                        }
                        return;
                    }
                    if (read.StartsWith("BEGIN_REGISTRATION"))
                    {
                        read = read.Replace("BEGIN_REGISTRATION", "");
                        string[] sp = read.Split('|');

                        var id = sp[2];
                        var comp = sp[3];
                        try
                        {
                            using (SqlConnection myConnection = new SqlConnection(dbConnectionString))
                            {
                                using (SqlCommand myCommand = new SqlCommand())
                                {
                                    myCommand.Connection = myConnection;
                                    myCommand.CommandText = String.Format("INSERT INTO machines (name, time, company) VALUES ('{0}',0,'{1}')", id, comp);
                                    myConnection.Open();
                                    myCommand.ExecuteNonQuery();
                                }
                            }
                            writer.Write(true);
                        }
                        catch (Exception e)
                        {
                            writer.Write(false);
                        }
                        return;
                    }
                }

                catch (Exception e)
                {
                    LogTo.Info(e.ToString());
                    if (clientStream.CanWrite)
                        writer.Write(e.ToString());

                }

            }

        }

        private static IEnumerable<string> GetResults(SqlConnection myConnection, string query)
        {
            using (SqlCommand myCommand = new SqlCommand(query, myConnection))
            {
                using (SqlDataReader fetch = myCommand.ExecuteReader())
                {

                    var values = new object[fetch.FieldCount];
                    while (fetch.Read())
                    {
                        fetch.GetValues(values);
                        yield return string.Join("|", values).Replace('~', '-');
                    }

                }
            }

        }

        public static string GenerateExcelData(BinaryReader clientStream, BinaryWriter outStream, string company, string uuid, long time, bool isClient, TcpClient client)
        {
            var textFileTwoSymbol = GetTextFilePath(uuid, time, company, TestType.TwoSymbol);
            var textFileThreeSymbol = GetTextFilePath(uuid, time, company, TestType.ThreeSymbol);
            LogTo.Info("GED");
            if ((!File.Exists(textFileTwoSymbol) && !GetTextAnalysis(uuid, time, TestType.TwoSymbol, textFileTwoSymbol))
            || (!File.Exists(textFileThreeSymbol) && !GetTextAnalysis(uuid, time, TestType.ThreeSymbol, textFileThreeSymbol)))
            { // Text files not available, make them
                LogTo.Info("making text files");

                string history = null;
                string dob = "";
                string opid = "";
                byte[] header = null;
                byte[] r2 = null;
                byte[] l2 = null;
                byte[] r3 = null;
                byte[] l3 = null;
                string tester = "";
                if (!isClient || CheckForTest(uuid, time))
                {

                    outStream.Write(true);
                    LogTo.Info("getting text files from database");
                    GetTestData(uuid, ref opid, ref dob, time, ref header, ref history, ref r2, ref l2, ref r3, ref l3, ref tester);
                }
                else
                {

                    outStream.Write(false);
                    LogTo.Info("getting text files from client");
                    var read = clientStream.ReadString();
                    LogTo.Info("Read: " + read);
                    //@5*^*RICE-1-00037|1469293512601
                    var sp = read.Split('|');
                    dob = sp[1];
                    opid = sp[3];
                    AddSubject(sp, company);
                    ReadTest(clientStream, company, uuid, ref opid, ref dob,
                        time, ref header, ref history, ref r2, ref l2, ref r3, ref l3, ref tester);
                }
                CreateTest(company, uuid, history, time, dob, opid, header, r2, l2, r3, l3, tester);
            }
            else
            {

                outStream.Write(true);
            }
            LogTo.Info("GEDCO");
            return CreatePDFReport(client, company, uuid, time);
        }
        static string GetFilePath(string uuid, long time, string company)
        {
            Directory.CreateDirectory($@"{baseURL}Results\{company}\");
            return $@"{baseURL}Results\{company}\{uuid}-{time}";
        }

        static string GetTextFilePath(string uuid, long time, string company, TestType type)
        {
            return $"{GetFilePath(uuid, time, company)}-{(int)type}.txt";
        }

        static void CreateTest(string company, string uuid, string history, long time, string dob, string opid, byte[] header,
            byte[] r2, byte[] l2, byte[] r3, byte[] l3, string tester)
        {
            var textFileTwoSymbol = GetTextFilePath(uuid, time, company, TestType.TwoSymbol);
            var textFileThreeSymbol = GetTextFilePath(uuid, time, company, TestType.ThreeSymbol);
            GenerateResults(uuid, opid, dob, time, header, history, r2, l2, r3, l3, tester, company, textFileTwoSymbol, textFileThreeSymbol);
            if (!File.Exists(textFileTwoSymbol))
            {
                File.WriteAllText(textFileTwoSymbol, "No data found");
            }
            if (!File.Exists(textFileThreeSymbol))
            {
                File.WriteAllText(textFileThreeSymbol, "No data found");
            }
        }

        public static string CreatePDFReport(TcpClient client, string company, string uuid, long time)
        {
            LogTo.Info("CPDF");
            var textFileTwoSymbol = GetTextFilePath(uuid, time, company, TestType.TwoSymbol);
            var textFileThreeSymbol = GetTextFilePath(uuid, time, company, TestType.ThreeSymbol);
            var resultsFileBaseName = GetFilePath(uuid, time, company);
            var pdfReportPath = resultsFileBaseName + ".pdf";
            var dropboxPath = $@"Dropbox\Apps\CloudConvert\Xlsx\";
            var completedExcelFile = $"{dropboxPath}{uuid}-{time}.xlsx";
            var convertedPdfFile = $"{uuid}-{time}.pdf";
            LogTo.Info("CPDF1");
            var template = File.Exists(company + ".xlsx") ? company + ".xlsx" : "default.xlsx";
            LogTo.Info("CPDF2");
            try
            {
                using (var stream = new MemoryStream())
                {

                    LogTo.Info("CPDF4");
                    File.OpenRead(template).CopyTo(stream);
                    stream.Position = 0;
                    LogTo.Info("copied template");
                    var doc = new XLWorkbook(stream, XLEventTracking.Enabled);

                    doc.Worksheet("Insert 2S Data File").Clear(XLClearOptions.Contents);
                    doc.Worksheet("Insert 2S Data File").SetValues(1, 1, ParseTextFile(textFileTwoSymbol));
                    doc.Worksheet("Insert 3S Data File").Clear(XLClearOptions.Contents);
                    doc.Worksheet("Insert 3S Data File").SetValues(1, 1, ParseTextFile(textFileThreeSymbol));
                    var i = 5;
                    foreach (var box in GetSportsBoxes(uuid, time, company))
                    {
                        doc.Worksheet("Past tests").SetValues(4, i, box.Item2);
                        doc.Worksheet("Past tests").SetValue(4, i - 1, box.Item1);
                        i = 19;
                    }
                    doc.CalculationOnSave = true;
                    LogTo.Info("sports");
                    //  var sbString = SerializeSportsBoxData(doc.Worksheet("Sports Output").GetValues(4, 14, 5, 21));
                    // StoreSportsBox(uuid, time, sbString);

                    doc.SaveAs(completedExcelFile);
                    LogTo.Info(completedExcelFile);
                }

                while (!File.Exists(dropboxPath + convertedPdfFile) && client.Connected) { Thread.Sleep(50); }
                while (File.Exists(dropboxPath + convertedPdfFile) && client.Connected)
                {
                    Thread.Sleep(1000);
                    try { File.Move(dropboxPath + convertedPdfFile, pdfReportPath); }
                    catch
                    {
                        try { File.Copy(dropboxPath + convertedPdfFile, pdfReportPath); break; }
                        catch
                        {
                            LogTo.Info("Attempt to send failed...");
                            return resultsFileBaseName;
                        }
                    }
                }

                return resultsFileBaseName;

                }
                catch (Exception e)
                {
                    LogTo.Info(e.ToString());
                }
            return resultsFileBaseName;
        }

        private static Regex spacesntabs = new Regex("[ \\t]+");
        private static IEnumerable<IEnumerable<string>> ParseTextFile(string textFileTwoSymbol)
        {
            return File.ReadLines(textFileTwoSymbol).Select(l => spacesntabs.Split(l.Trim()).Select(s => s.Trim()));
        }

        static void StoreSportsBox(string uuid, long time, string sportsBoxString)
        {
            using (var conn = new SqlConnection(dbConnectionString))
            {
                conn.Open();
                using (var com = conn.CreateCommand())
                {
                    com.CommandText = "UPDATE tests SET SportsTable = '" + sportsBoxString.Replace("'", "''") + "' WHERE uuid='" + uuid + "' AND time=" + time;
                    com.ExecuteNonQuery();
                }
            }
        }


        static IEnumerable<IEnumerable<object>> GetValues(this IXLWorksheet sheet, int left, int top, int right, int bottom)
        {
            return sheet.Range(top, left, bottom, right).Rows().Select(r => r.Cells().Select(c => c.Value));
        }


        static void SetValue(this IXLWorksheet sheet, int col, int row, object val)
        {
            sheet.Cell(row, col).Value = val;
        }

        static void SetValues(this IXLWorksheet sheet, int col, int row, IEnumerable<IEnumerable<object>> rows)
        {
            foreach (var r in rows)
            {
                int i = col;
                foreach (var cell in r)
                {
                    sheet.Cell(row, i).Value = cell;
                    i++;
                }
                row++;
            }
        }

        public static string ToAlpha(this int number)
        {
            string returnVal = "";
            char c = 'A';
            while (number >= 0)
            {
                returnVal = (char)(c + number % 26) + returnVal;
                number /= 26;
                number--;
            }

            return returnVal;
        }
        private static Regex letters = new Regex(@"[A-Za-z]+");
        public static bool UpdateValue(this Worksheet ws, string address, string val, CellValues type = CellValues.Error)
        {
            decimal d = 0;
            if (type == CellValues.Error) type = decimal.TryParse(val, NumberStyles.Any, NumberFormatInfo.InvariantInfo, out d) ? CellValues.Number : CellValues.String;
            bool updated = false;

            if (ws != null)
            {
                InsertCellInWorksheet(address, ws, val, type);
            }

            return updated;
        }

        private static Sheet GetWorksheet(this WorkbookPart wbPart, string sheetName)
        {
            return wbPart.Workbook.Descendants<Sheet>().FirstOrDefault(s => s.Name == sheetName);
        }

        private static Cell InsertCellInWorksheet(string cellReference, Worksheet worksheet, string val = null, CellValues type = CellValues.Error)
        {
            SheetData sheetData = worksheet.GetFirstChild<SheetData>();
            var rowIndex = GetRowIndex(cellReference);

            // If the worksheet does not contain a row with the specified row index, insert one.
            Row row;
            if (sheetData.Elements<Row>().Where(r => r.RowIndex == rowIndex).Count() != 0)
            {
                row = sheetData.Elements<Row>().Where(r => r.RowIndex == rowIndex).First();
            }
            else
            {
                row = new Row() { RowIndex = rowIndex };
                sheetData.Append(row);
            }

            // If there is not a cell with the specified column name, insert one.  
            if (row.Elements<Cell>().Any(c => c.CellReference.Value == cellReference))
            {
                var cell = row.Elements<Cell>().First(c => c.CellReference.Value == cellReference);
                if (val != null)
                {
                    if (cell.CellValue == null)
                        cell.CellValue = new CellValue { Text = val };
                    else
                        cell.CellValue.Text = val;
                    cell.DataType = type;
                }
                return cell;
            }
            else
            {
                // Cells must be in sequential order according to CellReference. Determine where to insert the new cell.
                Cell refCell = null;
                foreach (Cell cell in row.Elements<Cell>())
                {
                    if (cell.CellReference.Value.Length == cellReference.Length)
                    {
                        if (string.Compare(cell.CellReference.Value, cellReference, true) > 0)
                        {
                            refCell = cell;
                            break;
                        }
                    }
                }

                var newCell = new Cell { CellReference = cellReference };
                if (val != null)
                {
                    newCell.CellValue = new CellValue(val);
                    newCell.DataType = type;
                }
                row.InsertBefore(newCell, refCell);
                return newCell;
            }
        }
        // Return the row at the specified rowIndex located within
        // the sheet data passed in via wsData. If the row does not
        // exist, create it.
        private static Row GetRow(this SheetData wsData, UInt32 rowIndex)
        {
            if (wsData == null) return null;
            var row = wsData.Elements<Row>().
            FirstOrDefault(r => r.RowIndex.Value == rowIndex);
            if (row == null)
            {
                row = new Row();
                row.RowIndex = rowIndex;
                wsData.Append(row);
            }
            return row;
        }

        // Given an Excel address such as E5 or AB128, GetRowIndex
        // parses the address and returns the row index.
        private static UInt32 GetRowIndex(string address)
        {
            var nums = letters.Replace(address, "").Trim();
            return uint.Parse(nums, NumberStyles.Any, NumberFormatInfo.InvariantInfo);
        }


        static string SerializeSportsBoxData(IEnumerable<IEnumerable<object>> values)
        {
            return string.Join("|", values.Select(s => string.Join(",", s)));
        }

        static IEnumerable<string[]> ParseSportsBoxData(string data)
        {
            return data.Split('|').Select(col => col.Split(','));
        }


        static IEnumerable<Tuple<string, IEnumerable<string[]>>> GetSportsBoxes(string uuid, long time, string company)
        {
            using (var conn = new SqlConnection(dbConnectionString))
            {
                conn.Open();
                using (var com = conn.CreateCommand())
                {
                    com.CommandText = "SELECT TOP 2 SportsTable, time FROM tests WHERE uuid='" +
                        uuid + "' AND time < " + time + " ORDER BY time DESC";
                    using (var read = com.ExecuteReader())
                    {
                        while (read.Read())
                        {
                            if (!read.IsDBNull(0))
                                yield return new Tuple<string, IEnumerable<string[]>>(TestDateTime(read.IsDBNull(1) ? 0 : read.GetInt64(1), true), ParseSportsBoxData(read.GetString(0)));
                        }
                    }
                }
            }
        }

        static void SendPDF(NetworkStream clientStream, byte[] report, bool isClient)
        {
            var count = report.Length.ToString() + (isClient ? "\r\n" : "");
            clientStream.Write(Encoding.GetBytes(count), 0, Encoding.GetByteCount(count));
            clientStream.Read(new byte[10], 0, 10);
            clientStream.Write(report, 0, report.Length);
        }

        static string ValidateUser(string user, SqlConnection myConnection)
        {
            string company = "";
            var sp = user.Split('|');
            string cmd;
            if (sp.Length > 1)
            {
                cmd = "SELECT company FROM authorized_ids WHERE userid='" + sp[0] + "' AND paw='" + sp[1] + "' AND IsDeleted = 0";
            }
            else
            {
                cmd = "SELECT company FROM authorized_ids WHERE userid='" + user + "' AND IsDeleted = 0";
            }
            using (SqlCommand myCommand = new SqlCommand(cmd))
            {
                myCommand.Connection = myConnection;
                myConnection.Open();
                using (SqlDataReader fetch = myCommand.ExecuteReader())
                {
                    if (fetch.HasRows)
                    {
                        while (fetch.Read())
                        {
                            company = fetch.GetString(0);
                        }
                    }
                }
            }
            company = company.Trim();
            return company;
        }

        static void ReadTest(BinaryReader clientStream, string company, string uuid,
            ref string opid, ref string dob, long time, ref byte[] header, ref string history,
            ref byte[] r2, ref byte[] l2, ref byte[] r3, ref byte[] l3, ref string tester)
        {

            header = clientStream.ReadBytes(clientStream.ReadInt32());
            history = clientStream.ReadString();
            r2 = clientStream.ReadBytes(clientStream.ReadInt32());
            l2 = clientStream.ReadBytes(clientStream.ReadInt32());
            r3 = clientStream.ReadBytes(clientStream.ReadInt32());
            l3 = clientStream.ReadBytes(clientStream.ReadInt32());
            tester = clientStream.ReadString();
            StoreTestData(uuid, opid, dob, time, header, history, r2, l2, r3, l3, tester, company);
        }

        private static bool CheckForTest(string uuid, long time)
        {
            using (SqlConnection myConnection = new SqlConnection(dbConnectionString))
            {
                string query = "SELECT * FROM tests WHERE uuid='" + uuid + "' AND time=" + time;
                using (SqlCommand myCommand = new SqlCommand(query))
                {
                    myCommand.Connection = myConnection;
                    myConnection.Open();
                    using (SqlDataReader fetch = myCommand.ExecuteReader())
                    {
                        return fetch.HasRows;
                    }
                }
            }
        }

        public static string PrepareReport(BinaryReader reader, BinaryWriter writer, string uuid, long time, string company, string extension, bool isHandset, TcpClient client)
        {
            return GenerateExcelData(reader, writer, company, uuid, time, isHandset, client);
        }

        static void StorePDFFile(string uuid, long time, string type, byte[] pdf)
        {
            // We will use a local cache or Azure Blob to store these instead
            //if (pdf == null || pdf.Length < 1) return;
            //using (SqlConnection connection = new SqlConnection(dbConnectionString))
            //{
            //    using (SqlCommand command = new SqlCommand())
            //    {
            //        command.Connection = connection;
            //        command.CommandText = "UPDATE tests SET pdf" + type + " = @p1 WHERE uuid='" + uuid + "' AND time='" + time + "'";
            //        var pdfParam = new SqlParameter("p1", SqlDbType.VarBinary, pdf.Length);
            //        pdfParam.Value = pdf;
            //        command.Parameters.Add(pdfParam);
            //        connection.Open();
            //        command.ExecuteNonQuery();
            //    }
            //}
        }

        static bool GetTextAnalysis(string uuid, long time, TestType type, string path)
        {
            var sb = new StringBuilder("SELECT ").Append(type).Append(" FROM tests WHERE uuid='")
                .Append(uuid).Append("' AND time='").Append(time).Append("'");
            using (var conn = new SqlConnection(dbConnectionString))
            {
                using (var com = conn.CreateCommand())
                {
                    com.CommandText = sb.ToString();
                    conn.Open();
                    using (var read = com.ExecuteReader())
                    {
                        while (read.Read())
                        {
                            if (!read.IsDBNull(0))
                            {
                                File.WriteAllText(path, read.GetString(0));
                                return true;
                            }
                        }
                    }
                    conn.Close();
                }
            }
            return false;
        }

        static void StoreTextAnalysis(string uuid, long time, TestType type, string textFilePath)
        {
            var text = new StringBuilder(File.ReadAllText(textFilePath)).Replace("'", "''");
            var sb = new StringBuilder("UPDATE tests SET ").Append(type).Append(" = '").Append(text)
                .Append("' WHERE  uuid='").Append(uuid).Append("' AND time='").Append(time).Append("'");
            using (var conn = new SqlConnection(dbConnectionString))
            {
                using (var com = conn.CreateCommand())
                {
                    com.CommandText = sb.ToString();
                    conn.Open();
                    com.ExecuteNonQuery();
                    conn.Close();
                }
            }
        }

        static void GetTestData(string uuid, ref string opid, ref string dob, long time, ref byte[] header, ref string history,
            ref byte[] r2, ref byte[] l2, ref byte[] r3, ref byte[] l3, ref string tester)
        {
            using (SqlConnection myConnection = new SqlConnection(dbConnectionString))
            {
                using (SqlCommand myCommand = new SqlCommand())
                {
                    myCommand.Connection = myConnection;
                    myConnection.Open();
                    myCommand.CommandText = "SELECT hardware,history,right2,left2,right3,left3,tester,dob,opid " +
                        "FROM tests WHERE uuid='" + uuid + "' AND time=" + time;
                    using (SqlDataReader fetch = myCommand.ExecuteReader())
                    {
                        while (fetch.Read())
                        {
                            byte[] buffer = new byte[1048576];
                            long size = fetch.GetBytes(0, 0, buffer, 0, 1048576);
                            header = new byte[size];
                            Array.Copy(buffer, header, size);

                            history = fetch.GetString(1);

                            size = fetch.GetBytes(2, 0, buffer, 0, 1048576);
                            r2 = new byte[size];
                            Array.Copy(buffer, r2, size);

                            size = fetch.GetBytes(3, 0, buffer, 0, 1048576);
                            l2 = new byte[size];
                            Array.Copy(buffer, l2, size);

                            size = fetch.GetBytes(4, 0, buffer, 0, 1048576);
                            r3 = new byte[size];
                            Array.Copy(buffer, r3, size);

                            size = fetch.GetBytes(5, 0, buffer, 0, 1048576);
                            l3 = new byte[size];
                            Array.Copy(buffer, l3, size);

                            tester = fetch.GetString(6);

                            dob = fetch.GetString(7);

                            opid = fetch.GetString(8);
                        }
                    }
                }
            }
        }

        static void StoreTestData(string uuid, string opid, string dob, long time, byte[] header, string history,
            byte[] r2, byte[] l2, byte[] r3, byte[] l3, string tester, string company)
        {
            using (SqlConnection connection = new SqlConnection(dbConnectionString))
            {
                using (SqlCommand command = new SqlCommand(
                    "INSERT INTO tests (uuid, time, dob, hardware, history, right2, left2, right3, left3, tester, company, opid) VALUES (@p1, @p2, @p3, @p4, @p5, @p6, @p7, @p8, @p9, @p10, @p11, @p12);", connection))
                {
                    command.Parameters.AddWithValue("p1", uuid);
                    command.Parameters.AddWithValue("p2", time);
                    command.Parameters.AddWithValue("p3", dob);
                    command.Parameters.AddWithValue("p4", header);
                    command.Parameters.AddWithValue("p5", history);
                    command.Parameters.AddWithValue("p6", r2);
                    command.Parameters.AddWithValue("p7", l2);
                    command.Parameters.AddWithValue("p8", r3);
                    command.Parameters.AddWithValue("p9", l3);
                    command.Parameters.AddWithValue("p10", tester);
                    command.Parameters.AddWithValue("p11", company);
                    command.Parameters.AddWithValue("p12", opid);
                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }

        static void GenerateResults(string uuid, string opid, string dob, long time, byte[] header, string history,
            byte[] r2, byte[] l2, byte[] r3, byte[] l3, string tester, string company, string twoHandPath, string threeHandPath)
        {

            Directory.CreateDirectory(baseURL + "Tests\\" + company);
            Directory.CreateDirectory(baseURL + "Results\\" + company);


            if (opid.Equals(""))
            {
                opid = "None";
            }

            StartAnalysisForTestType(uuid, opid, dob, time, header, history, r2, l2,
                tester, company, twoHandPath, TestType.TwoSymbol);

            StartAnalysisForTestType(uuid, opid, dob, time, header, history, r3, l3,
                tester, company, threeHandPath, TestType.ThreeSymbol);
        }

        private static object _analysisLock = new object();

        static void StartAnalysisForTestType(string uuid, string opid, string dob, long time, byte[] header, string history,
            byte[] rightHandData, byte[] leftHandData,
            string tester, string company, string analysisDataPath, TestType type)
        {

            Process app = null;
            var testFolder = baseURL + "Tests\\";
            var testDataPath = company + "\\" + uuid + '-' + time + "-" + (int)type + ".roi";
            if (rightHandData.Length > 0 || leftHandData.Length > 0)
            {
                header[0] = 1;
                app = StartAnalysis(header, rightHandData, leftHandData, time, history, tester, opid, dob, testDataPath, testFolder);
            }
            if (app != null)
            {
                if (app.WaitForExit(10000))
                {
                    StoreTextAnalysis(uuid, time, type, analysisDataPath);
                }
                else
                {
                    app.Kill();
                    Thread.Sleep(5000);
                }
            }
        }

        static Process StartAnalysis(byte[] header, byte[] r2, byte[] l2, long time, string history,
            string tester, string opid, string dob, string testDataPath, string testFolder)
        {
            byte[] test = new byte[header.Length + r2.Length + l2.Length];
            header.CopyTo(test, 0);
            r2.CopyTo(test, header.Length);
            l2.CopyTo(test, header.Length + r2.Length);
            string path = testFolder + testDataPath;
            LogTo.Info(path);
            System.IO.File.WriteAllBytes(path, test);
            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.FileName = templateURL + "\\Dropbox\\builds\\Report - end\\Report - end\\Application.exe";
            startInfo.Arguments = " \"" + testDataPath + "\" \"" + TestDateTime(time) + "\" \""
                + dob + "\" \"" + opid + "\" " + "\"" + history + "tester*{&}$" + tester + "${&}*\" \"" + baseURL + "\"";
            return Process.Start(startInfo);
        }

        static string TestDateTime(long javaTime, bool dateOnly = false)
        {
            var epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Local);
            epoch = epoch.AddHours(TimeZoneInfo.Local.GetUtcOffset(DateTime.Now).Hours);
            return epoch.AddMilliseconds(javaTime).ToString(dateOnly ? "MM/dd/yyyy" : "MM/dd/yyyy hh:mm tt");
        }

        static byte[] GetBytes(string str)
        {
            byte[] bytes = new byte[str.Length * sizeof(char)];
            System.Buffer.BlockCopy(str.ToCharArray(), 0, bytes, 0, bytes.Length);
            return bytes;
        }

        static void AddSubject(string[] subject, string company)
        {
            using (SqlConnection myConnection = new SqlConnection(dbConnectionString))
            {
                using (SqlCommand myCommand = new SqlCommand())
                {
                    myCommand.Connection = myConnection;
                    myConnection.Open();

                    myCommand.CommandText = "SELECT * FROM subjects WHERE uuid='" + subject[4] + "'";

                    bool alreadyIn = false;
                    using (SqlDataReader fetch = myCommand.ExecuteReader())
                    {
                        alreadyIn = fetch.HasRows;
                    }
                    if (alreadyIn)
                    {
                        myCommand.CommandText = "UPDATE subjects SET name='" + subject[0] +
                                    "',dob='" + subject[1] +
                                    "',social='" + subject[2] +
                                    "',opid='" + subject[3] +
                                    "',company='" + company +
                                    "',num=" + DateTime.UtcNow.ToFileTime() +
                                    " WHERE uuid='" + subject[4] + "'";
                    }
                    else
                    {
                        myCommand.CommandText = "INSERT INTO subjects (name, dob, social, opid, uuid, company, num) VALUES ('" +
                            subject[0] + "','" +
                            subject[1] + "','" +
                            subject[2] + "','" +
                            subject[3] + "','" +
                            subject[4] + "','" +
                            company + "', " +
                            DateTime.UtcNow.ToFileTime() + ")";
                    }
                    myCommand.ExecuteNonQuery();
                }
            }
            return;
        }

        static string checkMachine(string machine)
        {
            string company = "Duplicate!!!";
            using (SqlConnection myConnection = new SqlConnection(dbConnectionString))
            {
                int pos1 = machine.IndexOf("|", StringComparison.OrdinalIgnoreCase);
                string name = machine.Substring(5, pos1 - 5);
                pos1++;
                int pos2 = machine.IndexOf("|", pos1, StringComparison.OrdinalIgnoreCase);
                long oldTime = long.Parse(machine.Substring(pos1, pos2 - pos1));
                pos2++;
                string newTime = machine.Substring(pos2).Trim();
                using (SqlCommand myCommand = new SqlCommand())
                {
                    myCommand.Connection = myConnection;
                    myCommand.CommandText = "SELECT time, company FROM machines WHERE name ='" + name + "'";
                    myConnection.Open();
                    bool found = false;
                    bool exists = false;
                    bool first = false;
                    long? licenseTime = null;
                    using (var read = myCommand.ExecuteReader())
                    {
                        if (read.Read())
                        {
                            exists = true;
                            licenseTime = read["time"] as long?;
                            found = true;// licenseTime == oldTime;
                            first = true;// licenseTime == 0;
                            company = read["company"] as string;
                        }
                    }
                    if (exists && !found && !first)
                    {
                        LogTo.Info("Handset log in failed - Duplicate");
                        return "Duplicate!!!";
                    }

                    if (!exists)
                    {
                        LogTo.Info("Handset log in failed - Not found");
                        return "Duplicate!!!";
                    }

                    if (found || first)
                    {
                        //using (var cmd = new SqlCommand("UPDATE machines SET time =" + newTime + " WHERE name='" + name + "'", myConnection))
                        //{
                        //    cmd.ExecuteNonQuery();
                        //}
                        //LogTo.Info(myCommand.CommandText);
                        return company;
                    }
                    LogTo.Info("Handset log in failed - default case");
                    return "Duplicate!!!";
                }
            }
        }


    }
}
