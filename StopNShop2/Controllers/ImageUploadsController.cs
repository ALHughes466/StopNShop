using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using StopNShop2.Data;
using StopNShop2.Models;

namespace StopNShop2.Controllers
{
    public class ImageUploadsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ImageUploadsController(ApplicationDbContext context)
        {
            _context = context;
        }

        [Authorize(Roles = "Admin")]
        // GET: ImageUploads
        public async Task<IActionResult> Index()
        {
            return View(await _context.ImageUpload.ToListAsync());
        }

        [Authorize(Roles = "Admin")]
        // GET: ImageUploads/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var imageUpload = await _context.ImageUpload
                .FirstOrDefaultAsync(m => m.ImageID == id);
            if (imageUpload == null)
            {
                return NotFound();
            }

            return View(imageUpload);
        }

        [Authorize(Roles = "Admin")]
        // GET: ImageUploads/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ImageUploads/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ImageID,ImagePath")] ImageUpload imageUpload)
        {
            if (ModelState.IsValid)
            {
                if (Request.Form.Files.Count > 0)
                {
                    IFormFile file = Request.Form.Files.FirstOrDefault();
                    using (var dataStream = new MemoryStream())
                    {
                        await file.CopyToAsync(dataStream);
                        imageUpload.ImagePath = dataStream.ToArray();
                        imageUpload.FileName = file.FileName;
                    }
                }
                else
                {
                    return RedirectToAction(nameof(Index));
                }
                _context.Add(imageUpload);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(imageUpload);
        }

        [Authorize(Roles = "Admin")]
        // GET: ImageUploads/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var imageUpload = await _context.ImageUpload.FindAsync(id);
            if (imageUpload == null)
            {
                return NotFound();
            }
            return View(imageUpload);
        }

        // POST: ImageUploads/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ImageID,ImagePath")] ImageUpload imageUpload)
        {
            if (id != imageUpload.ImageID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(imageUpload);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ImageUploadExists(imageUpload.ImageID))
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
            return View(imageUpload);
        }

        [Authorize(Roles = "Admin")]
        // GET: ImageUploads/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var imageUpload = await _context.ImageUpload
                .FirstOrDefaultAsync(m => m.ImageID == id);
            if (imageUpload == null)
            {
                return NotFound();
            }

            return View(imageUpload);
        }


        // POST: ImageUploads/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var imageUpload = await _context.ImageUpload.FindAsync(id);
            _context.ImageUpload.Remove(imageUpload);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ImageUploadExists(int id)
        {
            return _context.ImageUpload.Any(e => e.ImageID == id);
        }
    }
}
