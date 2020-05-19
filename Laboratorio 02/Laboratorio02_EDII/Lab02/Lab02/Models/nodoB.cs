using System;
using System.Collections.Generic;

namespace Lab02.Models
{
    public class nodoB<TKey, TData> where TKey : IComparable<TKey>
    {
        public List<TKey> nodoLlaves { get; set; }
        public List<TData> datosNodo { get; set; }
        public int posicionPrincipal { get; set; }
        public List<int> Hijos { get; set; }
        public int Padre { get; set; }
        public int Orden { get; set; }
        private int tamanioMaximoLlave;
        private int Count;
        public nodoB(int orden, int posicion, int tamanioMaximo)
        {
            nodoLlaves = new List<TKey>();
            datosNodo = new List<TData>();
            Hijos = new List<int>();
            Orden = orden;
            Count = 0;
            Padre = Generador.hacerNulo();
            posicionPrincipal = posicion;
            tamanioMaximoLlave = tamanioMaximo;
            limpiarHijos();
        }

        public void limpiarHijos()
        {
            if (Hijos.Count != 0)
            {
                for (int i = 0; i < Hijos.Count; i++)
                {
                    Hijos[i] = Generador.hacerNulo();
                }
            }
            else
            {
                for (int i = 0; i < Orden; i++)
                {
                    Hijos.Add(Generador.hacerNulo());
                }
            }
        }
        public int CompareTo(TKey otraLlave, int indice)
        {
            int m = nodoLlaves[indice].CompareTo(otraLlave);
            return m;
        }
        public bool esHoja()
        {
            int esNulo = Generador.hacerNulo();

            for (int i = 0; i < Hijos.Count; i++)
            {
                if (Hijos[i] != esNulo)
                {
                    return false;
                }
            }
            return true;
        }
        public bool estaLleno()
        {
            return datosNodo.Count == Orden - 1;
        }
        public TData obtenerValorIndice(int indice)
        {
            return datosNodo[indice];
        }
        public int obtenerNodoIndice(int indice)
        {
            return Hijos[indice];
        }
        public int obtenerUltimoHijo()
        {
            for (int i = Hijos.Count - 1; i >= 0; i--)
            {
                if (Hijos[i] != Generador.hacerNulo())
                {
                    return Hijos[i];
                }
            }
            return Generador.hacerNulo();
        }
        public void insertarhijoPrimero(int hijo)
        {
            for (int i = Hijos.Count - 1; i > 0; i--)
            {
                Hijos[i] = Hijos[i - 1];
            }
            Hijos[0] = hijo;
        }
        public void insertarHijoUltimo(int hijo)
        {
            for (int i = 0; i < Hijos.Count; i++)
            {
                if (Hijos[i] == Generador.hacerNulo())
                {
                    Hijos[i] = hijo;
                    return;
                }
            }
        }
        public TKey obtenerLlaveIndice(int indice)
        {
            return nodoLlaves[indice];
        }
        //------------------------------------------------------------------
        public void DeleteDataOfKey(TKey llave)
        {
            int indice = nodoLlaves.IndexOf(llave);
            nodoLlaves.Remove(llave);
            datosNodo.RemoveAt(indice);
        }
        public void insertarEnOrden(TKey llave, TData datos, int indice)
        {
            datosNodo.Add(default(TData));
            nodoLlaves.Add(default(TKey));
            for (int i = nodoLlaves.Count - 1; i > indice; i--)
            {
                nodoLlaves[i] = nodoLlaves[i - 1];
                datosNodo[i] = datosNodo[i - 1];
            }
            nodoLlaves[indice] = llave;
            datosNodo[indice] = datos;
        }
        public void insertar(TKey llave, TData datos)
        {
            int posicionPrincipal = posicionAproximada(llave);
            if (posicionPrincipal == nodoLlaves.Count)
            {
                nodoLlaves.Add(llave);
                datosNodo.Add(datos);
            }
            else
            {
                nodoLlaves.Add(default(TKey));
                datosNodo.Add(default(TData));
                for (int i = nodoLlaves.Count - 1; i > posicionPrincipal; i--)
                {
                    nodoLlaves[i] = nodoLlaves[i - 1];
                    datosNodo[i] = datosNodo[i - 1];
                }
                nodoLlaves[posicionPrincipal] = llave;
                datosNodo[posicionPrincipal] = datos;
                FixHijos(posicionPrincipal);
            }
        }
        public bool hijosLlenos()
        {
            for (int i = 0; i < Hijos.Count; i++)
            {
                if (Hijos[i] == Generador.hacerNulo())
                {
                    return false;
                }
            }
            return true;
        }
        public void FixHijos(int posicionAMover)
        {
            if (!hijosLlenos())
            {
                posicionAMover++;
                for (int i = Hijos.Count - 1; i > posicionAMover; i--)
                {
                    Hijos[i] = Hijos[i - 1];
                }
                Hijos[posicionAMover] = Generador.hacerNulo();
                Count++;
            }
        }
        public int posicionAproximada(TKey KeyToCompare)
        {
            for (int i = 0; i < nodoLlaves.Count; i++)
            {
                if (nodoLlaves[i].CompareTo(KeyToCompare) == 1)
                {
                    return i;
                }
            }
            return nodoLlaves.Count;
        }
        public TData nivelSuperior(ref TKey llave)
        {
            TData data = datosNodo[(Orden / 2)];
            datosNodo.Remove(data);
            llave = nodoLlaves[(Orden / 2)];
            nodoLlaves.Remove(llave);
            return data;
        }
        public TData nivelSuperior2(ref TKey llave, ref int indiceEliminado)
        {
            TData data = datosNodo[(Orden / 2)];
            datosNodo.Remove(data);
            llave = nodoLlaves[(Orden / 2)];
            indiceEliminado = nodoLlaves.IndexOf(llave);
            nodoLlaves.Remove(llave);
            return data;
        }
        public void insertarGrupoDeHijos(List<int> grupoHijos, int inicio, int posicionDeInsercion)
        {
            inicio++;
            for (int i = 0; i < grupoHijos.Count; i++)
            {
                Hijos.Add(Generador.hacerNulo());
            }
            int temporal = 0;
            int GroupCount = 0;
            for (int i = inicio; i < Hijos.Count - 1; i++)
            {
                temporal = Hijos[i];
                Hijos[i] = grupoHijos[GroupCount];
                grupoHijos[GroupCount] = temporal;
                if (GroupCount == grupoHijos.Count)
                {
                    GroupCount = 0;
                }
            }
            Hijos[Count - 1] = temporal;
            Hijos[inicio] = posicionDeInsercion;
        }
        public List<TKey> HijosAndValuesToBrother(TKey llaveAComparar, ref List<TData> datos, ref List<int> hijos)
        {
            int condicionPrincipal = nodoLlaves.Count;
            List<TKey> maximos = new List<TKey>();
            for (int i = 0; i < condicionPrincipal; i++)
            {
                if (nodoLlaves[i].CompareTo(llaveAComparar) == 1)
                {
                    maximos.Add(nodoLlaves[i]);
                    datos.Add(datosNodo[i]);
                    Hijos.Add(Hijos[i]);
                    Hijos[i] = Generador.hacerNulo();
                }
            }
            Hijos.Add(Hijos[condicionPrincipal]);
            Hijos[condicionPrincipal] = Generador.hacerNulo();
            for (int i = 0; i < maximos.Count; i++)
            {
                nodoLlaves.Remove(maximos[i]);
                datosNodo.Remove(datos[i]);
            }
            return maximos;
        }
        public List<TKey> hijosyvaloresActuales(TKey llaveAComparar, ref List<TData> datos, ref List<int> hijos)
        {
            int condition = nodoLlaves.Count;
            List<TKey> Maximuns = new List<TKey>();
            for (int i = 0; i < condition; i++)
            {
                if (nodoLlaves[i].CompareTo(llaveAComparar) == 1)
                {
                    Maximuns.Add(nodoLlaves[i]);
                    datos.Add(datosNodo[i]);
                    Hijos.Add(Hijos[i + 1]);
                    Hijos[i + 1] = Generador.hacerNulo();
                }
            }
            for (int i = 0; i < Maximuns.Count; i++)
            {
                nodoLlaves.Remove(Maximuns[i]);
                datosNodo.Remove(datos[i]);
            }
            return Maximuns;
        }
        public void insertarHijos(int posicion, int direccionHijo)
        {
            if (Hijos[posicion] != Generador.hacerNulo())
            {
                for (int i = Hijos.Count - 1; i > posicionPrincipal; i--)
                {
                    Hijos[i] = Hijos[i - 1];
                }
            }
            Hijos[posicionPrincipal] = direccionHijo;
        }
        public void limpiar()
        {
            limpiarHijos();
            datosNodo.Clear();
            nodoLlaves.Clear();
            Padre = Generador.hacerNulo();
        }
    }
}
