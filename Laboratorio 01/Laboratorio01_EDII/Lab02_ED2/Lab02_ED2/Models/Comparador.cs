namespace Lab02_ED2.Models
{
    public class Comparador
    {
        public int CompararPorNombre(Bebida Bebida1, Bebida Bebida2) {
           return Bebida1 == null || Bebida2 == null ? 1 : Bebida1.Nombre.CompareTo(Bebida2.Nombre);
        }
    }
}
