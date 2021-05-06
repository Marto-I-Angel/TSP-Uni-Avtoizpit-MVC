using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Listovki_TSP_Uni.Models;
using TSP_Uni_Listovki.Data;
using Microsoft.AspNetCore.Authorization;

namespace TSP_Uni_Listovki.Controllers
{

    public class IzpitModelsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public IzpitModelsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: IzpitModels
        [Authorize(Policy = "Admin")]
        public async Task<IActionResult> Index()
        {
            return View(await _context.IzpitModel.ToListAsync());
        }

        // GET: IzpitModels/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var izpitModel = await _context.IzpitModel
                .FirstOrDefaultAsync(m => m.id == id);
            if (izpitModel == null)
            {
                return NotFound();
            }

            return View(izpitModel);
        }

        // GET: IzpitModels/Create
        [Authorize(Policy = "User")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: IzpitModels/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "User")]
        public async Task<IActionResult> Create([Bind("id")] IzpitModel izpitModel)
        {
            //Решаване на листовка ако вече не е била направена.
            //Създаване и свързване с листовка, пренасочване към create страницата на листовката.
            //

            //право на 1 изпит на човек? 

            //След като вече е направен изпита, се пренасочва към Details където се показват точките/дали е минал или не
            //Допълнителна промяна може да се прави само от админ, който ще въведе информацията от кормуването.
            if (ModelState.IsValid)
            {
                _context.Add(izpitModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(izpitModel);
        }

        // GET: IzpitModels/Edit/5
        [Authorize(Policy = "Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var izpitModel = await _context.IzpitModel.FindAsync(id);
            if (izpitModel == null)
            {
                return NotFound();
            }
            return View(izpitModel);
        }

        // POST: IzpitModels/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "Admin")]
        public async Task<IActionResult> Edit(int id, [Bind("id")] IzpitModel izpitModel)
        {
            if (id != izpitModel.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(izpitModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!IzpitModelExists(izpitModel.id))
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
            return View(izpitModel);
        }

        // GET: IzpitModels/Delete/5
        [Authorize(Policy = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var izpitModel = await _context.IzpitModel
                .FirstOrDefaultAsync(m => m.id == id);
            if (izpitModel == null)
            {
                return NotFound();
            }

            return View(izpitModel);
        }

        // POST: IzpitModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "Admin")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var izpitModel = await _context.IzpitModel.FindAsync(id);
            _context.IzpitModel.Remove(izpitModel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool IzpitModelExists(int id)
        {
            return _context.IzpitModel.Any(e => e.id == id);
        }
    }
}
