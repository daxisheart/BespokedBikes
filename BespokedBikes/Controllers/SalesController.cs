using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BespokedBikes.Data;
using BespokedBikes.Models;
using BespokedBikes.Services;

namespace BespokedBikes.Controllers
{
    public class SalesController : Controller
    {
        private readonly BespokedBikesContext _context;
        private readonly BespokedBikeService _bespokedBikesService;

        public SalesController(BespokedBikesContext context, BespokedBikeService service)
        {
            _bespokedBikesService = service;
            _context = context;
        }

        // GET: Sales
        public async Task<IActionResult> Index(string sortOrder)
        {
            ViewBag.DateSortParm = String.IsNullOrEmpty(sortOrder) ? "Date" : "date_desc";
            var bespokedBikesContext = _context.Sale.Include(s => s.Customer).Include(s => s.Product).Include(s => s.Salesperson);

            switch (sortOrder)
            {
                case "date_desc":
                    bespokedBikesContext.OrderByDescending(s => s.SalesDate);
                    break;
                case "Date":
                    bespokedBikesContext.OrderBy(s => s.SalesDate);
                    break;
                default:
                    break;
            }


            ViewBag.DateSortParam = sortOrder == "Date" ? "Date" : "date_desc";
            return View(await bespokedBikesContext.AsNoTracking().ToListAsync());
        }
        public async Task<IActionResult> Index2()
        {
            var bespokedBikesContext = _context.Sale.Include(s => s.Customer).Include(s => s.Product).Include(s => s.Salesperson);
            return View(await bespokedBikesContext.ToListAsync());
        }

        // GET: Sales/Create
        public IActionResult Create()
        {
            ViewData["CustomerId"] = new SelectList(_context.Set<Customer>(), "Id", "Id");
            ViewData["ProductId"] = new SelectList(_context.Set<Product>(), "Id", "Id");
            ViewData["SalespersonId"] = new SelectList(_context.Salesperson, "Id", "Id"); 
            ViewBag.CustomerId = _context.Customer.Select(x => new SelectListItem { Text = x.FirstName.ToString() + " " + x.LastName, Value = x.CustomerId.ToString() });
            ViewBag.ProductId = _context.Product.Select(x => new SelectListItem { Text = x.Name.ToString(), Value = x.ProductId.ToString() }); ;
            ViewBag.SalespersonId = _context.Salesperson.Select(x => new SelectListItem { Text = x.FirstName.ToString() + " " + x.LastName, Value = x.SalespersonId.ToString() });
            return View("Create");
        }

        // POST: Sales/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ProductId,CustomerId,SalespersonId,SalesDate")] Sale sale)
        {
            if (ModelState.IsValid)
            {
                //Todo
                sale.SalesPrice =  _bespokedBikesService.GetSalesPriceAfterDiscount(sale);
                _context.Sale.Add(sale);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CustomerId"] = new SelectList(_context.Set<Customer>(), "Id", "Id", sale.CustomerId);
            ViewData["ProductId"] = new SelectList(_context.Set<Product>(), "Id", "Id", sale.ProductId);
            ViewData["SalespersonId"] = new SelectList(_context.Salesperson, "Id", "Id", sale.SalespersonId);
            return View(sale);
        }

        private bool SaleExists(int id)
        {
            return _context.Sale.Any(e => e.CustomerId == id);
        }
    }
}
