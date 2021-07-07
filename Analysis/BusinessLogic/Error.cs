using System;
using System.Diagnostics;

namespace Roi.Data
{
    static public class Error
	{
	//	static private string _path = @"c:\Logs\Error.txt";

		static private Exception _exception;

		static private string _extra = "";

		static public void LogError(Exception e)
		{
			_exception = e;
			logError();
		}

		static public void LogError(Exception e, string extra)
		{
			_exception = e;
			_extra = extra;
			logError();
		}

		static private void logError()
		{
			// Get stack trace for the exception with source file information
			var st = new StackTrace(_exception, true);
			// Get the top stack frame
			var frame = st.GetFrame(0);
			// Get the line number from the stack frame
			var line = frame.GetFileLineNumber();

			string errorLog = DateTime.Now.ToString("MM/dd/yyyy hh:mm tt") + "  ";

			errorLog += "Line number: " + line.ToString() + "  " + _extra;

			errorLog += "  " + _exception.Message;

			errorLog += _exception.InnerException != null ? "  " + _exception.InnerException.ToString() : "";

			errorLog += Environment.NewLine;

            //		File.AppendAllText(_path, errorLog);
            Console.WriteLine(errorLog);
            
		}
	}
}
