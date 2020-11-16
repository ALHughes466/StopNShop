using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using StopNShop2.Data;
using StopNShop2.Models;

namespace StopNShop2.Controllers
{
    public class ProductsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly String _connection;

        public ProductsController(ApplicationDbContext context, IConfiguration configuration, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
            _connection = configuration.GetConnectionString("DefaultConnection");
        }

        // GET: Products
        public async Task<IActionResult> Index(string searchString)
        {
            var Product = from p in _context.Product.Include(p => p.Category)
                .Include(p => p.ImageUpload) select p;

            if (!String.IsNullOrEmpty(searchString))
            {
                Product = Product.Where(p => p.Title.Contains(searchString));
            }
            return View(await Product.ToListAsync());
        }

        // GET: Products/Details/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Product
                .Include(p => p.Category)
                .Include(p => p.ImageUpload)
                .FirstOrDefaultAsync(m => m.ProductId == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // GET: Products/Create
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            ViewData["CategoryFK"] = new SelectList(_context.Category, "CategoryID", "CategoryName");
            ViewData["ImageFK"] = new SelectList(_context.ImageUpload, "ImageID", "FileName");
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProductId,Title,Price,PreviousPrice,Rating,ImagePath,FreeShipping,CategoryFK,ImageFK")] Product product)
        {
            if (ModelState.IsValid)
            {
                // image upload
                /*if (Request.Form.Files.Count > 0)
                {
                    IFormFile file = Request.Form.Files.FirstOrDefault();
                    using (var dataStream = new MemoryStream())
                    {
                        await file.CopyToAsync(dataStream);
                        product.ImagePath = dataStream.ToArray();
                    }
                }*/

                _context.Add(product);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryFK"] = new SelectList(_context.Category, "CategoryID", "CategoryName", product.CategoryFK);
            ViewData["ImageFK"] = new SelectList(_context.ImageUpload, "ImageID", "FileName", product.ImageFK);
            return View(product);
        }

        // GET: Products/Edit/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Product.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            ViewData["CategoryFK"] = new SelectList(_context.Category, "CategoryID", "CategoryName", product.CategoryFK);
            ViewData["ImageFK"] = new SelectList(_context.ImageUpload, "ImageID", "FileName", product.ImageFK);
            return View(product);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id, [Bind("ProductId,Title,Price,PreviousPrice,Rating,FreeShipping,CategoryFK,ImageFK")] Product product)
        {
            if (id != product.ProductId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                
                try
                {
                    _context.Update(product);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(product.ProductId))
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
            ViewData["CategoryFK"] = new SelectList(_context.Category, "CategoryID", "CategoryName", product.CategoryFK);
            ViewData["ImageFK"] = new SelectList(_context.ImageUpload, "ImageID", "FileName", product.ImageFK);
            return View(product);
        }

        // GET: Products/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Product
                .Include(p => p.Category)
                .FirstOrDefaultAsync(m => m.ProductId == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var product = await _context.Product.FindAsync(id);
            _context.Product.Remove(product);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        /*[Authorize("User")]
        public async Task<IActionResult> AddToCart(int? prodId)
        {

        }*/

        [Authorize(Roles = "Admin, User")]
        public async Task<IActionResult> Cart(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Product.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            ViewData["CategoryFK"] = new SelectList(_context.Category, "CategoryID", "CategoryName", product.CategoryFK);
            ViewData["ImageFK"] = new SelectList(_context.ImageUpload, "ImageID", "FileName", product.ImageFK);
            return View(product);
        }

        [Authorize(Roles = "Admin, User")]
        [HttpPost, ActionName("Cart")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Cart()
        {
            return RedirectToAction(nameof(Thanks));
        }

        public async Task<IActionResult> Thanks()
        {
            return View();
        }

        private bool ProductExists(int id)
        {
            return _context.Product.Any(e => e.ProductId == id);
        }
    }
}
