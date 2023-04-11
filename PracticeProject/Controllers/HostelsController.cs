using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PracticeProject;
using PracticeProject.Models;

namespace PracticeProject.Controllers
{
    public class HostelsController : Controller
    {
        
        private readonly CCDbContext _context;

       
        public HostelsController(CCDbContext context)
        {
            _context = context;
        }

        // GET: Hostels
        public async Task<IActionResult> Index()
        {
            var cCDbContext = _context.Hostel.Include(h => h.Student);
            return View(await cCDbContext.ToListAsync());
        }

        // GET: Hostels/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Hostel == null)
            {
                return NotFound();
            }

            var hostel = await _context.Hostel
                .Include(h => h.Student)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (hostel == null)
            {
                return NotFound();
            }

            return View(hostel);
        }

        // GET: Hostels/Create
        public IActionResult Create()
        {
            ViewData["StudentID"] = new SelectList(_context.Set<Student>(), "Id", "Id");
            return View();
        }

        // POST: Hostels/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Hostel hostel)
        {
            if (ModelState.IsValid)
            {
                string? relativePath = SaveProfileImage(hostel.ProfileImage);
                hostel.ProfileImagePath = relativePath;
                _context.Add(hostel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["StudentID"] = new SelectList(_context.Set<Student>(), "Id", "Id", hostel.StudentID);
            return View(hostel);
        }

        // GET: Hostels/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Hostel == null)
            {
                return NotFound();
            }

            var hostel = await _context.Hostel.FindAsync(id);
            if (hostel == null)
            {
                return NotFound();
            }
            ViewData["StudentID"] = new SelectList(_context.Set<Student>(), "Id", "Id", hostel.StudentID);
            return View(hostel);
        }

        // POST: Hostels/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Emptyseats,Totalseats,PAN,StudentID,Name,Contact,Email,Address")] Hostel hostel)
        {
            if (id != hostel.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(hostel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HostelExists(hostel.ID))
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
            ViewData["StudentID"] = new SelectList(_context.Set<Student>(), "Id", "Id", hostel.StudentID);
            return View(hostel);
        }

        // GET: Hostels/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Hostel == null)
            {
                return NotFound();
            }

            var hostel = await _context.Hostel
                .Include(h => h.Student)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (hostel == null)
            {
                return NotFound();
            }

            return View(hostel);
        }

        // POST: Hostels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Hostel == null)
            {
                return Problem("Entity set 'CCDbContext.Hostel'  is null.");
            }
            var hostel = await _context.Hostel.FindAsync(id);
            if (hostel != null)
            {
                _context.Hostel.Remove(hostel);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool HostelExists(int id)
        {
          return (_context.Hostel?.Any(e => e.ID == id)).GetValueOrDefault();
        }
        private string SaveProfileImage(IFormFile profileimage)
        {


            var fileName = profileimage.FileName;   //38b7feaa-39d3-4ead-aeeb-ee7dff72cd4a_person.jpg
            var uniqueFileName = $"{Guid.NewGuid()}_{fileName}";
            var relativePath = $"/images/profiles/{uniqueFileName}";
            var currentAppPath = Directory.GetCurrentDirectory();
            var fullFilePath = Path.Combine(currentAppPath, $"wwwroot/{relativePath}");

            var stream = System.IO.File.Create(fullFilePath);
            profileimage.CopyToAsync(stream);

            return relativePath;
        }
    }
}
