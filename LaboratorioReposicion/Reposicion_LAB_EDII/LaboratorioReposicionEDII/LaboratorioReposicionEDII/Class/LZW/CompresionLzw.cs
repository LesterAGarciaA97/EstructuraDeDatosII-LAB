using LaboratorioReposicionEDII.Models.LZW;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaboratorioReposicionEDII.Class.LZW
{
    public class CompresionLzw
    {/// <summary>
    /// Metodo donde genera el diccionario con cada porcion de palabras, en cada uno de los espacios del diccionario almacenara los fragmentos de las palabras
    /// </summary>
    /// <param name="ArchivoString"></param>
    /// <returns></returns>
        public  string LzwEncode(string ArchivoString)
        {
            if (!string.IsNullOrWhiteSpace(ArchivoString))
            {
                var dict = new Dictionary<string, int>();
                var data = (ArchivoString + "").ToCharArray();
                var _out = new int[] { };
                var phrase = data[0].ToString();
                var code = 256;
                for (var i = 1; i < data.Length; i++)
                {
                    var currChar = data[i];
                    if (dict.ContainsKey(phrase + currChar))//verifica si se encuentra la conbinacion de letras dentro del diccionario
                    {
                        phrase += currChar;
                    }
                    else
                    {
                        Array.Resize(ref _out, _out.Length + 1);
                        _out[_out.Length - 1] = (phrase.Length > 1 ? dict[phrase] : phrase[0]);
                        dict[phrase + currChar] = code;
                        code++;
                        phrase = currChar.ToString();
                    }
                }
                Array.Resize(ref _out, _out.Length + 1);
                _out[_out.Length - 1] = (phrase.Length > 1 ? dict[phrase] : phrase[0]);
                var out2 = new char[_out.Length];
                for (var i = 0; i < _out.Length; i++)
                {
                    out2[i] = Convert.ToChar(_out[i]); // Convierte a char cada uno de los almacenados dentro del primer recuento de salida
                }
                return string.Join("", out2); 
            }
            return string.Empty;
        }
    }
    
}
