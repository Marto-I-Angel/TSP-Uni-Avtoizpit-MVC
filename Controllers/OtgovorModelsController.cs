using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Listovki_TSP_Uni.Models;
using TSP_Uni_Listovki.Data;

namespace TSP_Uni_Listovki.Controllers
{
    public class OtgovorModelsController : Controller
    {
        public static int vuprosId;
        private readonly ApplicationDbContext _context;

        public OtgovorModelsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: OtgovorModels
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.OtgovorModel.Include(o => o.Vupros);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: OtgovorModels/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var otgovorModel = await _context.OtgovorModel
                .Include(o => o.Vupros)
                .FirstOrDefaultAsync(m => m.id == id);
            if (otgovorModel == null)
            {
                return NotFound();
            }

            return View(otgovorModel);
        }

        // GET: OtgovorModels/Create
        public IActionResult Create()
        {
            ViewData["VuprosID"] = new SelectList(_context.Set<VuprosModel>(), "id", "id");
            return View();
        }

        // POST: OtgovorModels/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ICollection<string> otgovorContent, ICollection<string> otgovorVeren, ICollection<string> otgovorIzobrajenie)
        {
            if (ModelState.IsValid)
            {
                List<OtgovorModel> otgovorModels = new List<OtgovorModel>();
                for(int i=0; i<4; i++)
                {
                    if(otgovorContent.ElementAt(i)!="" || otgovorIzobrajenie.ElementAt(i)!="")
                    otgovorModels.Add(new OtgovorModel(otgovorContent.ElementAt(i), otgovorIzobrajenie.ElementAt(i), (otgovorVeren.ElementAt(i)=="true"), vuprosId));
                }

                foreach(OtgovorModel otgovorModel in otgovorModels) {
                    _context.Add(otgovorModel);
                }
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View();
        }

        // GET: OtgovorModels/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var otgovorModel = await _context.OtgovorModel.FindAsync(id);
            if (otgovorModel == null)
            {
                return NotFound();
            }
            ViewData["VuprosID"] = new SelectList(_context.Set<VuprosModel>(), "id", "id", otgovorModel.VuprosID);
            return View(otgovorModel);
        }

        // POST: OtgovorModels/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,Content,izobrajenie,veren,VuprosID")] OtgovorModel otgovorModel)
        {
            if (id != otgovorModel.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(otgovorModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OtgovorModelExists(otgovorModel.id))
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
            return View(otgovorModel);
        }

        // GET: OtgovorModels/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var otgovorModel = await _context.OtgovorModel
                .Include(o => o.Vupros)
                .FirstOrDefaultAsync(m => m.id == id);
            if (otgovorModel == null)
            {
                return NotFound();
            }

            return View(otgovorModel);
        }

        // POST: OtgovorModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var otgovorModel = await _context.OtgovorModel.FindAsync(id);
            _context.OtgovorModel.Remove(otgovorModel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OtgovorModelExists(int id)
        {
            return _context.OtgovorModel.Any(e => e.id == id);
        }
    }
}
