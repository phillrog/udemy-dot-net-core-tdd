using CursoOnline.Dominio._Base;
using CursoOnline.Dominio.Alunos;
using CursoOnline.Dominio.Cursos;
using CursoOnline.Dominio.Matriculas;
using CursoOnline.Dominio.PublicoAlvo;
using CursoOnline.Dominio.Test._Builders;
using CursoOnline.Dominio.Test._Util;
using Moq;
using Xunit;

namespace CursoOnline.Dominio.Test.Matriculas
{
	public class CriacaoDeMatriculaTest
	{
		private readonly Mock<ICursoRepositorio> _cursoRepositorioMock;
		private readonly Mock<IAlunoRepositorio> _alunoRepositorioMock;
		private readonly Mock<IMatriculaRepositorio> _matriculaRepositorioMock;
		private readonly MatriculaDto _matriculaDto;
		private readonly CriacaoDeMatricula _criacaoDeMatricula;
		private readonly Aluno _aluno;
		private readonly Curso _curso;

		public CriacaoDeMatriculaTest()
		{
			_aluno = AlunoBuilder.Novo().ComId(12).ComPublicoAlvo(PublicoAlvoEnum.Empreendedor).Build();
			_curso = CursoBuilder.Novo().ComId(12).ComPublicoAlvo(PublicoAlvoEnum.Empreendedor).Build();
			_cursoRepositorioMock = new Mock<ICursoRepositorio>();
			_alunoRepositorioMock = new Mock<IAlunoRepositorio>();
			_matriculaRepositorioMock = new Mock<IMatriculaRepositorio>();

			_alunoRepositorioMock.Setup(d => d.ObterPorId(It.IsAny<int>())).Returns(_aluno);
			_cursoRepositorioMock.Setup(d => d.ObterPorId(It.IsAny<int>())).Returns(_curso);

			_matriculaDto = new MatriculaDto(_aluno.Id, _curso.Id, _curso.Valor);

			_criacaoDeMatricula = new CriacaoDeMatricula(_alunoRepositorioMock.Object, _cursoRepositorioMock.Object, _matriculaRepositorioMock.Object);
		}

		[Fact]
		public void DeveNotificarQuandoCursoNaoEncontrado()
		{
			Curso cursoNaoEncontrado = null;

			_cursoRepositorioMock.Setup(d => d.ObterPorId(It.IsAny<int>())).Returns(cursoNaoEncontrado);

			Assert.Throws<ExcecaoDeDominio>(() => _criacaoDeMatricula.Criar(_matriculaDto))
				.ComMensagem(Resource.CursoNaoEncontrado);
		}

		[Fact]
		public void DeveNotificarQuandoAlunoNaoEncontrado()
		{
			Aluno alunoNaoEncontrado = null;

			_alunoRepositorioMock.Setup(d => d.ObterPorId(It.IsAny<int>())).Returns(alunoNaoEncontrado);

			Assert.Throws<ExcecaoDeDominio>(() => _criacaoDeMatricula.Criar(_matriculaDto))
				.ComMensagem(Resource.AlunoNaoEncontrado);
		}

		[Fact]
		public void DeveAdicionarMatricula()
		{
			_criacaoDeMatricula.Criar(_matriculaDto);

			_matriculaRepositorioMock.Verify(r => r.Adicionar(It.Is<Matricula>(m => m.Aluno == _aluno && m.Curso == _curso)));
		}
	}
}
