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
    public class ChiTiet_PMController : Controller
    {
        private readonly QLThuVienDBContext _context;

        public ChiTiet_PMController(QLThuVienDBContext context)
        {
            _context = context;
        }

        // GET: ChiTiet_PM
        public async Task<IActionResult> Index()
        {
              return _context.ChiTiet_PMs != null ? 
                          View(await _context.ChiTiet_PMs.ToListAsync()) :
                          Problem("Entity set 'QLThuVienDBContext.ChiTiet_PMs'  is null.");
        }

        public async Task<IActionResult> SearchCT(string txtSearch)
        {
            var ChiTiet_PMDBContext = _context.ChiTiet_PMs.Where(m =>
            m.TenSach.Contains(txtSearch) || m.TenDocGia.Contains(txtSearch) || m.TrangThai.Contains(txtSearch))
                .Select(m => new ChiTiet_PM()
                {
                    ChiTietID = m.ChiTietID,
                    TenSach = m.TenSach,
                    TenDocGia = m.TenDocGia,
                    TrangThai = m.TrangThai,
                });
            return View(nameof(Index), await ChiTiet_PMDBContext.ToListAsync());
        }

        // GET: ChiTiet_PM/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.ChiTiet_PMs == null)
            {
                return NotFound();
            }

            var chiTiet_PM = await _context.ChiTiet_PMs
                .FirstOrDefaultAsync(m => m.ChiTietID == id);
            if (chiTiet_PM == null)
            {
                return NotFound();
            }

            return View(chiTiet_PM);
        }

        // GET: ChiTiet_PM/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ChiTiet_PM/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ChiTietID,PhieuMuonID,DocGiaID,TenDocGia,SachID,TenSach,SoLuong,TrangThai,NgayTraThucTe")] ChiTiet_PM chiTiet_PM)
        {
            if (ModelState.IsValid)
            {
                _context.Add(chiTiet_PM);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(chiTiet_PM);
        }

        // GET: ChiTiet_PM/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.ChiTiet_PMs == null)
            {
                return NotFound();
            }

            var chiTiet_PM = await _context.ChiTiet_PMs.FindAsync(id);
            if (chiTiet_PM == null)
            {
                return NotFound();
            }
            return View(chiTiet_PM);
        }

        // POST: ChiTiet_PM/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ChiTietID,PhieuMuonID,DocGiaID,TenDocGia,SachID,TenSach,SoLuong,TrangThai,NgayTraThucTe")] ChiTiet_PM chiTiet_PM)
        {
            if (id != chiTiet_PM.ChiTietID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(chiTiet_PM);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ChiTiet_PMExists(chiTiet_PM.ChiTietID))
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
            return View(chiTiet_PM);
        }

        // GET: ChiTiet_PM/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.ChiTiet_PMs == null)
            {
                return NotFound();
            }

            var chiTiet_PM = await _context.ChiTiet_PMs
                .FirstOrDefaultAsync(m => m.ChiTietID == id);
            if (chiTiet_PM == null)
            {
                return NotFound();
            }

            return View(chiTiet_PM);
        }

        // POST: ChiTiet_PM/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.ChiTiet_PMs == null)
            {
                return Problem("Entity set 'QLThuVienDBContext.ChiTiet_PMs'  is null.");
            }
            var chiTiet_PM = await _context.ChiTiet_PMs.FindAsync(id);
            if (chiTiet_PM != null)
            {
                _context.ChiTiet_PMs.Remove(chiTiet_PM);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ChiTiet_PMExists(int id)
        {
          return (_context.ChiTiet_PMs?.Any(e => e.ChiTietID == id)).GetValueOrDefault();
        }
    }
}
