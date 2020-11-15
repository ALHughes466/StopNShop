using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using StopNShop2.Data;
using StopNShop2.Models;

namespace StopNShop2.Controllers
{
    public class ShoppingCartsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ShoppingCartsController(ApplicationDbContext context)
        {
            _context = context;
        }

        [Authorize(Roles = "User,Admin")]
        // GET: ShoppingCarts
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.ShoppingCart.Include(s => s.Product);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: ShoppingCarts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var shoppingCart = await _context.ShoppingCart
                .Include(s => s.Product)
                .FirstOrDefaultAsync(m => m.ShoppingCartID == id);
            if (shoppingCart == null)
            {
                return NotFound();
            }

            return View(shoppingCart);
        }

        [Authorize(Roles = "Admin")]
        // GET: ShoppingCarts/Create
        public IActionResult Create()
        {
            ViewData["ProductFK"] = new SelectList(_context.Product, "ProductId", "Title");
            ViewData["UserFK"] = new SelectList(_context.ApplicationUser, "ProductId", "Title");
            return View();
        }

        // POST: ShoppingCarts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ShoppingCartID,Quantity,Submitted,UserFK,ProductFK")] ShoppingCart shoppingCart)
        {
            if (ModelState.IsValid)
            {
                _context.Add(shoppingCart);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ProductFK"] = new SelectList(_context.Product, "ProductId", "ProductId", shoppingCart.ProductFK);
            return View(shoppingCart);
        }

        [Authorize(Roles = "Admin")]
        // GET: ShoppingCarts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var shoppingCart = await _context.ShoppingCart.FindAsync(id);
            if (shoppingCart == null)
            {
                return NotFound();
            }
            ViewData["ProductFK"] = new SelectList(_context.Product, "ProductId", "ProductId", shoppingCart.ProductFK);
            return View(shoppingCart);
        }

        [Authorize(Roles = "Admin")]
        // POST: ShoppingCarts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ShoppingCartID,Quantity,Submitted,UserFK,ProductFK")] ShoppingCart shoppingCart)
        {
            if (id != shoppingCart.ShoppingCartID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(shoppingCart);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ShoppingCartExists(shoppingCart.ShoppingCartID))
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
            ViewData["ProductFK"] = new SelectList(_context.Product, "ProductId", "ProductId", shoppingCart.ProductFK);
            return View(shoppingCart);
        }

        [Authorize(Roles = "Admin")]
        // GET: ShoppingCarts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var shoppingCart = await _context.ShoppingCart
                .Include(s => s.Product)
                .FirstOrDefaultAsync(m => m.ShoppingCartID == id);
            if (shoppingCart == null)
            {
                return NotFound();
            }

            return View(shoppingCart);
        }

        [Authorize(Roles = "Admin")]
        // POST: ShoppingCarts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var shoppingCart = await _context.ShoppingCart.FindAsync(id);
            _context.ShoppingCart.Remove(shoppingCart);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ShoppingCartExists(int id)
        {
            return _context.ShoppingCart.Any(e => e.ShoppingCartID == id);
        }
    }
}
