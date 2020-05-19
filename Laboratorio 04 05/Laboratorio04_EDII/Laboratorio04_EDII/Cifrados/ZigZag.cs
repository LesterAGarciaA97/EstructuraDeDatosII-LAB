using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Laboratorio04_EDII.Cifrados
{
    public class ZigZag
    {
        private char[] VerificacionCadena(IFormFile Archivo, int TamañoCarril) {
            char[] TextoCompleto = null;
            char[] Resultante = null;
            char[] Removedor = null;
            string[] Receptor = null;
            string Temp;
            double ElementosOla = ((TamañoCarril * 2) - 2);
            double Olas = 0;
            int TempNumerico = 0;
            using (var Lectura = new StreamReader(Archivo.OpenReadStream()))
            {
                var CapturarArchivo = new StringBuilder();
                while (Lectura.Peek() >= 0)
                {
                    CapturarArchivo.AppendLine(Lectura.ReadLine());
                }
                Temp = CapturarArchivo.ToString();
                Removedor = Temp.ToCharArray();
                Receptor = new string[Removedor.Length - 2];
                for (int i = 0; i < Removedor.Length - 2 ; i++)
                {
                    Receptor[i] = Removedor[i].ToString();
                }
                Temp = String.Concat(Receptor);

                if (Temp.Contains("\r\n"))
                {
                    Temp = Temp.Replace("\r\n", "\n");
                }
                TextoCompleto = Temp.ToCharArray();
                
            }
            Olas = TextoCompleto.Length / ElementosOla;
            if (Olas % 1 != 0)
            {
                double resulado1 = ((Olas % 1) - 1) * -1;
                Olas = Olas + resulado1;
                Resultante = new char[Convert.ToInt32(Olas * ElementosOla)];
                for (int i = 0; i < TextoCompleto.Length; i++)
                {
                    Resultante[i] = TextoCompleto[i];
                }
                //double resulado = ((Olas % 1)-1)*-1;
                //Olas = Olas + resulado;
                TempNumerico = ((Convert.ToInt32(Olas * ElementosOla)) - 1);
                for (int i = TempNumerico-1; i < Olas*ElementosOla; i++)
                {
                    Resultante[i] = '#';
                }
            }
            else
            {
                Resultante = TextoCompleto;
            }
            return Resultante;        
        }

        public void CifradoZigZag(IFormFile Archivo, int TamañoCarril, string NombreArchivo, string path) {
            TamañoCarril = 3;
            char[] ArregloValores = VerificacionCadena(Archivo, TamañoCarril);
            string[] CadenaCifrada = new string[ArregloValores.Length];
            int contador = 0;
            if (TamañoCarril == 3)
            {
                for (int i = 1; i <= TamañoCarril; i++)//se verifica el tamaño de los carriles ingresados
                {
                    for (int j = i - 1; j < ArregloValores.Length; j = j + (TamañoCarril + 1)) //se toman unicamente los valores que esten en la primera y ultima fila
                    {
                        if (i <= TamañoCarril - 1 && i != (TamañoCarril - TamañoCarril + 1) && i != TamañoCarril)//se valida que se trabaje unicamente en los valores de carril el primero o el ultimo si no entra al metodo y escribe los carriles que esten dentro del rango de estos
                        {
                            for (int k = j; k < ArregloValores.Length; k = k + 2)
                            {
                                CadenaCifrada[contador] = ArregloValores[k].ToString();
                                contador++;
                            }
                            i++;
                            j = j - 3;//se regresa las posiciones, para poder obtener los valores de la columa final
                        }
                        else
                        {
                            CadenaCifrada[contador] = ArregloValores[j].ToString();
                            contador++;
                        }
                    }
                } 
            }
            EscrituraCifradoZigZag(CadenaCifrada, NombreArchivo, path);
        }
        private void EscrituraCifradoZigZag(string[] CadenaCifrada, string NombreArchivo, string pathArchivo) {
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

        public void DescifradoZigZag(IFormFile Archivo, int TamañoCarril, string NombreArchivo, string Path) {
            TamañoCarril = 3;
            string Temp = "";
            char[] removedor = null;
            string[] receptor = null;
            char[] charResultante = null;
            double CantidadOlas = 0;
            double CantidadElementos = 0;
            string[] CadenaInicio;
            string[] CadenaFinal; 
            string[] CadenaMedio; 

            using (var Lectura = new StreamReader(Archivo.OpenReadStream()))
            {
                var CapturarArchivo = new StringBuilder();
                while (Lectura.Peek() >= 0)
                {
                    CapturarArchivo.AppendLine(Lectura.ReadLine());
                }
                Temp = CapturarArchivo.ToString();
                removedor = Temp.ToCharArray();
                receptor = new string[removedor.Length - 2];
                for (int i = 0; i < removedor.Length - 2; i++)
                {
                    receptor[i] = removedor[i].ToString();
                }
                Temp = String.Concat(receptor);

                Temp = Temp.Replace("\r\n", "\n");
                if (Temp.Contains("\r\n"))
                {
                    Temp.Replace("\r\n", "\n");
                }
                charResultante = Temp.ToCharArray();
                CantidadElementos = (TamañoCarril * 2) - 2;
                CantidadOlas = charResultante.Length / CantidadElementos;
                CadenaInicio = new string[Convert.ToInt32(CantidadOlas)];
                CadenaFinal = new string[Convert.ToInt32(CantidadOlas)];
                CadenaMedio = new string[charResultante.Length - (Convert.ToInt32(CantidadOlas)*2)];
                int aux = 0;
                for (int i = 0; i < CantidadOlas; i++)//comenzaran del inicio hasta tomar la primera porcion de toda la cadena
                {
                    CadenaInicio[i] = charResultante[i].ToString();
                }

                for (int j = ((charResultante.Length)- Convert.ToInt32(CantidadOlas)); j < charResultante.Length ; j++)//comienza luego del (final - la cantidad de elementos que se toman) y los coloca en el arreglo del final
                {
                    CadenaFinal[aux] = charResultante[j].ToString();
                    aux++;
                }
                aux = 0;
                for (int k =Convert.ToInt32(CantidadOlas); k < (charResultante.Length - Convert.ToInt32(CantidadOlas)); k++)//captura los valores que estan en la cadena y quedan en el centro
                {
                    CadenaMedio[aux] = charResultante[k].ToString();
                    aux++;
                }

                ///////////////////////////////////////////////////Descifrado de las cadenas/////////////////////////////////////////////////////////////////////////////////////////////////////
                int ContadorInicio = 0;//para la cadena inicio
                int ContadorMedio = 0;//cadena medio
                int ContadorFinal = 0;//cadena final
                int ValorContador = 1;//para saber en cual le toca avanzar
                int TempContador = 0;//para guardar la posicion anterior que se avanzo
                string[] CadenaResultante = new string[charResultante.Length];//donde se almacenar el cifrado
                int data = 0; //aumentara en la medida de la cadena reusltante
                for (int n = 0; n < charResultante.Length; n = n)
                {
                    
                    if (ValorContador == 1)
                    {
                        CadenaResultante[data] = CadenaInicio[ContadorInicio]; 
                        ValorContador++;
                        n++;
                        ContadorInicio++;
                        TempContador = 1;
                        data++;
                    }
                    if (ValorContador == 2)
                    {
                        CadenaResultante[data] = CadenaMedio[ContadorMedio];
                        ValorContador++;
                        if (TempContador==3)
                        {
                            ValorContador = 1;
                        }
                        TempContador = 2;
                        n++;
                        ContadorMedio++;
                        data++;
                    }
                    if (ValorContador == 3)
                    {
                        CadenaResultante[data] = CadenaFinal[ContadorFinal];
                        TempContador = 3;
                        ValorContador--;
                        n++;
                        ContadorFinal++;
                        data++;
                    }
                }
                Lectura.Close();
                EscrituraDescifradoZigZag(CadenaResultante, NombreArchivo, Path);
            }
        
        }
        private void EscrituraDescifradoZigZag(string[] CadenaResultante, string NombreArchivo, string pathArchivo) {
            for (int i = 0; i < CadenaResultante.Length; i++)
            {
                if (CadenaResultante[i] == "#")
                {
                    CadenaResultante[i] = "";
                }
            }
            var path = Path.Combine(pathArchivo, System.IO.Path.GetFileNameWithoutExtension(NombreArchivo) + ".txt");
            using (StreamWriter Escritura = new StreamWriter(path))
            {
                foreach (string item in CadenaResultante)
                {
                    Escritura.Write(item);
                }
                Escritura.Close();
            }


        }
    }
}
