using CursoOnline.Dominio._Base;
using CursoOnline.Dominio.Matriculas;
using CursoOnline.Dominio.Test._Builders;
using CursoOnline.Dominio.Test._Util;
using Moq;
using Xunit;

namespace CursoOnline.Dominio.Test.Matriculas
{
	public class ConclusaoDaMatriculaTest
	{
		private readonly Mock<IMatriculaRepositorio> _matriculaRepositorioMock;
		private readonly Matricula _matricula;
		private readonly ConclusaoDaMatricula _conclusaoDaMatricula;

		public ConclusaoDaMatriculaTest()
		{
			_matriculaRepositorioMock = new Mock<IMatriculaRepositorio>();
			_conclusaoDaMatricula = new ConclusaoDaMatricula(_matriculaRepositorioMock.Object);
			_matricula = MatriculaBuilder.Novo().Build();
		}

		[Fact]
		public void DeveInformarNotaDoAluno()
		{
			var notaDoAlunoEsperado = 8;

			_matriculaRepositorioMock.Setup((d) => d.ObterPorId(_matricula.Id)).Returns(_matricula);
			
			_conclusaoDaMatricula.Concluir(_matricula.Id, notaDoAlunoEsperado);

			Assert.Equal(notaDoAlunoEsperado, _matricula.NotaDoAluno);
		}

		[Fact]
		public void DeveNotificarQuandoMatriculaNaoEncontrado()
		{
			Matricula matriculaInvalida = null;
			int matriculaIdInvalida = 1;
			double notaDoAluno = 2;

			_matriculaRepositorioMock.Setup(d => d.ObterPorId(It.IsAny<int>())).Returns(matriculaInvalida);
		
			Assert.Throws<ExcecaoDeDominio>(() => _conclusaoDaMatricula.Concluir(matriculaIdInvalida, notaDoAluno))
				.ComMensagem(Resource.MatriculaNaoEncontrada);
		}
	}

	
}
