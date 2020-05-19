using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoFinal_EDII.BStarTree
{
    public class GeneradorData
    {
        public static byte[] ConvertToBytes(string texto) {
            return Encoding.ASCII.GetBytes(texto);
        }
        public static string ConvertToString(byte[] bytes) {
            return Encoding.ASCII.GetString(bytes);
        }
        public static byte[] ConvertToBytes(char[] texto) {
            return Encoding.ASCII.GetBytes(texto);
        }
    }
}
