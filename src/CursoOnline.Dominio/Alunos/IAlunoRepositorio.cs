using System;
using System.Collections.Generic;
using System.Text;

namespace CursoOnline.Dominio.Alunos
{
	public interface IAlunoRepositorio
	{
		void Adicionar(Aluno curso);
		Aluno ObterPorId(int id);
		Aluno ObterPorCpf(string cpf);
	}
}
