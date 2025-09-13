using RTWEB.Data;
using RTWEB.Models;
using RTWEB.ViewModel;

namespace RTWEB.Repository
{
    public class UpdateRepository : IUpdateRepository
    {
        protected readonly Db _db;
        public UpdateRepository(Db db)
        {
            _db = db;
        }

        public IEnumerable<UpdateVM> GetUpdates()
        {
            var data =(from up in _db.Updates
                       join de in _db.Domains on up.DomainId equals de.Id 
                       join dev in _db.Teams on up.DeveloperId equals dev.Id
                       join te in _db.Teams on up.TesterId equals te.Id
                       select new UpdateVM
                       {
                        DomainName=de.DomainName,
                        UpdateDate=up.UpdateDate,
                        BranchName=up.BranchName,
                        DeveloperName=dev.Name,
                        TesterName=te.Name
                       }).ToList();

            return data;
        }
    }
}
