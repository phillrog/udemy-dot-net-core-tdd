using System.Linq;
using CursoOnline.Dominio._Base;
using CursoOnline.Dominio.Alunos;
using CursoOnline.Web.Util;
using Microsoft.AspNetCore.Mvc;

namespace CursoOnline.Web.Controllers
{
	public class AlunoController : Controller
	{
		private readonly ArmazenadorDeAluno _armazenadorDeAluno;
		private readonly IRepositorio<Aluno> _AlunoRepositorio;

		public AlunoController(ArmazenadorDeAluno armazenadorDeAluno, IRepositorio<Aluno> AlunoRepositorio)
		{
			_armazenadorDeAluno = armazenadorDeAluno;
			_AlunoRepositorio = AlunoRepositorio;
		}

		public IActionResult Index()
		{
			var Alunos = _AlunoRepositorio.Consultar();

			if (Alunos.Any())
			{
				var dtos = Alunos.Select(c => new AlunoParaListagemDto
				{
					Id = c.Id,
					Nome = c.Nome,
					Cpf = c.Cpf,
					PublicoAlvo = c.PublicoAlvo.ToString(),
					Email = c.Email
				});
				return View("Index", PaginatedList<AlunoParaListagemDto>.Create(dtos, Request));
			}

			return View("Index", PaginatedList<AlunoParaListagemDto>.Create(null, Request));
		}

		public IActionResult Editar(int id)
		{
			var Aluno = _AlunoRepositorio.ObterPorId(id);
			var dto = new AlunoDTO
			{
				Id = Aluno.Id,
				Nome = Aluno.Nome	
			};

			return View("NovoOuEditar", dto);
		}

		public IActionResult Novo()
		{
			return View("NovoOuEditar", new AlunoDTO());
		}

		[HttpPost]
		public IActionResult Salvar(AlunoDTO model)
		{
			_armazenadorDeAluno.Armazenar(model);
			return RedirectToAction("Index");
		}
	}
}