using System;
using System.Collections.Generic;
using System.Text;

namespace CursoOnline.Dominio.Alunos
{
	public interface IAlunoRepositorio
	{
		void Adicionar(Aluno curso);
		Aluno ObeterPeloNome(string nome);
		Aluno ObterPorId(int id);
	}
}
