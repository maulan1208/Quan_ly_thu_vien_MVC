using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BTL.Models;
using Newtonsoft.Json;

namespace BTL.Controllers
{
    public class PhieuMuonController : Controller
    {
        private readonly QLThuVienDBContext _context;

        public PhieuMuonController(QLThuVienDBContext context)
        {
            _context = context;
        }

        // GET: PhieuMuon
        public async Task<IActionResult> Index()
        {
              return _context.PhieuMuons != null ? 
                          View(await _context.PhieuMuons.ToListAsync()) :
                          Problem("Entity set 'QLThuVienDBContext.PhieuMuons'  is null.");
        }

        public async Task<IActionResult> SearchPM(string txtSearch)
        {
            var PhieuMuonDBContext = _context.PhieuMuons.Where(m =>
            m.TenDocGia.Contains(txtSearch))
                .Select(m => new PhieuMuon()
                {
                    PhieuMuonID = m.PhieuMuonID,
                    DocGiaID = m.DocGiaID,
                    TenDocGia = m.TenDocGia
                });
            return View(nameof(Index), await PhieuMuonDBContext.ToListAsync());
        }



        // GET: PhieuMuon/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.PhieuMuons == null)
            {
                return NotFound();
            }

            var phieuMuon = await _context.PhieuMuons
                .FirstOrDefaultAsync(m => m.PhieuMuonID == id);
            if (phieuMuon == null)
            {
                return NotFound();
            }

            return View(phieuMuon);
        }

        // GET: PhieuMuon/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: PhieuMuon/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PhieuMuonID,DocGiaID,TenDocGia,NgayMuon,NgayTra")] PhieuMuon phieuMuon)
        {
            if (ModelState.IsValid)
            {
                _context.Add(phieuMuon);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(phieuMuon);
        }

        // GET: PhieuMuon/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.PhieuMuons == null)
            {
                return NotFound();
            }

            var phieuMuon = await _context.PhieuMuons.FindAsync(id);
            if (phieuMuon == null)
            {
                return NotFound();
            }
            return View(phieuMuon);
        }

        // POST: PhieuMuon/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PhieuMuonID,DocGiaID,TenDocGia,NgayMuon,NgayTra")] PhieuMuon phieuMuon)
        {
            if (id != phieuMuon.PhieuMuonID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(phieuMuon);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PhieuMuonExists(phieuMuon.PhieuMuonID))
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
            return View(phieuMuon);
        }

        // GET: PhieuMuon/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.PhieuMuons == null)
            {
                return NotFound();
            }

            var phieuMuon = await _context.PhieuMuons
                .FirstOrDefaultAsync(m => m.PhieuMuonID == id);
            if (phieuMuon == null)
            {
                return NotFound();
            }

            return View(phieuMuon);
        }

        // POST: PhieuMuon/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.PhieuMuons == null)
            {
                return Problem("Entity set 'QLThuVienDBContext.PhieuMuons'  is null.");
            }
            var phieuMuon = await _context.PhieuMuons.FindAsync(id);
            if (phieuMuon != null)
            {
                _context.PhieuMuons.Remove(phieuMuon);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PhieuMuonExists(int id)
        {
          return (_context.PhieuMuons?.Any(e => e.PhieuMuonID == id)).GetValueOrDefault();
        }
    }
}
