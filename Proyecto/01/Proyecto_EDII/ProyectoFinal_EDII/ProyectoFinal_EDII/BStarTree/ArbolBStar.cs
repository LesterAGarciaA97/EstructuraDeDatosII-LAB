using ProyectoFinal_EDII.Interfaces;
using ProyectoFinal_EDII.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography.Xml;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoFinal_EDII.BStarTree
{
    public class ArbolBStar<T> where T : IComparable, IFixedSizeText
    {
        public int Orden { get; set; }
        public int Raiz { get; set; }
        public string Ruta { get; set; }
        public int UltimaPosicion { get; set; }

        public FileStream Archivo { get; set; }

        List<T> ElementosBuscados = new List<T>();

        private ICreateFixedSizeText<T> createFixedSizeText = null;

        public ArbolBStar()
        {

        }
        public ArbolBStar(int _Orden, string _Ruta, ICreateFixedSizeText<T> _createFixedSizeText)
        {
            this.createFixedSizeText = _createFixedSizeText;
            this.Orden = _Orden;
            this.Ruta = _Ruta;
            Nodo<T> Raiz = CreacionDelNodo(Orden);
            Encabezado _Encabezado = CreacionDelEncabezado(Orden);
            this.UltimaPosicion = _Encabezado.SiguientePosicion;
            using (var _FileStream = new FileStream(_Ruta, FileMode.OpenOrCreate))
            {
                _FileStream.Write(GeneradorData.ConvertToBytes(_Encabezado.ParaAjusteTamanoCadena()), 0, _Encabezado.AjusteTamanoCadena);
                _FileStream.Write(GeneradorData.ConvertToBytes(Raiz.ParaAjusteTamanoCadena()), 0, Raiz.TamanoTextoCorregido());
            }
        }


        public ArbolBStar(int _Orden, string _Ruta, ICreateFixedSizeText<T> createFixedSizeText, int C) {
            this.Orden = _Orden;
            this.Ruta = _Ruta;
            this.createFixedSizeText = createFixedSizeText;
            var buffer = new byte[Encabezado.tamanoAjustado];
            using (var _FileStream = new FileStream(_Ruta, FileMode.OpenOrCreate))
            {
                _FileStream.Read(buffer, 0, Encabezado.tamanoAjustado);
            }
            var EncabezadoCadena = GeneradorData.ConvertToString(buffer);
            var valores = EncabezadoCadena.Split(MetodosNecesarios.Separador);

            this.Raiz = Convert.ToInt16(valores[0]);
            this.Orden = Convert.ToInt16(valores[1]);
            this.UltimaPosicion = Convert.ToInt16(valores[2]);
        }


        private void EscrituraEncabezado() {
            Encabezado encabezado = new Encabezado { Raiz = this.Raiz, SiguientePosicion = this.UltimaPosicion, Order = this.Orden };
            using (var _fs = new FileStream(this.Ruta, FileMode.OpenOrCreate))
            {
                _fs.Seek(0, SeekOrigin.Begin);
                _fs.Write(GeneradorData.ConvertToBytes(encabezado.ParaAjusteTamanoCadena()), 0, encabezado.AjusteTamanoCadena);
            }
        }

        private Encabezado CreacionDelEncabezado(int Order) {
            Encabezado e = new Encabezado { Order = Order, Raiz = 1, SiguientePosicion = 2 };
            return e;
        }
        private Nodo<T> CreacionDelNodo(int order) {
            Nodo<T> node = new Nodo<T> { Orden = order, Padre = MetodosNecesarios.NullPointer, ID = 1 };
            node.Data = new List<T>();
            node.Hijos = new List<int>();
            for (int i = 0; i < (4 * (order - 1)) / 3 + 1; i++)
            {
                node.Hijos.Add(MetodosNecesarios.NullPointer);
            }
            for (int i = 0; i < (4 * (order - 1)) / 3; i++)
            {
                node.Data.Add(this.createFixedSizeText.CreateNull());
            }
            this.Raiz = node.ID;
            this.UltimaPosicion++;
            return node;
        }

        public void Add(T data) {
            if (data == null)
            {
                throw new ArgumentException("Se encuentra este valor de forma nula");
            }

        }

        private void Insert(int ActualPosicion, T data) {
            Nodo<T> node = new Nodo<T>();
            node = node.LecturaNodo(this.Ruta, this.Orden, this.Raiz, ActualPosicion, this.createFixedSizeText);
            if (node.PosicionDentroNodo(data) != 1)
            {
                throw new ArgumentException("El valor se encuentra creado con anterioridad dentro del nodo");
            }
            if (node.EsHoja)
            {
                UpData(node, data, MetodosNecesarios.NullPointer);
                EscrituraEncabezado();
            }

        }

        private void UpData(Nodo<T> node, T data, int Derecho) {
            if (node.CapacidadMax && node.Padre != MetodosNecesarios.NullPointer)
            {
                Nodo<T> nPadre = new Nodo<T>();
                nPadre.ID = node.Padre;
                nPadre = nPadre.LecturaNodo(this.Ruta, this.Orden, this.Raiz, node.Padre, createFixedSizeText);
                int posicion = 0;
                for (int i = 0; i < nPadre.Hijos.Count(); i++)
                {
                    if (nPadre.Hijos[i] == node.ID)
                    {
                        posicion = i;
                        break;
                    }
                }
                Nodo<T> TemporalNode = new Nodo<T>();
                if (nPadre.Hijos[posicion + 1] != MetodosNecesarios.NullPointer)
                {
                    TemporalNode = TemporalNode.LecturaNodo(this.Ruta, this.Orden, this.Raiz, nPadre.Hijos[posicion + 1], createFixedSizeText);
                    if (!TemporalNode.CapacidadMax)
                    {
                        node.Data.Add(data);
                        node.Data.Sort();
                        T TemporalData = node.Data[node.Data.Count() - 1];
                        node.Data.Remove(TemporalData);
                        node.EscrituraEnDiscoNodo(this.Ruta, this.Raiz);
                        T TemporalData2 = nPadre.Data[posicion - 1];
                        nPadre.Data.Insert(posicion - 1, TemporalData2);
                        TemporalNode.Data.Add(data);
                        TemporalNode.EscrituraEnDiscoNodo(this.Ruta, this.Raiz);
                    }
                }
                if (posicion > 0)
                {
                    TemporalNode = TemporalNode.LecturaNodo(this.Ruta, this.Orden, this.Raiz, nPadre.Hijos[posicion - 1], createFixedSizeText);
                    if (!TemporalNode.CapacidadMax)
                    {
                        node.Data.Add(data);
                        node.Data.Sort();
                        T TemporalData = node.Data[0];
                        node.Data.Remove(TemporalData);
                        node.EscrituraEnDiscoNodo(this.Ruta, this.Raiz);
                        T TemporalData2 = nPadre.Data[posicion - 1];
                        nPadre.Data.Insert(posicion - 1, TemporalData2);
                        TemporalNode.Data.Add(data);
                        TemporalNode.EscrituraEnDiscoNodo(this.Ruta, this.Raiz);
                        return;
                    }
                }
                List<T> SNodo = new List<T>();
                foreach (var item in node.Data)
                {
                    SNodo.Add(item);
                }
                if (nPadre.Hijos[posicion + 1] != MetodosNecesarios.NullPointer)
                {
                    SNodo.Add(nPadre.Data[posicion]);
                    TemporalNode = TemporalNode.LecturaNodo(this.Ruta, this.Orden, this.Raiz, nPadre.Hijos[posicion + 1], createFixedSizeText);
                }
                else if (posicion > 0)
                {
                    SNodo.Add(nPadre.Data[posicion - 1]);
                    TemporalNode = TemporalNode.LecturaNodo(this.Ruta, this.Orden, this.Raiz, nPadre.Hijos[posicion - 1], createFixedSizeText);
                }

                foreach (var item in TemporalNode.Data)
                {
                    SNodo.Add(item);
                }
                SNodo.Add(data);
                SNodo.Sort();

                int min = (2 * (this.Orden - 1)) / 3;
                node.Data.Clear();
                TemporalNode.Data.Clear();
                int contador = 0;
                for (int i = 0; i < this.Orden - 1; i++)
                {
                    if (contador < min)
                    {
                        node.Data.Add(SNodo[i]);
                    }
                    else
                    {
                        node.Data.Add(createFixedSizeText.CreateNull());
                    }
                    contador++;
                }
                nPadre.Data.Insert(posicion, SNodo[min]);
                nPadre.Data.Insert(posicion + 1, SNodo[(min * 2) + 1]);
                contador = 0;
                for (int i = 0; i < this.Orden - 1; i++)
                {
                    if (contador < min)
                    {
                        TemporalNode.Data.Add(SNodo[i + min + 1]);
                    }
                    else
                    {
                        TemporalNode.Data.Add(createFixedSizeText.CreateNull());
                    }
                    contador++;
                }
                contador = 0;
                Nodo<T> NNode = new Nodo<T>();
                NNode.Padre = nPadre.ID;
                NNode.ID = UltimaPosicion;
                NNode.Data = new List<T>();
                NNode.Hijos = new List<int>();
                UltimaPosicion++;
                for (int i = 0; i < this.Orden - 1; i++)
                {
                    if (contador < min)
                    {
                        NNode.Data.Add(SNodo[i + (2 * min) + 2]);
                    }
                    else
                    {
                        NNode.Data.Add(createFixedSizeText.CreateNull());
                    }
                }
                for (int i = 0; i < this.Orden; i++)
                {
                    NNode.Hijos.Add(MetodosNecesarios.NullPointer);
                }
                node.EscrituraEnDiscoNodo(this.Ruta, this.Raiz);
                TemporalNode.EscrituraEnDiscoNodo(this.Ruta, this.Raiz);
                nPadre.EscrituraEnDiscoNodo(this.Ruta, this.Raiz);
                NNode.EscrituraEnDiscoNodo(this.Ruta, this.Raiz);
                EscrituraEncabezado();
            }
            else if (true)
            {
                node.InsertarDatos(data);
                node.EscrituraEnDiscoNodo(this.Ruta, this.Raiz);
                return;
            }
            Nodo<T> _NNode = new Nodo<T>(this.Orden, this.UltimaPosicion, node.Padre, createFixedSizeText);
            this.UltimaPosicion++;
            T TUData = createFixedSizeText.CreateNull();

            node.SeparacionNodo(data, Derecho, _NNode, TUData, createFixedSizeText);
            Nodo<T> NodoHijo = new Nodo<T>();
            for (int i = 0; i < _NNode.Hijos.Count; i++)
            {
                if (_NNode.Hijos[i] != MetodosNecesarios.NullPointer)
                {
                    NodoHijo = NodoHijo.LecturaNodo(this.Ruta, this.Orden, this.Raiz, _NNode.Hijos[i], createFixedSizeText);
                    NodoHijo.Padre = _NNode.ID;
                    NodoHijo.EscrituraEnDiscoNodo(Ruta, this.Raiz);
                }
                else
                {
                    break;
                }
            }
            if (node.Padre == MetodosNecesarios.NullPointer)
            {
                Nodo<T> NRaiz = new Nodo<T>(this.Orden, this.UltimaPosicion, MetodosNecesarios.NullPointer, createFixedSizeText);
                this.UltimaPosicion++;
                NRaiz.Hijos[0] = node.ID;
                NRaiz.Hijos[1] = _NNode.ID;
                NRaiz.InsertarDatos(data, _NNode.ID);

                node.Padre = NRaiz.ID;
                NRaiz.Padre = MetodosNecesarios.NullPointer;
                _NNode.Padre = NRaiz.ID;
                this.Raiz = NRaiz.ID;

                node.EscrituraEnDiscoNodo(this.Ruta, this.Raiz);
                _NNode.EscrituraEnDiscoNodo(this.Ruta, this.Raiz);
                NRaiz.EscrituraEnDiscoNodo(this.Ruta, this.Raiz);
            }
            else
            {
                node.EscrituraEnDiscoNodo(this.Ruta, this.Raiz);
                _NNode.EscrituraEnDiscoNodo(this.Ruta, this.Raiz);
                Nodo<T> Padre = new Nodo<T>();
                Padre = Padre.LecturaNodo(this.Ruta, this.Orden, this.Raiz, node.Padre, createFixedSizeText);
                UpData(Padre, data, _NNode.ID);
            }
        }


        private Nodo<T> Obtener(int PosicionActual, out int posicion, T data) {
            Nodo<T> NActual = new Nodo<T>();
            NActual.LecturaNodo(this.Ruta, this.Orden, this.Raiz, PosicionActual, this.createFixedSizeText);
            posicion = NActual.PosicionDentroNodo(data);
            if (posicion != -1)
            {
                return NActual;
            }
            else
            {
                if (NActual.EsHoja)
                {
                    return null;
                }
                else
                {
                    int PosicionAproximada = NActual.PosicionAproximada(data);
                    return Obtener(NActual.Hijos[PosicionAproximada], out posicion, data);
                }
            }
        }
        public T Obtener(T data) {
            int posicion = -1;
            Nodo<T> NObtenido = Obtener(this.Raiz, out posicion, data);
            if (NObtenido == null)
            {
                throw new ArgumentException("El dato solicitado no se encuentra en el arbol");
            }
            else
            {
                return NObtenido.Data[posicion];
            }
        }
        public bool Contains(T data) {
            int Posicion = -1;
            Nodo<T> NObtenido = Obtener(this.Raiz, out Posicion, data);
            if (NObtenido != null)
            {
                return true;
            }
            return false;
        }
        private void EscrituraNodo(Nodo<T> node, StringBuilder TTxto) {
            Sucursal _Sucursal = new Sucursal();
            for (int i = 0; i < node.Data.Count; i++)
            {
                if (node.Data[i].ToFixedSizeString() != _Sucursal.ToFixedSizeString())
                {
                    TTxto.Append(node.Data[i].ToString());
                    TTxto.Append("---");
                }
                else
                {
                    return;
                }
            }
        }
        private void ObtenerNodo(int PosicionActual){
            if (PosicionActual == MetodosNecesarios.NullPointer)
            {
                return;
            }
            Nodo<T> nodo = new Nodo<T>();
            nodo = nodo.LecturaNodo(this.Ruta, this.Orden, this.Raiz, PosicionActual, this.createFixedSizeText);
            for (int i = 0; i < nodo.Data.Count; i++)
            {
                ObtenerNodo(nodo.Hijos[i]);
                ElementosBuscados.Add(nodo.Data[i]);
            }
        }
        public List<T> ConvertirALista() {
            ElementosBuscados = new List<T>();
            ObtenerNodo(this.Raiz);
            return ElementosBuscados;
        }

        private void RecorridoPreOrden(int PosicionActual, StringBuilder TTxto) {
            if (PosicionActual == MetodosNecesarios.NullPointer)
            {
                return;
            }
            Nodo<T> nodo = new Nodo<T>();
            nodo.LecturaNodo(this.Ruta, this.Orden, this.Raiz, PosicionActual, this.createFixedSizeText);
            EscrituraNodo(nodo, TTxto);
            for (int i = 0; i < nodo.Hijos.Count; i++)
            {
                RecorridoPreOrden(nodo.Hijos[i], TTxto);
            }
        }
        public string ImprimirPreOrder() {
            StringBuilder TTxto = new StringBuilder();
            RecorridoPreOrden(this.Raiz, TTxto);
            return TTxto.ToString();
        }

        private void Busqueda(int PosicionActual, T data, T Ndata) {
            if (PosicionActual == MetodosNecesarios.NullPointer)
            {
                return; 
            }
            Nodo<T> nodo = new Nodo<T>();
            nodo = nodo.LecturaNodo(this.Ruta, this.Orden, this.Raiz, PosicionActual, this.createFixedSizeText);
            for (int i = 0; i < nodo.Data.Count; i++)
            {
                ObtenerNodo(nodo.Hijos[i]);
                if (nodo.Data[i].ToString() == data.ToString())
                {
                    nodo.Data[i] = Ndata;
                    nodo.EscrituraEnDiscoNodo(this.Ruta, this.Raiz);
                }
            }
        }

        public void Actualizar(T _data, T NData){
            Busqueda(this.Raiz, _data, NData);
        }

        private void InOrder(int PosicionActual, StringBuilder TTxt) {
            Sucursal _Sucursal = new Sucursal();
            if (PosicionActual == MetodosNecesarios.NullPointer)
            {
                return;
            }
            Nodo<T> nodo = new Nodo<T>();
            nodo.LecturaNodo(this.Ruta, this.Orden, this.Raiz, PosicionActual, this.createFixedSizeText);
            for (int i = 0; i < nodo.Data.Count; i++)
            {
                InOrder(nodo.Hijos[i], TTxt);
                if ((i < nodo.Data.Count) && (nodo.Data[i].ToFixedSizeString() != _Sucursal.ToFixedSizeString()))
                {
                    TTxt.AppendLine(nodo.Data[i].ToString());
                    TTxt.AppendLine("---");
                }
            }
        }
        public string ImprimirInOrder() {
            StringBuilder TTxt = new StringBuilder();
            InOrder(this.Raiz, TTxt);
            return TTxt.ToString();
        }
        private void PostOrder(int PosicionActual, StringBuilder Ttxt) {
            if (PosicionActual == MetodosNecesarios.NullPointer)
            {
                return;
            }
            Nodo<T> node = new Nodo<T>();
            node.LecturaNodo(this.Ruta, this.Orden, this.Raiz, PosicionActual, this.createFixedSizeText);
            for (int i = 0; i < node.Hijos.Count; i++)
            {
                PostOrder(node.Hijos[i], Ttxt);
            }
            EscrituraNodo(node, Ttxt);
        }
        public string ImprimirPostOrden() {
            StringBuilder Ttxt = new StringBuilder();
            PostOrder(this.Raiz, Ttxt);
            return Ttxt.ToString();
        }
    }
}
