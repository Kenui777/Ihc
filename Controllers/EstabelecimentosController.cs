using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FormularioTeste.Data;
using FormularioTeste.Models;

namespace FormularioTeste.Controllers
{
    public class EstabelecimentosController : Controller
    {
        private readonly FormularioTesteContext _context;

        public EstabelecimentosController(FormularioTesteContext context)
        {
            _context = context;
        }

        // GET: Estabelecimentos
        public async Task<IActionResult> Index()
        {
            return View(await _context.Estabelecimentos.ToListAsync());
        }

        // GET: Estabelecimentos/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var estabelecimento = await _context.Estabelecimentos
                .FirstOrDefaultAsync(m => m.CnpjBasico == id);
            if (estabelecimento == null)
            {
                return NotFound();
            }

            return View(estabelecimento);
        }

        // GET: Estabelecimentos/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Estabelecimentos/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CnpjBasico,CnpjOrdem,CnpjDv,NomeFantasia,DataSituacaoCadastral,CnaePrincipal,TpLogradouro,Logradouro,Numero,Complemento,Bairro,CEP,UF,Cidade,Email")] Estabelecimento estabelecimento)
        {
            if (ModelState.IsValid)
            {
                _context.Add(estabelecimento);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(estabelecimento);
        }

        // GET: Estabelecimentos/Edit/5
        public async Task<IActionResult> Edit(string id, string cnpjordem, string cnpjdv)
        {
            if (id == null)
            {
                return NotFound();
            }

            var estabelecimento = await _context.Estabelecimentos.FindAsync(id, cnpjordem, cnpjdv);
            if (estabelecimento == null)
            {
                return NotFound();
            }
            return View(estabelecimento);
        }

        // POST: Estabelecimentos/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, string cnpjordem, string cnpjdv, [Bind("CnpjBasico,CnpjOrdem,CnpjDv,NomeFantasia,DataSituacaoCadastral,CnaePrincipal,TpLogradouro,Logradouro,Numero,Complemento,Bairro,CEP,UF,Cidade,Email")] Estabelecimento estabelecimento)
        {
            if (id != estabelecimento.CnpjBasico || cnpjordem != estabelecimento.CnpjOrdem || cnpjdv != estabelecimento.CnpjDv)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(estabelecimento);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EstabelecimentoExists(estabelecimento.CnpjBasico, estabelecimento.CnpjOrdem, estabelecimento.CnpjDv))
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
            return View(estabelecimento);
        }

        // GET: Estabelecimentos/Delete/5
        public async Task<IActionResult> Delete(string id, string cnpjordem, string cnpjdv)
        {
            if (id == null)
            {
                return NotFound();
            }

            var estabelecimento = await _context.Estabelecimentos
                .FirstOrDefaultAsync(m => m.CnpjBasico == id && m.CnpjOrdem == cnpjordem && m.CnpjDv == cnpjdv);
            if (estabelecimento == null)
            {
                return NotFound();
            }

            return View(estabelecimento);
        }

        // POST: Estabelecimentos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id, string cnpjordem, string cnpjdv)
        {
            var estabelecimento = await _context.Estabelecimentos.FindAsync(id, cnpjordem, cnpjdv);
            if (estabelecimento != null)
            {
                _context.Estabelecimentos.Remove(estabelecimento);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EstabelecimentoExists(string id, string cnpjordem, string cnpjdv)
        {
            return _context.Estabelecimentos.Any(e => e.CnpjBasico == id && e.CnpjOrdem == cnpjordem && e.CnpjDv == cnpjdv);
        }
    }
}
