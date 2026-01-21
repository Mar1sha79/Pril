using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Приложение.Data;
using Приложение.Models;

namespace Приложение.Controllers
{
    public class ResultsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ResultsController(ApplicationDbContext context) => _context = context;

        public async Task<IActionResult> Index()
        {
            var results = await _context.results
                .Include(r => r.Competition)
                .Include(r => r.Participant)
                .ToListAsync();
            return View(results);
        }

        // --- ДОБАВЛЕННЫЙ МЕТОД ДЛЯ КНОПКИ "ИНФО" ---
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var result = await _context.results
                .Include(r => r.Competition)
                .Include(r => r.Participant)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (result == null) return NotFound();
            return View(result);
        }

        public IActionResult Create()
        {
            ViewBag.CompetitionId = new SelectList(_context.competitions, "Id", "Name");
            ViewBag.ParticipantId = new SelectList(_context.participants, "Id", "FullName");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Result result)
        {
            _context.Add(result);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // --- ДОБАВЛЕННЫЕ МЕТОДЫ ДЛЯ КНОПКИ "РЕДАКТИРОВАТЬ" ---
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var result = await _context.results.FindAsync(id);
            if (result == null) return NotFound();

            // Заполняем списки, чтобы в форме редактирования можно было выбрать участника/соревнование
            ViewBag.CompetitionId = new SelectList(_context.competitions, "Id", "Name", result.CompetitionId);
            ViewBag.ParticipantId = new SelectList(_context.participants, "Id", "FullName", result.ParticipantId);
            return View(result);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Result result)
        {
            if (id != result.Id) return NotFound();

            if (ModelState.IsValid)
            {
                _context.Update(result);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(result);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var result = await _context.results.FindAsync(id);
            if (result != null) _context.results.Remove(result);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}