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
        private readonly BespokedBikeService _bespokedBikeService;

        public ReportsController(BespokedBikesContext context, BespokedBikeService service)
        {
            _bespokedBikeService = service;
        }

        // GET: Reports
        public async Task<IActionResult> Index(int quarter, int year)
        {

            ViewBag.Quarter = quarter;
            ViewBag.Year = year;

            return View( "Index",_bespokedBikeService.CreateReport(quarter, year));
        }

    }
}
