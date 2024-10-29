using Microsoft.AspNetCore.Mvc;
using caseCRM.Service.Interfaces;
using caseCRM.Entities.DTOs;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Azure;

namespace caseCRM.WebApp.Controllers
{
    [Authorize(Policy = "RequireSuperAdminRole", Roles = "superadmin")]
    public class CustomerController : Controller
    {
        private readonly ICustomerService _customerService;

        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        public async Task<IActionResult> Index()
        {
            var response = await _customerService.GetAllCustomersAsync();
            if (response.IsSuccessful)
            {
                return View(response.Data);
            }
            return View("Error", response.Message);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CustomerDto customerDto)
        {
            if (ModelState.IsValid)
            {
                var response = await _customerService.CreateCustomerAsync(customerDto);
                if (response.IsSuccessful)
                {
                    return Json(response);
                }
                else
                {
                    return Json(ResponseDto<EmptyDto>.Fail(response.Message, 500));
                }
            }
            else
            {
                var errors = ModelState.Values
                     .SelectMany(v => v.Errors)
                     .Select(e => e.ErrorMessage)
                     .ToList();

                return Json(ResponseDto<EmptyDto>.Fail(string.Join(", ", errors), 400));
            }
        }

        public async Task<IActionResult> Edit(string id)
        {
            var response = await _customerService.GetCustomerByIdAsync(id);
            if (response.IsSuccessful)
            {
                return View(response.Data);
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<JsonResult> Edit(string id, CustomerDto customerDto)
        {
            if (ModelState.IsValid)
            {
                var response = await _customerService.UpdateCustomerAsync(id, customerDto);
                if (response.IsSuccessful)
                {
                    return Json(response);
                }
                else
                {
                    return Json(ResponseDto<EmptyDto>.Fail(response.Message,500));
                }
            }
            else
            {
                var errors = ModelState.Values
                     .SelectMany(v => v.Errors)
                     .Select(e => e.ErrorMessage)
                     .ToList();

                return Json(ResponseDto<EmptyDto>.Fail(string.Join(", ", errors), 400));
            }
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(string id)
        {
            var response = await _customerService.DeleteCustomerAsync(id);
            if (response.IsSuccessful)
            {
                return Json(response);
            }
            return Json(ResponseDto<EmptyDto>.Fail(response.Message, 500));
        }


        public async Task<IActionResult> Details(string id)
        {
            var response = await _customerService.GetCustomerByIdAsync(id);
            if (response.IsSuccessful)
            {
                return View(response.Data);
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Search(string query)
        {
            var response = await _customerService.SearchCustomersAsync(query);
            if (response.IsSuccessful)
            {
                return View("Index", response.Data);
            }
            return View("Error", response.Message);
        }
    }
}
