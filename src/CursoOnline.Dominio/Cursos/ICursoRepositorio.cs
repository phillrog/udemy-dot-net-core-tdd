namespace CursoOnline.Dominio.Cursos
{
	public interface ICursoRepositorio
	{
		void Adicionar(Curso curso);

		Curso ObeterPeloNome(string nome);
	}
}