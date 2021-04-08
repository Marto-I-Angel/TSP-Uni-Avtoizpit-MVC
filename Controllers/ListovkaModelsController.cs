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
    public class ListovkaModelsController : Controller
    {
        private Random rng = new Random();
        private readonly ApplicationDbContext _context;

        public ListovkaModelsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: ListovkaModels
        public async Task<IActionResult> Index()
        {
            return View(await _context.ListovkaModel.ToListAsync());
        }

        // GET: ListovkaModels/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var listovkaModel = await _context.ListovkaModel
                .FirstOrDefaultAsync(m => m.id == id);
            if (listovkaModel == null)
            {
                return NotFound();
            }

            var vruzki =_context.VuprosiZaListovka.Where(v => v.ListovkaID == id).ToList();
            List<VuprosModel> vuprosi = new List<VuprosModel>();
            foreach(var vruzka in vruzki)
            {
                vuprosi.Add(_context.VuprosModel.Where(v => v.id == vruzka.VuprosId).Single());
            }
            foreach(var vupros in vuprosi)
            {
                vupros.Otgovori = _context.OtgovorModel.Where(v => v.VuprosID == vupros.id).ToList();
            }
            ViewData["vuprosi"] = vuprosi;
            return View(listovkaModel);
        }

        // GET: ListovkaModels/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ListovkaModels/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,timestamp,tochki")] ListovkaModel listovkaModel)
        {
            if (ModelState.IsValid)
            {

                var all = _context.VuprosModel.ToList();

                var listovka = generateListovka(all);

                listovkaModel.timestamp = DateTime.Now;
                listovkaModel.tochki = 0;
                var user = User.Identity;

                listovkaModel.userName=user.Name;

                _context.Add(listovkaModel);
                await _context.SaveChangesAsync();



                foreach (VuprosModel vupros in listovka)
                {
                    VuprosiZaListovka vuprosiZaListovka = new VuprosiZaListovka();
                    vuprosiZaListovka.ListovkaID = listovkaModel.id;
                    vuprosiZaListovka.VuprosId = vupros.id;
                    _context.Add(vuprosiZaListovka);
                }

                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(listovkaModel);
        }

        private List<VuprosModel> generateListovka(List<VuprosModel> all)
        {
            const int maxTochki = 97;
            int currTochki = 0;

            Shuffle(all);

            List<VuprosModel> listovka = new List<VuprosModel>(); 

            foreach(VuprosModel vupros in all)
            {
                if (maxTochki == currTochki) break;
                if (maxTochki - currTochki <= 3)
                {
                    listovka.Add(_context.VuprosModel.Where(x => x.tochki == maxTochki - currTochki).Single());
                        break;
                }
                else
                {
                    listovka.Add(vupros);
                    currTochki += vupros.tochki;
                }
            }
            return listovka;
        }


        public void Shuffle<T>(IList<T> list)
        {
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }

        // GET: ListovkaModels/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var listovkaModel = await _context.ListovkaModel
                .FirstOrDefaultAsync(m => m.id == id);
            if (listovkaModel == null)
            {
                return NotFound();
            }

            return View(listovkaModel);
        }

        // POST: ListovkaModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var listovkaModel = await _context.ListovkaModel.FindAsync(id);
            _context.ListovkaModel.Remove(listovkaModel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ListovkaModelExists(int id)
        {
            return _context.ListovkaModel.Any(e => e.id == id);
        }
    }
}
