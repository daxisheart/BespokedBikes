using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BespokedBikes.Data;
using BespokedBikes.Models;

namespace BespokedBikes.Controllers
{
    public class SalespersonsController : Controller
    {
        private readonly BespokedBikesContext _context;

        public SalespersonsController(BespokedBikesContext context)
        {
            _context = context;
        }

        // GET: Salespersons
        public async Task<IActionResult> Index()
        {
            return View(await _context.Salesperson.ToListAsync());
        }

        // GET: Salespersons/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var salesperson = await _context.Salesperson
                .FirstOrDefaultAsync(m => m.SalespersonId == id);
            if (salesperson == null)
            {
                return NotFound();
            }

            return View(salesperson);
        }

        // GET: Salespersons/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Salespersons/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,FirstName,LastName,Address,PhoneNumber,StartDate,TerminationDate,Manager")] Salesperson salesperson)
        {
            if (ModelState.IsValid)
            {
                _context.Add(salesperson);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(salesperson);
        }

        // GET: Salespersons/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var salesperson = await _context.Salesperson.FindAsync(id);
            if (salesperson == null)
            {
                return NotFound();
            }
            return View(salesperson);
        }

        // POST: Salespersons/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FirstName,LastName,Address,PhoneNumber,StartDate,TerminationDate,Manager")] Salesperson salesperson)
        {
            if (id != salesperson.SalespersonId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(salesperson);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SalespersonExists(salesperson.SalespersonId))
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
            return View(salesperson);
        }

        private bool SalespersonExists(int id)
        {
            return _context.Salesperson.Any(e => e.SalespersonId == id);
        }
    }
}
