using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Security.Cryptography;
using System.Text;

namespace Project_Web.Controllers
{
    public class EncryptionPW
    {
        public string originalValue { get; set;}
        public EncryptionPW(string value)
        {
            originalValue = value;
        }
        public string EncryptPass ()
        {
            Byte[] beforeEncrypt;
            Byte[] EncryptedValue;
            MD5 md5 = new MD5CryptoServiceProvider();
            beforeEncrypt = ASCIIEncoding.Default.GetBytes(originalValue);
            EncryptedValue = md5.ComputeHash(beforeEncrypt);
            return BitConverter.ToString(EncryptedValue);
        }
    }
}