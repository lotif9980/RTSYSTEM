namespace RTWEB.ViewModel
{
    public class UpdateVM
    {
      
        public int Id {  get; set; }
        public string? DomainName { get; set; }
        public DateTime UpdateDate { get; set; }
        public string? BranchName { get; set; }
        public string? DeveloperName { get; set; }
        public string? TesterName { get; set; }
        public int? Status { get; set; }
    }
}
