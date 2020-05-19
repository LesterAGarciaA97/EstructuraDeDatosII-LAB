using Lab03_EDII.Models;
using System;
using System.Collections.Generic;
using System.IO;

namespace Lab03_EDII
{
    public class Compresion
    {
        public static byte[] CompresionCompleta(byte[] ArchivoOriginal) {

            string NuevoCodigoDeCaracter;

            Dictionary<byte, int> Frecuencias = ObtenerFrecuencias(ArchivoOriginal);

            List<Nodo> ListadoLetras = ObtenerListaLetras(Frecuencias);

            colaDePrioridad<Nodo> colaLetraProridad = CrearColaPorPrioridad(ListadoLetras);

            Nodo NodoRaiz = ArbolCreacionHFF(colaLetraProridad);

            Dictionary<byte, string> CodigoCaracter = CreacionCodigoCaracter(NodoRaiz,out NuevoCodigoDeCaracter);

            MemoryStream EspacioMemoria = new MemoryStream();
            BinaryWriter EscrituraBinaria = new BinaryWriter(EspacioMemoria);
            byte[] ArbolDeBytes = ConvertirArbolBytes(NodoRaiz);
            EscrituraBinaria.Write(ArbolDeBytes);

            byte[] ComprimirDatos = ObtenerArchivoComprimido(ArchivoOriginal, CodigoCaracter, NuevoCodigoDeCaracter);
            EscrituraBinaria.Write(ComprimirDatos);
            EscrituraBinaria.Flush();
            return EspacioMemoria.ToArray();

        }
        private static void CompresionArbolBytesR(Nodo NodoAUtilizar, List<Nodo> ListadoDeNodos) {
            if (NodoAUtilizar == null)
            {
                return;
            }
            ListadoDeNodos.Add(NodoAUtilizar);
            CompresionArbolBytesR(NodoAUtilizar.left, ListadoDeNodos);
            CompresionArbolBytesR(NodoAUtilizar.right, ListadoDeNodos);
        }

        private static nodoMinimo[] CompresionDelArbol(Nodo nodoRaizArbol) {
            List<Nodo> ListadoDeNodos = new List<Nodo>();
            CompresionArbolBytesR(nodoRaizArbol, ListadoDeNodos);
            for (int i = 0; i < ListadoDeNodos.Count; i++)
            {
                ListadoDeNodos[i].id = i;
            }
            nodoMinimo[] NodoResultante = new nodoMinimo[ListadoDeNodos.Count];
            for (int i = 0; i < NodoResultante.Length; i++)
            {
                if (ListadoDeNodos[i].frecuencia == 0)
                {
                    int LadoIzquierdo = -2;
                    int LadoDerecho = -2;
                    NodoResultante[i] = new nodoMinimo(ListadoDeNodos[i].letra, LadoIzquierdo, LadoDerecho);
                }
                else
                {
                    int LadoIzquierdo = 0;
                    int LadoDerecho = 0;
                    if (ListadoDeNodos[i].left == null)
                    {
                        LadoIzquierdo = -1;
                    }
                    else
                    {
                        LadoIzquierdo = ListadoDeNodos[i].left.id;
                    }

                    if (ListadoDeNodos[i].right == null)
                    {
                        LadoDerecho = -1;
                    }
                    else
                    {
                        LadoDerecho = ListadoDeNodos[i].right.id;
                    }

                    NodoResultante[i] = new nodoMinimo(ListadoDeNodos[i].letra, LadoIzquierdo, LadoDerecho);
                }

            }
            return NodoResultante;
        }
        private static byte[] ConvertirArbolBytes(Nodo NodoRaizArbol) {
            List<byte> ListadoDeBytes = new List<byte>();

            nodoMinimo[] minimoNodos = CompresionDelArbol(NodoRaizArbol);

            byte[] CapacidadDelByte = BitConverter.GetBytes(minimoNodos.Length);

            ListadoDeBytes.AddRange(CapacidadDelByte);

            foreach (nodoMinimo Valor in minimoNodos)
            {
                byte LetraABytes = Valor.letra;
                byte[] BytesLadoIzquierdo = BitConverter.GetBytes(Valor.izquierdo);
                byte[] BytesLadoDerecho = BitConverter.GetBytes(Valor.derecho);
                if (BitConverter.IsLittleEndian)
                {
                    Array.Reverse(BytesLadoIzquierdo);
                    Array.Reverse(BytesLadoDerecho);
                }
                ListadoDeBytes.Add(LetraABytes);
                ListadoDeBytes.AddRange(BytesLadoIzquierdo);
                ListadoDeBytes.AddRange(BytesLadoDerecho);
            }
            return ListadoDeBytes.ToArray();
        }
        private static Dictionary<byte, int> ObtenerFrecuencias(byte[] ArchivoOriginal) {
            Dictionary<byte, int> Frecuencias = new Dictionary<byte, int>();
            //Recorrer Archivo encontrar caracter y visualizar su frecuencia
            foreach (byte Caracter in ArchivoOriginal)
            {
                if (Frecuencias.ContainsKey(Caracter))
                {
                    Frecuencias[Caracter] += 1;
                }
                else
                {
                    Frecuencias.Add(Caracter, 1);
                }
            }
            return Frecuencias;
        
        }
        private static List<Nodo> ObtenerListaLetras(Dictionary<byte, int> Frecuencias) {
            List<Nodo> LetraObtenida = new List<Nodo>();
            foreach (KeyValuePair<byte,int> Caracter in Frecuencias)
            {
                Nodo NuevoNodo = new Nodo(Caracter.Key, Caracter.Value, null, null, true);
                LetraObtenida.Add(NuevoNodo);
            }
            return LetraObtenida;
        }
        private static colaDePrioridad<Nodo> CrearColaPorPrioridad(List<Nodo> ListadoDeLetras) {
            colaDePrioridad<Nodo> ColaLetrasPrioridad = new colaDePrioridad<Nodo>();
            foreach (Nodo Caracter in ListadoDeLetras)
            {
                ColaLetrasPrioridad.push(Caracter);
            }
            return ColaLetrasPrioridad;
        }

        private static Nodo ArbolCreacionHFF(colaDePrioridad<Nodo> colaletraPrioridad) {
            colaletraPrioridad.push(new Nodo(System.Byte.MinValue, 0, null, null, true));
            while (colaletraPrioridad.contador != 1)
            {
                Nodo PrimerNodoCola = colaletraPrioridad.pop();
                Nodo SegundoNodoCola = colaletraPrioridad.pop();
                Nodo NuevoNodoArbol = new Nodo(System.Byte.MinValue, PrimerNodoCola.frecuencia + SegundoNodoCola.frecuencia, PrimerNodoCola, SegundoNodoCola, false);
                colaletraPrioridad.push(NuevoNodoArbol);
            }
            Nodo NodoRaizCola = colaletraPrioridad.pop();
            return NodoRaizCola;
        }
        private static Dictionary<byte, string> CreacionCodigoCaracter(Nodo NodoRaizArbolHFF, out string CodigoCaracter) {
            List<string> ListadoDeCaracteres = new List<string>();
            Dictionary<byte, string> DiccionarioNuevoCaracteres = new Dictionary<byte, string>();
            AsignacionCodigoNodo(DiccionarioNuevoCaracteres, NodoRaizArbolHFF,"", ListadoDeCaracteres);
            CodigoCaracter = ListadoDeCaracteres[0];
            return DiccionarioNuevoCaracteres;
        }
        private static void AsignacionCodigoNodo(Dictionary<byte, string> DiccionarioNuevoCaracteres, Nodo NodoACodificar, string CodigoAUtilizar, List<string> ListadoDeCaracteres) {
            if (NodoACodificar == null)
            {
                return;
            }
            string CodigoHijoIzquierdo = CodigoAUtilizar + "0";
            string CodigoHijoDerecho = CodigoAUtilizar + "1";
            AsignacionCodigoNodo(DiccionarioNuevoCaracteres, NodoACodificar.left, CodigoHijoIzquierdo, ListadoDeCaracteres);
            if (NodoACodificar.esLetra){

                if (NodoACodificar.frecuencia != 0){

                    DiccionarioNuevoCaracteres.Add(NodoACodificar.letra, CodigoAUtilizar);
                }
                else{

                    ListadoDeCaracteres.Add(CodigoAUtilizar);

                }
            }
            AsignacionCodigoNodo(DiccionarioNuevoCaracteres, NodoACodificar.right, CodigoHijoDerecho, ListadoDeCaracteres);
        }
        private static byte[] ObtenerArchivoComprimido(byte[] DatosCompletos, Dictionary<byte, string> DiccionarioDeDatos, string NuevoCodigoCaracter) {
            List<byte> ListaResultante = new List<byte>();
            List<bool> BufferDeDatos = new List<bool>();
            int UltimoByteEnDisco = 0;
            int DatoIncrementador = 1;
            foreach (byte BValor in DatosCompletos)
            {
                string CadenaCodigo = DiccionarioDeDatos[BValor];
                foreach (char CValor in CadenaCodigo)
                {
                    BufferDeDatos.Add(CValor == '1' ? true : false);
                    if (BufferDeDatos.Count == 8)
                    {
                        int DatosConvertidosBooleanos = Convert.ToByte(BufferDeDatos[0]) * 1 + Convert.ToByte(BufferDeDatos[1]) * 2 + Convert.ToByte(BufferDeDatos[2]) * 4 + Convert.ToByte(BufferDeDatos[3]) * 8 + Convert.ToByte(BufferDeDatos[4]) * 16 + Convert.ToByte(BufferDeDatos[5]) * 32 + Convert.ToByte(BufferDeDatos[6]) * 64 + Convert.ToByte(BufferDeDatos[7]) * 128;
                        ListaResultante.Add((byte)DatosConvertidosBooleanos);
                        BufferDeDatos.Clear();
                    }
                }
            }
            foreach (char CRValor in NuevoCodigoCaracter)
            {
                BufferDeDatos.Add(CRValor == '1' ? true : false);
                if (BufferDeDatos.Count == 8)
                {
                    int DatosConvertidosBooleanos = Convert.ToByte(BufferDeDatos[0]) * 1 + Convert.ToByte(BufferDeDatos[1]) * 2 + Convert.ToByte(BufferDeDatos[2]) * 4 + Convert.ToByte(BufferDeDatos[3]) * 8 + Convert.ToByte(BufferDeDatos[4]) * 16 + Convert.ToByte(BufferDeDatos[5]) * 32 + Convert.ToByte(BufferDeDatos[6]) * 64 + Convert.ToByte(BufferDeDatos[7]) * 128;
                    ListaResultante.Add((byte)DatosConvertidosBooleanos);
                    BufferDeDatos.Clear();
                }
            }
            for (int i = 0; i < BufferDeDatos.Count; i++)
            {
                if (BufferDeDatos[i])
                {
                    UltimoByteEnDisco += DatoIncrementador;
                }
                DatoIncrementador *= 2;
            }
            ListaResultante.Add((byte)UltimoByteEnDisco);
            return ListaResultante.ToArray();
        }
    }
}
