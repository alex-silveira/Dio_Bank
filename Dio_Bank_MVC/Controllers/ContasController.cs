using Dio_Bank_MVC.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dio_Bank_MVC.Models
{
    public class ContasController : Controller
    {
        private readonly Dio_Bank_MVCContext _context;

        public ContasController(Dio_Bank_MVCContext context)
        {
            _context = context;
        }

        // GET: Contas
        public async Task<IActionResult> Index()
        {
            return View(await _context.Conta.ToListAsync());
        }

        // GET: Contas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var conta = await _context.Conta
                .FirstOrDefaultAsync(m => m.IdConta == id);
            if (conta == null)
            {
                return NotFound();
            }

            return View(conta);
        }

        // GET: Contas/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Contas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdConta,Agencia,NumeroConta,DigitoVerificador,Tipo,Saldo,DataCriacao")] Conta conta)
        {
            if (ModelState.IsValid)
            {
                conta.UserName = User.Identity.Name;
                _context.Add(conta);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(conta);
        }

        // GET: Contas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var conta = await _context.Conta.FindAsync(id);
            if (conta == null)
            {
                return NotFound();
            }
            return View(conta);
        }

        // POST: Contas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdConta,Agencia,NumeroConta,DigitoVerificador,Tipo,Saldo,DataCriacao")] Conta conta)
        {
            if (id != conta.IdConta)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(conta);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ContaExists(conta.IdConta))
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
            return View(conta);
        }

        // GET: Contas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var conta = await _context.Conta
                .FirstOrDefaultAsync(m => m.IdConta == id);
            if (conta == null)
            {
                return NotFound();
            }

            return View(conta);
        }

        // POST: Contas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var conta = await _context.Conta.FindAsync(id);
            _context.Conta.Remove(conta);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ContaExists(int id)
        {
            return _context.Conta.Any(e => e.IdConta == id);
        }

        public async  Task<JsonResult> GetData(Conta conta)
        {
            return Json(await _context.Conta.ToListAsync());
        }
        [Produces("application/json")]
        
        [HttpGet]
        public ActionResult JsonData()
        {
            var settings = new JsonSerializer();
            int operacao = 0;

            var data = _context.Conta
               .Where(b => b.UserName == User.Identity.Name)
               .FirstOrDefault();
            


            if(data.Tipo == 1)
            {
                operacao = 13;
            }
            else
            {
                operacao = 14;
            }

            var dataJson = new List<Conta>
            {
                new Conta 
                {
                    Agencia = data.Agencia,
                    NumeroConta = data.NumeroConta,
                    DigitoVerificador = data.DigitoVerificador,
                    Tipo = operacao,
                    Saldo = data.Saldo 
                }
                
            };

            return new JsonResult(dataJson);
        }
    }
}
