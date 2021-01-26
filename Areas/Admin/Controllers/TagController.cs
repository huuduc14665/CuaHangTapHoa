using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CuaHangTapHoa.Data;
using CuaHangTapHoa.Models;
using CuaHangTapHoa.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CuaHangTapHoa.Areas.Admin.Controllers
{
    [Authorize(Roles = SD.QuanLi)]
    [Area("Admin")]
    public class TagController : Controller
    {
        private readonly ApplicationDbContext _db;
        public TagController(ApplicationDbContext db)
        {
            _db = db;
        }
        // GET: Index
        public ActionResult Index()
        {
            return View(_db.Tags.ToList());
        }

        
        // GET: Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Tag specialTags)
        {
            if (ModelState.IsValid)
            {
                _db.Add(specialTags);
                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(specialTags);
        }

        // GET: Edit
        public async Task<ActionResult> Edit(int? ma)
        {
            if (ma == null)
                return NotFound();
            var specialTag = await _db.Tags.FindAsync(ma);
            if (specialTag == null)
                return NotFound();

            return View(specialTag);
        }

        // POST: Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int ma, Tag specialTags)
        {
            if (ma != specialTags.MaTag)
                return NotFound();
            if (ModelState.IsValid)
            {
                _db.Update(specialTags);
                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(specialTags);
        }
        // GET: Details
        public async Task<ActionResult> Details(int? ma)
        {
            if (ma == null)
                return NotFound();
            var specialTag = await _db.Tags.FindAsync(ma);
            if (specialTag == null)
                return NotFound();

            return View(specialTag);
        }

        // GET: Delete
        public async Task<ActionResult> Delete(int? ma)
        {
            if (ma == null)
                return NotFound();
            var specialTag = await _db.Tags.FindAsync(ma);
            if (specialTag == null)
                return NotFound();

            return View(specialTag);
        }

        // POST: Delete
        [HttpPost]
        [ValidateAntiForgeryToken]
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int ma)
        {
            var specialTags = await _db.Tags.FindAsync(ma);
            _db.Remove(specialTags);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

    }
}