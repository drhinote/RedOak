namespace Roi.Data.Models
{
    public static class Connection
	{
		public static string ConnectionString { get { return @"Server=tcp:redoak.database.windows.net,1433;Initial Catalog=roi;Persist Security Info=False;User ID=redoak;password=qazwsx@1701;;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;MultipleActiveResultSets=True;"; } }
	}
}
