using System.Text;
using System.Security.Cryptography;
using System;
using System.Threading.Tasks;
using System.Security.Claims;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace VirtualHotel.Data.Services
{
    public static class AutheticationService
    {
		static byte[] GenerateSaltedPasswordHash(byte[] plainText, byte[] salt)
		{
			HashAlgorithm algorithm = new SHA256Managed();

			byte[] plainTextWithSaltBytes =
			  new byte[plainText.Length + salt.Length];

			for (int i = 0; i < plainText.Length; i++)
			{
				plainTextWithSaltBytes[i] = plainText[i];
			}
			for (int i = 0; i < salt.Length; i++)
			{
				plainTextWithSaltBytes[plainText.Length + i] = salt[i];
			}

			return algorithm.ComputeHash(plainTextWithSaltBytes);
		}


		public static IdentityUser CheckPassword(string email, string password)
		{
			var authorizedUser = InMemoryRepository.GetUser(email);
			if (authorizedUser == null)
				return null;

			var passwordBytes = Encoding.UTF8.GetBytes(password);
			var saltBytes = Encoding.UTF8.GetBytes(authorizedUser.Salt);

			var hashedPassword = Convert.FromBase64String(authorizedUser.PasswordHash);
			var userInputedPassword = GenerateSaltedPasswordHash(passwordBytes, saltBytes);

			if (hashedPassword.Length != userInputedPassword.Length)
			{
				return null;
			}

			for (int i = 0; i < hashedPassword.Length; i++)
			{
				if (hashedPassword[i] != userInputedPassword[i])
				{
					return null;
				}
			}

			return authorizedUser;
		}


	}
}
