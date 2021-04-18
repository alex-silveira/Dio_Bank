using Dio_Bank_MVC.Data;
using Dio_Bank_MVC.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Dio_Bank_MVC.Controllers
{
    [Authorize]
    public class TransferenciasController : Controller
    {
        private readonly Dio_Bank_MVCContext _context;

        public TransferenciasController(Dio_Bank_MVCContext context)
        {
            _context = context;
        }

        // GET: Transferencias
        public async Task<IActionResult> Index()
        {
            return View(await _context.Transferencias.ToListAsync());
        }

        // GET: Transferencias/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var transferencia = await _context.Transferencias
                .FirstOrDefaultAsync(m => m.IdTransferencia == id);
            if (transferencia == null)
            {
                return NotFound();
            }

            return View(transferencia);
        }

        // GET: Transferencias/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Transferencias/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdTransferencia,IdConta,Agencia,Conta,DigitoVerificador,TipoConta,Valor,Data,RepetirProximosMeses")] Transferencia transferencia)
        {
            if (ModelState.IsValid)
            {
                var dataTransfencia = DateTime.Now;
                transferencia.Conta_IdConta = 1;
                transferencia.Data = dataTransfencia.ToString();
                transferencia.Transferir(transferencia.Valor, _context, User.Identity.Name);

                _context.Add(transferencia);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(transferencia);
        }

        // GET: Transferencias/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var transferencia = await _context.Transferencias.FindAsync(id);
            if (transferencia == null)
            {
                return NotFound();
            }
            return View(transferencia);
        }

        // POST: Transferencias/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Agencia,Conta,DigitoVerificador,TipoConta,Valor,Data,RepetirProximosMeses")] Transferencia transferencia)
        {
            if (id != transferencia.IdTransferencia)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(transferencia);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TransferenciaExists(transferencia.IdTransferencia))
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
            return View(transferencia);
        }

        // GET: Transferencias/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var transferencia = await _context.Transferencias
                .FirstOrDefaultAsync(m => m.IdTransferencia == id);
            if (transferencia == null)
            {
                return NotFound();
            }

            return View(transferencia);
        }

        // POST: Transferencias/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var transferencia = await _context.Transferencias.FindAsync(id);
            _context.Transferencias.Remove(transferencia);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TransferenciaExists(int id)
        {
            return _context.Transferencias.Any(e => e.IdTransferencia == id);
        }
    }
}
