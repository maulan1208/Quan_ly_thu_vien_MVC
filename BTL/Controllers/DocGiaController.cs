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
    public class DocGiaController : Controller
    {
        private readonly QLThuVienDBContext _context;

        public DocGiaController(QLThuVienDBContext context)
        {
            _context = context;
        }

        // GET: DocGia
        public async Task<IActionResult> Index()
        {
              return _context.DocGias != null ? 
                          View(await _context.DocGias.ToListAsync()) :
                          Problem("Entity set 'QLThuVienDBContext.DocGias'  is null.");
        }

        public async Task<IActionResult> SearchDG(string txtSearch)
        {
            var DocGiaDBContext = _context.DocGias.Where(m =>
            m.TenDocGia.Contains(txtSearch)).Select(m => new DocGia()
            {
                DocGiaID = m.DocGiaID,
                TenDocGia = m.TenDocGia
            });
            return View(nameof(Index), await DocGiaDBContext.ToListAsync());
        }

        // GET: DocGia/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.DocGias == null)
            {
                return NotFound();
            }

            var docGia = await _context.DocGias
                .FirstOrDefaultAsync(m => m.DocGiaID == id);
            if (docGia == null)
            {
                return NotFound();
            }

            return View(docGia);
        }

        // GET: DocGia/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: DocGia/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DocGiaID,TenDocGia,SDT,DiaChi")] DocGia docGia)
        {
            if (ModelState.IsValid)
            {
                _context.Add(docGia);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(docGia);
        }

        // GET: DocGia/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.DocGias == null)
            {
                return NotFound();
            }

            var docGia = await _context.DocGias.FindAsync(id);
            if (docGia == null)
            {
                return NotFound();
            }
            return View(docGia);
        }

        // POST: DocGia/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("DocGiaID,TenDocGia,SDT,DiaChi")] DocGia docGia)
        {
            if (id != docGia.DocGiaID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(docGia);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DocGiaExists(docGia.DocGiaID))
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
            return View(docGia);
        }

        // GET: DocGia/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.DocGias == null)
            {
                return NotFound();
            }

            var docGia = await _context.DocGias
                .FirstOrDefaultAsync(m => m.DocGiaID == id);
            if (docGia == null)
            {
                return NotFound();
            }

            return View(docGia);
        }

        // POST: DocGia/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.DocGias == null)
            {
                return Problem("Entity set 'QLThuVienDBContext.DocGias'  is null.");
            }
            var docGia = await _context.DocGias.FindAsync(id);
            if (docGia != null)
            {
                _context.DocGias.Remove(docGia);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DocGiaExists(int id)
        {
          return (_context.DocGias?.Any(e => e.DocGiaID == id)).GetValueOrDefault();
        }
    }
}
