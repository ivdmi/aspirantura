using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using AspiranturaSqlite.Data;
using AspiranturaSqlite.Models;
using AspiranturaSqlite.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Aspirantura.Controllers
{
    public class HomeController : Controller
    {

        private readonly AspiranturaContext _context;

        public HomeController(AspiranturaContext context)
        {
            _context = context;
        }
                
        public IActionResult Index()
        {            
            return View();
        }

        
        public IActionResult Aspirants()
        {
            var aspirants = _context.Aspirants.ToList();
            return View(aspirants);
        }

        public async Task<ActionResult> About()
        {
            // IQueryable<KnowledgeInfoGroup> 
                
                var data = from spec in _context.Specialities
                           group spec by spec.KnowledgeId
                           into specGroup
                           select new KnowledgeInfoGroup
                           {
                               Number = specGroup.Key,
                               SpecialityCount = specGroup.Count(),
                               Name = specGroup.FirstOrDefault().Knowledge.Name
                           };
            return View(await data.AsNoTracking().ToListAsync());
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
