using System;
using System.Security.Cryptography;
using System.IO;
using System.Text;

public static class Encryptor {

	private static CryptKey keys = CryptKey.Convert("");
	public static CryptKey Keys {
		get { return keys;}
		set { keys = value;}
	}

	public static string Encrypt(string text) {
		byte[] textBytes = Encoding.UTF8.GetBytes (text);
		byte[] keyBytes = new Rfc2898DeriveBytes (keys.PasswordHash, Encoding.ASCII.GetBytes (keys.SaltKey)).GetBytes (256 / 8);

		var symmetricKey = new RijndaelManaged() {Mode = CipherMode.CBC, Padding = PaddingMode.Zeros};
		var encryptor = symmetricKey.CreateEncryptor (keyBytes, Encoding.ASCII.GetBytes (keys.VIKey));

		byte[] cipherTextBytes;

		using (var memoryStream = new MemoryStream ()) {
			using (var cryptoStream = new CryptoStream (memoryStream, encryptor, CryptoStreamMode.Write)) {
				cryptoStream.Write (textBytes, 0, textBytes.Length);
				cryptoStream.FlushFinalBlock ();
				cipherTextBytes = memoryStream.ToArray ();
				cryptoStream.Close ();
			}
			memoryStream.Close ();
		}

		return Convert.ToBase64String (cipherTextBytes);
	}

	public static string Decrypt(string encryptedText) {
		byte[] cipherTextBytes = Convert.FromBase64String (encryptedText);
		byte[] keyBytes = new Rfc2898DeriveBytes(keys.PasswordHash, Encoding.ASCII.GetBytes(keys.SaltKey)).GetBytes(256 / 8);

		var symmetricKey = new RijndaelManaged () { Mode = CipherMode.CBC, Padding = PaddingMode.None };
		var decryptor = symmetricKey.CreateDecryptor (keyBytes, Encoding.ASCII.GetBytes (keys.VIKey));

		var memoryStream = new MemoryStream (cipherTextBytes);
		var cryptoStream = new CryptoStream (memoryStream, decryptor, CryptoStreamMode.Read);
		byte[] textBytes = new byte[cipherTextBytes.Length];

		int decryptedByteCount = cryptoStream.Read (textBytes, 0, textBytes.Length);
		memoryStream.Close ();
		cryptoStream.Close ();
		return Encoding.UTF8.GetString (textBytes, 0, decryptedByteCount).TrimEnd ("\0".ToCharArray ());
	}
}
