using System;
using CursoOnline.Dominio.Cursos;

namespace CursoOnline.Dominio.Test._Builders
{
	public class CursoBuilder
	{
		private string _nome = "Informática básica";
		private PublicoAlvoEnum _publicoAlvo = PublicoAlvoEnum.Estudante;
		private double _valor = (double)950;
		private double _cargaHoraria = (double)80;
		private string _descricao = "Teste";
		private int _id;

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

		public CursoBuilder ComPublicoAlvo(PublicoAlvoEnum publicoAlvo)
		{
			_publicoAlvo = publicoAlvo;

			return this;
		}

		public CursoBuilder ComId(int id)
		{
			_id = id;

			return this;
		}

		public Curso Build()
		{
			var curso = new Curso(_nome, _cargaHoraria, _publicoAlvo, _valor, _descricao);
			var propertyInfo = curso.GetType().GetProperty("Id");
			propertyInfo.SetValue(curso, Convert.ChangeType(_id, propertyInfo.PropertyType, null));

			return curso; 
		}

	}
}
