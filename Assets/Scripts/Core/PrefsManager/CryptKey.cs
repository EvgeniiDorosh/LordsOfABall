using System;

public class CryptKey {

	public string PasswordHash;
	public string SaltKey;
	public string VIKey;

	public static CryptKey Convert(string value) {
		string keyText = value.PadRight (16).Replace(" ", "0");
		CryptKey result = new CryptKey ();
		result.PasswordHash = keyText;
		result.SaltKey = keyText.Substring (keyText.Length - 8).ToLower();
		result.VIKey = keyText.Substring (keyText.Length - 16).ToUpper ();

		return result;
	}
}
