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
				.Quando(cursoJaSalvo != null, "Nome do curso já consta no banco de dados")
				.Quando(!Enum.TryParse<PublicoAlvoEnum>(cursoDTO.PublicoAlvo, out var publicoAlvo), "Publico Alvo Inválido")
				.DispararExcecaoSeExistir();

			var curso = new Curso(cursoDTO.Nome,
				cursoDTO.CargaHoraria,
				publicoAlvo,
				cursoDTO.Valor,
				cursoDTO.Descricao);

			_cursoRepositorio.Adicionar(curso);
		}
	}
}