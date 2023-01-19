﻿using eTickets.Data;
using eTickets.Data.Services;
using eTickets.Data.Static;
using eTickets.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace eTickets.Controllers
{
    [Authorize(Roles = UserRoles.Admin)]
    public class CinemasController : Controller
    {
        private readonly ICinemasService _service;

        public CinemasController(ICinemasService service)
        {
            _service = service;
        }

        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            var data = await _service.GetAllAsync();
            return View(data);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create([Bind("Name,Logo,Description")] Cinema cinema)
        {
            if (!ModelState.IsValid)
            {
                return View(cinema);
            }
            await _service.AddAsync(cinema);
            return RedirectToAction(nameof(Index));
        }

        [AllowAnonymous]
        public async Task<IActionResult> Details(int id)
        {
            var details = await _service.GetByIdAsync(id);
            if (details == null)
            {
                return View("NotFound");
            }
            return View(details);
        }

        public async Task<IActionResult> Edit(int id)
        {

            var details = await _service.GetByIdAsync(id);
            if (details == null)
            {
                return View("NotFound");
            }
            return View(details);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, Cinema cinema)
        {
            if (!ModelState.IsValid)
            {
                return View(cinema);
            }
            if(id == cinema.Id)
            {
                await _service.UpdateAsync(id, cinema);
                return RedirectToAction(nameof(Index));
            }
            return View(cinema);
           
        }
        public async Task<IActionResult> Delete(int id)
        {

            var details = await _service.GetByIdAsync(id);
            if (details == null)
            {
                return View("NotFound");
            }
            return View(details);
        }


        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var details = await _service.GetByIdAsync(id);
            if (details == null)
            {
                return View("NotFound");
            }
            await _service.DeleteAsync(id);

            return RedirectToAction(nameof(Index));
        }
    }
}
