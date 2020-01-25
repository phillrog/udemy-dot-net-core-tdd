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
			if (string.IsNullOrEmpty(nome))
				throw new ArgumentException("Nome inválido");

			if (cargaHoraria < 1)
				throw new ArgumentException("Carga Horária inválido");

			if (valor < 1)
				throw new ArgumentException("Valor inválido");


			_nome = nome;
			_descricao = descricao;
			_cargaHoraria = cargaHoraria;
			_publicoAlvo = publicoAlvo;
			_valor = valor;
		}
	}
}
