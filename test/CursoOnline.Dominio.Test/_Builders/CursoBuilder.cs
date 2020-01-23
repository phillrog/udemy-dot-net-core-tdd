using CursoOnline.Dominio.Test.Cursos;
using System;
using System.Collections.Generic;
using System.Text;

namespace CursoOnline.Dominio.Test._Builders
{
	public class CursoBuilder
	{
		private string _nome = "Informática básica";
		private PublicoAlvo _publicoAlvo = PublicoAlvo.Estudante;
		private double _valor = (double)950;
		private double _cargaHoraria = (double)80;
		private string _descricao = "Teste";

		public static CursoBuilder Novo()
		{
			return new CursoBuilder();
		}

		public CursoBuilder ComNome(string nome)
		{
			_nome = nome;

			return this;
		}

		public CursoBuilder ComDescricao(string descricao)
		{
			_descricao = descricao;

			return this;
		}

		public CursoBuilder ComCargaHoraria(double cargaHoraria)
		{
			_cargaHoraria = cargaHoraria;

			return this;
		}

		public CursoBuilder ComValor(double valor)
		{
			_valor = valor;

			return this;
		}

		public CursoBuilder ComPublicoAlvo(PublicoAlvo publicoAlvo)
		{
			_publicoAlvo = publicoAlvo;

			return this;
		}

		public Curso Build()
		{
			return new Curso(_nome, _cargaHoraria, _publicoAlvo, _valor, _descricao);
		}
	}
}
