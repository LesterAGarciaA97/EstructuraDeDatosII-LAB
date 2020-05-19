using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace LaboratorioReposicionEDII.Class.LZW
{
    public class DescompresionLzw
    {/// <summary>
     /// Metodo donde genera el diccionario con cada porcion de palabras del archivo compreso, en cada uno de los espacios del diccionario almacenara los fragmentos de las palabras
     /// </summary>
     /// <param name="ArchivoString">Archivo de entrada que se encuentra cifrado</param>
     /// <returns></returns>
        public string LzwDecode(string ArchivoString)
        {
            var dict = new Dictionary<int, string>();
            var data = (ArchivoString + "").ToCharArray();
            var currChar = data[0];
            var oldPhrase = currChar.ToString();
            var _out = new string[] { currChar.ToString() };
            var code = 256;

            for (var i = 1; i < data.Length; i++)//volvera a generar el diccionario mediante los caracateres del archivo comprimido.
            {
                var currCode = data[i];
                var phrase = "";
                if (currCode < 256)
                {
                    phrase = data[i].ToString();
                }
                else
                {
                    phrase = dict.ContainsKey(currCode) ? dict[currCode] : (oldPhrase + currChar).ToString();
                }
                Array.Resize(ref _out, _out.Length + 1);
                _out[_out.Length - 1] = phrase;
                currChar = phrase[0];
                dict[code] = (oldPhrase + currChar);
                code++;
                oldPhrase = phrase;
            }
            return string.Join("", _out);
        }

    }
}
