namespace Lab03_EDII
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
