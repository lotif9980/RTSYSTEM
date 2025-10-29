using RTWEB.Models;
using RTWEB.ViewModel;

namespace RTWEB.Repository
{
    public interface IOurCustomerRepository
    {
        public IEnumerable<OurCustomerVM> GetAll();
        public IEnumerable<OurCustomer> CustomerList();
        public void Save(OurCustomer customer);
        public OurCustomer? GetLastCustomer();
        public string GenerateNewCode();
        public void Delete(int id);
        public Task<bool>IsUsedCustomer(int id);
        public bool ExestingName(string contactNo, int? id = null);
        public OurCustomer GetById(int id);
        public void Update(OurCustomer customer);
    }
}
