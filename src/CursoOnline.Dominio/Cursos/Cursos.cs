using CursoOnline.Dominio._Base;
using System;

namespace CursoOnline.Dominio.Cursos
{
	public class Curso : Entidade
	{
		private double _valor;
		private string _descricao;
		private double _cargaHoraria;
		private string _nome;
		private PublicoAlvoEnum _publicoAlvo;

		public PublicoAlvoEnum PublicoAlvo { get => _publicoAlvo; set => _publicoAlvo = value; }
		public string Nome { get => _nome; set => _nome = value; }
		public double CargaHoraria { get => _cargaHoraria; set => _cargaHoraria = value; }
		public double Valor { get => _valor; set => _valor = value; }
		public string Descricao { get => _descricao; set => _descricao = value; }

		public Curso(string nome, double cargaHoraria, PublicoAlvoEnum publicoAlvo, double valor, string descricao)
		{

			ValidadorDeRegra.Novo()
				.Quando(string.IsNullOrEmpty(nome), Resource.NomeInvalido)
				.Quando(cargaHoraria < 1, Resource.CargaHorariaInvalida)
				.Quando(valor < 1, Resource.ValorInvalido)
				.DispararExcecaoSeExistir();

			_nome = nome;
			_descricao = descricao;
			_cargaHoraria = cargaHoraria;
			_publicoAlvo = publicoAlvo;
			_valor = valor;
		}

		public void AlterarNome(string nome)
		{
			ValidadorDeRegra.Novo()
				.Quando(string.IsNullOrEmpty(nome), Resource.NomeInvalido)
				.DispararExcecaoSeExistir();

			Nome = nome;
		}

		public void AlterarCargaHoraria(double cargaHoraria)
		{
			ValidadorDeRegra.Novo()
				.Quando(cargaHoraria < 1, Resource.CargaHorariaInvalida)
				.DispararExcecaoSeExistir();

			CargaHoraria = cargaHoraria;
		}

		public void AlterarValor(double valor)
		{
			ValidadorDeRegra.Novo()
				.Quando(valor < 1, Resource.ValorInvalido)
				.DispararExcecaoSeExistir();

			Valor = valor;
		}
	}
}
