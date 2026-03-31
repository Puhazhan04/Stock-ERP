using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StockERP.Data;
using StockERP.Models;

namespace StockERP.Controllers
{
    public class ProductsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProductsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Products
        public async Task<IActionResult> Index()
        {
            var products = await _context.Products
                .Include(p => p.Category)
                .Include(p => p.Supplier)
                .ToListAsync();
            return View(products);
        }

        // GET: Products/Create
        public async Task<IActionResult> Create()
        {
            ViewBag.CategoryId = new Microsoft.AspNetCore.Mvc.Rendering.SelectList(await _context.Categories.ToListAsync(), "Id", "Name");
            ViewBag.SupplierId = new Microsoft.AspNetCore.Mvc.Rendering.SelectList(await _context.Suppliers.ToListAsync(), "Id", "Name");
            return View();
        }

        // POST: Products/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Price,Stock,CategoryId,SupplierId")] Product product)
        {
            if (ModelState.IsValid)
            {
                _context.Add(product);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewBag.CategoryId = new Microsoft.AspNetCore.Mvc.Rendering.SelectList(await _context.Categories.ToListAsync(), "Id", "Name", product.CategoryId);
            ViewBag.SupplierId = new Microsoft.AspNetCore.Mvc.Rendering.SelectList(await _context.Suppliers.ToListAsync(), "Id", "Name", product.SupplierId);
            return View(product);
        }

        // GET: Products/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var product = await _context.Products.FindAsync(id);
            if (product == null) return NotFound();

            ViewBag.CategoryId = new Microsoft.AspNetCore.Mvc.Rendering.SelectList(await _context.Categories.ToListAsync(), "Id", "Name", product.CategoryId);
            ViewBag.SupplierId = new Microsoft.AspNetCore.Mvc.Rendering.SelectList(await _context.Suppliers.ToListAsync(), "Id", "Name", product.SupplierId);
            return View(product);
        }

        // POST: Products/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Price,Stock,CategoryId,SupplierId")] Product product)
        {
            if (id != product.Id) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(product);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(product.Id)) return NotFound();
                    else throw;
                }
                return RedirectToAction(nameof(Index));
            }
            ViewBag.CategoryId = new Microsoft.AspNetCore.Mvc.Rendering.SelectList(await _context.Categories.ToListAsync(), "Id", "Name", product.CategoryId);
            ViewBag.SupplierId = new Microsoft.AspNetCore.Mvc.Rendering.SelectList(await _context.Suppliers.ToListAsync(), "Id", "Name", product.SupplierId);
            return View(product);
        }

        // GET: Products/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var product = await _context.Products
                .FirstOrDefaultAsync(m => m.Id == id);
            if (product == null) return NotFound();

            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product != null)
            {
                _context.Products.Remove(product);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // POST: Products/CreateCategory (AJAX)
        [HttpPost]
        public async Task<IActionResult> CreateCategory([FromBody] string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                return BadRequest("Name darf nicht leer sein.");

            var category = new Category { Name = name.Trim() };
            _context.Categories.Add(category);
            await _context.SaveChangesAsync();

            return Json(new { id = category.Id, name = category.Name });
        }

        // POST: Products/CreateSupplier (AJAX)
        [HttpPost]
        public async Task<IActionResult> CreateSupplier([FromBody] string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                return BadRequest("Name darf nicht leer sein.");

            var supplier = new Supplier { Name = name.Trim() };
            _context.Suppliers.Add(supplier);
            await _context.SaveChangesAsync();

            return Json(new { id = supplier.Id, name = supplier.Name });
        }

        private bool ProductExists(int id)
        {
            return _context.Products.Any(e => e.Id == id);
        }
    }
}
