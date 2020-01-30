using Bogus;
using Bogus.Extensions.Brazil;
using CursoOnline.Dominio._Base;
using CursoOnline.Dominio.Alunos;
using CursoOnline.Dominio.Cursos;
using CursoOnline.Dominio.PublicoAlvo;
using CursoOnline.Dominio.Test._Builders;
using CursoOnline.Dominio.Test._Util;
using Moq;
using System;
using Xunit;

namespace CursoOnline.Dominio.Test.Alunos
{
	public class ArmazenadorDeAlunoTest
	{
		private readonly Mock<IAlunoRepositorio> _alunoRepositorioMock;
		private readonly Mock<IConversorDePublicoAlvo> _conversorDePublicoAlvoMock;
		private readonly ArmazenadorDeAluno _armazenadorDeAluno;
		private readonly Faker _faker;
		private readonly AlunoDTO _alunoDTO;

		public ArmazenadorDeAlunoTest()
		{
			_faker = new Faker();

			_alunoDTO = new AlunoDTO
			{
				Nome = _faker.Name.FullName(),
				PublicoAlvo = "Estudante",
				Cpf = _faker.Person.Cpf(true),
				Email = _faker.Person.Email
			};

			_alunoRepositorioMock = new Mock<IAlunoRepositorio>();
			_conversorDePublicoAlvoMock = new Mock<IConversorDePublicoAlvo>();
			_armazenadorDeAluno = new ArmazenadorDeAluno(_alunoRepositorioMock.Object, _conversorDePublicoAlvoMock.Object);
		}

		[Fact]
		public void DeveAdicionarAluno()
		{
			_armazenadorDeAluno.Armazenar(_alunoDTO);

			_alunoRepositorioMock.Verify(d => d.Adicionar(
				It.Is<Aluno>(c =>
					c.Nome.Equals(_alunoDTO.Nome)
				 && c.Cpf.Equals(_alunoDTO.Cpf))));
		}

		[Fact]
		public void NaoDeveAdicionarAlunoComMesmoCpfDeOutroJaSalvo()
		{
			var alunoJaSalvo = AlunoBuilder.Novo().ComId(_faker.Random.Int(1,999999999)).Build();

			_alunoRepositorioMock.Setup(c => c.ObterPorCpf(_alunoDTO.Cpf)).Returns(alunoJaSalvo);

			Assert.Throws<ExcecaoDeDominio>(() => _armazenadorDeAluno.Armazenar(_alunoDTO))
				.ComMensagem(Resource.CpfAlunoJaExiste);
		}

		[Fact]
		public void DeveAlterarNomeDoAluno()
		{
			_alunoDTO.Nome = _faker.Person.FullName;
			var aluno = AlunoBuilder.Novo().ComNome(_alunoDTO.Nome).Build();
			_alunoRepositorioMock.Setup(c => c.ObterPorId(_alunoDTO.Id)).Returns(aluno);

			_armazenadorDeAluno.Armazenar(_alunoDTO);

			Assert.Equal(_alunoDTO.Nome, aluno.Nome);			
		}

		[Fact]
		public void NaoDeveAlterarTodosCamposDoAluno()
		{
			_alunoDTO.Cpf = _faker.Person.Cpf(true);

			var aluno = AlunoBuilder.Novo().ComCpf(_alunoDTO.Cpf).Build();
			_alunoRepositorioMock.Setup(c => c.ObterPorId(_alunoDTO.Id)).Returns(aluno);

			_armazenadorDeAluno.Armazenar(_alunoDTO);

			Assert.Equal(_alunoDTO.Cpf, aluno.Cpf);
		}

		[Fact]
		public void NaoDeveAdicionarNoRepositorioQuandoOCursoJaExiste()
		{
			_alunoDTO.Id = _faker.Random.Int(1, 999999999);
			var aluno = AlunoBuilder.Novo().Build();
			_alunoRepositorioMock.Setup(c => c.ObterPorId(_alunoDTO.Id)).Returns(aluno);

			_armazenadorDeAluno.Armazenar(_alunoDTO);

			_alunoRepositorioMock.Verify(c => c.Adicionar(It.IsAny<Aluno>()), Times.Never);
		}
	}
}
