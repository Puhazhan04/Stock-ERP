using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StockERP.Data;
using StockERP.Models;
using System.Diagnostics;

namespace StockERP.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly ApplicationDbContext _context;

    public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
    {
        _logger = logger;
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        ViewBag.ProductCount = await _context.Products.CountAsync();
        ViewBag.CustomerCount = await _context.Customers.CountAsync();
        ViewBag.LowStockCount = await _context.Products.Where(p => p.Stock < 5).CountAsync();
        ViewBag.TotalValue = await _context.Products.SumAsync(p => p.Price * p.Stock);
        
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
