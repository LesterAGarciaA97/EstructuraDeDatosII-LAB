using PROJED2SEGUNDA.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PROJED2SEGUNDA.BtreeStar
{
    public class ArbolSucursal_Producto
    {
        public NodoSucursal_Producto NodoRaiz;
        private int TamanoMaximo;
        public List<Sucursal_Producto> ContenidoArbol = new List<Sucursal_Producto>();
        public int Contador = 0;
        public ArbolSucursal_Producto(int TamanoMaximo)
        {
            if (TamanoMaximo < 8)
            {
                throw new Exception("Se debe mantener el arbol con el grado mayor a 5");
            }
            this.TamanoMaximo = TamanoMaximo;
            NodoRaiz = new NodoSucursal_Producto(TamanoMaximo + 1);
            NodoRaiz.esNodoHoja = true;
        }
        public void InsertarSucursal_Producto(Sucursal_Producto _NSucursal_Producto)
        {
            InsertarNodoSucursal_Producto(NodoRaiz, _NSucursal_Producto);
        }
        private void InsertarNodoSucursal_Producto(NodoSucursal_Producto Padre, Sucursal_Producto _NDato)
        {
            int Index = Padre.Tamano - 1;
            if (Padre.Tamano > 0)
            {
                while (Index >= 0 && _NDato.IdSucursal < Padre.LlavesNodos[Index].IdSucursal)
                {
                    Index--;
                }
            }
            if (NodoRaiz.estaCapacidadMax)
            {
                PartirRaiz(Padre);
                InsertarNodoSucursal_Producto(NodoRaiz, _NDato);
            }
            else if (NodoRaiz.esNodoHoja)
            {
                InsertarNoLleno(Padre, _NDato);
                Contador++;
                if (Contador == TamanoMaximo)
                {
                    Contador = 0;
                    NodoRaiz.estaCapacidadMax = true;
                }
            }
            else if (Padre.Hijos[Index] != null && Padre.Hijos[Index].esNodoHoja)
            {
                InsertarEnHoja(Padre, Index, _NDato);
            }
            else
            {
                InsertarNodoSucursal_Producto(Padre.Hijos[Index], _NDato);
            }
        }
        private void InsertarEnHoja(NodoSucursal_Producto Padre, int Index, Sucursal_Producto _Dato)
        {
            if (Padre.Hijos[Index].estaCapacidadMax)
            {
                if (Padre.Hijos[Index + 1] != null)
                {
                    if (Padre.Hijos[Index + 1] != null)
                    {
                        PartirEnDosTres(Padre, Index, _Dato, false);
                    }
                    else
                    {
                        RotacionDerecha(Padre, Index, _Dato);
                    }
                }
                else if (Padre.Hijos[Index - 1] != null)
                {
                    if (Padre.Hijos[Index - 1].estaCapacidadMax)
                    {
                        PartirEnDosTres(Padre, Index, _Dato, false);
                    }
                    else
                    {
                        RotacionIzquierda(Padre, Index, _Dato);
                    }
                }
            }
            else
            {
                InsertarNoLleno(Padre.Hijos[Index], _Dato);
            }
        }
        private void InsertarNoLleno(NodoSucursal_Producto NodoActual, Sucursal_Producto _Dato)
        {
            var i = NodoActual.Tamano - 1;
            var m = NodoActual.Tamano;
            if (!NodoActual.esNodoHoja)
            {
                while (i >= 0 && _Dato.IdSucursal < NodoActual.LlavesNodos[i].IdSucursal)
                {
                    NodoActual.LlavesNodos[i + 1] = NodoActual.LlavesNodos[i];
                    NodoActual.Hijos[i + 2] = NodoActual.Hijos[i + 1];
                    i--;
                }
            }
            else
            {
                while (i >= 0 && _Dato.IdSucursal < NodoActual.LlavesNodos[i].IdSucursal)
                {
                    NodoActual.LlavesNodos[i + 1] = NodoActual.LlavesNodos[i];
                    i--;
                }
            }
            NodoActual.LlavesNodos[i + 1] = _Dato;
            NodoActual.Tamano++;

            if (!NodoActual.esNodoHoja)
            {
                var _x = i + 1;
                var _c = i + 2;
                NodoActual.Hijos[_c] = NodoActual.Hijos[_x];
                NodoActual.Hijos[_x] = new NodoSucursal_Producto(TamanoMaximo);
                int j = 0;
                for (; NodoActual.Hijos[_c].LlavesNodos[j].IdSucursal < _Dato.IdSucursal; j++)
                {
                    NodoActual.Hijos[_x].LlavesNodos[j] = NodoActual.Hijos[_c].LlavesNodos[j];
                    if (!NodoActual.Hijos[_c].esNodoHoja)
                    {
                        NodoActual.Hijos[_x].Hijos[j] = NodoActual.Hijos[_c].Hijos[j];
                    }
                    NodoActual.Hijos[_c].Tamano--;
                    NodoActual.Hijos[_x].Tamano++;
                }
                int _k;
                for (_k = j; _k < m; _k++)
                {
                    NodoActual.Hijos[_c].LlavesNodos[_k - j] = NodoActual.Hijos[_c].LlavesNodos[_k];
                    NodoActual.Hijos[_c].LlavesNodos[_k] = null;
                    if (!NodoActual.Hijos[_c].esNodoHoja)
                    {
                        NodoActual.Hijos[_c].Hijos[_k - j] = NodoActual.Hijos[_c].Hijos[_k];
                        NodoActual.Hijos[_c].Hijos[_k] = null;
                    }
                }
                if (!NodoActual.Hijos[_c].esNodoHoja)
                {
                    NodoActual.Hijos[_c].Hijos[NodoActual.Hijos[_c].Tamano] = NodoActual.Hijos[_c].Hijos[_k - 1];
                }
                if (NodoActual.Tamano == TamanoMaximo - 1)
                {
                    NodoActual.estaCapacidadMax = true;
                }
            }
        }
        private void RotacionDerecha(NodoSucursal_Producto Padre, int Id_Hijo, Sucursal_Producto _NDato)
        {
            int llaveDelId_Padre;
            int TamanoNodoSucursal_Producto = Padre.Hijos[Id_Hijo].Tamano;
            if (Id_Hijo == 0)
            {
                llaveDelId_Padre = 0;
            }
            else if (Id_Hijo == TamanoNodoSucursal_Producto)
            {
                llaveDelId_Padre = TamanoNodoSucursal_Producto - 1;
            }
            else
            {
                llaveDelId_Padre = Id_Hijo;
            }

            int TamanoNodoSucursal_ProductoHermano = Padre.Hijos[Id_Hijo + 1].Tamano;
            for (int j = TamanoNodoSucursal_ProductoHermano - 1; j >= 0; j--)
            {
                Padre.Hijos[Id_Hijo + 1].LlavesNodos[j + 1] = Padre.Hijos[Id_Hijo + 1].LlavesNodos[j];
            }
            if (!Padre.Hijos[Id_Hijo].esNodoHoja)
            {
                for (int j = TamanoNodoSucursal_ProductoHermano; j >= 0; j--)
                {
                    Padre.Hijos[Id_Hijo + 1].Hijos[0] = Padre.Hijos[Id_Hijo + 1].Hijos[j];

                    Padre.Hijos[Id_Hijo + 1].Hijos[0] = Padre.Hijos[Id_Hijo].Hijos[TamanoNodoSucursal_ProductoHermano];
                    Padre.Hijos[Id_Hijo].Hijos[TamanoNodoSucursal_ProductoHermano] = null;
                }
            }
            Padre.Hijos[Id_Hijo + 1].LlavesNodos[0] = Padre.LlavesNodos[llaveDelId_Padre];
            Padre.Hijos[Id_Hijo + 1].Tamano++;
            if (Padre.Hijos[Id_Hijo].esNodoHoja && _NDato.IdSucursal < Padre.LlavesNodos[llaveDelId_Padre].IdSucursal && _NDato.IdSucursal > Padre.Hijos[Id_Hijo].LlavesNodos[TamanoNodoSucursal_Producto - 1].IdSucursal)
            {
                Padre.LlavesNodos[llaveDelId_Padre] = _NDato;
            }
            else
            {
                Padre.LlavesNodos[llaveDelId_Padre] = Padre.Hijos[Id_Hijo].LlavesNodos[TamanoNodoSucursal_Producto - 1];
                Padre.Hijos[Id_Hijo].LlavesNodos[TamanoNodoSucursal_Producto - 1] = null;
                Padre.Hijos[Id_Hijo].Tamano--;
                InsertarNoLleno(Padre.Hijos[Id_Hijo], _NDato);
            }
            if (Padre.Hijos[Id_Hijo + 1].Tamano == TamanoNodoSucursal_Producto)
            {
                Padre.Hijos[Id_Hijo + 1].estaCapacidadMax = true;
            }
        }
        private void RotacionIzquierda(NodoSucursal_Producto Padre, int IndiceHijo, Sucursal_Producto _NDato)
        {
            int llaveIndexPadre;

            if (IndiceHijo == 0)
                llaveIndexPadre = 0;
            else if (IndiceHijo == TamanoMaximo)
                llaveIndexPadre = TamanoMaximo - 1;
            else
                llaveIndexPadre = IndiceHijo - 1;


            InsertarNoLleno(Padre.Hijos[IndiceHijo - 1], Padre.LlavesNodos[llaveIndexPadre]);
            if (Padre.Hijos[IndiceHijo].esNodoHoja && _NDato.IdSucursal > Padre.LlavesNodos[llaveIndexPadre].IdSucursal && _NDato.IdSucursal < Padre.Hijos[IndiceHijo].LlavesNodos[0].IdSucursal)
            {
                Padre.LlavesNodos[llaveIndexPadre] = _NDato;
            }
            else
            {
                Padre.LlavesNodos[llaveIndexPadre] = Padre.Hijos[IndiceHijo].LlavesNodos[0];

                int tamañoNodoP = Padre.Hijos[IndiceHijo].Tamano;
                for (int i = 0; i < tamañoNodoP - 1; i++)
                {
                    Padre.Hijos[IndiceHijo].LlavesNodos[i] = Padre.Hijos[IndiceHijo].LlavesNodos[i + 1];
                }
                if (!Padre.Hijos[IndiceHijo].esNodoHoja)
                {
                    Padre.Hijos[IndiceHijo].Hijos[tamañoNodoP] = Padre.Hijos[IndiceHijo].Hijos[0];
                    for (int i = 0; i < tamañoNodoP - 1; i++)
                    {
                        Padre.Hijos[IndiceHijo].Hijos[i] = Padre.Hijos[IndiceHijo].Hijos[i + 1];
                    }
                }

                Padre.Hijos[IndiceHijo].LlavesNodos[tamañoNodoP - 1] = null;

                Padre.Hijos[IndiceHijo].Tamano--;
                Padre.Hijos[IndiceHijo].estaCapacidadMax = false;

                InsertarNoLleno(Padre.Hijos[IndiceHijo], _NDato);
            }
        }
        private void PartirEnDosTres(NodoSucursal_Producto Padre, int IndiceHijoCapacidadMaxima, Sucursal_Producto _NDato, bool EsHermanoIzq)
        {
            int IndiceLadoIzquierdo = EsHermanoIzq ? (IndiceHijoCapacidadMaxima - 1) : IndiceHijoCapacidadMaxima;
            int IndiceLadoDerecho = EsHermanoIzq ? (IndiceHijoCapacidadMaxima) : IndiceHijoCapacidadMaxima + 1;
            int TamanoDivision = (TamanoMaximo * 2) / 3;


            int IndiceLlavePadre = EsHermanoIzq ? (IndiceHijoCapacidadMaxima - 1) : (IndiceHijoCapacidadMaxima);
            int TamanoMaximoNodoSucursal_Producto = TamanoMaximo - 1;
            bool esNodoHoja = Padre.Hijos[IndiceHijoCapacidadMaxima].esNodoHoja;
            int _j = 0;
            var arregloP = new Sucursal_Producto[TamanoMaximo * 2];
            var hijosP = new NodoSucursal_Producto[TamanoMaximo * 2];
            for (int i = 0; i < TamanoMaximoNodoSucursal_Producto; i++)
            {
                arregloP[_j++] = Padre.Hijos[IndiceLadoIzquierdo].LlavesNodos[i];
                arregloP[_j++] = Padre.Hijos[IndiceLadoDerecho].LlavesNodos[i];
            }
            arregloP[_j++] = _NDato;
            arregloP[_j++] = Padre.LlavesNodos[IndiceLlavePadre];
            if (!esNodoHoja)
            {
                int _x = 0;
                for (int i = 0; i < TamanoMaximo; i++)
                {
                    hijosP[_x++] = Padre.Hijos[IndiceLadoIzquierdo].Hijos[i];
                }
                for (int i = 0; i < TamanoMaximo; i++)
                {
                    hijosP[_x++] = Padre.Hijos[IndiceLadoDerecho].Hijos[i];
                }
            }
            _j = 0;
            int k;

            for (k = 0; k < TamanoDivision; k++)
            {
                Padre.Hijos[IndiceLadoIzquierdo].LlavesNodos[k] = arregloP[_j++];
            }
            while (k < TamanoMaximoNodoSucursal_Producto)
            {
                Padre.Hijos[IndiceLadoIzquierdo].LlavesNodos[k++] = null;
                Padre.Hijos[IndiceLadoIzquierdo].Tamano--;
            }
            Padre.Hijos[IndiceLadoIzquierdo].estaCapacidadMax = false;

            Sucursal_Producto PadreN = arregloP[_j++];
            Padre.LlavesNodos[IndiceLlavePadre] = PadreN;

            for (k = 0; k < TamanoDivision; k++)
            {
                Padre.Hijos[IndiceLadoDerecho].LlavesNodos[k] = arregloP[_j++];
            }
            while (k < TamanoMaximoNodoSucursal_Producto)
            {
                Padre.Hijos[IndiceLadoDerecho].LlavesNodos[k++] = null;
                Padre.Hijos[IndiceLadoDerecho].Tamano--;
            }
            Padre.Hijos[IndiceLadoDerecho].estaCapacidadMax = false;

            Sucursal_Producto Padre2N = arregloP[_j++];
            var NodoNuevoSucursal_Producto = new NodoSucursal_Producto(TamanoMaximo);

            for (k = 0; _j < TamanoMaximo * 2; k++)
            {
                NodoNuevoSucursal_Producto.LlavesNodos[k] = arregloP[_j++];
                NodoNuevoSucursal_Producto.Tamano++;
            }
            var NuevoNodoDerecho = Padre.Hijos[IndiceLadoDerecho];
            if (Padre.estaCapacidadMax)
            {
                if (Padre.Padre == null)
                {
                    PartirRaiz(Padre);
                }
                else
                {
                    InsertarEnHoja(Padre.Padre, Padre.IndiceHijoPadre, Padre2N);
                }
            }
            else
            {
                int m = NuevoNodoDerecho.Padre.Tamano - 1;
                for (; m < IndiceLlavePadre; m--)
                {
                    NuevoNodoDerecho.Padre.Hijos[m + 1] = NuevoNodoDerecho.Padre.Hijos[m + 1];
                }
                NuevoNodoDerecho.Padre.LlavesNodos[m + 1] = Padre2N;
                NuevoNodoDerecho.Padre.Hijos[m + 2] = NodoNuevoSucursal_Producto;
                NuevoNodoDerecho.Padre.Tamano++;
                NodoNuevoSucursal_Producto.Padre = NuevoNodoDerecho.Padre;
                NodoNuevoSucursal_Producto.esNodoHoja = esNodoHoja;
            }
            if (NuevoNodoDerecho.Padre.Tamano == TamanoMaximo - 1)
            {
                NuevoNodoDerecho.Padre.estaCapacidadMax = true;
            }
        }
        private void PartirRaiz(NodoSucursal_Producto NodoActual)
        {
            int IndiceMedio = NodoActual.Tamano / 2;
            var RaizNueva = new NodoSucursal_Producto(TamanoMaximo);
            var NodoSucursal_ProductoNuevo = new NodoSucursal_Producto(TamanoMaximo);
            int j = 0;
            int i = IndiceMedio + 1;
            if (!NodoActual.esNodoHoja)
            {
                while (i < TamanoMaximo - 1)
                {
                    NodoSucursal_ProductoNuevo.LlavesNodos[j] = NodoActual.LlavesNodos[i];
                    NodoSucursal_ProductoNuevo.Hijos[j] = NodoActual.Hijos[i];
                    NodoSucursal_ProductoNuevo.Hijos[j].Padre = NodoSucursal_ProductoNuevo;

                    NodoActual.LlavesNodos[i] = null;
                    NodoActual.Hijos[i] = null;
                    NodoActual.Tamano--;
                    j++;
                    i++;
                }
                NodoSucursal_ProductoNuevo.Hijos[j] = NodoActual.Hijos[i];
                NodoActual.Hijos[i] = null;
            }
            else
            {
                while (i < TamanoMaximo - 1)
                {
                    NodoSucursal_ProductoNuevo.LlavesNodos[j] = NodoActual.LlavesNodos[i];
                    NodoActual.LlavesNodos[i] = null;
                    NodoActual.Tamano--;
                    NodoSucursal_ProductoNuevo.Tamano++;
                    i++;
                    j++;
                }
                NodoSucursal_ProductoNuevo.esNodoHoja = true;
                NodoActual.esNodoHoja = true;
            }
            NodoActual.Padre = RaizNueva;
            NodoActual.IndiceHijoPadre = 0;

            NodoSucursal_ProductoNuevo.Padre = NodoRaiz;
            NodoSucursal_ProductoNuevo.IndiceHijoPadre = 1;
            RaizNueva.Padre = null;
            RaizNueva.LlavesNodos[0] = NodoActual.LlavesNodos[IndiceMedio];
            RaizNueva.Hijos[0] = NodoActual;
            RaizNueva.Hijos[1] = NodoSucursal_ProductoNuevo;
            RaizNueva.Tamano++;
            NodoActual.LlavesNodos[IndiceMedio] = null;
            NodoActual.Tamano--;
            NodoActual.estaCapacidadMax = false;
            this.NodoRaiz = RaizNueva;
        }
        public bool BusquedaNodo(int Id_NodoSucursal, int Id_NodoProducto)
        {
            Sucursal_Producto NodoResultante = BusquedaNodoComparar(NodoRaiz, Id_NodoSucursal, Id_NodoProducto);
            if (NodoResultante != null)
            {
                return true;
            }
            return false;
        }
        private Sucursal_Producto BusquedaNodoComparar(NodoSucursal_Producto NodoActual, int ID_NodoSucursal , int ID_NodoProducto)
        {
            NodoSucursal_Producto Temp = NodoActual;
            int i = 0;
            while (i <= NodoActual.Tamano - 1 && ID_NodoSucursal > NodoActual.LlavesNodos[i].IdSucursal)
            {
                i++;
            }
            if (i <= NodoActual.Tamano - 1 && ID_NodoSucursal == NodoActual.LlavesNodos[i].IdSucursal && ID_NodoProducto == NodoActual.LlavesNodos[i].IDProducto)
            {
                return NodoActual.LlavesNodos[i];
            }
            else if (NodoActual.esNodoHoja)
            {
                return null;
            }
            else
            {
                return BusquedaNodoComparar(NodoActual.Hijos[i], ID_NodoSucursal , ID_NodoProducto);
            }
        }
        public List<Sucursal_Producto> DevolverTodo()
        {
            var _EnListarDatos = ConvertirALista(NodoRaiz);
            return _EnListarDatos;
        }
        private List<Sucursal_Producto> ConvertirALista(NodoSucursal_Producto _NodoCompleto)
        {
            var NodosEnlistados = new List<Sucursal_Producto>();
            if (!_NodoCompleto.esNodoHoja)
            {
                for (int i = 0; _NodoCompleto.Hijos[i] != null; i++)
                {
                    ConvertirALista(_NodoCompleto.Hijos[i]);
                }
            }
            for (int j = 0; j < _NodoCompleto.LlavesNodos.Length; j++)
            {
                NodosEnlistados.Add(_NodoCompleto.LlavesNodos[j]);
            }
            return NodosEnlistados;
        }
    }
}
