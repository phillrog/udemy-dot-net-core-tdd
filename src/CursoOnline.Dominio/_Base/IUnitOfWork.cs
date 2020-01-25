using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CursoOnline.Dominio._Base
{
	public interface IUnitOfWork
	{
		Task Commit();
	}
}
