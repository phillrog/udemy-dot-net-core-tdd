using CursoOnline.Dominio._Base;
using CursoOnline.Dominio.Cursos;
using System;

namespace CursoOnline.Dominio.Alunos
{
	public class ArmazenadorDeAluno
	{
		private readonly IAlunoRepositorio _alunoRepositorio;

		public ArmazenadorDeAluno(IAlunoRepositorio alunoRepositorio)
		{
			_alunoRepositorio = alunoRepositorio;
		}

		public void Armazenar(AlunoDTO alunoDTO)
		{
			var alunoJaSalvo = _alunoRepositorio.ObeterPeloNome(alunoDTO.Nome);

			ValidadorDeRegra.Novo()
				.Quando(alunoJaSalvo != null && alunoJaSalvo.Id != alunoDTO.Id, Resource.NomeAlunoJaExiste)
				.Quando(!Enum.TryParse<PublicoAlvoEnum>(alunoDTO.PublicoAlvo, out var publicoAlvo), Resource.PublicoAlvoInvalido)
				.DispararExcecaoSeExistir();

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
