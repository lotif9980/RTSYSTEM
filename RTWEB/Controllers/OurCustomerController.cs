using HMSYSTEM.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RTWEB.Models;
using RTWEB.Repository;
using RTWEB.ViewModel;
using System.Threading.Tasks;

namespace RTWEB.Controllers
{
    [Authorize]
    public class OurCustomerController : Controller
    {
        protected readonly IUnitofWork _unitofWork;

        public OurCustomerController(IUnitofWork unitofwork)
        {
            _unitofWork = unitofwork;
        }

        public IActionResult Index(int page=1, int pageSize= 10)
        {
            var data =_unitofWork.OurCustomerRepository.GetAll()
                    .OrderByDescending(d => d.Id)
                    .AsQueryable()
                    .ToPagedList(page, pageSize); ;
            return View(data);
        }

        [HttpGet]
        public IActionResult Save()
        {

            var data = _unitofWork.DomainRepository.GetAll();
            ViewBag.Domains= data;

            var model = new OurCustomerVM
            {
                Code=_unitofWork.OurCustomerRepository.GenerateNewCode(),
            };

            return View(model);

        }

        [HttpPost]
        public IActionResult Save(OurCustomerVM model)
        {
            if(model.DoaminId==null || model.CustomerName==null || model.ContactNo==null)
            {
                TempData["Message"] = "❌ Please Input Valid data";
                TempData["MessageType"] = "danger";


                var vm = _unitofWork.DomainRepository.GetAll();
                ViewBag.Domains = vm;

                var modelRe = new OurCustomerVM
                {
                    Code = _unitofWork.OurCustomerRepository.GenerateNewCode(),
                };

                return View(modelRe);
            }

           
            bool exestingName= _unitofWork.OurCustomerRepository.ExestingName(model.CustomerName, model.DoaminId);
            if (exestingName)
            {
                TempData["Message"] = "❌ Already Customer Added";
                TempData["MessageType"] = "danger";

                var vm = _unitofWork.DomainRepository.GetAll();
                ViewBag.Domains = vm;

                return View(model);
            }

            var data = new OurCustomer
            {
                Code=model.Code,
                CustomerName=model.CustomerName,
                Address=model.Address,
                ContactNo=model.ContactNo,
                DomainId=model.DoaminId
            };

            _unitofWork.OurCustomerRepository.Save(data);
            _unitofWork.Complete();

            TempData["Message"] = "✅ Saved Successfully";
            TempData["MessageType"] = "success";

            return RedirectToAction("Save");
        }

        public async Task<IActionResult> Delete(int id)
        {
            var isUsed=await _unitofWork.OurCustomerRepository.IsUsedCustomer(id);
            if (isUsed)
            {
                TempData["Message"] = "✅The customer has a transaction.";
                TempData["MessageType"] = "danger";
                return RedirectToAction("Index");
            }

            _unitofWork.OurCustomerRepository.Delete(id);
            _unitofWork.Complete();

            TempData["Message"] = "✅ Delete Successfully";
            TempData["MessageType"] = "success";
            return RedirectToAction("Index");
        }

    }
}
