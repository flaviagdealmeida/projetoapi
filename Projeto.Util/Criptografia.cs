using System;
using System.Collections.Generic;
using System.Text;
using System.Security.Cryptography;

namespace Projeto.Util
{
    public class Criptografia
    {
        //metodo para retornar dado encriptado

        public static string GetMd5(string value)
        {
            //1º Passo - converter parametro para byte
            var valueBytes = Encoding.UTF8.GetBytes(value);

            //2º Passo Criptografar o valor recebido e armazenar o resultado
            var md5 = new MD5CryptoServiceProvider();
            var hash = md5.ComputeHash(valueBytes);

            //3º Passo transformar o hash em string hexadecimal
            string result = string.Empty;

            foreach (var pos in hash)
            {
                result += pos.ToString("x2"); //x2 = hexadecimal 
            }

            return result;

        }

        //Criptografia com Sha1
        public static string GetSha1(string value)
        {
            //1º Passo - converter parametro para byte
            var valueBytes = Encoding.UTF8.GetBytes(value);

            //2º Passo Criptografar o valor recebido e armazenar o resultado
            var sha1 = new SHA1CryptoServiceProvider();
            var hash = sha1.ComputeHash(valueBytes);

            //3º Passo transformar o hash em string hexadecimal
            string result = string.Empty;

            foreach (var pos in hash)
            {
                result += pos.ToString("x2"); //x2 = hexadecimal 
            }

            return result;

        }
    }
}
