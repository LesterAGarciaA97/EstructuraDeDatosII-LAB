namespace Lab03_EDII
{
    public class Compresiones
    {
        public string nombreOriginal { get; set; }
        public string ruta { get; set; }
        public double razonCompresion { get; set; } 
        public double factorCompresion { get; set; }

        public Compresiones(string nombre, string rutaArchivo, double razon, double factor)
        {
            nombreOriginal = nombre;
            this.ruta = rutaArchivo;
            razonCompresion = razon;
            factorCompresion = factor;
        }
    }
}
