using System;
using System.Collections.Generic;

namespace Lab02_ED2.Models
{
    public delegate int CompararCon<T>(T Primer_valor, T Segundo_valor);
    public class nodoB<T> {
        public T[] Valores { get; set; }
        public nodoB<T>[] Hijos { get; set; }
        public int Contador { get; set; }
        public nodoB() {
            Valores = new T[5];
            Hijos = new nodoB<T>[5];
            Contador = 0;
        }
    }
    public class arbolB<T> {
        public nodoB<T> raiz { get; set; }
        public int Orden { get; set; }
        public int Minimo { get; set; }
        public int Maximo { get; set; }
        public Lazy<List<T>> Listado { get; set; }
        public CompararCon<T> Comparador { get; set; }
        public arbolB() {
            raiz = null;
        }
        public arbolB(int Orden_1, CompararCon<T> comparar) {
            this.Orden = Orden_1;
            Minimo = (Orden_1 - 1) / 2;
            Maximo = Orden_1 - 1;
            this.raiz = null;
            this.Comparador = comparar;
            this.Listado = new Lazy<List<T>>();
        }
        public static bool EstaVacio(nodoB<T> nuevoNodo) {
             return nuevoNodo == null;
        }
        private void BusquedaNodo(T llave_valor, nodoB<T> nodoP,ref bool localizado, ref int ContAux) {
            if (Comparador(llave_valor, nodoP.Valores[1])==-1){
                localizado = false;
                ContAux = 0;
            }
            else {
                ContAux = nodoP.Contador;
                while (Comparador(llave_valor,nodoP.Valores[ContAux])== -1 && ContAux > 1) {
                    ContAux -= 1;
                    localizado = (Comparador(llave_valor, nodoP.Valores[ContAux]) == 0);
                }
            }
        }
        public void Busqueda(T Llave_valor, nodoB<T> Nodo_principal, ref bool Localizado, ref nodoB<T> Nodo_aux, ref int P) {
            if (EstaVacio(Nodo_principal)){
                Localizado = false;
            }
            else{
                BusquedaNodo(Llave_valor, Nodo_principal, ref Localizado, ref P);
                if (Localizado) {
                    Nodo_aux = Nodo_principal;
                }
                else {
                    Busqueda(Llave_valor, Nodo_principal.Hijos[P], ref Localizado, ref Nodo_aux, ref P);
                }
            }
        }
        private void RecorridoInorder(nodoB<T> NodoArbol) {
            if (!EstaVacio(NodoArbol)) {
                RecorridoInorder(NodoArbol.Hijos[0]);
                for (int i = 1; i < NodoArbol.Contador; i++) {
                    Listado.Value.Add(NodoArbol.Valores[i]);
                    RecorridoInorder(NodoArbol.Hijos[i]);
                }
            }
        }
        public List<T> ConvertirALista() {
            Listado = new Lazy<List<T>>();
            RecorridoInorder(raiz);
            return Listado.Value;
        }
        private void InsertarEnHoja(T H, nodoB<T> HDerecho, nodoB<T> Nodo, int A) {
            for (int i = Nodo.Contador; i >= A + 1; i--) {
                Nodo.Valores[i + 1] = Nodo.Valores[i];
                Nodo.Hijos[i + 1] = Nodo.Hijos[i];
            }
            Nodo.Valores[A + 1] = H;
            Nodo.Hijos[A + 1] = HDerecho;
            Nodo.Contador = Nodo.Contador + 1;
        }
        private void PartirNodo(T H, nodoB<T> HDerecho, nodoB<T> Nodo, int A, ref T Mitad, ref nodoB<T> MitadDerecha) {
            int PosicionDeLaMitad;
            PosicionDeLaMitad = A;
            if (A <= Minimo) {
                A = Minimo;
            }
            else {
                A = Minimo + 1;
            }
            MitadDerecha = new nodoB<T>();
            for (int i = PosicionDeLaMitad + 1; i < Orden; i++) {
                MitadDerecha.Valores[i - PosicionDeLaMitad] = Nodo.Valores[i];
                MitadDerecha.Hijos[i - PosicionDeLaMitad] = Nodo.Hijos[i];
            }
            MitadDerecha.Contador = Maximo - PosicionDeLaMitad;
            Nodo.Contador = PosicionDeLaMitad;
            if (A <= Orden / 2) {
                InsertarEnHoja(H, HDerecho, Nodo, A);
            }
            else {
                var ValorNuevo = A - PosicionDeLaMitad;
                InsertarEnHoja(H, HDerecho, MitadDerecha, ValorNuevo);
            }
            Mitad = Nodo.Valores[Nodo.Contador];
            MitadDerecha.Hijos[0] = Nodo.Hijos[Nodo.Contador];
            Nodo.Contador = Nodo.Contador - 1;
        }
        private void Movilizar(T N, nodoB<T> M, ref bool SubirNodo, ref T O, ref nodoB<T> NuevaRaiz) {
            var Aux = default(int);
            var SiAplica = default(bool);

            if (EstaVacio(M)) {
                SubirNodo = true;
                O = N;
                NuevaRaiz = null;
            }
            else {
                BusquedaNodo(N, M, ref SiAplica, ref Aux);

                if (SiAplica) {
                    return;
                }
                Movilizar(N, M.Hijos[Aux], ref SubirNodo, ref O, ref NuevaRaiz);

                if (SubirNodo) {
                    if (M.Contador < Maximo) {
                        SubirNodo = false;
                        InsertarEnHoja(O, NuevaRaiz, M, Aux);
                    }
                    else {
                        SubirNodo = true;
                        PartirNodo(O, NuevaRaiz, M, Aux, ref O, ref NuevaRaiz);
                    }
                }
            }
        }
        private void Insertar(T AB, ref nodoB<T> Raiz) {
            var EmpujarHaciaArriba = default(bool);
            var Aux2 = default(nodoB<T>);
            var Aux = default(T);
            Movilizar(AB, Raiz, ref EmpujarHaciaArriba, ref Aux, ref Aux2);
            if (EmpujarHaciaArriba) {
                var Z = new nodoB<T>();
                Z.Contador = 1;
                Z.Valores[1] = Aux;
                Z.Hijos[0] = Raiz;
                Z.Hijos[1] = Aux2;
                Raiz = Z;
            }
        }
        public void Insertar(T AB) {
            var NuevaRaiz = this.raiz;
            Insertar(AB, ref NuevaRaiz);
            this.raiz = NuevaRaiz;
        }
        private void Inorder(nodoB<T> Q) {
            if (!EstaVacio(Q)) {
                Inorder(Q.Hijos[0]);
                for (int i = 1; i <= Q.Contador; i++) {
                    Listado.Value.Add(Q.Valores[i]);
                    Inorder(Q.Hijos[i]);
                }
            }
        }
    }
}
