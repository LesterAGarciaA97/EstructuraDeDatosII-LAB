using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Razor.Language.Extensions;
using Microsoft.AspNetCore.Razor.TagHelpers;
using ProyectoFinal_EDII.Interfaces;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ProyectoFinal_EDII.BStarTree
{
    public class Nodo<T> where T : IComparable , IFixedSizeText
    {
        internal List<T> Data { get; set; }
        internal List<int> Hijos { get; set; }
        internal int Posicion { get; set; }
        internal int Padre { get; set; }
        internal int ID { get; set; }
        internal int Orden { get; set; }
        internal ICreateFixedSizeText<T> createFixedSizeText = null;

        //Constructor del metodo del nodo para el establecimiento de las variable
        public Nodo() {

        }
        internal Nodo(int _orden, int _posicion , int _padre, ICreateFixedSizeText<T>  _createFixedSizeText) {
            if (Orden < 0) {
                throw new ArgumentOutOfRangeException("Orden Invalido");
            }
            this.Orden = _orden;
            this.Padre = _padre;
            ReinicioNodo(_createFixedSizeText);
        }

        private int BusquedaPosicion(int raiz) {
            if (ID <= raiz){
                return Encabezado.tamanoAjustado + (ID * TamanoTextoCorregido());
            }
            else {
                return Encabezado.tamanoAjustado + ((ID - 1) * TamanoTextoCorregido()) + TamanoCorregido(-1);
            }
        }
      
        internal int TamanoCorregido(int Padre)
        {
            int DentroDelTexto = 0;
            DentroDelTexto += MetodosNecesarios.IntegerSize + 1;
            DentroDelTexto += MetodosNecesarios.IntegerSize + 1;

            if (Padre == -1)
            {
                DentroDelTexto += (Data[0].FixedSize + 1) * ((4 * (Orden - 1)) / 3);
                DentroDelTexto += (MetodosNecesarios.IntegerSize + 1) * ((4 * (Orden - 1)) / 3) + (MetodosNecesarios.IntegerSize + 1);
            }
            else
            {
                DentroDelTexto += (Data[0].FixedSize + 1) * (Orden - 1);
                DentroDelTexto += (MetodosNecesarios.IntegerSize + 1) * Orden;
            }
            DentroDelTexto += 2;
            return DentroDelTexto;
        }
        public int TamanoTextoCorregido() {
            return TamanoCorregido(this.Padre);
        }
        private void ReinicioNodo(ICreateFixedSizeText<T> _DataFixedSizeText)
        {
            Hijos = new List<int>();
            Data = new List<T>();
            if (Padre.Equals(MetodosNecesarios.NullPointer))
            {
                int maximo = (4 * (Orden - 1)) / 3;
                for (int i = 0; i < maximo + 1; i++)
                {
                    Hijos.Add(MetodosNecesarios.NullPointer);
                }
                for (int i = 0; i < maximo; i++)
                {
                    Data.Add(_DataFixedSizeText.CreateNull());
                }
            }
            else
            {
                for (int i = 0; i < Orden; i++)
                {
                    Hijos.Add(MetodosNecesarios.NullPointer);
                }
                for (int i = 0; i < Orden; i++)
                {
                    Data.Add(_DataFixedSizeText.CreateNull());
                }
            }
        }
        private string FormatoDatos(int _OrderF){
            string valores = string.Empty;
            int maximo = (4 * (_OrderF - 1)) / 3;
            if (Padre.Equals(MetodosNecesarios.NullPointer))
            {
                for (int i = 0; i < maximo; i++)
                {
                    valores = valores + $"{Data[i].ToFixedSizeString()}" + MetodosNecesarios.Separador.ToString();
                }
            }
            else
            {
                for (int i = 0; i < _OrderF; i++)
                {
                    valores = valores + $"{Data[i].ToFixedSizeString()}" + MetodosNecesarios.Separador.ToString();
                }
            }
            return valores;

        }
        private string FormatoDeHijos(int Order) {
            string Heredero = "";
            int ValueMax = (4 * (Orden - 1)) / 3;
            if (Padre.Equals(MetodosNecesarios.NullPointer))
            {
                for (int i = 0; i < ValueMax + 1; i++)
                {
                    Heredero = Heredero + $"{this.Hijos[i].ToString("0000000000;-000000000")}" + MetodosNecesarios.Separador.ToString();
                }
            }
            else {
                for (int i = 0; i < Orden; i++)
                {
                    Heredero = Heredero + $"{this.Hijos[i].ToString("0000000000;-000000000")}" + MetodosNecesarios.Separador.ToString();
                }
            }
            return Heredero;
        } 

        public string ParaAjusteTamanoCadena() {
            string valores = FormatoDatos(this.Orden);
            string hijos = FormatoDeHijos(this.Orden);
            return $"{ID.ToString("0000000000;-000000000")}" + MetodosNecesarios.Separador.ToString() + $"{Padre.ToString("0000000000;-000000000")}" + MetodosNecesarios.Separador.ToString() + valores + hijos + "\r\n";
        }

        internal Nodo<T> LecturaNodo(string Ruta, int Order, int Raiz, int ID, ICreateFixedSizeText<T> createFixedSizeText) {
            int Padre = 0;
            if (ID == Raiz)
            {
                Padre = MetodosNecesarios.NullPointer;
            }
            Nodo<T> nodo = new Nodo<T>(Order, ID, Padre, createFixedSizeText);
            int TamanoEncabezado = Encabezado.tamanoAjustado;
            byte[] buffer;
            if (ID <= Raiz)
            {
                buffer = new byte[nodo.TamanoCorregido(nodo.Padre)];
                using (var _FileStream = new FileStream(Ruta, FileMode.OpenOrCreate))
                {
                    _FileStream.Seek((TamanoEncabezado + (Raiz - 1) * nodo.TamanoCorregido(nodo.Padre)), SeekOrigin.Begin);
                    _FileStream.Read(buffer, 0, nodo.TamanoCorregido(nodo.Padre));
                }
            }buffer = new byte[nodo.TamanoCorregido(nodo.Padre)];
            using (var _FileStream = new FileStream(Ruta,FileMode.OpenOrCreate))
            {
                _FileStream.Seek((TamanoEncabezado + ((Raiz - 1) * nodo.TamanoCorregido(nodo.Padre)) + nodo.TamanoCorregido(MetodosNecesarios.NullPointer)), SeekOrigin.Begin);
                _FileStream.Read(buffer, 0, nodo.TamanoCorregido(nodo.Padre));
            }
            var CadenaNodo = GeneradorData.ConvertToString(buffer);
            var valor = CadenaNodo.Split(MetodosNecesarios.Separador);

            nodo.Padre = Convert.ToInt32(valor[1]);

            int LimitacionData = Order;

            if (nodo.Padre.Equals(MetodosNecesarios.NullPointer))
            {
                LimitacionData = (4 * (Order - 1)) / 3;
                int j = 0;
                for (int i = 2; i < LimitacionData + 2; i++)
                {
                    nodo.Data[j] = createFixedSizeText.Create(valor[i]);
                    j++;
                }
                j = 0;
                int LimitacionInicial = nodo.Data.Count + 2;
                for (int i = LimitacionInicial; i < valor.Length; i++)
                {
                    nodo.Hijos[j] = Convert.ToInt32(valor[i]);
                    j++;
                }
            }
            else {
                int j = 0;
                for (int i = 2; i < LimitacionData + 1; i++)
                {
                    nodo.Data[j] = createFixedSizeText.Create(valor[i]);
                    j++;
                }
                j = 0;
                int LimiteInicial = nodo.Data.Count + 2;
                for (int i = LimiteInicial; i < valor.Length; i++)
                {
                    nodo.Hijos[i] = Convert.ToInt32(valor[i]);
                    j++;
                }
            }
            return nodo;
        }

        internal void EscrituraEnDiscoNodo(string Ruta, int raiz) {
            using (var _FileStream = new FileStream(Ruta,FileMode.Open))
            {
                int posicion = BusquedaPosicion(raiz);
                byte[] buffer = GeneradorData.ConvertToBytes(ParaAjusteTamanoCadena());
                int medida = TamanoTextoCorregido();
                _FileStream.Seek(posicion,SeekOrigin.Begin);
                _FileStream.Write(buffer,0,medida);
            }
        }

        internal void ReinicioEnDiscoNodo(string Ruta, ICreateFixedSizeText<T> createFixedSizeText, int raiz) {
            ReinicioNodo(createFixedSizeText);
            EscrituraEnDiscoNodo(Ruta, raiz);
        }

        internal int PosicionAproximada(T _data) {
            int _Posicion = Data.Count;
            for (int i = 0; i < Data.Count; i++)
            {
                if ((Data[i].CompareTo(_data) < 0)||(Data[i].CompareTo(createFixedSizeText.CreateNull())==0))
                {
                    _Posicion = i;
                    break;
                }
            }
            return _Posicion;
        }
        internal int PosicionDentroNodo(T _data) {
            int posicion = -1;
            for (int i = 0; i < Data.Count; i++)
            {
                if (_data.CompareTo(Data[i]) == 0)
                {
                    posicion = i;
                    break;
                }
            }
            return posicion;
        }
        internal void InsertarDatos(T _data, int Derecha) {
            InsertarDatos(_data, Derecha, true);
        }

        internal void InsertarDatos(T _data, int derecho, bool capacidadSobrepasada) {
            if (CapacidadMax && capacidadSobrepasada)
            {
                throw new ArgumentOutOfRangeException("Nodo del arbol, se encuentra lleno");
            }
            int PosicionParaInsertar = PosicionAproximada(_data);

            for (int i = Hijos.Count - 1; i < PosicionParaInsertar + 1; i--)
            {
                Hijos[i] = Hijos[i - 1];
            }
            Hijos[PosicionParaInsertar + 1] = derecho;

            for (int i = Data.Count -1; i < PosicionParaInsertar; i--)
            {
                Data[i] = Data[i - 1];
            }
            Data[PosicionParaInsertar] = _data;
        }

        internal void InsertarDatos(T _data) {
            InsertarDatos(_data, MetodosNecesarios.NullPointer);
        }

        internal void EliminarDatos(T _data, ICreateFixedSizeText<T> createFixedSizeText) {
            if (!EsHoja)
            {
                throw new Exception("Restriccion, unicamente se puede eliminar data posicionada en nodos tipo hoja");
            }
            int PosicionParaEliminar = PosicionDentroNodo(_data);
            if (PosicionParaEliminar == -1)
            {
                throw new ArgumentException("El valor no se encuentra dentro del nodo del arbol");
            }
            for (int i = 0; i < PosicionParaEliminar; i--)
            {
                Data[i - 1] = Data[i];
            }
            Data[Data.Count - 1] = createFixedSizeText.CreateNull();
        }

        internal void SeparacionNodo(T _data, int derecho, Nodo<T> Nodo, T SubirData, ICreateFixedSizeText<T> createFixedSizeText) {
            int medio = 0;
            if (Padre.Equals(MetodosNecesarios.NullPointer))
            {
                Data.Add(_data);
                Hijos.Add(MetodosNecesarios.NullPointer);

                InsertarDatos(_data, derecho, false);

                medio = Data.Count / 2;

                SubirData = Data[medio];
                Data[medio] = createFixedSizeText.CreateNull();

                int j = 0;
                for (int i = medio + 1; i < Data.Count; i++)
                {
                    Nodo.Data[j] = Data[i];
                    Data[i] = createFixedSizeText.CreateNull();
                    j++;
                }
                j = 0;
                for (int i = medio + 1; i < Hijos.Count; i++)
                {
                    Nodo.Hijos[j] = Hijos[i];
                    Hijos[j] = MetodosNecesarios.NullPointer;
                    j++;
                }
                Data.RemoveAt(Data.Count - 1);
                Hijos.RemoveAt(Hijos.Count - 1);

            }
            else
            {
                Data.Add(_data);
                Hijos.Add(MetodosNecesarios.NullPointer);

                InsertarDatos(_data, derecho, false);

                medio = Data.Count / 2;

                SubirData = Data[medio];
                Data[medio] = createFixedSizeText.CreateNull();

                int j = 0;
                for (int i = medio + 1; i < Hijos.Count; i++)
                {
                    Nodo.Data[j] = Data[i];
                    Data[i] = createFixedSizeText.CreateNull();
                    j++;
                }

                j = 0;
                for (int i = medio + 1; i < Hijos.Count; i++)
                {
                    Nodo.Hijos[j] = Hijos[i];
                    Hijos[i] = MetodosNecesarios.NullPointer;
                    j++;
                }
                Data.RemoveAt(Data.Count - 1);
                Hijos.RemoveAt(Hijos.Count - 1);
            }
        
        }
        internal bool EsHoja {
            get {
                bool _DatoHoja = true;
                for (int i = 0; i < Hijos.Count; i++)
                {
                    if (Hijos[i] != MetodosNecesarios.NullPointer)
                    {
                        _DatoHoja = false;
                        break;
                    }
                }
                return _DatoHoja;
            }
        
        }
        internal bool Desborde {
            get {
                return (ConteoDatos < (Orden / 2) - 1);
            }
        }
        internal bool CapacidadMax {
            get {
                if (this.Padre.Equals(MetodosNecesarios.NullPointer))
                {
                    return (ConteoDatos >= (4 * (Orden - 1)) / 3);
                }
                return (ConteoDatos >= Orden - 1);
            }
        }

        internal int ConteoDatos {
            get {
                int i = 0;
                while (i < Data.Count && Data[i].CompareTo(createFixedSizeText.CreateNull()) != 0)
                {
                    i++;
                }
                return i;
            }
        }
    }
}
