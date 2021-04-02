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
    public class VuprosModelsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public VuprosModelsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: VuprosModels
        public async Task<IActionResult> Index()
        {
            return View(await _context.VuprosModel.ToListAsync());
        }

        // GET: VuprosModels/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vuprosModel = await _context.VuprosModel
                .FirstOrDefaultAsync(m => m.id == id);
            if (vuprosModel == null)
            {
                return NotFound();
            }

            return View(vuprosModel);
        }

        // GET: VuprosModels/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: VuprosModels/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        //Qvno tuk idva informaciqta ot formata. Shte trqbva po nqkakuv nachin da suzdavame 3-4 Otgovora i da gi svurzvame s tozi vupros
        public async Task<IActionResult> Create(VuprosModel vuprosModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(vuprosModel);
                await _context.SaveChangesAsync();
                OtgovorModelsController.vuprosId = vuprosModel.id;
                return RedirectToAction("Create","OtgovorModels");
            }
            return View(vuprosModel);
        }

        // GET: VuprosModels/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vuprosModel = await _context.VuprosModel.FindAsync(id);
            if (vuprosModel == null)
            {
                return NotFound();
            }
            return View(vuprosModel);
        }

        // POST: VuprosModels/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,uslovie,img,tochki")] VuprosModel vuprosModel)
        {
            if (id != vuprosModel.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(vuprosModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VuprosModelExists(vuprosModel.id))
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
            return View(vuprosModel);
        }

        // GET: VuprosModels/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vuprosModel = await _context.VuprosModel
                .FirstOrDefaultAsync(m => m.id == id);
            if (vuprosModel == null)
            {
                return NotFound();
            }

            return View(vuprosModel);
        }

        // POST: VuprosModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var vuprosModel = await _context.VuprosModel.FindAsync(id);
            _context.VuprosModel.Remove(vuprosModel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VuprosModelExists(int id)
        {
            return _context.VuprosModel.Any(e => e.id == id);
        }
    }
}
