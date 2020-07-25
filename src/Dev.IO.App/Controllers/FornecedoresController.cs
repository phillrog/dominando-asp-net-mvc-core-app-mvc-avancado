using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Dev.IO.App.Data;
using Dev.IO.App.ViewModels;
using DevIO.Business.Interfaces;
using AutoMapper;
using DevIO.Business.Models;

namespace Dev.IO.App.Controllers
{
	public class FornecedoresController : BaseController
	{
		private readonly IFornecedorRepository _fornecedorRepository;
		private readonly IMapper _mapper;

		public FornecedoresController(IFornecedorRepository fornecedorRepository, IMapper mapper)
		{
			_fornecedorRepository = fornecedorRepository;
			_mapper = mapper;

		}

		// GET: Fornecedores
		public async Task<IActionResult> Index()
		{
			return View(_mapper.Map<IEnumerable<FornecedorViewModel>>(await _fornecedorRepository.ObterTodos()));
		}

		// GET: Fornecedores/Details/5
		public async Task<IActionResult> Details(Guid id)
		{
			var fornecedorViewModel = await ObterFornecedorEndereco(id);

			if (fornecedorViewModel == null)
			{
				return NotFound();
			}

			return View(fornecedorViewModel);
		}

		// GET: Fornecedores/Create
		public IActionResult Create()
		{
			return View();
		}

		// POST: Fornecedores/Create
		// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
		// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create(FornecedorViewModel fornecedorViewModel)
		{
			if (!ModelState.IsValid) return View(fornecedorViewModel);

			var fornecedor = _mapper.Map<Fornecedor>(fornecedorViewModel);

			await _fornecedorRepository.Adicionar(fornecedor);

			return RedirectToAction("Index");
		}

		// GET: Fornecedores/Edit/5
		public async Task<IActionResult> Edit(Guid id)
		{
			var fornecedorViewModel = await ObterFornecedorProdutosEndereco(id);
			if (fornecedorViewModel == null)
			{
				return NotFound();
			}
			return View(fornecedorViewModel);
		}

		// POST: Fornecedores/Edit/5
		// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
		// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(Guid id, FornecedorViewModel fornecedorViewModel)
		{
			if (id != fornecedorViewModel.Id)return NotFound();

			if (!ModelState.IsValid) return View(fornecedorViewModel);

			var fornecedor = _mapper.Map<Fornecedor>(fornecedorViewModel);

			await _fornecedorRepository.Atualizar(fornecedor);

			return RedirectToAction("Index");
		}

		// GET: Fornecedores/Delete/5
		public async Task<IActionResult> Delete(Guid id)
		{
			var fornecedorViewModel = await ObterFornecedorEndereco(id);

			if (fornecedorViewModel == null)return NotFound();

			return View(fornecedorViewModel);
		}

		// POST: Fornecedores/Delete/5
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> DeleteConfirmed(Guid id)
		{
			var fornecedorViewModel = await ObterFornecedorEndereco(id);

			if (fornecedorViewModel == null) return NotFound();

			await _fornecedorRepository.Remover(id);

			return RedirectToAction(nameof(Index));
		}

		private async Task<FornecedorViewModel> ObterFornecedorEndereco(Guid id)
		{
			return _mapper.Map<FornecedorViewModel>(await _fornecedorRepository.ObterFornecedorEndereco(id));
		}

		private async Task<FornecedorViewModel> ObterFornecedorProdutosEndereco(Guid id)
		{
			return _mapper.Map<FornecedorViewModel>(await _fornecedorRepository.ObterFornecedorProdutosEndereco(id));
		}
	}
}
