﻿using System;
using System.Collections.Generic;
using System.Text;

namespace CursoOnline.Dominio.Alunos
{
	public class AlunoDTO
	{
		public int Id { get; set; }
		public string Nome { get; set; }
		public string Cpf { get; set; }
		public string Email { get; set; }
		public string PublicoAlvo { get; set; }
	}
}
