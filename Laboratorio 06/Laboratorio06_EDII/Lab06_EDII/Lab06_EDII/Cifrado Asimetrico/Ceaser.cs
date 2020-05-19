using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab06_EDII.Cifrado_Asimetrico
{
    public class Ceaser
    {
        static string[] AlfabetoBase = {"/n"," ","A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "Ñ", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z", "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "ñ", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z" };
        static string[] TxtCifrado = null;
        Diffie_Hellman _diffie_hellman = new Diffie_Hellman();
        RSA _RSA = new RSA();

        /// <summary>
        /// metodo que procede a quitar caracteres innecesarios al momento de leer el archivo
        /// </summary>
        /// <param name="ArchivoEntrada">Archivo ingresado desde la API</param>
        /// <returns></returns>
        private char[] ReduccionArchivo(IFormFile ArchivoEntrada) {
            string Temp;
            char[] TextoResultante = null;
            char[] Removedor = null;           
            string[] Receptor = null;

            using (var Lectura = new StreamReader(ArchivoEntrada.OpenReadStream()))
            {
                var CapturarArchivo = new StringBuilder();
                while (Lectura.Peek() >= 0)
                {
                    CapturarArchivo.AppendLine(Lectura.ReadLine());
                }
                Temp = CapturarArchivo.ToString();
                Removedor = Temp.ToCharArray();
                Receptor = new string[Removedor.Length - 2];
                for (int i = 0; i < Removedor.Length - 2; i++)
                {
                    Receptor[i] = Removedor[i].ToString();
                }
                Temp = String.Concat(Receptor);
                if (Temp.Contains("\r\n"))
                {
                    Temp = Temp.Replace("\r\n", "\n");
                }
                TextoResultante = Temp.ToCharArray();
                Lectura.Close();
            }
            return TextoResultante;
        
        }
        /// <summary>
        /// Metodo que procede a la busqueda del caracter dentro del alfabeto predeterminado y se asigna dependiendo el corrimiento
        /// </summary>
        /// <param name="valor">caracter a buscar</param>
        /// <param name="n">valor de corrimiento</param>
        /// <returns></returns>
        private string BusquedaAlfabeto(string valor, int n)
        {
            string resultante = string.Empty;
            int temp = 0;
            //metodo de comparacion del alfabeto original con el de corrimiento.
            for (int i = 0; i < AlfabetoBase.Length; i++)
            {
                if (AlfabetoBase[i].ToString() == valor)
                {
                    if ((i + n) >= AlfabetoBase.Length)
                    {
                        temp = (AlfabetoBase.Length - (i + n)) * -1;
                        resultante = AlfabetoBase[temp].ToString();
                        i = AlfabetoBase.Length;
                    }
                    else if ((AlfabetoBase.Length - (i + n)) >= 0)
                    {
                        resultante = AlfabetoBase[(i + n)].ToString();
                        i = AlfabetoBase.Length;
                    }
                    else
                    {
                        resultante = AlfabetoBase[i + n].ToString();
                        i = AlfabetoBase.Length;
                    }
                }
            }
            return resultante;
        }
        /// <summary>
        /// Proceso donde almacena los valores devueltos del metodo de BusquedaAlfabeto
        /// </summary>
        /// <param name="ArchivoEntrada">Archivo ingresado desde la API</param>
        /// <param name="valorCorrimiento">valor de corrimiento, que posteriormente sera cifrado mediante Diffie-Hellman y RSA </param>
        /// <param name="Path">Ruta donde se creara el archivo re</param>
        public void CifradoCeaser(IFormFile ArchivoEntrada, int valorCorrimiento, string Path) {
            char[] TxtFuente = ReduccionArchivo(ArchivoEntrada);
            TxtCifrado = new string[TxtFuente.Length];
            for (int i = 0; i < TxtFuente.Length; i++)
            {
                TxtCifrado[i] = BusquedaAlfabeto(TxtFuente[i].ToString(),valorCorrimiento);
            }
            EscrituraCifrado(Path, valorCorrimiento);
        }
        /// <summary>
        /// escritura de las llaves cifradas y ademas del texto cifrado con el algoritmo con Ceaser
        /// </summary>
        /// <param name="pathArchivo">ruta del archivo donde sera creado</param>
        /// <param name="valorCorrimiento">valor de corrimiento pendiente de ser cifrado</param>
        public void EscrituraCifrado(string pathArchivo, int valorCorrimiento) {
            int CorrimientoCifradaDiffie = _diffie_hellman.ReturnPublicKey(valorCorrimiento);
            int CorrimientoCifradaRSA = _RSA.ReturnPublicKey(valorCorrimiento);
            string NombreArchivo = string.Empty;
            DateTime Time = DateTime.Now;
            if (Time.ToString().Contains('/'))
            {
               NombreArchivo = Time.ToString().Replace('/', '-');
               NombreArchivo = NombreArchivo.Replace(' ', '-'); 
            }
            var Path1 = Path.Combine(pathArchivo, "Cifrado" + ".txt");
            using (StreamWriter Escritura = new StreamWriter(Path1))
            {
                for (int i = 0; i <= 2; i++)
                {
                    if (i==0)
                    {
                        Escritura.Write("La LLave de corrimiento Cifrada(Diffie_Hellman) : " + CorrimientoCifradaDiffie);
                    }
                    if (i==1)
                    {
                        Escritura.Write(Environment.NewLine +"La LLave de corrimiento Cifrada(RSA) : " + CorrimientoCifradaRSA);
                    }
                    if (i==2)
                    {
                        Escritura.Write(Environment.NewLine + "Texto Cifrado Ceaser: " +  String.Concat(TxtCifrado));
                    }
                }
            }

        }
    }
}
