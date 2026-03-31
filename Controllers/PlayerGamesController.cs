using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GamePlatform.Data;
using GamePlatform.Models;

namespace Wprawka1.Controllers
{
    public class PlayerGamesController : Controller
    {
        private readonly GamePlatformContext _context;

        public PlayerGamesController(GamePlatformContext context)
        {
            _context = context;
        }

        // GET: PlayerGames
        public async Task<IActionResult> Index()
        {
            var playerGames = await _context.PlayerGame
                .Include(pg => pg.Player)
                .Include(pg => pg.Game)
                .ToListAsync();

            return View(playerGames);
        }

        // GET: PlayerGames/Details/5
        public async Task<IActionResult> Details(int playerId, int gameId)
        {
            var playerGame = await _context.PlayerGame
                .Include(pg => pg.Player)
                .Include(pg => pg.Game)
                .FirstOrDefaultAsync(pg => pg.PlayerId == playerId && pg.GameId == gameId);

            if (playerGame == null) return NotFound();

            return View(playerGame);
        }

        // GET: PlayerGames/Create
        public IActionResult Create()
        {
            ViewBag.PlayerId = new SelectList(_context.Player, "Id", "Username");
            ViewBag.GameId = new SelectList(_context.Game, "Id", "Title");
            return View();
        }

        // POST: PlayerGames/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PlayerGame playerGame)
        {
            // Sprawdź czy taka kombinacja już istnieje
            bool alreadyExists = await _context.PlayerGame
                .AnyAsync(pg => pg.PlayerId == playerGame.PlayerId
                             && pg.GameId == playerGame.GameId);

            if (alreadyExists)
            {
                ModelState.AddModelError("", "Ten gracz jest już przypisany do tej gry.");
                ViewBag.PlayerId = new SelectList(_context.Player, "Id", "Username", playerGame.PlayerId);
                ViewBag.GameId = new SelectList(_context.Game, "Id", "Title", playerGame.GameId);
                return View(playerGame);
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.PlayerGame.Add(playerGame);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateException)
                {
                    ModelState.AddModelError("", "Nie udało się zapisać. Sprawdź czy gracz i gra istnieją.");
                    ViewBag.PlayerId = new SelectList(_context.Player, "Id", "Username", playerGame.PlayerId);
                    ViewBag.GameId = new SelectList(_context.Game, "Id", "Title", playerGame.GameId);
                    return View(playerGame);
                }
            }

            ViewBag.PlayerId = new SelectList(_context.Player, "Id", "Username", playerGame.PlayerId);
            ViewBag.GameId = new SelectList(_context.Game, "Id", "Title", playerGame.GameId);
            return View(playerGame);
        }

        // GET: PlayerGames/Edit/5
        public async Task<IActionResult> Edit(int playerId, int gameId)
        {
            var playerGame = await _context.PlayerGame
                .FirstOrDefaultAsync(pg => pg.PlayerId == playerId && pg.GameId == gameId);

            if (playerGame == null) return NotFound();

            ViewBag.PlayerId = new SelectList(_context.Player, "Id", "Username", playerGame.PlayerId);
            ViewBag.GameId = new SelectList(_context.Game, "Id", "Title", playerGame.GameId);
            return View(playerGame);
        }

        // POST: PlayerGames/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int playerId, int gameId, PlayerGame playerGame)
        {
            if (playerId != playerGame.PlayerId || gameId != playerGame.GameId)
                return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(playerGame);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PlayerGameExists(playerGame.PlayerId, playerGame.GameId))
                        return NotFound();
                    else
                        throw;
                }
                return RedirectToAction(nameof(Index));
            }

            ViewBag.PlayerId = new SelectList(_context.Player, "Id", "Username", playerGame.PlayerId);
            ViewBag.GameId = new SelectList(_context.Game, "Id", "Title", playerGame.GameId);
            return View(playerGame);
        }

        // GET: PlayerGames/Delete/5
        public async Task<IActionResult> Delete(int playerId, int gameId)
        {
            var playerGame = await _context.PlayerGame
                .Include(pg => pg.Player)
                .Include(pg => pg.Game)
                .FirstOrDefaultAsync(pg => pg.PlayerId == playerId && pg.GameId == gameId);

            if (playerGame == null) return NotFound();

            return View(playerGame);
        }

        // POST: PlayerGames/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int playerId, int gameId)
        {
            var playerGame = await _context.PlayerGame
                .FirstOrDefaultAsync(pg => pg.PlayerId == playerId && pg.GameId == gameId);

            if (playerGame != null)
            {
                _context.PlayerGame.Remove(playerGame);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        private bool PlayerGameExists(int playerId, int gameId)
        {
            return _context.PlayerGame.Any(pg => pg.PlayerId == playerId && pg.GameId == gameId);
        }
    }
}