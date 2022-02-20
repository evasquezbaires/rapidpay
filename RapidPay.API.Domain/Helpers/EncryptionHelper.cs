using System;
using System.Text;

namespace RapidPay.API.Domain.Helpers
{
    public static class EncryptionHelper
    {
		public static string EncryptBase64(string value)
		{
			string result = string.Empty;
			try
			{
				if (!string.IsNullOrEmpty(value))
				{
					byte[] bytes = UnicodeEncoding.UTF8.GetBytes(value);
					result = Convert.ToBase64String(bytes);
				}
			}
			catch { }

			return result;
		}

		public static string DecryptBase64(string value)
		{
			string result = string.Empty;

			try
			{
				byte[] bytes = Convert.FromBase64String(value);
				result = Encoding.UTF8.GetString(bytes);
			}
			catch { }

			return result;
		}
	}
}
