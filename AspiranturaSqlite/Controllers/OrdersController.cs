using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AspiranturaSqlite.Data;
using AspiranturaSqlite.Models;
using AspiranturaSqlite.Models.ViewModels;

namespace AspiranturaSqlite.Controllers
{
    public class OrdersController : Controller
    {
        private readonly AspiranturaContext _context;

        public OrdersController(AspiranturaContext context)
        {
            _context = context;
        }

        // GET: Orders
        public async Task<IActionResult> Index()
        {
            var aspiranturaContext = _context.Orders.Include(o => o.OrderType);
            return View(await aspiranturaContext.ToListAsync());
        }

        //                                                                                                      Custom
        // GET: Orders/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Order order = await GetOrder(id);

            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        //public IActionResult New()
        //{
        //    PopulateOrdertypesDropDownList();
        //    return View();
        //}

        //                                                                                                                     Custom
        // GET: Orders/Create
        public IActionResult Create()
        {
            Order order = new Order { Aspirants = PopulateAspirantDataAsync(OrderTypeEnum.Зарахування), Date = DateTime.Now };
            PopulateOrdertypesDropDownList();
            return View(order);
        }

        //                                                                                                                     Custom
        // POST: Orders/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateAsync([Bind("Id,Number,Date,OrdertypeId,Aspirants")] Order order)
        {
            if (ModelState.IsValid)
            {
                //Order order = new Order { Number = orderVM.Number, Date=orderVM.Date, OrdertypeId=orderVM.OrdertypeId};

                // if (order.OrderType.Id == (int)OrderTypeEnum.Переведення)
                //

                _context.Add(order);
                await _context.SaveChangesAsync();
                await AddOrUpdateAssignedAspirants(order);
                return RedirectToAction(nameof(Index));
            }

            PopulateOrdertypesDropDownList(order.OrdertypeId);
            return View(order);
        }

        // Новий наказ про зарахування                                                                                                                    Custom
        // GET: Orders/Create
        public IActionResult CreateEnroll()
        {
            Order order = new Order { Aspirants = PopulateAspirantDataAsync(OrderTypeEnum.Зарахування), Date = DateTime.Now, OrdertypeId = (int)OrderTypeEnum.Зарахування };
            //      PopulateOrdertypesDropDownList();
            return View(order);
        }


        /*
        //                                                                                                                      Custom
        // GET: Orders/Edit/5                                                                                                           
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Order order = await GetOrder(id);            

            if (order == null)
            {
                return NotFound();
            }
            PopulateOrdertypesDropDownList(order.OrdertypeId);
            order.Aspirants = PopulateAspirantDataAsync(order.Id);

            return View(order);
        }
   
        // POST: Orders/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditAsync(int id, [Bind("Id,Number,Date,Context,OrdertypeId,Aspirants")] Order order)
        {
            if (id != order.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    //Order order = _context.Orders.FirstOrDefault(i => i.Id == id);
                    //order.Number = orderVM.Number;
                    //order.Date = orderVM.Date;
                    //order.Context = orderVM.Context;
                    //order.OrdertypeId = orderVM.OrdertypeId;
                    _context.Update(order);
                    await _context.SaveChangesAsync();
                    await AddOrUpdateAssignedAspirants(order);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrderExists(order.Id))
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
            // ViewData["OrdertypeId"] = new SelectList(_context.Ordertypes, "Id", "Name", order.OrdertypeId);
            PopulateOrdertypesDropDownList(order.OrdertypeId);
            return View(order);
        }
        */


        // GET: Orders/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Orders
                .Include(o => o.OrderType)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        // POST: Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var order = await _context.Orders.SingleOrDefaultAsync(m => m.Id == id);
            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private async Task<Order> GetOrder(int? id)
        {
            return await _context.Orders
                .Include(o => o.OrderType)
                .Include(a => a.AsppirantOrders)
                .ThenInclude(a => a.Aspirant)
                .AsNoTracking()
                .SingleOrDefaultAsync(m => m.Id == id);
        }

        private bool OrderExists(int id)
        {
            return _context.Orders.Any(e => e.Id == id);
        }

        // Custom
        private void PopulateOrdertypesDropDownList(object selected = null)
        {
            ViewData["OrdertypeId"] = new SelectList(_context.Ordertypes, "Id", "Name", selected);
        }

        // получить список аспирантов с отметками принадлежности приказу
        private ICollection<AssignedAspirantData> PopulateAspirantDataAsync(OrderTypeEnum orderType, int orderId = -1)
        {
            IQueryable<Aspirant> aspirants = from a in _context.Aspirants.
                                             Include(s => s.StatusType).
                                             Include(o => o.AsppirantOrders).
                                             ThenInclude(o => o.Order).
                                             AsNoTracking().
                                             OrderBy(k => k.InputDate).
                                             OrderBy(s => s.Name)
                                             select a;

            var assignedAspirants = new List<AssignedAspirantData>();

            if (orderType == OrderTypeEnum.Зарахування)
            {
                aspirants = aspirants.Where(s => s.StatustypeId == (int)StatusTypeEnum.новий);

                foreach (var item in aspirants)
                {
                    assignedAspirants.Add(new AssignedAspirantData
                    {
                        Id = item.Id,
                        Name = item.Name,
                        Surename = item.Surename,
                        Patronymic = item.Patronymic,
                        SpecialityId = item.SpecialityId,
                        Budget = true,
                        DayForm = true,
                        StationaryForm = true                        
                    });
                }
            }

            /*
             
            assignedAspirants.Add(new AssignedAspirantData
                    {
                        Id = item.Id,
                        Name = item.Name,
                        Surename = item.Surename,
                        Patronymic = item.Patronymic,
                        Course = item.Course,
                        StatusName = item.StatusType.Name,
                        InputDate = item.InputDate,
                        Protection = item.Protection,
                        ProtectionDate = item.ProtectionDate,
                        Assigned = _context.AspirantOrders.Any(k => (k.AspirantId == item.Id && k.OrderId == orderId))
                    });
             
             */




            return assignedAspirants.ToList();
        }

        // изменить список аспирантов, принадлежащих к приказу
        private async Task AddOrUpdateAssignedAspirants(Order order)
        {
            foreach (var aspirant in order.Aspirants)
            {
                AspirantOrder aspirantOrder = await _context.AspirantOrders.FirstOrDefaultAsync(a => (a.AspirantId == aspirant.Id && a.OrderId == order.Id));
                if (aspirant.Assigned)
                {
                    if (aspirantOrder != null)
                    {
                        aspirantOrder.AspirantId = aspirant.Id;
                        aspirantOrder.OrderId = order.Id;
                        _context.AspirantOrders.Update(aspirantOrder);
                    }
                    else
                    {
                        aspirantOrder = new AspirantOrder { AspirantId = aspirant.Id, OrderId = order.Id };
                        _context.AspirantOrders.Add(aspirantOrder);
                    }
                }
                else
                {
                    if (aspirantOrder != null)
                    {
                        _context.AspirantOrders.Remove(aspirantOrder);
                    }
                }
            }
            await _context.SaveChangesAsync();
        }

        // изменить курс и статус аспиранта
        private async Task UpdateAspirantStatus(Order order)
        {
            foreach (var assignedAspirant in order.Aspirants.Where(aa => aa.Assigned))
            {
                Aspirant aspirant = _context.Aspirants.First(a => a.Id == assignedAspirant.Id);
                if (aspirant != null)
                {
                    aspirant.Course++;
                    switch (order.OrderType.Id)
                    {
                        case (int)OrderTypeEnum.Зарахування:
                            aspirant.Course = 1;
                            //                            aspirant.StatustypeId = assignedAspirant.
                            break;

                    }
                }

                AspirantOrder aspirantOrder = await _context.AspirantOrders.FirstOrDefaultAsync(a => (a.AspirantId == assignedAspirant.Id && a.OrderId == order.Id));
                if (assignedAspirant.Assigned)
                {
                    if (aspirantOrder != null)
                    {
                        aspirantOrder.AspirantId = assignedAspirant.Id;
                        aspirantOrder.OrderId = order.Id;
                        _context.AspirantOrders.Update(aspirantOrder);
                    }
                    else
                    {
                        aspirantOrder = new AspirantOrder { AspirantId = assignedAspirant.Id, OrderId = order.Id };
                        _context.AspirantOrders.Add(aspirantOrder);
                    }
                }
                else
                {
                    if (aspirantOrder != null)
                    {
                        _context.AspirantOrders.Remove(aspirantOrder);
                    }
                }
            }
            await _context.SaveChangesAsync();
        }
    }
}
