using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartCityApp.Data;
using SmartCityApp.Models;
using SmartCityApp.ViewModels;
using System;

namespace SmartCityApp.Controllers
{
    [ApiController]
    [Route("api/reports")]
    public class ReportsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ReportsController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetReports([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            if (page <= 0 || pageSize <= 0)
            {
                return BadRequest(new { message = "Page and pageSize must be greater than 0" });
            }

            var totalReports = await _context.Reports.CountAsync();

            var reports = await _context.Reports
                .OrderByDescending(r => r.CreatedAt)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            var response = new
            {
                TotalReports = totalReports,
                Reports = reports
            };

            return Ok(response);
        }
    }
}