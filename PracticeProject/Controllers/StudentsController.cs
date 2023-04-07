using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PracticeProject;
using PracticeProject.Models;

namespace PracticeProject.Controllers
{
    public class StudentsController : Controller
    {
        private readonly CCDbContext _context;

        public StudentsController(CCDbContext context)
        {
            _context = context;
        }

        // GET: Students
        public async Task<IActionResult> Index()
        {
              return _context.Student != null?
              View(await _context.Student.ToListAsync()) :
              Problem("Entity set 'CCDbContext.Student'  is null.");
        }

        // GET: Students/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Student == null)
            {
                return NotFound();
            }

            var student = await _context.Student
                .FirstOrDefaultAsync(m => m.Id == id);
            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }

        // GET: Students/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Students/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(/*[Bind("Id,DOB,Citizenshipnumber,Fathersname,Mothersname,Occupation,Name,Contact,Email,Address,ProfileImagePath")] */Student student)
        {


            
            if (ModelState.IsValid)
            {
                //var profileImage = student.ProfileImage;

                //var fileName = profileImage.FileName;   //38b7feaa-39d3-4ead-aeeb-ee7dff72cd4a_person.jpg
                //var uniqueFileName = $"{Guid.NewGuid()}_{fileName}";
                //var relativePath = $"/images/profiles/{uniqueFileName}";
                //var currentAppPath = Directory.GetCurrentDirectory();
                //var fullFilePath = Path.Combine(currentAppPath, $"wwwroot/{relativePath}");

                //var stream = System.IO.File.Create(fullFilePath);
                //await profileImage.CopyToAsync(stream);
                var relativePath = await SaveProfileImage(student.ProfileImage);
                student.ProfileImagePath = relativePath;
                _context.Add(student);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(student);
        }

        // GET: Students/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Student == null)
            {
                return NotFound();
            }

            var student = await _context.Student.FindAsync(id);
            if (student == null)
            {
                return NotFound();
            }
            return View(student);
        }

        // POST: Students/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,DOB,Citizenshipnumber,Fathersname,Mothersname,Occupation,Name,Contact,Email,Address")] Student student)
        {
            if (id != student.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(student);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StudentExists(student.Id))
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
            return View(student);
        }

        // GET: Students/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Student == null)
            {
                return NotFound();
            }

            var student = await _context.Student
                .FirstOrDefaultAsync(m => m.Id == id);
            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }

        // POST: Students/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Student == null)
            {
                return Problem("Entity set 'CCDbContext.Student'  is null.");
            }
            var student = await _context.Student.FindAsync(id);
            if (student != null)
            {
                _context.Student.Remove(student);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StudentExists(int id)
        {
          return (_context.Student?.Any(e => e.Id == id)).GetValueOrDefault();
        }


        private async Task<string> SaveProfileImage(IFormFile profileimage)
        {
            

            var fileName = profileimage.FileName;   //38b7feaa-39d3-4ead-aeeb-ee7dff72cd4a_person.jpg
            var uniqueFileName = $"{Guid.NewGuid()}_{fileName}";
            var relativePath = $"/images/profiles/{uniqueFileName}";
            var currentAppPath = Directory.GetCurrentDirectory();
            var fullFilePath = Path.Combine(currentAppPath, $"wwwroot/{relativePath}");

            var stream = System.IO.File.Create(fullFilePath);
            await profileimage.CopyToAsync(stream);

            return relativePath;
        }
    }
}
