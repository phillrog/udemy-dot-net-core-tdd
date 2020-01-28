using System.Text.RegularExpressions;

namespace CursoOnline.Dominio._Util
{
	public static class EmailValidoExtension
	{
		public static bool IsValidEmail(this string email)
		{
			if (string.IsNullOrEmpty(email)) return false;

			string pattern = @"^(?!\.)(""([^""\r\\]|\\[""\r\\])*""|" + @"([-a-z0-9!#$%&'*+/=?^_`{|}~]|(?<!\.)\.)*)(?<!\.)" + @"@[a-z0-9][\w\.-]*[a-z0-9]\.[a-z][a-z\.]*[a-z]$";
			var regex = new Regex(pattern, RegexOptions.IgnoreCase);
			return regex.IsMatch(email);
		}
	}
}
