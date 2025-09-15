using Microsoft.EntityFrameworkCore;
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
                        Id=up.Id,
                        DomainName=de.DomainName,
                        UpdateDate=up.UpdateDate ?? DateTime.Now,
                        BranchName=up.BranchName,
                        DeveloperName=dev.Name,
                        TesterName=te.Name
                       }).ToList();

            return data;
        }

        public void Save(Update update)
        {
            _db.Updates.Add(update);
            _db.SaveChanges();
        }

        public void Delete(int id)
        {
           var data =_db.Updates.Include(p=>p.UpdateDetails).FirstOrDefault(p=>p.Id==id);

            _db.UpdateDetails.RemoveRange(data.UpdateDetails);
            _db.Updates.Remove(data);
            _db.SaveChanges();
        }

        public UpdateVM GetDetails(int id)
        {
            var data = (from up in _db.Updates
                        join team in _db.Teams on up.TesterId equals team.Id
                        join dom in _db.Domains on up.DomainId equals dom.Id
                        join en in _db.Teams on up.DeveloperId equals en.Id
                        where up.Id == id
                        select new UpdateVM
                        {
                           Id=up.Id,
                           DomainName=dom.DomainName,
                           TesterName=team.Name,
                           DeveloperName=en.Name,
                           BranchName=up.BranchName,
                           UpdateDate=up.UpdateDate?? DateTime.Now,

                        }).FirstOrDefault();

            data.UpdateDetails=(from upd in _db.UpdateDetails
                                join iss in _db.Issues on upd.IssueId equals iss.Id
                                where upd.UpdateId==id 
                                select new UpdateDetailsVM
                                {
                                    IssueName = iss.Title,
                                    UpdateId=upd.UpdateId
                                }).ToList();

            return data;
        }

        public IEnumerable<UpdateDetail> GetbyUpdateId(int id)
        {
            return _db.UpdateDetails.Where(p => p.UpdateId == id).ToList();
        }
    }
}
