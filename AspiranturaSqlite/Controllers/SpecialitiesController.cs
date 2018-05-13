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
    public class SpecialitiesController : Controller
    {
        private readonly AspiranturaContext _context;

        public SpecialitiesController(AspiranturaContext context)
        {
            _context = context;
        }

        // GET: Specialities
        //public async Task<IActionResult> Index()
        //{
        //    var aspiranturaContext = _context.Specialities.Include(s => s.Knowledge);
        //    return View(await aspiranturaContext.ToListAsync());
        //}
        

            public ActionResult Knowledges()
        {
            var kn = _context.Knowledges.ToList();
            return View(kn);
        }

        public async Task<IActionResult> Index(string sortOrder, string currentFilter, string searchString, int? page)      // Фильтр ToUpper() - не работает, пришлось переделать в List - ActionResult  !!!
        {            
            // Сортировка
            ViewData["CurrentSort"] = sortOrder;

            ViewData["CodeSortParm"] = String.IsNullOrEmpty(sortOrder) ? "Code_desc" : "";
            ViewData["NameSortParm"] = sortOrder == "Name" ? "Name_desc" : "Name";
            ViewData["KnowledgeShifrSortParm"] = sortOrder == "KnowledgeShifr" ? "KnowledgeShifr_desc" : "KnowledgeShifr";
            ViewData["KnowledgeNameSortParm"] = sortOrder == "KnowledgeName" ? "KnowledgeName_desc" : "KnowledgeName";

            // Для разбиения на страницы
            if (searchString != null)
            {
                page = 1;               // новый фильтр в строке поиска
            }
            else
            {
                searchString = currentFilter;       // для отобр в строке поиска текущего фильтра
            }

            ViewData["CurrentFilter"] = searchString;

            IQueryable<Speciality> specialities = (from s in _context.Specialities.Include(s => s.Knowledge) select s);
            

            if (!String.IsNullOrEmpty(searchString))
            {
                specialities = (specialities.Where(s => s.Name.Contains(searchString) || s.Knowledge.Name.Contains(searchString)));
            }
                                    
            switch (sortOrder)
            {
                case "Code_desc":
                    specialities = specialities.OrderByDescending(s => s.Id);
                    break;
                case "Name_desc":
                    specialities = specialities.OrderByDescending(s => s.Name);
                    break;
                case "Knowkedge_desc":
                    specialities = specialities.OrderByDescending(s => s.Knowledge.Name);
                    break;
                case "KnowledgeShifr_desc":
                    specialities = specialities.OrderByDescending(s => s.Knowledge.Id);
                    break;
                case "Name":
                    specialities = specialities.OrderBy(s => s.Name);
                    break;
                case "KnowledgeShifr":
                    specialities = specialities.OrderBy(s => s.Knowledge.Id);
                    break;
                case "KnowledgeName":
                    specialities = specialities.OrderBy(s => s.Knowledge.Name);
                    break;
                default:
                    specialities = specialities.OrderBy(s => s.Id);
                    break;
            }

            // Строк на листе
            int pageSize = 10;
            return View(await PaginatedList<Speciality>.CreateAsync(specialities.AsNoTracking(), page ?? 1, pageSize));
            // return View(await specialities.ToListAsync());
        }

        public ActionResult IndexList(string sortOrder, string searchString)                    // Так будет работать регистронезависимо, но не по феншую. Надо async Task<IActionResult>
        {

            // Сортировка
            ViewData["CodeSortParm"] = String.IsNullOrEmpty(sortOrder) ? "Code_desc" : "";
            ViewData["NameSortParm"] = sortOrder == "Name" ? "Name_desc" : "Name";
            ViewData["KnowledgeShifrSortParm"] = sortOrder == "KnowledgeShifr" ? "KnowledgeShifr_desc" : "KnowledgeShifr";
            ViewData["KnowledgeNameSortParm"] = sortOrder == "KnowledgeName" ? "KnowledgeName_desc" : "KnowledgeName";

            IList<Speciality> specialities = (from s in _context.Specialities.Include(s => s.Knowledge) select s).ToList();
                        
            ViewData["CurrentFilter"] = searchString;

            if (!String.IsNullOrEmpty(searchString))
            {
                specialities = (specialities.Where(s => s.Name.ToUpper().Contains(searchString.ToUpper()) || s.Knowledge.Name.ToUpper().Contains(searchString.ToUpper())).ToList());
            }

            switch (sortOrder)
            {
                case "Code_desc":
                    specialities = specialities.OrderByDescending(s => s.Id).ToList();
                    break;
                case "Name_desc":
                    specialities = specialities.OrderByDescending(s => s.Name).ToList();
                    break;
                case "Knowkedge_desc":
                    specialities = specialities.OrderByDescending(s => s.Knowledge.Name).ToList();
                    break;
                case "KnowledgeShifr_desc":
                    specialities = specialities.OrderByDescending(s => s.Knowledge.Id).ToList();
                    break;
                case "Name":
                    specialities = specialities.OrderBy(s => s.Name).ToList();
                    break;
                case "KnowledgeShifr":
                    specialities = specialities.OrderBy(s => s.Knowledge.Id).ToList();
                    break;
                case "KnowledgeName":
                    specialities = specialities.OrderBy(s => s.Knowledge.Name).ToList();
                    break;
                default:
                    specialities = specialities.OrderBy(s => s.Id).ToList();
                    break;
            }

            return View(specialities);
        }



        // GET: Specialities/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var speciality = await _context.Specialities
                .Include(s => s.Knowledge)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (speciality == null)
            {
                return NotFound();
            }

            return View(speciality);
        }

        // GET: Specialities/Create
        public IActionResult Create()
        {
            ViewData["KnowledgeId"] = new SelectList(_context.Knowledges, "Id", "Name");                // Custom
            return View();
        }

        // POST: Specialities/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Code,Name,KnowledgeId")] Speciality speciality)
        {
            if (ModelState.IsValid)
            {
                _context.Add(speciality);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["KnowledgeId"] = new SelectList(_context.Knowledges, "Id", "Name", speciality.KnowledgeId);                // Custom
            return View(speciality);
        }

        // GET: Specialities/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var speciality = await _context.Specialities.SingleOrDefaultAsync(m => m.Id == id);
            if (speciality == null)
            {
                return NotFound();
            }
            ViewData["KnowledgeId"] = new SelectList(_context.Knowledges, "Id", "Name", speciality.KnowledgeId);                // Custom
            return View(speciality);
        }

        // POST: Specialities/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Code,Name,KnowledgeId")] Speciality speciality)
        {
            if (id != speciality.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(speciality);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SpecialityExists(speciality.Id))
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
            ViewData["KnowledgeId"] = new SelectList(_context.Knowledges, "Id", "Name", speciality.KnowledgeId);                // Custom
            return View(speciality);
        }

        // GET: Specialities/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var speciality = await _context.Specialities
                .Include(s => s.Knowledge)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (speciality == null)
            {
                return NotFound();
            }

            return View(speciality);
        }

        // POST: Specialities/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var speciality = await _context.Specialities.SingleOrDefaultAsync(m => m.Id == id);
            _context.Specialities.Remove(speciality);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SpecialityExists(int id)
        {
            return _context.Specialities.Any(e => e.Id == id);
        }
    }
}
