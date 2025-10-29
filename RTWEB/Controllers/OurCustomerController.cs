using HMSYSTEM.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using RTWEB.Models;
using RTWEB.Repository;
using RTWEB.ViewModel;
using System.Diagnostics;
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

        public IActionResult Index()
        {
            var data = _unitofWork.OurCustomerRepository.GetAll()
                    .OrderByDescending(d => d.Id)
                    .AsQueryable();
                    
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

           
            bool exestingName= _unitofWork.OurCustomerRepository.ExestingName(model.ContactNo);
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


        [HttpGet]
        public IActionResult Edit(int id)
        {
            var data = _unitofWork.OurCustomerRepository.GetById(id);
            var ourCustomer = new OurCustomerVM
            {
                Id=data.Id,
                Code=data.Code,
                CustomerName=data.CustomerName,
                Address=data.Address,
                ContactNo=data.ContactNo,
                //CreateDate=data.CreateDate,
                DoaminId=data.DomainId
            };


            ViewBag.DomainList = _unitofWork.DomainRepository.GetAll()
                          .Select(x => new SelectListItem
                          {
                              Value = x.Id.ToString(),
                              Text = x.DomainName
                          }).ToList();

            return View(ourCustomer);
        }

        [HttpPost]
        public IActionResult Update(OurCustomerVM model)
        {
            if (model.DoaminId == null || model.CustomerName == null || model.ContactNo == null)
            {
                TempData["Message"] = "❌ Please Input Valid data";
                TempData["MessageType"] = "danger";



                ViewBag.DomainList = _unitofWork.DomainRepository.GetAll()
                              .Select(x => new SelectListItem
                              {
                                  Value = x.Id.ToString(),
                                  Text = x.DomainName
                              }).ToList();

                return View("Edit",model);
            }


            bool exestingName = _unitofWork.OurCustomerRepository.ExestingName(model.ContactNo);
            if (exestingName)
            {
                TempData["Message"] = "❌ Already Customer Added";
                TempData["MessageType"] = "danger";

                var vm = _unitofWork.DomainRepository.GetAll();
                ViewBag.Domains = vm;

                return View("Edit",model);
            }

            var entity = new OurCustomer
            {
                Id = model.Id,
                Code = model.Code,
                CustomerName = model.CustomerName,
                ContactNo = model.ContactNo,
                Address = model.Address,
                DomainId = model.DoaminId
            };


            _unitofWork.OurCustomerRepository.Update(entity);
            _unitofWork.Complete();

            TempData["Message"] = "✅ Saved Successfully";
            TempData["MessageType"] = "success";

            return RedirectToAction("Index");
        }

    }
}
