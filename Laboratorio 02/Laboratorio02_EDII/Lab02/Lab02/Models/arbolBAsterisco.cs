using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Lab02.Models
{
    public class arbolBAsterisco<TKey, TData> where TKey : IComparable<TKey>
    {
        private FileStream archivoArbol;
        private List<int> espaciosDisponibles;
        int Orden { get; set; }
        int Cuenta { get; set; }
        int Altura { get; set; }
        public int ultimaPosicion { get; set; }
        public nodoB<TKey, TData> Raiz { get; private set; }
        private string Encabezado;
        private int tamanioMaximoLlave;
        private string rutaArchivo;

        public delegate TKey ConvertStringToKey(string s);
        public delegate TData ConvertStringToData(string s);
        public delegate IList<string> GetData(TData data);
        public delegate IList<int> GetDataLength();

        public ConvertStringToKey ConverterStringToTkey;
        public ConvertStringToData ConverterStringToTData;
        public GetData retornarDatos;
        public GetDataLength retornarTamanioDatos;
        public arbolBAsterisco(int TreeOrder, string archivoArbolName, string archivoArbolPath, ConvertStringToKey KeyConverter, ConvertStringToData StringConverter, GetData DataConverter, int KeyMaxLength, GetDataLength DataMaxLength)
        {
            Raiz = null;
            Orden = TreeOrder;
            Cuenta = 0;
            Altura = 0;
            ultimaPosicion = 0;
            rutaArchivo = archivoArbolPath + archivoArbolName;
            archivoArbol = File.Create(rutaArchivo);//Se creará en la carpeta del proyecto
            espaciosDisponibles = new List<int>();
            Encabezado = Generador.armarEncabezado(Generador.hacerNulo().ToString(), Generador.tamanioPosicionesFijas(0), Orden, 0) + Environment.NewLine;
            archivoArbol.Write(ConvertStringTo_ByteChain(Encabezado), 0, Encabezado.Length);
            ConverterStringToTkey = KeyConverter;
            ConverterStringToTData = StringConverter;
            retornarDatos = DataConverter;
            tamanioMaximoLlave = KeyMaxLength;
            retornarTamanioDatos = DataMaxLength;
            cerrarArchivo();
        }
        public arbolBAsterisco(string archivoArbolPath, string archivoArbolName, ConvertStringToKey convertidorLlave, ConvertStringToData convertidorString, GetData convertidorDatos, int maximaLongitudLlave, GetDataLength maximaLongitudDatos)
        {
            rutaArchivo = archivoArbolPath + archivoArbolName;
            ConverterStringToTkey = convertidorLlave;
            ConverterStringToTData = convertidorString;
            retornarDatos = convertidorDatos;
            tamanioMaximoLlave = maximaLongitudLlave;
            retornarTamanioDatos = maximaLongitudDatos;
            StreamReader lector = new StreamReader(archivoArbolPath + archivoArbolName);
            int RaizposicionPrincipal = int.Parse(lector.ReadLine());
            ultimaPosicion = int.Parse(lector.ReadLine());
            lector.ReadLine();
            Orden = int.Parse(lector.ReadLine());
            Cuenta = 0;
            Altura = int.Parse(lector.ReadLine());
            espaciosDisponibles = new List<int>();
            lector.Close();
            archivoArbol = File.Open(rutaArchivo, FileMode.Open);
            Raiz = AccessTonodoB(RaizposicionPrincipal);
        }
        public void Insert(TKey newKey, TData newData)
        {
            cerrarArchivo();
            archivoArbol = File.Open(rutaArchivo, FileMode.Open);
            if (Raiz == null)
            {
                nodoB<TKey, TData> newnodoB = NewnodoB();
                newnodoB.insertar(newKey, newData);
                Raiz = newnodoB;
                archivoArbol.Seek(saltarEncabezado(), SeekOrigin.Begin);
                archivoArbol.Write(ConvertStringTo_ByteChain(mostrar(Raiz)), 0, mostrar(Raiz).Length);
                Cuenta++;
                Altura++;
            }
            else
            {
                insertarRecursivamente(newKey, newData, Raiz);
            }
            archivoArbol.Seek(0, SeekOrigin.Begin);
            if (Raiz == null)
            {
                Encabezado = Generador.armarEncabezado(Generador.hacerNulo().ToString(), Generador.tamanioPosicionesFijas(ultimaPosicion), Orden, Altura);
            }
            else
            {
                Encabezado = Generador.armarEncabezado(Generador.tamanioPosicionesFijas(Raiz.posicionPrincipal), Generador.tamanioPosicionesFijas(ultimaPosicion), Orden, Altura);
            }
            archivoArbol.Write(ConvertStringTo_ByteChain(Encabezado), 0, Encabezado.Length);
            cerrarArchivo();
        }
        private void insertarRecursivamente(TKey newKey, TData newData, nodoB<TKey, TData> Current)
        {
            if (Current.esHoja())
            {
                if (!Current.estaLleno()) /*Insertará si y solo si es hoja y está vacío*/
                {
                    Current.insertar(newKey, newData);
                    actaulizarNodo(Current);
                }
                else
                {
                    //Esto indica que donde se iba a inseratr está lleno
                    nodoB<TKey, TData> aux = AccessTonodoB(Current.Padre);
                    InsertPop(Current, newData, newKey, posicionPrincipal(aux, newKey));
                }
            }
            else
            {
                //Se moviliza hacia otro nodo
                if (newKey.CompareTo(Current.nodoLlaves[0]) == -1)
                {
                    insertarRecursivamente(newKey, newData, AccessTonodoB(Current.Hijos[0]));
                }
                else if (newKey.CompareTo(Current.nodoLlaves[0]) == 1)
                {
                    int index = obtenerIndiceParaInsertar(Current, newKey);
                    insertarRecursivamente(newKey, newData, AccessTonodoB(Current.Hijos[index]));
                }
                else
                {
                    Cuenta--;
                }
            }
        }
        private nodoB<TKey, TData> InsertPop(nodoB<TKey, TData> Actual, TData DataPop, TKey KeyPop, int posicionI)
        {
            if (Actual.estaLleno())
            {
                Actual.insertar(KeyPop, DataPop);
                DataPop = Actual.nivelSuperior(ref KeyPop);
                nodoB<TKey, TData> Hermano = NewnodoB();
                nodoB<TKey, TData> Padre = AccessTonodoB(Actual.Padre);
                Hermano.Padre = Actual.Padre;
                actaulizarNodo(Hermano);
                actaulizarNodo(Actual);
                if (Padre == null)
                {
                    nodoB<TKey, TData> newRaiz = NewnodoB();
                    newRaiz.insertar(KeyPop, DataPop);
                    newRaiz.insertarHijos(0, Actual.posicionPrincipal);
                    newRaiz.insertarHijos(1, Hermano.posicionPrincipal);
                    Raiz = newRaiz;
                    Actual.Padre = newRaiz.posicionPrincipal;
                    Hermano.Padre = newRaiz.posicionPrincipal;
                    actaulizarNodo(Raiz);
                    actaulizarNodo(Actual);
                    actaulizarNodo(Hermano);
                    Altura++;
                    if (posicionI < 0)
                    {
                        List<int> leftSon = new List<int>();
                        List<TData> nData = new List<TData>();
                        Hermano.nodoLlaves = Actual.HijosAndValuesToBrother(KeyPop, ref nData, ref leftSon);
                        Hermano.datosNodo = nData;
                        actualizarGrupoHijos(Actual, Actual.Hijos);
                        actualizarGrupoHijos(Hermano, leftSon);
                        actaulizarNodo(Actual);
                        actaulizarNodo(Hermano);
                        return Actual;
                    }
                    else
                    {
                        List<int> leftSon = new List<int>();
                        List<TData> nData = new List<TData>();
                        Hermano.nodoLlaves = Actual.hijosyvaloresActuales(KeyPop, ref nData, ref leftSon);
                        Hermano.datosNodo = nData;
                        actualizarGrupoHijos(Actual, Actual.Hijos);
                        actualizarGrupoHijos(Hermano, leftSon);
                        actaulizarNodo(Actual);
                        actaulizarNodo(Hermano);
                        return Hermano;
                    }
                }
                else
                {
                    List<int> leftSon = new List<int>();
                    List<TData> nData = new List<TData>();
                    nodoB<TKey, TData> auxPadre = InsertPop(Padre, DataPop, KeyPop, posicionPrincipal(Padre, KeyPop));
                    int aproximateposicionPrincipal = auxPadre.posicionAproximada(KeyPop);
                    auxPadre.insertarHijos(aproximateposicionPrincipal, Hermano.posicionPrincipal);
                    Hermano.Padre = auxPadre.posicionPrincipal;
                    Actual = AccessTonodoB(Actual.posicionPrincipal);
                    Padre = AccessTonodoB(Padre.posicionPrincipal);
                    if (posicionI < 0)
                    {
                        Hermano.nodoLlaves = Actual.HijosAndValuesToBrother(KeyPop, ref nData, ref leftSon);
                        Hermano.datosNodo = nData;
                        actualizarGrupoHijos(Hermano, leftSon);
                        actaulizarNodo(auxPadre);
                        actaulizarNodo(Actual);
                        actaulizarNodo(Hermano);
                        actaulizarNodo(Padre);
                        actualizarGrupoHijos(Padre, Padre.Hijos);
                        actualizarGrupoHijos(auxPadre, auxPadre.Hijos);
                        return Actual;
                    }
                    else if (posicionI > 0)
                    {
                        Hermano.nodoLlaves = Actual.hijosyvaloresActuales(KeyPop, ref nData, ref leftSon);
                        Hermano.datosNodo = nData;
                        actualizarGrupoHijos(Hermano, leftSon);
                        Hermano.Padre = auxPadre.posicionPrincipal;
                        actaulizarNodo(auxPadre);
                        actaulizarNodo(Actual);
                        actaulizarNodo(Hermano);
                        actaulizarNodo(Padre);
                        actualizarGrupoHijos(Padre, Padre.Hijos);
                        actualizarGrupoHijos(auxPadre, auxPadre.Hijos);
                        auxPadre = AccessTonodoB(auxPadre.posicionPrincipal);
                        Actual = AccessTonodoB(Actual.posicionPrincipal);
                        Hermano = AccessTonodoB(Hermano.posicionPrincipal);
                        Padre = AccessTonodoB(Padre.posicionPrincipal);
                        return Hermano;
                    }
                    else
                    {
                        Hermano.nodoLlaves = Actual.hijosyvaloresActuales(KeyPop, ref nData, ref leftSon);
                        Hermano.datosNodo = nData;
                        actualizarGrupoHijos(Hermano, leftSon);
                        actaulizarNodo(auxPadre);
                        actaulizarNodo(Actual);
                        actaulizarNodo(Hermano);
                        actaulizarNodo(Padre);
                        actualizarGrupoHijos(Padre, Padre.Hijos);
                        actualizarGrupoHijos(auxPadre, auxPadre.Hijos);
                        return Hermano;
                    }
                }
            }
            else
            {
                Actual.insertar(KeyPop, DataPop);
                actaulizarNodo(Actual);
                if (Actual.posicionPrincipal == Raiz.posicionPrincipal)
                {
                    Raiz = Actual;
                }
                return Actual;
            }
        }
        public nodoB<TKey, TData> Search(TKey key)
        {
            cerrarArchivo();
            archivoArbol = File.Open(rutaArchivo, FileMode.Open);
            if (Raiz != null)
            {
                return SearchRecursive(Raiz, key);
            }
            cerrarArchivo();
            return null;
        }
        private nodoB<TKey, TData> SearchRecursive(nodoB<TKey, TData> currentnodoB, TKey key)
        {
            if (currentnodoB.nodoLlaves.Exists(x => key.CompareTo(x) == 0))
            {
                return currentnodoB;
            }
            else
            {
                int index = currentnodoB.posicionAproximada(key);
                if (currentnodoB.obtenerNodoIndice(index) == Generador.hacerNulo())
                {
                    return null;
                }
                return SearchRecursive(AccessTonodoB(currentnodoB.Hijos[index]), key);
            }
        }
        public List<string> InOrder(nodoB<TKey, TData> currentnodoB, List<string> data)
        {
            cerrarArchivo();
            archivoArbol = File.Open(rutaArchivo, FileMode.Open);
            data = InOrderRecursivo(currentnodoB, data);
            cerrarArchivo();
            return data;
        }
        private List<string> InOrderRecursivo(nodoB<TKey, TData> currentnodoB, List<string> data)
        {
            if (currentnodoB == null)
            {
                return data;
            }
            if (currentnodoB.esHoja())
            {
                for (int i = 0; i < currentnodoB.datosNodo.Count; i++)
                {
                    IList<string> values = retornarDatos(currentnodoB.datosNodo[i]);
                    string line = "";
                    for (int j = 0; j < values.Count; j++)
                    {
                        line += values[j] + "|";
                    }
                    data.Add(line.Remove(line.Length - 1) + "\n");
                }
                return data;
            }
            else
            {
                for (int i = 0; i < currentnodoB.Hijos.Count; i++)
                {
                    data.Concat(InOrderRecursivo(AccessTonodoB(currentnodoB.Hijos[i]), data));
                    if (i < currentnodoB.datosNodo.Count)
                    {
                        IList<string> values = retornarDatos(currentnodoB.datosNodo[i]);
                        string line = "";
                        for (int j = 0; j < values.Count; j++)
                        {
                            line += values[j] + "|";
                        }
                        data.Add(line.Remove(line.Length - 1) + "\n");
                    }
                }
                return data;
            }
        }
        private void agregarTextoEnArchivo(string texto)
        {
            texto += Environment.NewLine;
            byte[] info = new UTF8Encoding(true).GetBytes(texto);
            archivoArbol.Write(info, 0, info.Length);
        }
        private byte[] ConvertStringTo_ByteChain(string text)
        {
            return new UTF8Encoding(true).GetBytes(text);
        }
        private int obtenerIndiceParaInsertar(nodoB<TKey, TData> Current, TKey newkey)
        {
            int indice = 0;
            while (newkey.CompareTo(Current.nodoLlaves[indice]) == 1)
            {
                if (indice + 1 == Current.nodoLlaves.Count)
                {
                    return indice + 1;
                }
                indice++;
            }
            return indice;
        }
        private int posicionPrincipal(nodoB<TKey, TData> Padre, TKey KeyPop)
        {
            if (Padre == null)
            {
                return 1;
            }
            if (Padre.posicionAproximada(KeyPop) == 0)
            {
                return -1;
            }
            else if (Padre.posicionAproximada(KeyPop) == Padre.nodoLlaves.Count)
            {
                return 1;
            }
            else if (Padre.posicionAproximada(KeyPop) >= Orden / 2)
            {
                return 0;
            }
            else
            {
                return -1;
            }
        }
        private void actualizarGrupoHijos(nodoB<TKey, TData> rightSon, List<int> HijosForNewnodoB)
        {
            nodoB<TKey, TData> hijo;
            for (int i = 0; i < HijosForNewnodoB.Count; i++)
            {
                if (HijosForNewnodoB[i] != Generador.hacerNulo())
                {
                    rightSon.Hijos[i] = HijosForNewnodoB[i];
                    hijo = AccessTonodoB(HijosForNewnodoB[i]);
                    hijo.Padre = rightSon.posicionPrincipal;
                    actaulizarNodo(hijo);
                }
            }
            actaulizarNodo(rightSon);
        }
        private nodoB<TKey, TData> NewnodoB()
        {
            nodoB<TKey, TData> nodoBAvailable;
            if (espaciosDisponibles.Count == 0)
            {
                return nodoBAvailable = new nodoB<TKey, TData>(Orden, ultimaPosicion++, tamanioMaximoLlave);
            }
            else
            {
                int p = espaciosDisponibles.Last();
                espaciosDisponibles.Remove(p);
                nodoB<TKey, TData> n = AccessTonodoB(p);
                n.limpiar();
                return n;
            }
        }
        private nodoB<TKey, TData> prestamoHermano(nodoB<TKey, TData> Padre, nodoB<TKey, TData> actual)
        {
            int index = Padre.Hijos.IndexOf(actual.posicionPrincipal);
            List<int> Hijos = new List<int>();
            for (int i = 0; i < Padre.Hijos.Count; i++)
            {
                if (Padre.Hijos[i] != Generador.hacerNulo())
                {
                    Hijos.Add(Padre.Hijos[i]);
                }
            }
            if (index == 0)
            {
                nodoB<TKey, TData> right = AccessTonodoB(Hijos[index + 1]);
                if (right.nodoLlaves.Count > (Orden % 2 == 0 ? Orden / 2 - 1 : Orden / 2))
                {
                    return right;
                }
            }
            else if (index == Hijos.Count - 1)
            {
                nodoB<TKey, TData> left = AccessTonodoB(Hijos[index - 1]);
                if (left.nodoLlaves.Count > (Orden % 2 == 0 ? Orden / 2 - 1 : Orden / 2))
                {
                    return left;
                }
            }
            else
            {
                nodoB<TKey, TData> left = AccessTonodoB(Hijos[index - 1]);
                nodoB<TKey, TData> right = AccessTonodoB(Hijos[index + 1]);
                if (left != null && right != null)
                {
                    if (left.nodoLlaves.Count >= right.nodoLlaves.Count)
                    {
                        if (left.nodoLlaves.Count > (Orden % 2 == 0 ? Orden / 2 - 1 : Orden / 2) && left.posicionPrincipal != actual.posicionPrincipal)
                        {
                            return left;
                        }
                    }
                    else
                    {
                        if (right.nodoLlaves.Count > (Orden % 2 == 0 ? Orden / 2 - 1 : Orden / 2) && right.posicionPrincipal != actual.posicionPrincipal)
                        {
                            return right;
                        }
                    }
                }
            }
            return null;
        }
        private nodoB<TKey, TData> prestarAHermano(nodoB<TKey, TData> Padre, nodoB<TKey, TData> actual)
        {
            int index = Padre.Hijos.IndexOf(actual.posicionPrincipal);
            List<int> Hijos = new List<int>();
            for (int i = 0; i < Padre.Hijos.Count; i++)
            {
                if (Padre.Hijos[i] != Generador.hacerNulo())
                {
                    Hijos.Add(Padre.Hijos[i]);
                }
            }
            if (index == 0)
            {
                nodoB<TKey, TData> right = AccessTonodoB(Hijos[index + 1]);
                return right;
            }
            else if (index == Hijos.Count - 1)
            {
                nodoB<TKey, TData> left = AccessTonodoB(Hijos[index - 1]);
                return left;
            }
            else
            {
                nodoB<TKey, TData> left = AccessTonodoB(Hijos[index - 1]);
                nodoB<TKey, TData> right = AccessTonodoB(Hijos[index + 1]);
                if (left.posicionPrincipal != actual.posicionPrincipal)
                {
                    return left;
                }
                if (right.posicionPrincipal != actual.posicionPrincipal)
                {
                    return right;
                }
            }
            return null;
        }
        private nodoB<TKey, TData> ultimoNodoDerecho(nodoB<TKey, TData> LeftSonOfCurrentnodoB)
        {
            if (LeftSonOfCurrentnodoB.Hijos[0] == Generador.hacerNulo())
            {
                return LeftSonOfCurrentnodoB;
            }
            else
            {
                for (int j = LeftSonOfCurrentnodoB.Hijos.Count - 1; j >= 0; j--)
                {
                    if (LeftSonOfCurrentnodoB.Hijos[j] != Generador.hacerNulo())
                    {
                        return ultimoNodoDerecho(AccessTonodoB(LeftSonOfCurrentnodoB.Hijos[j]));
                    }
                }
                return null;
            }
        }
        private int saltarLinea(int LengthOfData, int LengthOfKeys)
        {
            int NullLength = Generador.contadorCaracteresNulos();
            int MaxKeys = (Orden - 1);
            return 1 + ((NullLength + 1) + (MaxKeys * LengthOfKeys + MaxKeys) + (MaxKeys * LengthOfData + MaxKeys) + (NullLength + 1) + (NullLength * Orden + Orden)) + 1;
        }
        private int saltarEncabezado()
        {
            return (Generador.contadorCaracteresNulos() * 5) + (5 * 2);
        }
        public nodoB<TKey, TData> AccessTonodoB(int posicionPrincipal)
        {
            if (posicionPrincipal == Generador.hacerNulo())
            {
                return null;
            }
            int saltos = 0;
            saltos = saltarLineasYEncabezados(posicionPrincipal);
            StreamReader reader = new StreamReader(archivoArbol);
            reader.BaseStream.Seek(saltos, SeekOrigin.Begin);
            string[] nodoBLine = reader.ReadLine().Split('|');
            nodoB<TKey, TData> AuxnodoB = new nodoB<TKey, TData>(Orden, int.Parse(nodoBLine[0]), tamanioMaximoLlave);
            for (int i = 1; i < Orden; i++)
            {
                if (nodoBLine[i] != Generador.hacerNuloLlaves(tamanioMaximoLlave))
                {
                    AuxnodoB.nodoLlaves.Add(ConverterStringToTkey(Generador.retornarLlaveOriginal(nodoBLine[i])));
                }
            }
            int condicion = (Orden);
            for (int i = condicion; i < condicion + (Orden - 1); i++)
            {
                if (nodoBLine[i] != Generador.hacerNuloDatos(GetMaxLenghtData(retornarTamanioDatos())) || nodoBLine[i].Contains('#'))
                {
                    AuxnodoB.datosNodo.Add(ConverterStringToTData(Generador.retornarDatosOriginales(nodoBLine[i])));
                }
            }
            condicion += (Orden - 1);
            AuxnodoB.Padre = int.Parse(nodoBLine[condicion]);
            condicion += 1;
            int c = 0;
            for (int i = condicion; i < condicion + Orden; i++)
            {
                AuxnodoB.Hijos[c++] = int.Parse(nodoBLine[i]);
            }
            return AuxnodoB;
        }
        private void actaulizarNodo(nodoB<TKey, TData> nnodoB)
        {
            int jumps = saltarLineasYEncabezados(nnodoB.posicionPrincipal);
            archivoArbol.Seek(jumps, SeekOrigin.Begin);
            archivoArbol.Write(ConvertStringTo_ByteChain(mostrar(nnodoB)), 0, mostrar(nnodoB).Length);
            archivoArbol.Flush();
        }
        private int saltarLineasYEncabezados(int posicionPrincipalToGo)
        {
            return saltarEncabezado() + (saltarLinea(GetMaxLenghtData(retornarTamanioDatos()), tamanioMaximoLlave) * posicionPrincipalToGo);
        }
        private int GetMaxLenghtData(IEnumerable<int> data)
        {
            int i = 0;
            foreach (var item in data)
            {
                i += item + 1;
            }
            return i == 0 ? i : i - 1;
        }
        public string mostrar(nodoB<TKey, TData> Current)
        {
            string chain = "";
            chain += Generador.tamanioPosicionesFijas(Current.posicionPrincipal) + "|" + listaCadenas(Current.nodoLlaves) + listaCadenas(Current.datosNodo) + Generador.tamanioPosicionesFijas(Current.Padre) + "|" + stringOfList(Current.Hijos) + Environment.NewLine;
            return chain;
        }
        private string stringOfList(List<int> s)
        {
            string chain = "";
            for (int i = 0; i < Orden; i++)
            {
                if (i < s.Count)
                {
                    chain += Generador.tamanioPosicionesFijas(s[i]) + "|";
                }
                else
                {
                    chain += Generador.hacerNulo() + "|";
                }
            }
            return chain;
        }
        private string listaCadenas(List<TKey> s)
        {
            string chain = "";
            for (int i = 0; i < Orden - 1; i++)
            {
                if (i < s.Count)
                {
                    chain += Generador.tamanioFijoLlaves(s[i].ToString(), tamanioMaximoLlave) + "|";
                }
                else
                {
                    chain += Generador.hacerNuloLlaves(tamanioMaximoLlave) + "|";
                }
            }
            return chain;
        }
        private string listaCadenas(List<TData> s)
        {
            string chain = "";
            for (int i = 0; i < Orden - 1; i++)
            {
                string aux = "";
                if (i < s.Count)
                {
                    IList<string> valuesFromData = retornarDatos(s[i]);
                    IList<int> valuesLenght = retornarTamanioDatos();
                    for (int j = 0; j < valuesFromData.Count; j++)
                    {
                        aux += Generador.tamanioDatosFijos(valuesFromData[j], valuesLenght[j]) + "#";
                    }
                    chain += aux.Remove(aux.Length - 1) + "|";
                }
                else
                {
                    IEnumerable<int> valuesFromData = retornarTamanioDatos();
                    foreach (var item in valuesFromData)
                    {
                        aux += Generador.hacerNuloDatos(item) + "~";
                    }
                    chain += aux.Remove(aux.Length - 1) + "|";
                }
            }
            return chain;
        }
        private void cerrarArchivo()
        {
            if (archivoArbol != null)
            {
                archivoArbol.Dispose();
            }
        }
    }
}
