using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Listovki_TSP_Uni.Models;
using TSP_Uni_Listovki.Data;

namespace TSP_Uni_Listovki.Controllers
{
    public class KormuvaneModelsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public KormuvaneModelsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: KormuvaneModels
        public async Task<IActionResult> Index()
        {
            return View(await _context.KormuvaneModel.ToListAsync());
        }

        // GET: KormuvaneModels/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var kormuvaneModel = await _context.KormuvaneModel
                .FirstOrDefaultAsync(m => m.id == id);
            if (kormuvaneModel == null)
            {
                return NotFound();
            }

            return View(kormuvaneModel);
        }

        // GET: KormuvaneModels/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: KormuvaneModels/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,izpitvasht,instruktor,tochki,chasoveKarani")] KormuvaneModel kormuvaneModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(kormuvaneModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(kormuvaneModel);
        }

        // GET: KormuvaneModels/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var kormuvaneModel = await _context.KormuvaneModel.FindAsync(id);
            if (kormuvaneModel == null)
            {
                return NotFound();
            }
            return View(kormuvaneModel);
        }

        // POST: KormuvaneModels/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,izpitvasht,instruktor,tochki,chasoveKarani")] KormuvaneModel kormuvaneModel)
        {
            if (id != kormuvaneModel.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(kormuvaneModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!KormuvaneModelExists(kormuvaneModel.id))
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
            return View(kormuvaneModel);
        }

        // GET: KormuvaneModels/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var kormuvaneModel = await _context.KormuvaneModel
                .FirstOrDefaultAsync(m => m.id == id);
            if (kormuvaneModel == null)
            {
                return NotFound();
            }

            return View(kormuvaneModel);
        }

        // POST: KormuvaneModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var kormuvaneModel = await _context.KormuvaneModel.FindAsync(id);
            _context.KormuvaneModel.Remove(kormuvaneModel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool KormuvaneModelExists(int id)
        {
            return _context.KormuvaneModel.Any(e => e.id == id);
        }
    }
}
