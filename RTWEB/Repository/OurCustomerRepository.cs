using RTWEB.Data;
using RTWEB.Models;
using RTWEB.ViewModel;

namespace RTWEB.Repository
{
    public class OurCustomerRepository : IOurCustomerRepository
    {
        protected readonly Db _db;
        public OurCustomerRepository(Db db)
        {
            _db = db;
        }

        
        public IEnumerable<OurCustomerVM> GetAll()
        {
            var data =(from cus in _db.OurCustomers
                       join dom in _db.Domains on cus.DomainId equals dom.Id
                       select new OurCustomerVM
                       {
                           Id = cus.Id,
                           Code=cus.Code,
                           CustomerName=cus.CustomerName,
                           Address=cus.Address,
                           DomainName=dom.DomainName,
                           ContactNo=cus.ContactNo
                           
                       }).ToList();

            return data;
        }

        public IEnumerable<OurCustomer> CustomerList()
        {
            return _db.OurCustomers;
        }

        public void Save(OurCustomer customer)
        {
            _db.Add(customer);
        }

        public OurCustomer? GetLastCustomer()
        {
           return _db.OurCustomers.OrderByDescending(d=>d.Id).FirstOrDefault();
        }

        public string GenerateNewCode()
        {
            var lastcustomer=GetLastCustomer();
            string newCode = "1";

            if(lastcustomer !=null && int.TryParse(lastcustomer.Code,out int lastCode))
            {
                newCode=(lastCode +1 ).ToString("D5");
            }

            return newCode;
        }


    }
}
