using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Lab06_EDII.Cifrado_Asimetrico
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
            KeyValues.Add(n);
            KeyValues.Add(e);
            KeyValues.Add(d);
            WritterKeys(FilePath);

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
        public void WritterKeys(string pathArchivo)
        {

            var path = Path.Combine(pathArchivo, Time.Minute.ToString() + "RSAKeys" + ".txt");
            using (StreamWriter Escritura = new StreamWriter(path))
            {

                for (int i = 0; i <= 1; i++)
                {
                    if (i == 0)
                    {
                        Escritura.Write("RSA" + Environment.NewLine + "LLave Privada: [" +KeyValues[1]+","+KeyValues[0]+"]");
                    }
                    else
                    {
                        Escritura.Write(Environment.NewLine + "LLave Publica: [" + KeyValues[2] + "," + KeyValues[0] + "]");
                    }
                }
                Escritura.Close();
            }
        }
        public int ReturnPublicKey(int CorrimientoValor) {
            KeyValues.Add(Convert.ToInt32(Math.Pow(CorrimientoValor, KeyValues[2]) % p));
            return KeyValues[3];
        }
    }
}
