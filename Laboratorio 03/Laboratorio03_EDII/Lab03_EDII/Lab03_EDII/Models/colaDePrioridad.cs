using System;
using System.Collections.Generic;

namespace Lab03_EDII.Models
{
    public class colaDePrioridad<T>
    {
        IComparer<T> comparador;
        T[] heap;
        public int contador { get; private set; }
        public colaDePrioridad() : this(null) { }
        public colaDePrioridad(int capacidad) : this(capacidad, null) { }
        public colaDePrioridad(IComparer<T> comparer) : this(16, comparer) { }
        public colaDePrioridad(int capacidad, IComparer<T> comparer)
        {
            this.comparador = (comparer == null) ? Comparer<T>.Default : comparer;
            this.heap = new T[capacidad];
        }
        public void push(T v)
        {
            if (contador >= heap.Length) Array.Resize(ref heap, contador * 2);
            heap[contador] = v;
            SiftUp(contador++);
        }
        public T pop()
        {
            var v = top();
            heap[0] = heap[--contador];
            if (contador > 0) SiftDown(0);
            return v;
        }
        public T top()
        {
            if (contador > 0) return heap[0];
            throw new InvalidOperationException("No existen elementos en el Heap");
        }
        void SiftUp(int n)
        {
            var v = heap[n];
            for (var n2 = n / 2; n > 0 && comparador.Compare(v, heap[n2]) > 0; n = n2, n2 /= 2) heap[n] = heap[n2];
            heap[n] = v;
        }
        void SiftDown(int n)
        {
            var v = heap[n];
            for (var n2 = n * 2; n2 < contador; n = n2, n2 *= 2)
            {
                if (n2 + 1 < contador && comparador.Compare(heap[n2 + 1], heap[n2]) > 0) n2++;
                if (comparador.Compare(v, heap[n2]) >= 0) break;
                heap[n] = heap[n2];
            }
            heap[n] = v;
        }
    }
}
