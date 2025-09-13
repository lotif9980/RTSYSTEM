using Microsoft.AspNetCore.Mvc;
using RTWEB.Data;
using RTWEB.Models;
using RTWEB.Repository;
using RTWEB.ViewModel;

namespace RTWEB.Controllers
{
    public class UpdateController : Controller
    {
        protected readonly IUnitofWork _unitofwork;
        public UpdateController(IUnitofWork unitOfWork)
        {
            _unitofwork = unitOfWork;
        }


        public IActionResult Index()
        {
            var data = _unitofwork.UpdateRepository.GetUpdates();
            return View(data);
        }

        public IActionResult Save()
        {
            var data = new UpdateSaveVM
            {
                Update=new Update(),
                Tester=_unitofwork.TeamRepository.GetTester() ,
                Developer=_unitofwork.TeamRepository.GetDeveleper() 
            };
            return View(data);
        }
    }
}
