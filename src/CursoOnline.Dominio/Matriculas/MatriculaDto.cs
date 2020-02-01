namespace CursoOnline.Dominio.Matriculas
{
	public class MatriculaDto
	{
		public int AlunoId { get; set; }
		public int CursoId { get; set; }
		public double ValorPago { get; set; }

		public MatriculaDto(int alunoId, int cursoId, double valor)
		{
			AlunoId = alunoId;
			CursoId = cursoId;
			ValorPago = valor;
		}
	}
}
