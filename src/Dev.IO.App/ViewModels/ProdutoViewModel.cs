﻿using Dev.IO.App.Extensions;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Dev.IO.App.ViewModels
{
	public class ProdutoViewModel
	{
		[Key]
		public Guid Id { get; set; }

		[Required(ErrorMessage = "O campo {0} é obrigatório")]
		[DisplayName("Fornecedor")]
		public Guid FornecedorId { get; set; }

		[Required(ErrorMessage ="O campo {0} é obrigatório")]
		[StringLength(200, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 2)]
		public string Nome { get; set; }

		[DisplayName("Descrição")]
		[Required(ErrorMessage = "O campo {0} é obrigatório")]
		[StringLength(1000, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 10)]
		public string Descricao { get; set; }

		[DisplayName("Imagem do produto")]
		public IFormFile ImagemUpload { get; set; }
		public string Imagem { get; set; }

		[Required(ErrorMessage = "O campo {0} é obrigatório")]
		[Moeda]
		public decimal Valor { get; set; } = 0;

		[ScaffoldColumn(false)]
		public DateTime DataCadastro { get; set; }

		[DisplayName("Ativo?")]
		public bool Ativo { get; set; }

		public FornecedorViewModel Fornecedor { get; set; }
		public IEnumerable< FornecedorViewModel> Fornecedores { get; set; }
	}
}
