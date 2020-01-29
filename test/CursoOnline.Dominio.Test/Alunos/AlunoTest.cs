using Bogus;
using Bogus.Extensions.Brazil;
using CursoOnline.Dominio.Alunos;
using Xunit;
using Xunit.Abstractions;
using ExpectedObjects;
using CursoOnline.Dominio._Base;
using CursoOnline.Dominio.Test._Builders;
using CursoOnline.Dominio.Test._Util;
using CursoOnline.Dominio.PublicoAlvo;

namespace CursoOnline.Dominio.Test.Alunos
{
	public class AlunoTest
	{
		private readonly Faker _faker;
		private readonly string _nome;
		private readonly PublicoAlvoEnum _publicoAlvo;
		private readonly string _cpf;
		private readonly string _email;

		public AlunoTest(ITestOutputHelper output)
		{
			_faker = new Faker();

			_nome = _faker.Name.FullName();
			_publicoAlvo = PublicoAlvoEnum.Estudante;
			_cpf = _faker.Person.Cpf(true);
			_email = _faker.Person.Email;		
		}

		[Fact]
		public void DeveCriarAluno()
		{
			var alunoEsperado = new
			{
				Nome = _nome,
				Cpf = _cpf,
				Email = _email,
				PublicoAlvo = _publicoAlvo
			};

			var aluno = new Aluno(alunoEsperado.Nome,
						 alunoEsperado.Cpf,
						 alunoEsperado.Email,
						 alunoEsperado.PublicoAlvo);

			alunoEsperado.ToExpectedObject().ShouldMatch(aluno);

		}

		[Theory]
		[InlineData("")]
		[InlineData(null)]
		public void NaoDeveAlunoTerNomeInvalido(string nomeInvalido)
		{
			Assert.Throws<ExcecaoDeDominio>(() => AlunoBuilder.Novo().ComNome(nomeInvalido).Build())
				.ComMensagem(Resource.NomeInvalido);
		}

		[Theory]
		[InlineData("")]
		[InlineData(null)]
		[InlineData("123123123")]
		[InlineData("132443")]
		public void NaoDeveAlunoTerCpfInvalido(string cpfInvalido)
		{
			Assert.Throws<ExcecaoDeDominio>(() => AlunoBuilder.Novo().ComCpf(cpfInvalido).Build())
				.ComMensagem(Resource.CpfInvalido);
		}

		[Theory]
		[InlineData("")]
		[InlineData(null)]
		[InlineData("a@hot")]
		[InlineData("@com.")]
		[InlineData("vr@com.")]
		public void NaoDeveAlunoTerEmailInvalido(string emailInvalido)
		{
			Assert.Throws<ExcecaoDeDominio>(() => AlunoBuilder.Novo().ComEmail(emailInvalido).Build())
				.ComMensagem(Resource.EmailInvalido);
		}


		[Fact]
		public void DeveAlterarNome()
		{
			var nomeEsperado = _faker.Person.FullName;

			var aluno = AlunoBuilder.Novo().ComNome(nomeEsperado).Build();

			aluno.AlterarNome(nomeEsperado);

			Assert.Equal(nomeEsperado, aluno.Nome);
		}

		[Theory]
		[InlineData("")]
		[InlineData(null)]
		public void NaoDeveAlterarComNomeInvalido(string nomeInvalido)
		{
			var aluno = AlunoBuilder.Novo().Build();

			Assert.Throws<ExcecaoDeDominio>(() => aluno.AlterarNome(nomeInvalido)).ComMensagem(Resource.NomeInvalido);
		}
	}
}
