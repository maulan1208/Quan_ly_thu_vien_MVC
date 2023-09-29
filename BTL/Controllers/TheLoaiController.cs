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
    public class TheLoaiController : Controller
    {
        private readonly QLThuVienDBContext _context;

        public TheLoaiController(QLThuVienDBContext context)
        {
            _context = context;
        }

        // GET: TheLoai
        public async Task<IActionResult> Index(int id)
        {
              return _context.TheLoais != null ? 
                          View(await _context.TheLoais.ToListAsync()) :
                          Problem("Entity set 'QLThuVienDBContext.TheLoais'  is null.");
        }
        

        public async Task<IActionResult> SearchTL(string txtSearch)
        {
            var TheLoaiDBContext = _context.TheLoais.Where(m =>
            m.TenTheLoai.Contains(txtSearch))
                .Select(m => new TheLoai()
                {
                    TheLoaiID = m.TheLoaiID,
                    TenTheLoai = m.TenTheLoai,
                    
                });
            return View(nameof(Index), await TheLoaiDBContext.ToListAsync());
        }

        // GET: TheLoai/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.TheLoais == null)
            {
                return NotFound();
            }

            var theLoai = await _context.TheLoais
                .FirstOrDefaultAsync(m => m.TheLoaiID == id);
            if (theLoai == null)
            {
                return NotFound();
            }

            return View(theLoai);
        }

        // GET: TheLoai/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TheLoai/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TheLoaiID,TenTheLoai")] TheLoai theLoai)
        {
            if (ModelState.IsValid)
            {
                _context.Add(theLoai);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(theLoai);
        }

        // GET: TheLoai/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.TheLoais == null)
            {
                return NotFound();
            }

            var theLoai = await _context.TheLoais.FindAsync(id);
            if (theLoai == null)
            {
                return NotFound();
            }
            return View(theLoai);
        }

        // POST: TheLoai/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TheLoaiID,TenTheLoai")] TheLoai theLoai)
        {
            if (id != theLoai.TheLoaiID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(theLoai);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TheLoaiExists(theLoai.TheLoaiID))
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
            return View(theLoai);
        }

        // GET: TheLoai/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.TheLoais == null)
            {
                return NotFound();
            }

            var theLoai = await _context.TheLoais
                .FirstOrDefaultAsync(m => m.TheLoaiID == id);
            if (theLoai == null)
            {
                return NotFound();
            }

            return View(theLoai);
        }

        // POST: TheLoai/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.TheLoais == null)
            {
                return Problem("Entity set 'QLThuVienDBContext.TheLoais'  is null.");
            }
            var theLoai = await _context.TheLoais.FindAsync(id);
            if (theLoai != null)
            {
                _context.TheLoais.Remove(theLoai);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TheLoaiExists(int id)
        {
          return (_context.TheLoais?.Any(e => e.TheLoaiID == id)).GetValueOrDefault();
        }
    }
}
