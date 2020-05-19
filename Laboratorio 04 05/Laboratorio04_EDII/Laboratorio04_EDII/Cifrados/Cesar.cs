using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Laboratorio04_EDII.Cifrados
{
    public class Cesar
    {
        private char[] VerificacionCadena(IFormFile Archivo, int LongitudLlave)
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
        string Llave { get; set; }
        List<char> AlfabetoOriginal = new List<char>();
        List<char> AlfabetoNuevo = new List<char>();
        bool Condicion;
        //Constructor de clase
        public Cesar()
        {
        }
        //Primero se construye el alfabeto tomando de base el original y el modificado haciendo un corrimiento dependiendo la longitug de la llave
        public void ConstruirAlfabeto()
        {
            List<byte> original = new List<byte>();
            List<byte> alterado = new List<byte>();
            byte[] BytesLlave = Encoding.ASCII.GetBytes(Llave);
            alterado.AddRange(BytesLlave);
            for (int i = 97; i < 123; i++)
            {
                original.Add((byte)i);
                alterado.Add((byte)i);
            }
            alterado = alterado.Distinct().ToList();
            for (int i = 0; i < original.Count; i++)
            {
                AlfabetoOriginal.Add(Convert.ToChar(original[i]));
            }
            for (int i = 0; i < alterado.Count; i++)
            {
                AlfabetoNuevo.Add(Convert.ToChar(alterado[i]));
            }
        }

        private int BusquedaEnAlfabeto(char Letra)
        {
            for (int i = 0; i < AlfabetoOriginal.Count; i++)
            {
                if (Letra == AlfabetoOriginal[i])
                {
                    Condicion = false;
                    return i;
                }
                else if (Letra == Char.ToUpper(AlfabetoOriginal[i]))//Se valida que todo sea igual en mayúsculas
                {
                    Condicion = true;
                    return i;
                }
            }
            return -1;
        }
        //Luego de armar el nuevo alfabeto, se hará la búsqueda de cada letra con su nueva equivalencia
        private int BusquedaEnAlfabetoNuevo(char Letra)
        {
            for (int i = 0; i < AlfabetoNuevo.Count; i++)
            {
                if (Letra == AlfabetoNuevo[i])
                {
                    Condicion = false;
                    return i;
                }
                else if (Letra == Char.ToUpper(AlfabetoNuevo[i]))//Se valida que todo sea igual en mayúsculas
                {
                    Condicion = true;
                    return i;
                }
            }
            return -1;
        }
        public void CifradoCesar(IFormFile Archivo, int LongitudLlave, string NombreArchivo, string path)
        {

            string TextoCifrado = "";
            string TextoCompleto = Archivo.ToString();
            TextoCompleto = Archivo.ToString();
            char[] ArregloValores = VerificacionCadena(Archivo, LongitudLlave);
            string[] CadenaCifrada = new string[ArregloValores.Length];
            //Esto va a eliminar el carácter extra que agrega el método de Lectura/Escritura al final de todo el texto leido
            TextoCompleto = TextoCompleto.Remove(TextoCompleto.Length - 1);
            TextoCompleto = TextoCompleto.Remove(TextoCompleto.Length - 1);
            for (int i = 0; i < TextoCompleto.Length; i++)
            {
                int temp = BusquedaEnAlfabeto(TextoCompleto[i]);
                if (temp == -1)
                {
                    TextoCifrado += TextoCompleto[i];
                }
                else
                {
                    if (Condicion) 
                    { 
                        TextoCifrado += char.ToUpper(AlfabetoNuevo[temp]);//Se pasa todo a mayúsculas
                    }
                    else 
                    { 
                        TextoCifrado += AlfabetoNuevo[temp];
                    }
                }
            }
            EscrituraCifradoCesar(CadenaCifrada, NombreArchivo, path);
        }
        private void EscrituraCifradoCesar(string[] CadenaCifrada, string NombreArchivo, string pathArchivo)
        {
            var path = Path.Combine(pathArchivo, System.IO.Path.GetFileNameWithoutExtension(NombreArchivo) + ".txt");
            using (StreamWriter Escritura = new StreamWriter(path))
            {
                foreach (string item in CadenaCifrada)
                {
                    Escritura.Write(item);
                }
                Escritura.Close();
            }
        }
        public void DescifradoCesar(IFormFile Archivo, int LongitudLlave, string NombreArchivo, string path)
        {

            string TextoDescifrado = "";
            string TextoCompleto = Archivo.ToString();
            TextoCompleto = Archivo.ToString();
            //Esto va a eliminar el carácter extra que agrega el método de Lectura/Escritura al final de todo el texto leido
            TextoCompleto = TextoCompleto.Remove(TextoCompleto.Length - 1);
            TextoCompleto = TextoCompleto.Remove(TextoCompleto.Length - 1);
            for (int i = 0; i < TextoCompleto.Length; i++)
            {
                int temp = BusquedaEnAlfabetoNuevo(TextoCompleto[i]);
                if (temp == -1)
                {
                    TextoDescifrado += TextoCompleto[i];
                }
                else
                {
                    if (Condicion) 
                    { 
                        TextoDescifrado += char.ToUpper(AlfabetoOriginal[temp]);//Se pasa todo a mayúsculas
                    }
                    else 
                    { 
                        TextoDescifrado += AlfabetoOriginal[temp];
                    }
                }
            }
        }
    }
}
