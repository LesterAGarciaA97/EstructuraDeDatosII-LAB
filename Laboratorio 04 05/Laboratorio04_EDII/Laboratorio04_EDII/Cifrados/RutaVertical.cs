using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Laboratorio04_EDII.Cifrados
{
    public class RutaVertical
    {
        private char[] VerificacionCadena(IFormFile Archivo, int m, int n, char matriz)
        {
            char[] TextoCompleto = null;
            char[] Resultante = null;
            string Temp;
            using (var Lectura = new StreamReader(Archivo.OpenReadStream()))
            {
                var CapturarArchivo = new StringBuilder();
                while (Lectura.Peek() >= 0)
                {
                    CapturarArchivo.AppendLine(Lectura.ReadLine());
                }
                Temp = CapturarArchivo.ToString();
                if (Temp.Contains("\r\n"))
                {
                    Temp = Temp.Replace("\r\n", "\n");
                }
                TextoCompleto = Temp.ToCharArray();
            }
            return Resultante;
        }
        //Constructor de clase
        public RutaVertical()
        {
        }
        int m { get; set; }
        int n { get; set; }
        char [,] matriz { get; set; }
        public string NuevoNombre { get; set; }
    }
}
