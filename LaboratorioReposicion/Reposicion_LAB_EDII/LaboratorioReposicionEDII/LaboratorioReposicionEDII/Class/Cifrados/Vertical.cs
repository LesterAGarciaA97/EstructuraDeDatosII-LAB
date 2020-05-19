using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LaboratorioReposicionEDII.Class.Cifrados
{
    public class Vertical
    {
        private char[,] matrizDatos;
        public string CifradoVertical(string ArchivoCargadoTexto, int n, int m) {
            matrizDatos= new char[m,n];
            var TextoCifrado = string.Empty;
            List<char> CaracteresCompletos = new List<char>();
            CaracteresCompletos = ArchivoCargadoTexto.ToArray().ToList(); 
            int ValorPosicion = 0;
            while (CaracteresCompletos.Count != 0)
            {
                for (int i = 0; i < n; i++)
                {
                    for (int j = 0; j < m; j++)
                    {
                        if (ValorPosicion != ArchivoCargadoTexto.Length)
                        {
                            matrizDatos[j, i] = ArchivoCargadoTexto[ValorPosicion];
                            CaracteresCompletos.RemoveAt(0);
                        }
                        else
                        {
                            matrizDatos[j, i] = '↔';
                            CaracteresCompletos.RemoveAt(0);
                        }
                        ValorPosicion++;
                    }
                }
                for (int i = 0; i < m; i++)
                {
                    for (int j = 0; j < n; j++)
                    {
                        TextoCifrado = TextoCifrado + matrizDatos[i, j];
                    }
                }
            }
            
            return TextoCifrado;
        }

        public string DecifradoVertical(string ArchivoCargadoTexto, int n, int m) {
            matrizDatos = new char[m, n];
            var TextoResultante = string.Empty;
            int ValorPosicion = 0;
            List<char> CaracteresCompletos = new List<char>();
            CaracteresCompletos = ArchivoCargadoTexto.ToArray().ToList();
            while (CaracteresCompletos.Count != 0)
            {
                for (int i = 0; i < m; i++)
                {
                    for (int j = 0; j < n; j++)
                    {
                        matrizDatos[j, i] = ArchivoCargadoTexto[ValorPosicion];
                        ValorPosicion++;
                        CaracteresCompletos.RemoveAt(0);
                    }
                }
                for (int i = 0; i < n; i++)
                {
                    for (int j = 0; j < m; j++)
                    {
                        TextoResultante = TextoResultante + matrizDatos[i, j];
                    }
                } 
            }
            if (TextoResultante.Contains("↔"))
            {
                TextoResultante = TextoResultante.Replace("↔", "");
            }
            return TextoResultante;
        }
    }
}
