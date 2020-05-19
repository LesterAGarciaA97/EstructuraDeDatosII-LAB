using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaboratorioReposicionEDII.Class
{
    public class Ceaser
    {
        static string[] AlfabetoBase = { "/n", " ", "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "Ñ", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z", "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "ñ", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z" };
        static string[] TxtCifrado = null;
        Diffie_Hellman _diffie_hellman = new Diffie_Hellman();
        RSA _RSA = new RSA();
        public static int[] llavesParaCifrarDecifrar;


        public int[] RetornoLLaves(IFormFile ArchivoLLaves) {
            int[] LLaveResultantes = null;
            string[] LLavesRString = null;
            string Temp;
            char[] TextoResultante = null;
            char[] Removedor = null;
            string[] Receptor = null;
            using (var Lectura = new StreamReader(ArchivoLLaves.OpenReadStream()))
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
                LLavesRString= Temp.Split(',');
                LLaveResultantes = new int[LLavesRString.Length];
                for (int i = 0; i < LLavesRString.Length; i++)
                {
                    LLaveResultantes[i] = Convert.ToInt32(LLavesRString[i]);
                }
            }

            return LLaveResultantes;

        
        }
        /// <summary>
        /// metodo que procede a quitar caracteres innecesarios al momento de leer el archivo
        /// </summary>
        /// <param name="ArchivoEntrada">Archivo ingresado desde la API</param>
        /// <returns></returns>
        private char[] ReduccionArchivo(IFormFile ArchivoEntrada)
        {
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
        public void CifradoCeaser(IFormFile ArchivoEntrada,IFormFile ArchivoLlaves , int valorCorrimiento, string Path)
        {
            char[] TxtFuente = ReduccionArchivo(ArchivoEntrada);
            llavesParaCifrarDecifrar = RetornoLLaves(ArchivoLlaves);
            TxtCifrado = new string[TxtFuente.Length];
            for (int i = 0; i < TxtFuente.Length; i++)
            {
                TxtCifrado[i] = BusquedaAlfabeto(TxtFuente[i].ToString(), valorCorrimiento);
            }
            EscrituraLLaveCifrado(Path, valorCorrimiento);
            EscrituraCifrado(Path);
        }
        /// <summary>
        /// escritura de las llaves cifradas y ademas del texto cifrado con el algoritmo con Ceaser
        /// </summary>
        /// <param name="pathArchivo">ruta del archivo donde sera creado</param>
        /// <param name="valorCorrimiento">valor de corrimiento pendiente de ser cifrado</param>
        public void EscrituraLLaveCifrado(string pathArchivo, int valorCorrimiento)
        {
            //int CorrimientoCifradaDiffie = _diffie_hellman.ReturnPublicKey(valorCorrimiento);
            int CorrimientoCifradaRSA = _RSA.ReturnCipherKey(valorCorrimiento,llavesParaCifrarDecifrar[0],llavesParaCifrarDecifrar[1]);
            string NombreArchivo = string.Empty;
            DateTime Time = DateTime.Now;
            var Path1 = Path.Combine(pathArchivo, "CorrimientoCifrado" + ".txt");
            using (StreamWriter Escritura = new StreamWriter(Path1))
            {
                for (int i = 0; i < 1; i++)
                {
                    if (i == 0)
                    {
                        Escritura.Write(CorrimientoCifradaRSA);
                    }
                }
            }
        }
        public void EscrituraCifrado(string pathArchivo) {
            var Path1 = Path.Combine(pathArchivo, "Cifrado" + ".txt");
            using (StreamWriter Escritura = new StreamWriter(Path1))
            {
                for (int i = 0; i < 1; i++)
                {
                    if (i == 0)
                    {
                        Escritura.Write(String.Concat(TxtCifrado));
                    }
                }
            }

        }

        public int RetornoCorrimientoCifrado(IFormFile CifradoCorrimiento) {
            int LLaveResultante = 0;
            string Temp;
            char[] Removedor = null;
            string[] Receptor = null;
            using (var Lectura = new StreamReader(CifradoCorrimiento.OpenReadStream()))
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

                Lectura.Close();
                
            }
            LLaveResultante = Convert.ToInt32(Temp);
            return LLaveResultante;


        }
        
    }
}
