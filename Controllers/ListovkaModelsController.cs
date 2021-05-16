using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Listovki_TSP_Uni.Models;
using TSP_Uni_Listovki.Data;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace TSP_Uni_Listovki.Controllers
{
    [Authorize(Policy ="User")]
    public class ListovkaModelsController : Controller
    {
        private List<VuprosiZaListovka> vruzki;
        private List<VuprosModel> vuprosi;
        private Random rng = new Random();
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public ListovkaModelsController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: ListovkaModels
        public IActionResult Index()
        {
            var list = _context.ListovkaModel.Where(l => l.userId == _userManager.GetUserId(User)).ToList();
            return View(list);
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

            vruzki =_context.VuprosiZaListovka.Where(v => v.ListovkaID == id).ToList();
            vuprosi = new List<VuprosModel>();
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

        public async Task<IActionResult> Reshavane(int? id, ICollection<string> otbelqzanOtg)
        {
            if (id == null)
            {
                return NotFound();
            }

            if (otbelqzanOtg.Count!=0)
            {   //Proverka na otgovora
                //List<VuprosiZaListovka> vruzki = _context.VuprosiZaListovka.Where(v => v.ListovkaID == id).ToList();
                var listovka = _context.ListovkaModel.Where(x => x.id == id).Single();
                var vuprosi = loadQuestions(_context, id);
                var opit = new Dictionary<VuprosModel,bool>(); 
                int tochki = 0;
                foreach (VuprosModel vupros in vuprosi)
                {
                    bool result = true;
                    foreach(OtgovorModel otgovor in vupros.Otgovori)
                    {
                        if (otgovor.veren ^ otbelqzanOtg.Contains(otgovor.id.ToString()))
                        {
                            result = false; break;
                        }
                    }
                    if (result) tochki += vupros.tochki;
                    opit.Add(vupros, result);
                }
                listovka.tochki = tochki;

                listovka.userId = _userManager.GetUserId(User);

                _context.SaveChanges();

                return RedirectToAction(nameof(Details), new { id = id}); //redirektvane kum stranica za pokazvane na pravilnite otgovori na listovkata.
            }
            else 
            {   //Vzimane na vuprosite s tehnite otgovori ot bazata danni
                var listovkaModel = await _context.ListovkaModel
                    .FirstOrDefaultAsync(m => m.id == id);
                if (listovkaModel == null)
                {
                    return NotFound();
                }

                List<VuprosModel> vuprosi = loadQuestions(_context, id);
                ViewData["vuprosi"] = vuprosi;
                return View();
            }
        }
        private List<VuprosModel> loadQuestions( DbContext context,int? id)
        {
            var vruzki = _context.VuprosiZaListovka.Where(v => v.ListovkaID == id).ToList();
            List<VuprosModel> vuprosi = new List<VuprosModel>();
            foreach (var vruzka in vruzki)
            {
                vuprosi.Add(_context.VuprosModel.Where(v => v.id == vruzka.VuprosId).Single());
            }
            foreach (var vupros in vuprosi)
            {
                vupros.Otgovori = _context.OtgovorModel.Where(v => v.VuprosID == vupros.id).ToList();
            }
            return vuprosi;
        }

        public async Task<IActionResult> Create(int? izpitId)
        {
            //Vsichki vuprosi
            var all = _context.VuprosModel.ToList();

            var listovka = generateListovka(all);
            ListovkaModel listovkaModel = new ListovkaModel();

            listovkaModel.timestamp = DateTime.Now;
            listovkaModel.tochki = 0;
            var user = User.Identity;

            _context.Add(listovkaModel);
            await _context.SaveChangesAsync();

            foreach (VuprosModel vupros in listovka)
            {
                VuprosiZaListovka vuprosiZaListovka = new VuprosiZaListovka();
                vuprosiZaListovka.ListovkaID = listovkaModel.id;
                vuprosiZaListovka.VuprosId = vupros.id;
                _context.Add(vuprosiZaListovka);
            }

            if (izpitId.HasValue) {             
                _context.IzpitModel.Where(p => p.id == izpitId).Single().listovkaId = listovkaModel.id;
            }

            await _context.SaveChangesAsync();


            return Redirect(nameof(Reshavane) + "/" + listovkaModel.id.ToString());
        }

        private List<VuprosModel> generateListovka(List<VuprosModel> all)
        {
            int currTochki = 0;

            Shuffle(all);
            var list3 = new List<VuprosModel>();
            var list2 = new List<VuprosModel>();
            var list1 = new List<VuprosModel>();

            foreach(VuprosModel vuprosModel in all)
            {
                switch (vuprosModel.tochki)
                {
                    case 1: list1.Add(vuprosModel); break;
                    case 2: list2.Add(vuprosModel); break;
                    case 3: list3.Add(vuprosModel); break;
                }
            }

            List<VuprosModel> listovka = new List<VuprosModel>(); 

            foreach(VuprosModel vuprosModel in list3)
            {
                if (currTochki >= 48) break;
                listovka.Add(vuprosModel);
                currTochki += vuprosModel.tochki;
            }

            foreach (VuprosModel vuprosModel in list2)
            {
                if (currTochki >= 88) break;
                listovka.Add(vuprosModel);
                currTochki += vuprosModel.tochki;
            }

            foreach (VuprosModel vuprosModel in list1)
            {
                if (currTochki >= 97) break;
                listovka.Add(vuprosModel);
                currTochki += vuprosModel.tochki;
            }

            Shuffle(listovka);

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
