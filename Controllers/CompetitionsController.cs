using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Приложение.Data;
using Приложение.Models;

namespace Приложение.Controllers
{
    public class CompetitionsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CompetitionsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // 1. СПИСОК (Index)
        public async Task<IActionResult> Index()
        {
            var competitions = await _context.competitions.ToListAsync();
            return View(competitions);
        }

        // --- ДОБАВЛЕННЫЙ МЕТОД ДЛЯ КНОПКИ "ИНФО" ---
        // Отображает подробную информацию об одном соревновании
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            // Поиск соревнования по ID
            var competition = await _context.competitions
                .FirstOrDefaultAsync(m => m.Id == id);

            if (competition == null) return NotFound();

            return View(competition);
        }

        // 2. ДОБАВЛЕНИЕ (Create)
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Competition competition)
        {
            if (ModelState.IsValid)
            {
                _context.Add(competition);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(competition);
        }

        // 3. РЕДАКТИРОВАНИЕ (Edit)
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var competition = await _context.competitions.FindAsync(id);
            if (competition == null) return NotFound();

            return View(competition);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Competition competition)
        {
            if (id != competition.Id) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(competition);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CompetitionExists(competition.Id)) return NotFound();
                    else throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(competition);
        }

        // 4. УДАЛЕНИЕ (Delete)
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var competition = await _context.competitions.FindAsync(id);
            if (competition != null)
            {
                _context.competitions.Remove(competition);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        private bool CompetitionExists(int id)
        {
            return _context.competitions.Any(e => e.Id == id);
        }
    }
}