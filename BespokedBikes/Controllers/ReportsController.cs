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
    public class ReportsController : Controller
    {
        private readonly BespokedBikesContext _context;
        private readonly BespokedBikeService _bespokedBikeService;

        public ReportsController(BespokedBikesContext context, BespokedBikes.Services.BespokedBikeService service)
        {
            _bespokedBikeService = service;
            _context = context;
        }

        // GET: Reports
        public async Task<IActionResult> Index(int quarter, int year)
        {

            //create new report
            return View( "Index",_bespokedBikeService.CreateReport(quarter, year));
            //return View(await _context.Report.ToListAsync());
        }


        private bool ReportExists(int id)
        {
            return _context.Report.Any(e => e.ReportId == id);
        }
    }
}
