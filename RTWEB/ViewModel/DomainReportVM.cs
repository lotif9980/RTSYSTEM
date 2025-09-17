namespace RTWEB.ViewModel
{
    public class DomainReportVM
    {
        public int Id { get; set; }
        public string? DomainName { get; set; }
        public DateTime? DateTime { get; set; }
        public string DeveloperName {  get; set; }
        public string TesterName { get; set; }
        public string BranchName {  get; set; }

    }
}
