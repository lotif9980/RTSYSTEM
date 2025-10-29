using RTWEB.Enum;
using System.ComponentModel.DataAnnotations.Schema;

namespace RTWEB.Models
{
    public class Team
    {
        public int Id { get; set; }
        public string Name { get; set; }
        //public int Status { get; set; }
        public TeamType Status {  get; set; }
      
    }
}
