using Bogus;
using Bogus.Extensions.Brazil;
using CursoOnline.Dominio.Alunos;
using CursoOnline.Dominio.Cursos;
using CursoOnline.Dominio.PublicoAlvo;
using System;
using System.Collections.Generic;
using System.Text;

namespace CursoOnline.Dominio.Test._Builders
{
	public class AlunoBuilder
	{
		private readonly Faker _faker;
		private string _nome;
		private PublicoAlvoEnum _publicoAlvo;
		private string _cpf;
		private string _email;
		private int _id;

		public static AlunoBuilder Novo()
		{
			return new AlunoBuilder();
		}

		public AlunoBuilder()
		{
			_faker = new Faker();

			_nome = _faker.Name.FullName();
			_publicoAlvo = PublicoAlvoEnum.Estudante;
			_cpf = _faker.Person.Cpf(true);
			_email = _faker.Person.Email;
		}

		public AlunoBuilder ComNome(string nome)
		{
			_nome = nome;
			return this;
		}

		public AlunoBuilder ComCpf(string cpf)
		{
			_cpf = cpf;
			return this;
		}

		public AlunoBuilder ComEmail(string email)
		{
			_email = email;
			return this;
		}

		public AlunoBuilder ComId(int id)
		{
			_id = id;

			return this;
		}

		public Aluno Build()
		{
			var aluno = new Aluno(_nome, _cpf, _email, _publicoAlvo);

			var propertyInfo = aluno.GetType().GetProperty("Id");
			propertyInfo.SetValue(aluno, Convert.ChangeType(_id, propertyInfo.PropertyType, null));

			return aluno;
		}
	}
}
