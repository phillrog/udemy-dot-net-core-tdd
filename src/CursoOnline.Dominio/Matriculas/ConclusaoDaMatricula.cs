using CursoOnline.Dominio._Base;

namespace CursoOnline.Dominio.Matriculas
{
	public class ConclusaoDaMatricula
	{
		private IMatriculaRepositorio _matriculaRepositorio;

		public ConclusaoDaMatricula(IMatriculaRepositorio matriculaRepositorio)
		{
			_matriculaRepositorio = matriculaRepositorio;
		}

		public void Concluir(int matriculaId, double notaDoAlunoEsperado)
		{
			var matricula = _matriculaRepositorio.ObterPorId(matriculaId);

			ValidadorDeRegra.Novo()
				.Quando(matricula == null, Resource.MatriculaNaoEncontrada)
				.DispararExcecaoSeExistir();

			matricula.InformarNota(notaDoAlunoEsperado);
		}
	}
}
