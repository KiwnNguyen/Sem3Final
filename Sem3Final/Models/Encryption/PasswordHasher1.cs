using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sem3Final.Models.Encryption
{
    public class PasswordHasher1
    {
        public static string EncodePasswordToBase64(string password)
        {
            try
            {
                if (password != null)
                {
                    byte[] encData_byte = new byte[password.Length];
                    encData_byte = System.Text.Encoding.UTF8.GetBytes(password);
                    string encodeData = Convert.ToBase64String(encData_byte);
                    return encodeData;

                }
            }
            catch (Exception e)
            {
                throw e;
            }
            return null;
        }


        public static string DecodeFrom64(string Encodepassword)
        {
            try
            {
                if (Encodepassword != null)
                {
                    System.Text.UTF8Encoding encoder = new System.Text.UTF8Encoding();
                    System.Text.Decoder utf8Decode = encoder.GetDecoder();
                    byte[] tocode_byte = Convert.FromBase64String(Encodepassword);
                    int charCount = utf8Decode.GetCharCount(tocode_byte, 0, tocode_byte.Length);
                    char[] decoded_char = new char[charCount];
                    utf8Decode.GetChars(tocode_byte, 0, tocode_byte.Length, decoded_char, 0);
                    string result = new string(decoded_char);
                    return result;
                }

            }
            catch (Exception e)
            {
                throw e;
            }
            return null;
        }
    }
}