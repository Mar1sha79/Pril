using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Приложение.Data;
using Приложение.Models;

namespace Приложение.Controllers
{
    public class ParticipantsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ParticipantsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Список участников
        public async Task<IActionResult> Index()
        {
            return View(await _context.participants.ToListAsync());
        }

        // --- ДОБАВЛЕННЫЙ МЕТОД ДЛЯ КНОПКИ ИНФО ---
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            // Поиск участника по ID в базе данных
            var participant = await _context.participants
                .FirstOrDefaultAsync(m => m.Id == id);

            if (participant == null) return NotFound();

            return View(participant);
        }

        // Страница создания
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Participant participant)
        {
            if (ModelState.IsValid)
            {
                _context.Add(participant);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(participant);
        }

        // Редактирование
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();
            var participant = await _context.participants.FindAsync(id);
            if (participant == null) return NotFound();
            return View(participant);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Participant participant)
        {
            if (id != participant.Id) return NotFound();
            if (ModelState.IsValid)
            {
                _context.Update(participant);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(participant);
        }

        // Удаление
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();
            var participant = await _context.participants.FindAsync(id);
            if (participant != null)
            {
                _context.participants.Remove(participant);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}