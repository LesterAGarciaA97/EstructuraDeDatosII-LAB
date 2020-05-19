using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LaboratorioReposicionEDII.Models.Huffman
{
    public class Nodo : IComparable<Nodo>
    {

        public int id;
        public bool esLetra;
        public byte letra;
        public int frecuencia;
        public Nodo left, right;
        public Nodo() { }
        public Nodo(byte letter, int frequency, Nodo left, Nodo right, bool isLetter)
        {
            this.letra = letter;
            this.frecuencia = frequency;
            this.left = left;
            this.right = right;
            this.esLetra = isLetter;
        }
        public int CompareTo(Nodo other)
        {
            return (this.frecuencia > other.frecuencia) ? -1 : ((this.frecuencia == other.frecuencia) ? 0 : 1);
        }
    }
}
