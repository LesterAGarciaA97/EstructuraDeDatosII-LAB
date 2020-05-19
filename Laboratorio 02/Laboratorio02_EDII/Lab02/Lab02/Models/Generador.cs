using System;

namespace Lab02.Models
{
    public class Generador
    {
            public static int hacerNulo()
            {
                return int.MinValue;
            }
            public static int contadorCaracteresNulos()
            {
                return hacerNulo().ToString().Length;
            }
            public static string armarEncabezado(string direccionRaiz, string ultimaDireccion, int ordenArbol, int altura)
            {
                string encabezado = "";
                encabezado += direccionRaiz + Environment.NewLine;
                encabezado += ultimaDireccion + Environment.NewLine;
                encabezado += Generador.tamanioPosicionesFijas(Math.Abs(ordenArbol - 1)) + Environment.NewLine;
                encabezado += tamanioPosicionesFijas(ordenArbol) + Environment.NewLine;
                encabezado += tamanioPosicionesFijas(altura) + Environment.NewLine;

                return encabezado;
            }
            public static string tamanioPosicionesFijas(int posicion1)
            {
                string esNulo = Generador.hacerNulo().ToString();

                if (posicion1.ToString().Length == esNulo.Length)
                {
                    return posicion1.ToString();
                }
                else
                {
                    string posicionNueva = posicion1.ToString();
                    for (int i = posicion1.ToString().Length; i < esNulo.Length; i++)
                    {
                        posicionNueva = "0" + posicionNueva;
                    }
                    return posicionNueva;
                }
            }
            public static string tamanioDatosFijos(string datos, int tamanioMaximoDatos)
            {
                if (datos.Length == tamanioMaximoDatos)
                {
                    return datos;
                }
                else
                {
                    string datosNuevos = datos;
                    for (int i = datos.ToString().Length; i < tamanioMaximoDatos; i++)
                    {
                        datosNuevos = "~" + datosNuevos;
                    }
                    return datosNuevos;
                }
            }
            public static string tamanioFijoLlaves(string llave, int tamanioMaximoLlaves)
            {
                if (llave.Length == tamanioMaximoLlaves)
                {
                    return llave;
                }
                else
                {
                    string datosNuevos = llave;
                    for (int i = llave.ToString().Length; i < tamanioMaximoLlaves; i++)
                    {
                        datosNuevos = "~" + datosNuevos;
                    }
                    return datosNuevos;
                }
            }
            public static string retornarDatosOriginales(string datos)
            {
                string datosNuevos = "";
                for (int i = 0; i < datos.Length; i++)
                {
                    if (datos[i].ToString() != "~")
                    {
                        datosNuevos += datos[i].ToString();
                    }
                }
                return datosNuevos;
            }
            public static string retornarLlaveOriginal(string datos)
            {
                string nuevosDatos = "";
                for (int i = 0; i < datos.Length; i++)
                {
                    if (datos[i].ToString() != "~")
                    {
                        nuevosDatos += datos[i].ToString();
                    }
                }
                return nuevosDatos;
            }
            public static string hacerNuloDatos(int tamanioMaximoDatos)
            {
                string datosNulos = "";
                for (int i = 0; i < tamanioMaximoDatos; i++)
                {
                    datosNulos += "~";
                }
                return datosNulos;
            }
            public static string hacerNuloLlaves(int tamanioMaximoLlaves)
            {
                string llaveNula = "";
                for (int i = 0; i < tamanioMaximoLlaves; i++)
                {
                    llaveNula += "~";
                }
                return llaveNula;
            }
    }
}
