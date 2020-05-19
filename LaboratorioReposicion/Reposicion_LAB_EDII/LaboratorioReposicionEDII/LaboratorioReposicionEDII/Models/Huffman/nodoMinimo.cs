using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LaboratorioReposicionEDII.Models.Huffman
{
    public class nodoMinimo
    {
        public byte letra;
        public int izquierdo, derecho;
        public nodoMinimo() { }
        public nodoMinimo(byte letter, int left, int right)
        {
            this.letra = letter;
            this.izquierdo = left;
            this.derecho = right;
        }
    }
}
