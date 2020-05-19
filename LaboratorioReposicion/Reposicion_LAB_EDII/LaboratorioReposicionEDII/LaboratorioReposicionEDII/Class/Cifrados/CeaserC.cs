using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LaboratorioReposicionEDII.Class.Cifrados
{
    public class CeaserC
    {
        public string Llave;
        public string CaracteresCompleto;
        public List<char> ListadoDeCaracteres;
        public List<char> CaracteresAUtilizar;
        public List<char> TextoCompletoALista = new List<char>();
        public List<char> ListaCifrada = new List<char>();
        public Dictionary<char, char> DicccionarioCifrado = new Dictionary<char, char>();
        public CeaserC(string palabra, string ContenidoArchivo)
        {
            Llave = palabra;
            CaracteresCompleto = ContenidoArchivo;
            CaracteresAUtilizar = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnñopqrstuvwxyz".ToArray().ToList();
            TextoCompletoALista = CaracteresCompleto.ToArray().ToList();
        }

        public string Cifrado()
        {
            int posicion = 0;
            string cifrado = "";
            BusquedaCaracter();
            for (int i = 0; i < TextoCompletoALista.Count(); i++)
            {
                if (CaracteresAUtilizar.Contains(TextoCompletoALista.ElementAt(i)))
                {
                    posicion = CaracteresAUtilizar.IndexOf(TextoCompletoALista.ElementAt(i));
                    ListaCifrada.Add(ListadoDeCaracteres.ElementAt(posicion));
                }
                else
                {
                    ListaCifrada.Add(TextoCompletoALista.ElementAt(i));
                }

            }
            cifrado = string.Join('↔', ListaCifrada);
            cifrado = cifrado.Replace("↔", "");
            return cifrado;
        }

        public string Descifrado()
        {
            int posicion = 0;
            string descifrado = "";
            BusquedaCaracter();
            for (int i = 0; i < TextoCompletoALista.Count(); i++)
            {
                if (ListadoDeCaracteres.Contains(TextoCompletoALista.ElementAt(i)))
                {
                    posicion = ListadoDeCaracteres.IndexOf(TextoCompletoALista.ElementAt(i));
                    ListaCifrada.Add(CaracteresAUtilizar.ElementAt(posicion));
                }
                else
                {
                    ListaCifrada.Add(TextoCompletoALista.ElementAt(i));
                }

            }
            descifrado = string.Join('↔', ListaCifrada);
            descifrado = descifrado.Replace("↔", "");
            return descifrado;
        }

        public void BusquedaCaracter()
        {
            int posicion = 0;
            ListadoDeCaracteres = Llave.ToArray().ToList();
            ListadoDeCaracteres = ((from s in ListadoDeCaracteres select s).Distinct()).ToList();
            ListadoDeCaracteres = ListadoDeCaracteres.Union(CaracteresAUtilizar).ToList();
            ListadoDeCaracteres = ((from s in ListadoDeCaracteres select s).Distinct()).ToList();


        }
    }
}
