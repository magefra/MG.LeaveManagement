using MG.LeaveManagement.MVC.Contracts;
using MG.LeaveManagement.MVC.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace MG.LeaveManagement.MVC.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class LeaveTypesController : Controller
    {
        private readonly IleaveTypeService _ileaveTypeService;

        private readonly ILeaveAllocationService _leaveAllocationService;


        public LeaveTypesController(IleaveTypeService ileaveTypeService, ILeaveAllocationService leaveAllocationService)
        {
            _ileaveTypeService = ileaveTypeService;
            this._leaveAllocationService = leaveAllocationService;
        }


        // GET: LeaveTypesController
        public async Task<ActionResult> Index()
        {
            var model = await _ileaveTypeService.GetLeaveTypes();

            return View(model);
        }

        // GET: LeaveTypesController/Details/5
        public async Task<ActionResult> Details(int id)
        {
            var model = await _ileaveTypeService.GetLeaveTypeDetails(id);
            return View(model);
        }

        // GET: LeaveTypesController/Create
        public async Task<ActionResult> Create()
        {
            return View();
        }

        // POST: LeaveTypesController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(CreateLeaveTypeVM createLeaveType)
        {
            try
            {
                var response = await _ileaveTypeService.CreateLeaveType(createLeaveType);

                if (response.Success)
                {
                    return RedirectToAction(nameof(Index));
                }

                ModelState.AddModelError("", response.ValidationErrors);
                
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
            }

            return View();
        }

        // GET: LeaveTypesController/Edit/5
        public async Task<ActionResult> Edit(int id)
        {

            var model = await _ileaveTypeService.GetLeaveTypeDetails(id);
            return View(model);
        }

        // POST: LeaveTypesController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, LeaveTypeVM leave)
        {
            try
            {
                var response = await _ileaveTypeService.UpdateLeaveType(id, leave);

                if (response.Success)
                {
                    return RedirectToAction(nameof(Index));
                }

                ModelState.AddModelError("", response.ValidationErrors);

            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
            }

            return View();
        }

      

        // POST: LeaveTypesController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id, IFormCollection collection)
        {
            try
            {
                var response = await _ileaveTypeService.DeleteLeaveType(id);
                if (response.Success)
                {
                    return RedirectToAction(nameof(Index));
                }

                ModelState.AddModelError("", response.ValidationErrors);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
            }

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Allocate(int id)
        {
            try
            {
                var response = await _leaveAllocationService.CreateLeaveAllocations(id);
                if (response.Success)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
            }

            return BadRequest();
        }
    }
}
