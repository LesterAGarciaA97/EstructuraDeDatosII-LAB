using PROJED2SEGUNDA.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PROJED2SEGUNDA.BtreeStar
{
    public class ArbolSucursal
    {
        public NodoSucursal NodoRaiz;
        private int TamanoMaximo;
        public List<Sucursal> ContenidoArbol = new List<Sucursal>();
        public int Contador = 0;
        public ArbolSucursal(int TamanoMaximo)
        {
            if (TamanoMaximo < 8)
            {
                throw new Exception("Se debe mantener el arbol con el grado mayor a 5");
            }
            this.TamanoMaximo = TamanoMaximo;
            NodoRaiz = new NodoSucursal(TamanoMaximo + 1);
            NodoRaiz.esNodoHoja = true;
        }
        public void InsertarSucursal(Sucursal _NSucursal)
        {
            InsertarNodoSucursal(NodoRaiz, _NSucursal);
        }
        private void InsertarNodoSucursal(NodoSucursal Padre, Sucursal _NDato)
        {
            int Index = Padre.Tamano - 1;
            if (Padre.Tamano > 0)
            {
                while (Index >= 0 && _NDato.Id < Padre.LlavesNodos[Index].Id)
                {
                    Index--;
                }
            }
            if (NodoRaiz.estaCapacidadMax)
            {
                PartirRaiz(Padre);
                InsertarNodoSucursal(NodoRaiz, _NDato);
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
                InsertarNodoSucursal(Padre.Hijos[Index], _NDato);
            }
        }
        private void InsertarEnHoja(NodoSucursal Padre, int Index, Sucursal _Dato)
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
        private void InsertarNoLleno(NodoSucursal NodoActual, Sucursal _Dato)
        {
            var i = NodoActual.Tamano - 1;
            var m = NodoActual.Tamano;
            if (!NodoActual.esNodoHoja)
            {
                while (i >= 0 && _Dato.Id < NodoActual.LlavesNodos[i].Id)
                {
                    NodoActual.LlavesNodos[i + 1] = NodoActual.LlavesNodos[i];
                    NodoActual.Hijos[i + 2] = NodoActual.Hijos[i + 1];
                    i--;
                }
            }
            else
            {
                while (i >= 0 && _Dato.Id < NodoActual.LlavesNodos[i].Id)
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
                NodoActual.Hijos[_x] = new NodoSucursal(TamanoMaximo);
                int j = 0;
                for (; NodoActual.Hijos[_c].LlavesNodos[j].Id < _Dato.Id; j++)
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
        private void RotacionDerecha(NodoSucursal Padre, int Id_Hijo, Sucursal _NDato)
        {
            int llaveDelId_Padre;
            int TamanoNodoSucursal = Padre.Hijos[Id_Hijo].Tamano;
            if (Id_Hijo == 0)
            {
                llaveDelId_Padre = 0;
            }
            else if (Id_Hijo == TamanoNodoSucursal)
            {
                llaveDelId_Padre = TamanoNodoSucursal - 1;
            }
            else
            {
                llaveDelId_Padre = Id_Hijo;
            }

            int TamanoNodoSucursalHermano = Padre.Hijos[Id_Hijo + 1].Tamano;
            for (int j = TamanoNodoSucursalHermano - 1; j >= 0; j--)
            {
                Padre.Hijos[Id_Hijo + 1].LlavesNodos[j + 1] = Padre.Hijos[Id_Hijo + 1].LlavesNodos[j];
            }
            if (!Padre.Hijos[Id_Hijo].esNodoHoja)
            {
                for (int j = TamanoNodoSucursalHermano; j >= 0; j--)
                {
                    Padre.Hijos[Id_Hijo + 1].Hijos[0] = Padre.Hijos[Id_Hijo + 1].Hijos[j];

                    Padre.Hijos[Id_Hijo + 1].Hijos[0] = Padre.Hijos[Id_Hijo].Hijos[TamanoNodoSucursalHermano];
                    Padre.Hijos[Id_Hijo].Hijos[TamanoNodoSucursalHermano] = null;
                }
            }
            Padre.Hijos[Id_Hijo + 1].LlavesNodos[0] = Padre.LlavesNodos[llaveDelId_Padre];
            Padre.Hijos[Id_Hijo + 1].Tamano++;
            if (Padre.Hijos[Id_Hijo].esNodoHoja && _NDato.Id < Padre.LlavesNodos[llaveDelId_Padre].Id && _NDato.Id > Padre.Hijos[Id_Hijo].LlavesNodos[TamanoNodoSucursal - 1].Id)
            {
                Padre.LlavesNodos[llaveDelId_Padre] = _NDato;
            }
            else
            {
                Padre.LlavesNodos[llaveDelId_Padre] = Padre.Hijos[Id_Hijo].LlavesNodos[TamanoNodoSucursal - 1];
                Padre.Hijos[Id_Hijo].LlavesNodos[TamanoNodoSucursal - 1] = null;
                Padre.Hijos[Id_Hijo].Tamano--;
                InsertarNoLleno(Padre.Hijos[Id_Hijo], _NDato);
            }
            if (Padre.Hijos[Id_Hijo + 1].Tamano == TamanoNodoSucursal)
            {
                Padre.Hijos[Id_Hijo + 1].estaCapacidadMax = true;
            }
        }
        private void RotacionIzquierda(NodoSucursal Padre, int IndiceHijo, Sucursal _NDato)
        {
            int llaveIndexPadre;

            if (IndiceHijo == 0)
                llaveIndexPadre = 0;
            else if (IndiceHijo == TamanoMaximo)
                llaveIndexPadre = TamanoMaximo - 1;
            else
                llaveIndexPadre = IndiceHijo - 1;


            InsertarNoLleno(Padre.Hijos[IndiceHijo - 1], Padre.LlavesNodos[llaveIndexPadre]);
            if (Padre.Hijos[IndiceHijo].esNodoHoja && _NDato.Id > Padre.LlavesNodos[llaveIndexPadre].Id && _NDato.Id < Padre.Hijos[IndiceHijo].LlavesNodos[0].Id)
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
        private void PartirEnDosTres(NodoSucursal Padre, int IndiceHijoCapacidadMaxima, Sucursal _NDato, bool EsHermanoIzq)
        {
            int IndiceLadoIzquierdo = EsHermanoIzq ? (IndiceHijoCapacidadMaxima - 1) : IndiceHijoCapacidadMaxima;
            int IndiceLadoDerecho = EsHermanoIzq ? (IndiceHijoCapacidadMaxima) : IndiceHijoCapacidadMaxima + 1;
            int TamanoDivision = (TamanoMaximo * 2) / 3;


            int IndiceLlavePadre = EsHermanoIzq ? (IndiceHijoCapacidadMaxima - 1) : (IndiceHijoCapacidadMaxima);
            int TamanoMaximoNodoSucursal = TamanoMaximo - 1;
            bool esNodoHoja = Padre.Hijos[IndiceHijoCapacidadMaxima].esNodoHoja;
            int _j = 0;
            var arregloP = new Sucursal[TamanoMaximo * 2];
            var hijosP = new NodoSucursal[TamanoMaximo * 2];
            for (int i = 0; i < TamanoMaximoNodoSucursal; i++)
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
            while (k < TamanoMaximoNodoSucursal)
            {
                Padre.Hijos[IndiceLadoIzquierdo].LlavesNodos[k++] = null;
                Padre.Hijos[IndiceLadoIzquierdo].Tamano--;
            }
            Padre.Hijos[IndiceLadoIzquierdo].estaCapacidadMax = false;

            Sucursal PadreN = arregloP[_j++];
            Padre.LlavesNodos[IndiceLlavePadre] = PadreN;

            for (k = 0; k < TamanoDivision; k++)
            {
                Padre.Hijos[IndiceLadoDerecho].LlavesNodos[k] = arregloP[_j++];
            }
            while (k < TamanoMaximoNodoSucursal)
            {
                Padre.Hijos[IndiceLadoDerecho].LlavesNodos[k++] = null;
                Padre.Hijos[IndiceLadoDerecho].Tamano--;
            }
            Padre.Hijos[IndiceLadoDerecho].estaCapacidadMax = false;

            Sucursal Padre2N = arregloP[_j++];
            var NodoNuevoSucursal = new NodoSucursal(TamanoMaximo);

            for (k = 0; _j < TamanoMaximo * 2; k++)
            {
                NodoNuevoSucursal.LlavesNodos[k] = arregloP[_j++];
                NodoNuevoSucursal.Tamano++;
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
                NuevoNodoDerecho.Padre.Hijos[m + 2] = NodoNuevoSucursal;
                NuevoNodoDerecho.Padre.Tamano++;
                NodoNuevoSucursal.Padre = NuevoNodoDerecho.Padre;
                NodoNuevoSucursal.esNodoHoja = esNodoHoja;
            }
            if (NuevoNodoDerecho.Padre.Tamano == TamanoMaximo - 1)
            {
                NuevoNodoDerecho.Padre.estaCapacidadMax = true;
            }
        }
        private void PartirRaiz(NodoSucursal NodoActual)
        {
            int IndiceMedio = NodoActual.Tamano / 2;
            var RaizNueva = new NodoSucursal(TamanoMaximo);
            var NodoSucursalNuevo = new NodoSucursal(TamanoMaximo);
            int j = 0;
            int i = IndiceMedio + 1;
            if (!NodoActual.esNodoHoja)
            {
                while (i < TamanoMaximo - 1)
                {
                    NodoSucursalNuevo.LlavesNodos[j] = NodoActual.LlavesNodos[i];
                    NodoSucursalNuevo.Hijos[j] = NodoActual.Hijos[i];
                    NodoSucursalNuevo.Hijos[j].Padre = NodoSucursalNuevo;

                    NodoActual.LlavesNodos[i] = null;
                    NodoActual.Hijos[i] = null;
                    NodoActual.Tamano--;
                    j++;
                    i++;
                }
                NodoSucursalNuevo.Hijos[j] = NodoActual.Hijos[i];
                NodoActual.Hijos[i] = null;
            }
            else
            {
                while (i < TamanoMaximo - 1)
                {
                    NodoSucursalNuevo.LlavesNodos[j] = NodoActual.LlavesNodos[i];
                    NodoActual.LlavesNodos[i] = null;
                    NodoActual.Tamano--;
                    NodoSucursalNuevo.Tamano++;
                    i++;
                    j++;
                }
                NodoSucursalNuevo.esNodoHoja = true;
                NodoActual.esNodoHoja = true;
            }
            NodoActual.Padre = RaizNueva;
            NodoActual.IndiceHijoPadre = 0;

            NodoSucursalNuevo.Padre = NodoRaiz;
            NodoSucursalNuevo.IndiceHijoPadre = 1;
            RaizNueva.Padre = null;
            RaizNueva.LlavesNodos[0] = NodoActual.LlavesNodos[IndiceMedio];
            RaizNueva.Hijos[0] = NodoActual;
            RaizNueva.Hijos[1] = NodoSucursalNuevo;
            RaizNueva.Tamano++;
            NodoActual.LlavesNodos[IndiceMedio] = null;
            NodoActual.Tamano--;
            NodoActual.estaCapacidadMax = false;
            this.NodoRaiz = RaizNueva;
        }
        public bool BusquedaNodo(int Id_Nodo)
        {
            Sucursal NodoResultante = BusquedaNodoComparar(NodoRaiz, Id_Nodo);
            if (NodoResultante != null)
            {
                return true;
            }
            return false;
        }
        private Sucursal BusquedaNodoComparar(NodoSucursal NodoActual, int ID_Nodo)
        {
            NodoSucursal Temp = NodoActual;
            int i = 0;
            while (i <= NodoActual.Tamano - 1 && ID_Nodo > NodoActual.LlavesNodos[i].Id)
            {
                i++;
            }
            if (i <= NodoActual.Tamano - 1 && ID_Nodo == NodoActual.LlavesNodos[i].Id)
            {
                return NodoActual.LlavesNodos[i];
            }
            else if (NodoActual.esNodoHoja)
            {
                return null;
            }
            else
            {
                return BusquedaNodoComparar(NodoActual.Hijos[i], ID_Nodo);
            }
        }
        public List<Sucursal> DevolverTodo()
        {
            var _EnListarDatos = ConvertirALista(NodoRaiz);
            return _EnListarDatos;
        }
        private List<Sucursal> ConvertirALista(NodoSucursal _NodoCompleto)
        {
            var NodosEnlistados = new List<Sucursal>();
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
