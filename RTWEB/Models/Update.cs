namespace RTWEB.Models
{
    public class Update
    {
        public int Id { get; set; }
        public int ? DomainId { get; set; }
        public DateTime UpdateDate { get; set; }
        public string ? BranchName { get; set; }
        public int ? DeveloperId {  get; set; }
        public int ? TesterId { get; set; }
        public int? Status { get; set; }
    }
}
