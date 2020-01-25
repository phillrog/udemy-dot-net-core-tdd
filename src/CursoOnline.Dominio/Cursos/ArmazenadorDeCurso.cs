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

			if (cursoJaSalvo != null)
				throw new ArgumentException("Nome do curso já consta no banco de dados");

			if (!Enum.TryParse<PublicoAlvoEnum>(cursoDTO.PublicoAlvo, out var publicoAlvo))
				throw new ArgumentException("Publico Alvo Inválido");

			var curso = new Curso(cursoDTO.Nome,
				cursoDTO.CargaHoraria,
				publicoAlvo,
				cursoDTO.Valor,
				cursoDTO.Descricao);

			_cursoRepositorio.Adicionar(curso);
		}
	}
}