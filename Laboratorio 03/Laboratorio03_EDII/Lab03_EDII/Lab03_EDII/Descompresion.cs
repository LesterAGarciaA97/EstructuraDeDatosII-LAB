using System;
using System.Collections;
using System.Collections.Generic;

namespace Lab03_EDII
{
    public class Descompresion
    {
        public static byte[] DescompresionCompleta(byte[] fullBytes)
        {
            byte[] lenghtBytes = { fullBytes[0], fullBytes[1], fullBytes[2], fullBytes[3] };
            int longitud = BitConverter.ToInt32(lenghtBytes, 0);
            List<nodoMinimo> nodosMinimos = new List<nodoMinimo>();
            for (int i = 0; i < longitud; i++)
            {
                byte[] bytesLetras = { fullBytes[4 + i * 9] };
                byte[] bytesIzquierdos = { fullBytes[4 + i * 9 + 1], fullBytes[4 + i * 9 + 2], fullBytes[4 + i * 9 + 3], fullBytes[4 + i * 9 + 4] };
                byte[] bytesDerechos = { fullBytes[4 + i * 9 + 5], fullBytes[4 + i * 9 + 6], fullBytes[4 + i * 9 + 7], fullBytes[4 + i * 9 + 8] };

                if (BitConverter.IsLittleEndian)
                {
                    Array.Reverse(bytesLetras);
                    Array.Reverse(bytesIzquierdos);
                    Array.Reverse(bytesDerechos);
                }
                byte letra = bytesLetras[0];
                int izquierdo = BitConverter.ToInt32(bytesIzquierdos, 0);
                int derecho = BitConverter.ToInt32(bytesDerechos, 0);

                nodosMinimos.Add(new nodoMinimo(letra, izquierdo, derecho));
            }

            List<byte> listadoDescomprimido = new List<byte>();

            int nodoActual = 0;
            byte[] bytesComprimidos = new byte[fullBytes.Length - 4 - longitud * 9];
            for (int i = 0; i < bytesComprimidos.Length; i++)
            {
                bytesComprimidos[i] = fullBytes[4 + longitud * 9 + i];
            }

            BitArray bitArray = new BitArray(bytesComprimidos);
            for (int i = 0; i < bitArray.Length; i++)
            {
                if (bitArray[i])
                {
                    nodoActual = nodosMinimos[nodoActual].derecho;
                    if (nodosMinimos[nodoActual].izquierdo == nodosMinimos[nodoActual].derecho && nodosMinimos[nodoActual].izquierdo == -1)
                    {
                        listadoDescomprimido.Add(nodosMinimos[nodoActual].letra);
                        nodoActual = 0;
                    }
                    else if (nodosMinimos[nodoActual].izquierdo == nodosMinimos[nodoActual].derecho && nodosMinimos[nodoActual].izquierdo == -2)
                    {
                        i = bitArray.Length - 1;
                    }
                }
                else
                {
                    nodoActual = nodosMinimos[nodoActual].izquierdo;
                    if (nodosMinimos[nodoActual].izquierdo == nodosMinimos[nodoActual].derecho && nodosMinimos[nodoActual].izquierdo == -1)
                    {
                        listadoDescomprimido.Add(nodosMinimos[nodoActual].letra);
                        nodoActual = 0;
                    }
                    else if (nodosMinimos[nodoActual].izquierdo == nodosMinimos[nodoActual].derecho && nodosMinimos[nodoActual].izquierdo == -2)
                    {
                        i = bitArray.Length - 1;
                    }
                }
            }
            return listadoDescomprimido.ToArray();
        }
    }
}
