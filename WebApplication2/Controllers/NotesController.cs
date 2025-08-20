using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApplication2.Db;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
    public class FormModel
    {
        public string? SearchQuery { get; set; }
    }
    
    public class NotesController : Controller
    {
        private readonly NoteDbContext _context;

        public NotesController(NoteDbContext context)
        {
            _context = context;
        }

        // GET: Notes
        public async Task<IActionResult> Index()
        {
            var user = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var role = User.FindFirstValue(ClaimTypes.Role);
            
            return View(await _context.Notes
                .Include(a => a.Tags)
                .Where(a => a.ProfileId == user || role  == "Admin")
                .ToListAsync()
            );
        }

        [HttpPost]
        public async Task<IActionResult> Index(FormModel model)
        {
            var user = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var role = User.FindFirstValue(ClaimTypes.Role);
            if (model.SearchQuery == null)
            {
                model.SearchQuery = "";
            }
            model.SearchQuery = model.SearchQuery.ToLower();
            Console.WriteLine(model.SearchQuery);
            return View(
                await _context.Notes
                    .Include(a => a.Tags)
                    .Where(a => a.ProfileId == user || role  == "Admin")
                    .Where(note => (note.Name.ToLower().IndexOf(model.SearchQuery) != -1 || note.Content.ToLower().IndexOf(model.SearchQuery) != -1))
                    .ToListAsync()
            );
        }

        // GET: Notes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var noteEntityModel = await _context.Notes
                .Include(a => a.Tags)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (noteEntityModel == null)
            {
                return NotFound();
            }

            return View(noteEntityModel);
        }

        // GET: Notes/Create
        public IActionResult Create()
        {
            ViewData["Tags"] = new MultiSelectList(_context.Tags, "Id", "Name");
            return View();
        }

        // POST: Notes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(NoteViewModel noteViewModel)
        {
            Console.WriteLine(ModelState.IsValid);
            if (ModelState.IsValid)
            {
                Console.WriteLine("model is valid");
                var user = User.FindFirstValue(ClaimTypes.NameIdentifier);
                Console.WriteLine(user);
                
                var tags = _context.Tags
                    .Where(t => noteViewModel.TagIds.Contains(t.Id))
                    .ToList();
                
                var noteEntityModel = NoteEntityModel.FromViewModel(noteViewModel, user, tags);
                
                
                _context.Add(noteEntityModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return StatusCode(400);
        }

        // GET: Notes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var noteEntityModel = await _context.Notes.FindAsync(id);
            if (noteEntityModel == null)
            {
                return NotFound();
            }
            ViewData["Tags"] = new MultiSelectList(_context.Tags, "Id", "Name");

            var viewModel = NoteViewModel.FromEntity(noteEntityModel);
            
            return View(viewModel);
        }

        // POST: Notes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, NoteViewModel noteViewModel)
        {
            var user = User.FindFirstValue(ClaimTypes.NameIdentifier);
            Console.WriteLine(user);
            
                 
            // var noteEntityModel = NoteEntityModel.FromViewModel(noteViewModel, user, tags);
            // noteEntityModel.Id = id;
            
            var existingNote = await _context.Notes
                .Include(n => n.Tags) // Include the tags relationship
                .Where(n => n.ProfileId == user)
                .FirstOrDefaultAsync(n => n.Id == id);

            if (existingNote == null) return NotFound();
            
            var tags = _context.Tags
                .Where(t => noteViewModel.TagIds.Contains(t.Id))
                .ToList();
            
            if (ModelState.IsValid)
            {
                try
                {
                    // _context.Update(noteEntityModel);
                    existingNote.Tags = tags;
                    existingNote.Content = noteViewModel.Content;
                    existingNote.Name = noteViewModel.Name;
                    
                    
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NoteEntityModelExists(id))
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
            return View(noteViewModel);
        }

        // GET: Notes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var noteEntityModel = await _context.Notes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (noteEntityModel == null)
            {
                return NotFound();
            }

            return View(noteEntityModel);
        }

        // POST: Notes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var noteEntityModel = await _context.Notes.FindAsync(id);
            if (noteEntityModel != null)
            {
                _context.Notes.Remove(noteEntityModel);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool NoteEntityModelExists(int id)
        {
            return _context.Notes.Any(e => e.Id == id);
        }
    }
}
