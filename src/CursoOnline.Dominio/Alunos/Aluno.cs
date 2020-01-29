using System;
using CursoOnline.Dominio._Base;
using CursoOnline.Dominio._Util;
using CursoOnline.Dominio.PublicoAlvo;

namespace CursoOnline.Dominio.Alunos
{
	public class Aluno: Entidade
	{
		private string _nome;
		private string _cpf;
		private string _email;
		private PublicoAlvoEnum _publicoAlvo;

		public Aluno(string nome, string cpf, string email, PublicoAlvoEnum publicoAlvo)
		{
			ValidadorDeRegra.Novo()
				.Quando(string.IsNullOrEmpty(nome), Resource.NomeInvalido)		
				.Quando(string.IsNullOrEmpty(cpf) || (!string.IsNullOrEmpty(cpf) && !cpf.IsCpf()), Resource.CpfInvalido)
				.Quando(string.IsNullOrEmpty(email) || (!string.IsNullOrEmpty(email) && !email.IsValidEmail()), Resource.EmailInvalido)
				.DispararExcecaoSeExistir();

			_nome = nome;
			_publicoAlvo = publicoAlvo;
			_email = email;
			_cpf = cpf;

			this.Nome = nome;
			this.Cpf = cpf;
			this.Email = email;
			this.PublicoAlvo = publicoAlvo;
		}

		public string Nome { get => _nome; set => _nome = value; }
		public string Cpf { get => _cpf; set => _cpf = value; }
		public string Email { get => _email; set => _email = value; }
		public PublicoAlvoEnum PublicoAlvo { get => _publicoAlvo; set => _publicoAlvo = value; }

		public void AlterarNome(string nome)
		{
			ValidadorDeRegra.Novo()
				.Quando(string.IsNullOrEmpty(nome), Resource.NomeInvalido)
				.DispararExcecaoSeExistir();

			Nome = nome;
		}
	}
}