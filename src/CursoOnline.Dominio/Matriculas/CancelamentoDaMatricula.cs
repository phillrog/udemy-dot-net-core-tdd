using CursoOnline.Dominio._Base;

namespace CursoOnline.Dominio.Matriculas
{
	public class CancelamentoDaMatricula
	{
		private IMatriculaRepositorio _matriculaRepositorio;

		public CancelamentoDaMatricula(IMatriculaRepositorio matriculaRepositorio)
		{
			_matriculaRepositorio = matriculaRepositorio;
		}

		public bool Cancelada { get; private set; }

		public void Cancelar(int id)
		{
			var matricula = _matriculaRepositorio.ObterPorId(id);

			ValidadorDeRegra.Novo()
				.Quando(matricula == null, Resource.MatriculaNaoEncontrada)
				.DispararExcecaoSeExistir();

			Cancelada = true;

			matricula.Cancelar();
		}
	}
}