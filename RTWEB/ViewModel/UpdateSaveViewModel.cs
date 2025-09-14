namespace RTWEB.ViewModel
{
    public class UpdateSaveViewModel
    {
        public int Id { get; set; }
        public int? DomainId { get; set; }
        public DateTime? UpdateDate { get; set; }
        public string? BranchName { get; set; }
        public int? DeveloperId { get; set; }
        public int? TesterId { get; set; }
        public int? Status { get; set; }

        public List<UpdateDetailsVM> UpdateDetails {  get; set; }=new List<UpdateDetailsVM>();
    }

    //public class UpdateDetailsVM
    //{
    //    public int Id { get; set; }
    //    public int UpdateId { get; set; }
    //    public int IssueId { get; set; }
    //}
}
