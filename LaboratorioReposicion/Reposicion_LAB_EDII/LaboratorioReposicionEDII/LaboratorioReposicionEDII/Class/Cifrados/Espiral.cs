using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LaboratorioReposicionEDII.Class.Cifrados
{
    public class Espiral
    {
        /// <summary>
        /// Algoritmo para el cifrado de forma de espiral
        /// </summary>
        /// <param name="ArchivoCargadoTexto">cadena de entrada que sera cifrada </param>
        /// <param name="n">tamaño de las filas </param>
        /// <param name="m">tamaño de las columnas</param>
        /// <returns>El texto cifrado con espiral</returns>
        public string CifradoEspiral(string ArchivoCargadoTexto, int n, int m) {
            var TextoResultanto = string.Empty;
            var N = n;
            var M = m;
            var Texto = ArchivoCargadoTexto;
            var matriz = new char[N, M];
            var ContLetras = 0;
            Texto = Texto.PadRight(N * M, '|');
            for (int i = 0; i < N; i++)
            {
                for (int j = 0; j < M; j++)
                {
                    matriz[j, i] = Texto[ContLetras];
                    ContLetras++;
                }
            }
            var txt = string.Empty;
            var PosicionDeX = 0;
            var PosicionDeY = 0;
            var CicloCircular = 0;
            var DireccionAbajo = true;
            var DireccionArriba = false;
            var DireccionDerecha = false;
            var DireccionIzquierda = false;

            for (int i = 0; i < Texto.Length; i++)
            {
                if (DireccionAbajo && PosicionDeY != N - 1 - CicloCircular)
                {
                    txt += matriz[PosicionDeX, PosicionDeY];
                    PosicionDeY++;
                }
                else if (DireccionAbajo && PosicionDeY == N - 1 - CicloCircular)
                {
                    txt += matriz[PosicionDeX, PosicionDeY];
                    PosicionDeX++;
                    DireccionAbajo = false; DireccionDerecha = true;
                }
                else if (DireccionDerecha && PosicionDeX != M - 1 - CicloCircular)
                {
                    txt += matriz[PosicionDeX, PosicionDeY];
                    PosicionDeX++;
                }
                else if (DireccionDerecha && PosicionDeX == M - 1 - CicloCircular)
                {
                    txt += matriz[PosicionDeX, PosicionDeY];
                    PosicionDeY--;
                    DireccionDerecha = false; DireccionArriba = true;
                }
                else if (DireccionArriba && PosicionDeY != CicloCircular)
                {
                    txt += matriz[PosicionDeX, PosicionDeY];
                    PosicionDeY--;
                }
                else if (DireccionArriba && PosicionDeY == CicloCircular)
                {
                    txt += matriz[PosicionDeX, PosicionDeY];
                    CicloCircular++;
                    PosicionDeX--;
                    DireccionArriba = false; DireccionIzquierda = true;
                }
                else if (DireccionIzquierda && PosicionDeX != CicloCircular)
                {
                    txt += matriz[PosicionDeX, PosicionDeY];
                    PosicionDeX--;
                }
                else if (DireccionIzquierda && PosicionDeX == CicloCircular)
                {
                    txt += matriz[PosicionDeX, PosicionDeY];
                    PosicionDeY++;
                    DireccionIzquierda = false; DireccionAbajo = true;
                }
            }
            txt = txt.TrimEnd('|');
            TextoResultanto = txt;


            return TextoResultanto;
        }
        /// <summary>
        /// Algoritmo para el decifrado de forma de espiral
        /// </summary>
        /// <param name="ArchivoCargadoTexto">cadena de entrada que sera cifrada </param>
        /// <param name="n">tamaño de las filas </param>
        /// <param name="m">tamaño de las columnas</param>
        /// <returns>El texto cifrado con espiral</returns>
        public string DescrifradoEspiral(string ArchivoCargadoTexto, int n, int m) {
            var TextoResultante = string.Empty;
            var M = m;
            var N = n;
            var txt = string.Empty;
            var Matriz = new char[N, M];
            var PosicionDeX = 0;
            var PosicionDeY = 0;
            var CicloCircular = 0;
            var DireccionAbajo = true;
            var DireccionArriba = false;
            var DireccionDerecha = false;
            var DireccionIzquierda = false;
            var Texto = ArchivoCargadoTexto;

            for (int i = 0; i < Texto.Length; i++)
            {
                if (DireccionAbajo && PosicionDeY != N - 1 - CicloCircular)
                {
                    Matriz[PosicionDeX, PosicionDeY] = Texto[i];
                    PosicionDeY++;
                }
                else if (DireccionAbajo && PosicionDeY == N - 1 - CicloCircular)
                {
                    Matriz[PosicionDeX, PosicionDeY] = Texto[i];
                    PosicionDeX++;
                    DireccionAbajo = false; DireccionDerecha = true;
                }
                else if (DireccionDerecha && PosicionDeX != M - 1 - CicloCircular)
                {
                    Matriz[PosicionDeX, PosicionDeY] = Texto[i];
                    PosicionDeX++;
                }
                else if (DireccionDerecha && PosicionDeX == M - 1 - CicloCircular)
                {
                    Matriz[PosicionDeX, PosicionDeY] = Texto[i];
                    PosicionDeY--;
                    DireccionDerecha = false; DireccionArriba = true;
                }
                else if (DireccionArriba && PosicionDeY != CicloCircular)
                {
                    Matriz[PosicionDeX, PosicionDeY] = Texto[i];
                    PosicionDeY--;
                }
                else if (DireccionArriba && PosicionDeY == CicloCircular)
                {
                    Matriz[PosicionDeX, PosicionDeY] = Texto[i];
                    CicloCircular++;
                    PosicionDeX--;
                    DireccionArriba = false; DireccionIzquierda = true;
                }
                else if (DireccionIzquierda && PosicionDeX != CicloCircular)
                {
                    Matriz[PosicionDeX, PosicionDeY] = Texto[i];
                    PosicionDeX--;
                }
                else if (DireccionIzquierda && PosicionDeX == CicloCircular)
                {
                    Matriz[PosicionDeX, PosicionDeY] = Texto[i];
                    PosicionDeY++;
                    DireccionIzquierda = false; DireccionAbajo = true;
                }
            }

            for (int i = 0; i < N; i++)
            {
                for (int j = 0; j < M; j++)
                {
                    txt += Matriz[j, i];
                }
            }

            TextoResultante = txt;
            return TextoResultante;
        }
        
    }
}
