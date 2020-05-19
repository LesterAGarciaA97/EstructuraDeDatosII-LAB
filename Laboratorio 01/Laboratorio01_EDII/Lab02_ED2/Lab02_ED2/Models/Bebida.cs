namespace Lab02_ED2.Models
{
    public class Bebida {
        //Constructor de modelo Bebida
        public Bebida(string _nombre,string _sabor, string _volumen, string _precio, string _casa_productora) {
            Nombre = _nombre;
            Sabor = _sabor;
            Volumen = _volumen;
            Precio = _precio;
            casaProductora = _casa_productora;
        }
        public Bebida(){

        }
        public string Nombre { get; set; }
        public string Sabor { get; set; }
        public string Volumen { get; set; }
        public string Precio { get; set; }
        public string casaProductora { get; set; }
    }
}
