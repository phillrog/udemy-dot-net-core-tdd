using CursoOnline.Dominio._Base;
using CursoOnline.Dominio.Matriculas;
using CursoOnline.Dominio.Test._Builders;
using CursoOnline.Dominio.Test._Util;
using Moq;
using Xunit;

namespace CursoOnline.Dominio.Test.Matriculas
{
	public class CancelamentoDaMatriculaTest
	{
		private readonly Mock<IMatriculaRepositorio> _matriculaRepositorioMock;
		private readonly CancelamentoDaMatricula _cancelamentoDaMatricula;

		public CancelamentoDaMatriculaTest()
		{
			_matriculaRepositorioMock = new Mock<IMatriculaRepositorio>();
			_cancelamentoDaMatricula = new CancelamentoDaMatricula(_matriculaRepositorioMock.Object);
		}

		[Fact]
		public void DeveCancelarMatricula()
		{
			var matricula = MatriculaBuilder.Novo().Build();

			_matriculaRepositorioMock.Setup(d => d.ObterPorId(It.IsAny<int>())).Returns(matricula);

			_cancelamentoDaMatricula.Cancelar(matricula.Id);

			Assert.True(matricula.Cancelada);
		}

		[Fact]
		public void DeveNotificarQuandoMatriculaNaoEncontrado()
		{
			Matricula matriculaInvalida = null;
			const int matriculaIdInvalida = 1;

			_matriculaRepositorioMock.Setup(d => d.ObterPorId(It.IsAny<int>())).Returns(matriculaInvalida);

			Assert.Throws<ExcecaoDeDominio>(()=> _cancelamentoDaMatricula.Cancelar(matriculaIdInvalida))
				.ComMensagem(Resource.MatriculaNaoEncontrada);
		}
	}
}
