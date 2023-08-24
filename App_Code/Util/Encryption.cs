#region Namespaces
using System;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
#endregion

/// <summary>
/// Summary description for Encryption
/// </summary>
public class Encryption
{
    #region Global Data Members
    private static byte[] SALT = new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 };
    #endregion

    #region Constructors
    public Encryption()
	{
    }
    #endregion

    #region Public Methods
    /// <summary>
    /// Decrypt a string
    /// </summary>
    /// <param name="encryptedText">Encrypted string</param>
    /// <param name="encryptionKey">Encryption key</param>
    /// <returns>Plain text</returns>
    public static string Decrypt(string encryptedText, string encryptionKey)
    {
        string plainText = string.Empty;
        if (encryptedText.Trim().Length > 0) plainText = DecryptFromBase64String(encryptedText, encryptionKey);

        return plainText;
    }

    /// <summary>
    /// Encrypt a string
    /// </summary>
    /// <param name="plainText">Plain text</param>
    /// <param name="encryptionKey">Encryption key</param>
    /// <returns>Encrypted string</returns>
    public static string Encrypt(string plainText, string encryptionKey)
    {
        string encryptedText = string.Empty;
        if (plainText.Trim().Length > 0) encryptedText = EncryptToBase64String(plainText, encryptionKey);

        return encryptedText;
    }
    #endregion

    #region Private Methods
    /// <summary>
    /// Decrypt from base 64 string
    /// </summary>
    /// <param name="encryptedText">Encrypted string</param>
    /// <param name="encryptionKey">Encryption key</param>
    /// <returns>Plain text</returns>
    private static string DecryptFromBase64String(string encryptedText, string encryptionKey)
    {
        byte[] decryptedData = null;

        // Workaround: 64-bit encoding does not work well with spaces in the string.
        //             Replace each of the spaces with "+". "+" is interpreted as space 
        //             by FromBase64String method.
        byte[] encryptedData = Convert.FromBase64String(encryptedText.Replace(" ", "+"));

        PasswordDeriveBytes pdb = new PasswordDeriveBytes(encryptionKey, SALT);
        using (MemoryStream ms = new MemoryStream())
        {
            Rijndael alg = Rijndael.Create();
            alg.Key = pdb.GetBytes(32);
            alg.IV = pdb.GetBytes(16);

            using (CryptoStream cs = new CryptoStream(ms, alg.CreateDecryptor(), CryptoStreamMode.Write))
            {
                cs.Write(encryptedData, 0, encryptedData.Length);
                cs.Close();
            }
            decryptedData = ms.ToArray();
        }

        return System.Text.Encoding.Unicode.GetString(decryptedData);
    }

    /// <summary>
    /// Encrypt to base 64 string
    /// </summary>
    /// <param name="plainText">Plain text</param>
    /// <param name="encryptionKey">Encryption key</param>
    /// <returns>Encrypted string</returns>
    private static string EncryptToBase64String(string plainText, string encryptionKey)
    {
        byte[] encryptedData = null;
        byte[] plainData = System.Text.Encoding.Unicode.GetBytes(plainText);
        PasswordDeriveBytes pdb = new PasswordDeriveBytes(encryptionKey, SALT);
        using (MemoryStream ms = new MemoryStream())
        {
            Rijndael alg = Rijndael.Create();
            alg.Key = pdb.GetBytes(32);
            alg.IV = pdb.GetBytes(16);

            using (CryptoStream cs = new CryptoStream(ms, alg.CreateEncryptor(), CryptoStreamMode.Write))
            {
                cs.Write(plainData, 0, plainData.Length);
                cs.Close();
            }

            encryptedData = ms.ToArray();
        }

        return Convert.ToBase64String(encryptedData);
    }
    #endregion
}
