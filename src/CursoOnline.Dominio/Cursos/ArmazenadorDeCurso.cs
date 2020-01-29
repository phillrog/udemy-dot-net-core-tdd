using CursoOnline.Dominio._Base;
using CursoOnline.Dominio.PublicoAlvo;
using System;

namespace CursoOnline.Dominio.Cursos
{
	public class ArmazenadorDeCurso
	{
		private readonly ICursoRepositorio _cursoRepositorio;
		private readonly IConversorDePublicoAlvo _conversorDePublicoAlvo;

		public ArmazenadorDeCurso(ICursoRepositorio cursoRepositorio, IConversorDePublicoAlvo conversorDePublicoAlvo)
		{
			_cursoRepositorio = cursoRepositorio;
			_conversorDePublicoAlvo = conversorDePublicoAlvo;
		}

		public void Armazenar(CursoDTO cursoDTO)
		{
			var cursoJaSalvo = _cursoRepositorio.ObeterPeloNome(cursoDTO.Nome);
			
			ValidadorDeRegra.Novo()
				.Quando(cursoJaSalvo != null && cursoJaSalvo.Id != cursoDTO.Id, Resource.NomeCursoJaExiste)
				.DispararExcecaoSeExistir();

			var publicoAlvo = _conversorDePublicoAlvo.Converter(cursoDTO.PublicoAlvo);

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