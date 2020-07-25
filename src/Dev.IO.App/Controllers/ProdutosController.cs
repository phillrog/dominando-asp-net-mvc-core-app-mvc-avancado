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
using Microsoft.AspNetCore.Http;
using System.IO;

namespace Dev.IO.App.Controllers
{
	public class ProdutosController : BaseController
	{
		private readonly IProdutoRepository _produtoRepository;
		private readonly IFornecedorRepository _fornecedorRepository;
		private readonly IMapper _mapper;

		public ProdutosController(IProdutoRepository produtoRepository,
			IFornecedorRepository fornecedorRepository,
			IMapper mapper)
		{
			_produtoRepository = produtoRepository;
			_mapper = mapper;
			_fornecedorRepository = fornecedorRepository;
		}

		// GET: Produtos
		public async Task<IActionResult> Index()
		{
			return View(_mapper.Map<IEnumerable<ProdutoViewModel>>(await _produtoRepository.ObterProdutosPorFornecedores()));
		}

		// GET: Produtos/Details/5
		public async Task<IActionResult> Details(Guid id)
		{
			var produtoViewModel = await ObeterProduto(id);

			if (produtoViewModel == null) return NotFound();

			return View(produtoViewModel);
		}

		// GET: Produtos/Create
		public async Task<IActionResult> Create()
		{
			var produtoViewModel = await PopularFornecedores(new ProdutoViewModel());

			return View(produtoViewModel);
		}

		// POST: Produtos/Create
		// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
		// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create(ProdutoViewModel produtoViewModel)
		{
			produtoViewModel = await PopularFornecedores(produtoViewModel);

			if (!ModelState.IsValid) return View(produtoViewModel);

			var imgPrefixo = Guid.NewGuid() + "_";

			if (!await UploadArquivo(produtoViewModel.ImagemUpload, imgPrefixo)) return View(produtoViewModel);

			produtoViewModel.Imagem = imgPrefixo + produtoViewModel.ImagemUpload.FileName;

			_produtoRepository.Adicionar(_mapper.Map<Produto>(produtoViewModel));

			return RedirectToAction("Index");
		}

		private async Task<bool> UploadArquivo(IFormFile arquivo, string imgPrefixo)
		{
			if (arquivo.Length <= 0) return false;

			var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/imagens", imgPrefixo + arquivo.FileName);

			if (System.IO.File.Exists(path))
			{
				ModelState.AddModelError(string.Empty, "Já existe um arquivo com este nome!");
				return false;
			}

			using (var stream = new FileStream(path, FileMode.Create))
			{
				await arquivo.CopyToAsync(stream);
			}

			return true;
		}

		// GET: Produtos/Edit/5
		public async Task<IActionResult> Edit(Guid id)
		{
			var produtoViewModel = await ObeterProduto(id);

			if (produtoViewModel == null) return NotFound();

			return View(produtoViewModel);
		}

		// POST: Produtos/Edit/5
		// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
		// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(Guid id, ProdutoViewModel produtoViewModel)
		{
			if (id != produtoViewModel.Id) return NotFound();

			if (!ModelState.IsValid) return View(produtoViewModel);

			_produtoRepository.Atualizar(_mapper.Map<Produto>(produtoViewModel));

			return RedirectToAction("Index");
		}

		// GET: Produtos/Delete/5
		public async Task<IActionResult> Delete(Guid id)
		{
			if (id == null) return NotFound();

			var produtoViewModel = await ObeterProduto(id);

			if (produtoViewModel == null) return NotFound();

			return View(produtoViewModel);
		}

		// POST: Produtos/Delete/5
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> DeleteConfirmed(Guid id)
		{
			var produtoViewModel = await ObeterProduto(id);

			if (produtoViewModel == null) return NotFound();

			await _produtoRepository.Remover(id);

			return RedirectToAction(nameof(Index));
		}

		private async Task<ProdutoViewModel> ObeterProduto(Guid id)
		{
			var produto = _mapper.Map<ProdutoViewModel>(await _produtoRepository.ObterProdutoFornecedor(id));
			produto.Fornecedores = _mapper.Map<IEnumerable<FornecedorViewModel>>(await _fornecedorRepository.ObterTodos());
			return produto;
		}

		private async Task<ProdutoViewModel> PopularFornecedores(ProdutoViewModel produto)
		{
			produto.Fornecedores = _mapper.Map<IEnumerable<FornecedorViewModel>>(await _fornecedorRepository.ObterTodos());
			return produto;
		}
	}
}
