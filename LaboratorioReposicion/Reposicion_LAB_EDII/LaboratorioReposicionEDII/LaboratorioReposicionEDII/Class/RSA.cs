using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace LaboratorioReposicionEDII.Class
{
    public class RSA
    {
        public static int n;
        public static int d;
        public static int e;
        public static int p;
        DateTime Time = DateTime.Now;
        public static List<int> KeyValues = new List<int>();
        public void GenerarLlaves(int numero1, int numero2, string FilePath)
        {
            p = numero1;
            var q = numero2;
            //Variable n
            n = p * q;
            //Aquí se calcula Q(n)
            var QN = (p - 1) * (q - 1);
            //Aquí se calcula e
            int count; int count1;
            for (var i = 2; i < QN; i++)
            {
                count = MCD(i, n);
                count1 = MCD(i, QN);
                if (count == 1 && count1 == 1)
                {
                    e = i;
                    break;
                }
            }
            d = CalcularD(QN, QN, e, 1, QN);
            KeyValues.Add(n);//0
            KeyValues.Add(e);//1
            KeyValues.Add(d);//2
            WritterPublicKey(FilePath);
            WritterPrivateKey(FilePath);

        }
        //Aquí se calcula d
        private int CalcularD(int QN1, int QN2, int e, int valor, int QNOriginal)
        {
            var div = QN1 / e;
            var mult1 = e * div;
            var mult2 = valor * div;
            var resultado1 = QN1 - mult1;
            var resultado2 = QN2 - mult2;
            if (resultado2 < 0)
            {
                resultado2 = QNOriginal % resultado2;
            }
            if (resultado1 == 1)
            {
                return resultado2;
            }
            else
            {
                QN1 = e;
                e = resultado1;
                QN2 = valor;
                valor = resultado2;
                return CalcularD(QN1, QN2, e, valor, QNOriginal);
            }
        }
        private int MCD(int a, int b)
        {
            int res;
            do
            {
                res = b;
                b = a % b;
                a = res;
            }
            while (b != 0);
            {
                return res;
            }

        }
        public void WritterPublicKey(string pathArchivo)
        {

            var path = Path.Combine(pathArchivo, Time.Minute.ToString() + "RSAPublicKey" + ".txt");
            using (StreamWriter Escritura = new StreamWriter(path))
            {

                for (int i = 0; i < 1; i++)
                {
                    if (i == 0)
                    {
                        Escritura.Write(KeyValues[0] + "," + KeyValues[1]);//0 = n , 1 = e
                    }
                }
                Escritura.Close();
            }
        }

        public void WritterPrivateKey(string pathArchivo) {
            var path = Path.Combine(pathArchivo, Time.Minute.ToString() + "RSAPrivateKey" + ".txt");
            using (StreamWriter Escritura = new StreamWriter(path))
            {
                for (int i = 0; i < 1; i++)
                {
                    if (i == 0)
                    {
                        Escritura.Write(KeyValues[0] + "," + KeyValues[2]);//0 = n , 2 = d
                    }
                }
            }
        
        }
        public int ReturnCipherKey(int CorrimientoValor, int n , int e)
        {
            int Cipherkey = 0;
            Cipherkey = Convert.ToInt32(Math.Pow(CorrimientoValor, e) % n);
            return Cipherkey;
        }
        public int ReturnDecipherKey(int CorrimientoValor, int n, int d) {
            int Decipherkey = 0;
            Decipherkey = Convert.ToInt32(Math.Pow(CorrimientoValor, d) % n);
            return Decipherkey;
        }
    }
}
