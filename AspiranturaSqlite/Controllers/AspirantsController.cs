using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AspiranturaSqlite.Data;
using AspiranturaSqlite.Models;

namespace AspiranturaSqlite.Controllers
{
    public class AspirantsController : Controller
    {
        private readonly AspiranturaContext _context;

        public AspirantsController(AspiranturaContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> List()
        {
            IQueryable<Aspirant> aspirants = from a in _context.Aspirants.
                Include(s => s.StatusType).
                Include(o => o.AsppirantOrders).
                ThenInclude(o => o.Order).
                AsNoTracking().
                OrderBy(s => s.Name)
                select a;

            return View(await aspirants.ToListAsync());
        }

        // Custom
        // GET: Aspirants
        public async Task<IActionResult> Index()
        {
            IQueryable<Aspirant> aspirants = from a in _context.Aspirants.
                Include(s => s.StatusType).
                Include(o => o.AsppirantOrders).
                ThenInclude(o => o.Order).
                AsNoTracking().
                OrderBy(s => s.Name)
                select a;

            return View(await aspirants.ToListAsync());
        }

        // GET: Aspirants/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var aspirant = await _context.Aspirants
                .Include(a => a.StatusType)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (aspirant == null)
            {
                return NotFound();
            }

            return View(aspirant);
        }

        // GET: Aspirants/Create
        public IActionResult Create()
        {                                                                                                                 // Custom
            PopulateStatusesDropDownList();
            return View();
        }

        // POST: Aspirants/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Surename,Patronymic,Birthday,DateInput,ProtectionDate,Protection,Course,StatustypeId,SpecialityId")] Aspirant aspirant)
        {
            if (ModelState.IsValid)
            {
                _context.Add(aspirant);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }                                                                                                                             // Custom
                PopulateStatusesDropDownList(aspirant.StatustypeId);
            return View(aspirant);
        }

        // GET: Aspirants/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var aspirant = await _context.Aspirants.SingleOrDefaultAsync(m => m.Id == id);
            if (aspirant == null)
            {
                return NotFound();
            }
            // ViewData["StatustypeId"] = new SelectList(_context.Statuses, "Id", "Name");                             // Custom
            PopulateStatusesDropDownList();
            return View(aspirant);
        }

        // POST: Aspirants/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Surename,Patronymic,Birthday,DateInput,ProtectionDate,Protection,Course,StatustypeId,SpecialityId")] Aspirant aspirant)
        {
            if (id != aspirant.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(aspirant);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AspirantExists(aspirant.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            // ViewData["StatustypeId"] = new SelectList(_context.Statuses, "Id", "Name");                             // Custom
            PopulateStatusesDropDownList(aspirant.StatustypeId);
            return View(aspirant);
        }

        // GET: Aspirants/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var aspirant = await _context.Aspirants
                .Include(a => a.StatusType)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (aspirant == null)
            {
                return NotFound();
            }

            return View(aspirant);
        }

        // POST: Aspirants/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var aspirant = await _context.Aspirants.SingleOrDefaultAsync(m => m.Id == id);
            _context.Aspirants.Remove(aspirant);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AspirantExists(int id)
        {
            return _context.Aspirants.Any(e => e.Id == id);
        }

        // Custom
        private void PopulateStatusesDropDownList(object selected = null)
        {
            ViewData["StatustypeId"] = new SelectList(_context.Statuses, "Id", "Name", selected);
            ViewData["SpecialityId"] = new SelectList(_context.Specialities, "Id", "Name", selected);
        }

    }
}
