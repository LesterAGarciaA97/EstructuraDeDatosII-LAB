using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace LaboratorioReposicionEDII.Class
{
    public class Diffie_Hellman
    {
        static double g = 43;
        static double p = 107;
        Random NumberRandom = new Random();
        DateTime Time = DateTime.Now;
        static List<int> KeyValues = new List<int>();
        static double a;
        public void CreateKeys(int Rnd_ab, string path)
        {
            double a = Rnd_ab;
            int A = Convert.ToInt32(Math.Pow(g, a) % p); // creacion llave publica
            //--------------------------------------------------------------
            //int K = Convert.ToInt32(Math.Pow(ValueAB, a) % p); // Creacion llave privada
            KeyValues.Add(A);
            KeyValues.Add(Convert.ToInt32(a));
            WritterKeys(path);
        }

        public int ReturnPublicKey(int ValueB)
        {
            KeyValues.Add(Convert.ToInt32(Math.Pow(ValueB, KeyValues[1]) % p));
            return KeyValues[2];
        }

        public void WritterKeys(string pathArchivo)
        {

            var path = Path.Combine(pathArchivo, Time.Minute.ToString() + "Diffie-HellmanPrivateKey" + ".txt");
            using (StreamWriter Escritura = new StreamWriter(path))
            {

                for (int i = 0; i < KeyValues.Count; i++)
                {
                    string item = KeyValues[i].ToString();
                    if (i == 0)
                    {
                        Escritura.Write("Diffie Hellman" + Environment.NewLine + "LLave Privada: " + item);
                    }
                }
                Escritura.Close();
            }
        }
    }
}
