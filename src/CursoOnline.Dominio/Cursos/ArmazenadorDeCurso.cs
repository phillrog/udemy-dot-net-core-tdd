using CursoOnline.Dominio._Base;
using System;

namespace CursoOnline.Dominio.Cursos
{
	public class ArmazenadorDeCurso
	{
		private readonly ICursoRepositorio _cursoRepositorio;

		public ArmazenadorDeCurso(ICursoRepositorio cursoRepositorio)
		{
			_cursoRepositorio = cursoRepositorio;
		}

		public void Armazenar(CursoDTO cursoDTO)
		{
			var cursoJaSalvo = _cursoRepositorio.ObeterPeloNome(cursoDTO.Nome);

			ValidadorDeRegra.Novo()
				.Quando(cursoJaSalvo != null, Resource.NomeCursoJaExiste)
				.Quando(!Enum.TryParse<PublicoAlvoEnum>(cursoDTO.PublicoAlvo, out var publicoAlvo), Resource.PublicoAlvoInvalido)
				.DispararExcecaoSeExistir();

			var curso = new Curso(cursoDTO.Nome,
				cursoDTO.CargaHoraria,
				publicoAlvo,
				cursoDTO.Valor,
				cursoDTO.Descricao);

			if (cursoDTO.Id > 0)
			{
				curso = _cursoRepositorio.ObterPorId(cursoDTO.Id);
				curso.AlterarNome(cursoDTO.Nome);
				curso.AlterarCargaHoraria(cursoDTO.CargaHoraria);
				curso.AlterarValor(cursoDTO.Valor);
			}
			else
			{
				_cursoRepositorio.Adicionar(curso);
			}


		}
	}
}