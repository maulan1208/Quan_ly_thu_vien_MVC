using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BTL.Models;

namespace BTL.Controllers
{
    public class TacGiaController : Controller
    {
        private readonly QLThuVienDBContext _context;

        public TacGiaController(QLThuVienDBContext context)
        {
            _context = context;
        }

        // GET: TacGia
        public async Task<IActionResult> Index()
        {
              return _context.TacGias != null ? 
                          View(await _context.TacGias.ToListAsync()) :
                          Problem("Entity set 'QLThuVienDBContext.TacGias'  is null.");
        }

        public async Task<IActionResult> SearchTG(string txtSearch)
        {
            var TacGiaDBContext = _context.TacGias.Where(m =>
            m.TenTacGia.Contains(txtSearch))
                .Select(m => new TacGia()
                {
                    TacGiaID = m.TacGiaID,
                    TenTacGia = m.TenTacGia
                    
                });
            return View(nameof(Index), await TacGiaDBContext.ToListAsync());
        }

        // GET: TacGia/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.TacGias == null)
            {
                return NotFound();
            }

            var tacGia = await _context.TacGias
                .FirstOrDefaultAsync(m => m.TacGiaID == id);
            if (tacGia == null)
            {
                return NotFound();
            }

            return View(tacGia);
        }

        // GET: TacGia/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TacGia/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TacGiaID,TenTacGia")] TacGia tacGia)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tacGia);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tacGia);
        }

        // GET: TacGia/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.TacGias == null)
            {
                return NotFound();
            }

            var tacGia = await _context.TacGias.FindAsync(id);
            if (tacGia == null)
            {
                return NotFound();
            }
            return View(tacGia);
        }

        // POST: TacGia/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TacGiaID,TenTacGia")] TacGia tacGia)
        {
            if (id != tacGia.TacGiaID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tacGia);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TacGiaExists(tacGia.TacGiaID))
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
            return View(tacGia);
        }

        // GET: TacGia/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.TacGias == null)
            {
                return NotFound();
            }

            var tacGia = await _context.TacGias
                .FirstOrDefaultAsync(m => m.TacGiaID == id);
            if (tacGia == null)
            {
                return NotFound();
            }

            return View(tacGia);
        }

        // POST: TacGia/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.TacGias == null)
            {
                return Problem("Entity set 'QLThuVienDBContext.TacGias'  is null.");
            }
            var tacGia = await _context.TacGias.FindAsync(id);
            if (tacGia != null)
            {
                _context.TacGias.Remove(tacGia);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TacGiaExists(int id)
        {
          return (_context.TacGias?.Any(e => e.TacGiaID == id)).GetValueOrDefault();
        }
    }
}
