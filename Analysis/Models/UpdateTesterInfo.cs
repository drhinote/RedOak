namespace Roi.Analysis.Api.Models
{
    public class UpdateTesterInfo
    {
        public string oldName { get; set; }
        public string newName { get; set; }
        public string paw { get; set; }
        public int status { get; set; } = 0;
    }
}