namespace Lab02.Models
{
    public class DataSingelton
    {
        private static DataSingelton _instance = null;
        public static DataSingelton Instance
        {
            get
            {
                if (_instance == null) _instance = new DataSingelton();
                return _instance;
            }
        }
        private static Comparador _Comparador = new Comparador();
        //public arbolBAsterisco<Bebida> arbol = new arbolBAsterisco<Bebida>(5, _Comparador.CompararPorNombre);
    }
}
