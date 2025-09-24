using RTWEB.Enum;


namespace RTWEB.Models
{
    public class Domain
    {
        public int Id { get; set; }
        public string  DomainName { get; set; }
        //public int? DomainType { get; set; }
        public bool? Status { get; set; } = true;
        public DomainEnum DomainType { get; set; }
        public string? UpdateBranch {  get; set; }
        public DateTime? LastUpdateDate { get; set; }
        public int? ProjectId {  get; set; }
   
    }
}
