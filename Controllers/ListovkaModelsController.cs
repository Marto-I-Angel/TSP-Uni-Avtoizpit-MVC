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
                
                foreach(VuprosModel vupros in listovka)
                {
                    //vupros.ListovkaID = listovkaModel.id;  << dava greshka, no trqbva da im assignem listovka.
                    //_context.Update(vupros); mai ne e nujno
                }

                //await _context.SaveChangesAsync();

                listovkaModel.timestamp = DateTime.Now;
                listovkaModel.tochki = 0;
                var user = User.Identity;

                listovkaModel.userName=user.Name;

                _context.Add(listovkaModel);
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
                if (maxTochki - currTochki <= 3)
                {
                    switch(currTochki)
                    {
                     //   case 94: add3pts(); break;
                    //    case 95: add2pts(); break;
                     //  case 96: add1pts(); break;
                    }
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
