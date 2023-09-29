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
    public class SachController : Controller
    {
        private readonly QLThuVienDBContext _context;

        public SachController(QLThuVienDBContext context)
        {
            _context = context;
        }

        // GET: Sach
        public async Task<IActionResult> Index()
        {
              return _context.Sachs != null ? 
                          View(await _context.Sachs.ToListAsync()) :
                          Problem("Entity set 'QLThuVienDBContext.Sachs'  is null.");
        }

        /*
        public async Task<IActionResult> Index(int? id)
        {
            ViewBag.Sach = _context.Sachs.FirstOrDefault(sach => sach.SachID == id);

            var QLThuVienDBContext = _context.Sachs.Where(s => id == null || s.SachID == id)
                .Include(s  => s.SachID);
            return View(await QLThuVienDBContext.ToListAsync());
        }
        */

        public async Task<IActionResult> SearchSach(string txtSearch)
        {
            var SachDBContext = _context.Sachs.Where(m =>
            m.TenSach.Contains(txtSearch) || m.TenTheLoai.Contains(txtSearch) || m.TacGia.Contains(txtSearch))
                .Select(m => new Sach()
                {
                    SachID = m.SachID,
                    TenSach = m.TenSach,
                    TenTheLoai = m.TenTheLoai,
                    TacGia = m.TacGia
                });
            return View(nameof(Index), await SachDBContext.ToListAsync());
        }

        // GET: Sach/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Sachs == null)
            {
                return NotFound();
            }

            var sach = await _context.Sachs
                .FirstOrDefaultAsync(m => m.SachID == id);
            if (sach == null)
            {
                return NotFound();
            }

            return View(sach);
        }

        // GET: Sach/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Sach/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SachID,TenSach,TenTheLoai,TacGia,SoLuong,TacGiaID,TheLoaiID")] Sach sach)
        {
            if (ModelState.IsValid)
            {
                _context.Add(sach);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(sach);
        }

        // GET: Sach/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Sachs == null)
            {
                return NotFound();
            }

            var sach = await _context.Sachs.FindAsync(id);
            if (sach == null)
            {
                return NotFound();
            }
            return View(sach);
        }

        // POST: Sach/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("SachID,TenSach,TenTheLoai,TacGia,SoLuong,TacGiaID,TheLoaiID")] Sach sach)
        {
            if (id != sach.SachID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(sach);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SachExists(sach.SachID))
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
            return View(sach);
        }

        // GET: Sach/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Sachs == null)
            {
                return NotFound();
            }

            var sach = await _context.Sachs
                .FirstOrDefaultAsync(m => m.SachID == id);
            if (sach == null)
            {
                return NotFound();
            }

            return View(sach);
        }

        // POST: Sach/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Sachs == null)
            {
                return Problem("Entity set 'QLThuVienDBContext.Sachs'  is null.");
            }
            var sach = await _context.Sachs.FindAsync(id);
            if (sach != null)
            {
                _context.Sachs.Remove(sach);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SachExists(int id)
        {
          return (_context.Sachs?.Any(e => e.SachID == id)).GetValueOrDefault();
        }
    }
}
