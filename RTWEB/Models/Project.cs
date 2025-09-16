namespace RTWEB.Models
{
    public class Project
    {
        public int Id { get; set; } 
        public string ProjectName { get; set; }
        public String? UpdateBranch { get; set; }
        public DateTime? LastUpdateDate { get; set; }

    }
}
