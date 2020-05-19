using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using System.Text;
using Lab03_EDII;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
using Lab03_EDII.Controllers;
using static Lab03_EDII.Controllers.ManejoArchivoController;

namespace Lab03_EDII
{
    public class EscrituraD
    {
        public static IWebHostEnvironment _environment;
        public string ConvertidorAStringFile(IFormFile ArchivoCompresor) {
            string DataConvertida = ArchivoCompresor.ToString();
            return DataConvertida;
        }
        public void ComprimirData(string Archivo, string objName)
        {
            byte[] DatosCompresosBytes = Encoding.ASCII.GetBytes(Archivo);
            byte[] ArchivoComprimido = Compresion.CompresionCompleta(DatosCompresosBytes);
            File.WriteAllBytes(objName += ".huff",ArchivoComprimido);
            
        }
        public void DescomprimirData(string ArchivoCompreso, string ObjName) {

            if (ObjName.Contains(".huff"))
            {
                ObjName = ObjName.Replace(".huff", "");
            }
            if (ArchivoCompreso.Contains('"'))
            {
                ArchivoCompreso.Replace('"', ' ');
            }
            byte[] DatosDescomprimidosEnBytes = Encoding.ASCII.GetBytes(ArchivoCompreso);
            
           /* for (int i = 0; i < ArchivoCompreso.Length; i++)
            {
                DatosDescomprimidosEnBytes[i] = Convert.ToByte(ArchivoCompreso[i]);
            }*/

            byte[] ArchivoDescomprimido = Descompresion.DescompresionCompleta(DatosDescomprimidosEnBytes);
            File.WriteAllBytes(ObjName , ArchivoDescomprimido);

        }

    }
}
