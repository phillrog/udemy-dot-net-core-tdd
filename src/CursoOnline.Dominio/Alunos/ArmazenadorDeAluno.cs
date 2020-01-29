using CursoOnline.Dominio._Base;
using CursoOnline.Dominio.PublicoAlvo;
using System;

namespace CursoOnline.Dominio.Alunos
{
	public class ArmazenadorDeAluno
	{
		private readonly IAlunoRepositorio _alunoRepositorio;
		private readonly IConversorDePublicoAlvo _conversorDePublicoAlvo;

		public ArmazenadorDeAluno(IAlunoRepositorio alunoRepositorio, IConversorDePublicoAlvo conversorDePublicoAlvo)
		{
			_alunoRepositorio = alunoRepositorio;
			_conversorDePublicoAlvo = conversorDePublicoAlvo;
		}

		public void Armazenar(AlunoDTO alunoDTO)
		{
			var alunoJaSalvo = _alunoRepositorio.ObterPorCpf(alunoDTO.Cpf);

			ValidadorDeRegra.Novo()
				.Quando(alunoJaSalvo != null && alunoJaSalvo.Id != alunoDTO.Id, Resource.CpfAlunoJaExiste)				
				.DispararExcecaoSeExistir();

			var publicoAlvo = _conversorDePublicoAlvo.Converter(alunoDTO.PublicoAlvo);

			var aluno = new Aluno(alunoDTO.Nome,
				alunoDTO.Cpf,
				alunoDTO.Email,
				publicoAlvo);

			if (alunoDTO.Id > 0)
			{
				aluno = _alunoRepositorio.ObterPorId(alunoDTO.Id);
				aluno.AlterarNome(alunoDTO.Nome);
			}
			else
			{
				_alunoRepositorio.Adicionar(aluno);
			}
		}
	}
}
