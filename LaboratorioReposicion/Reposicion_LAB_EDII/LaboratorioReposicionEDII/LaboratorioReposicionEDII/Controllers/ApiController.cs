using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using LaboratorioReposicionEDII.Class;
using LaboratorioReposicionEDII.Class.Cifrados;
using LaboratorioReposicionEDII.Class.LZW;
using LaboratorioReposicionEDII.Models;
using LaboratorioReposicionEDII.Models.Cifrados;
using LaboratorioReposicionEDII.Models.Huffman;
using LaboratorioReposicionEDII.Models.LZW;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LaboratorioReposicionEDII.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ApiController : ControllerBase
    {
        public static IWebHostEnvironment _environment;
        public ApiController(IWebHostEnvironment environment)
        {
            _environment = environment;
        }
        /// <summary>
        /// Laboratorio No. 0
        /// </summary>
        [HttpPost]
        public void RecepcionNodos(Movie movie) {
            Data _Data = new Data();
            _Data.Add(movie);
        }

        [HttpGet]
        public List<Movie> RetornarNodos() {
            Data _Data = new Data();
            return _Data.ReturnList();
        }
        ///////////////////////////////////////////
        /// <summary>
        /// Laboratorio No. 1
        /// </summary>
        [HttpPost("ceaser2")]
        public void CreateCipher([FromForm] DataCeaser _DataCeaser)
        {
            if (!Directory.Exists(_environment.WebRootPath + "\\ArchivosCifradosCeaser2\\"))
            {
                Directory.CreateDirectory(_environment.WebRootPath + "\\ArchivosCifradosCeaser2\\");
            }
            Ceaser _Ceaser = new Ceaser();
            _Ceaser.CifradoCeaser(_DataCeaser.ArchivoEntrada, _DataCeaser.ArchivoLlaves, _DataCeaser.n, _environment.WebRootPath + "\\ArchivosCifradosCeaser2\\");
            //Proceso de cifrado
        }
        [HttpPost("getPublicKey")]
        public void GetKeysCreator([FromForm]DataRequired _dataRequired)
        {
            if (!Directory.Exists(_environment.WebRootPath + "\\CipherKeys\\"))
            {
                Directory.CreateDirectory(_environment.WebRootPath + "\\CipherKeys\\");
            }
            Diffie_Hellman diffie_Hellman = new Diffie_Hellman();
            diffie_Hellman.CreateKeys( _dataRequired.Rnd_ab, _environment.WebRootPath + "\\CipherKeys\\");
            RSA _RSA = new RSA();
            _RSA.GenerarLlaves(_dataRequired.p, _dataRequired.q, _environment.WebRootPath + "\\CipherKeys\\");
            //Proceso de creacion de llaves​
        }
        /////////////////////////////////////////////

        /// <summary>
        /// Laboratorio 3.1, Proceso compresion de Huffman
        /// </summary>
        /// <param name="_DataHuffman">Se envia el archivo, y el nuevo nombre del archivo</param>
        [HttpPost("compressHuffman")]
        public void CompresionHuffman([FromForm] DataHuffman _DataHuffman) {
            if (!Directory.Exists(_environment.WebRootPath + "\\ArchivosCompresosHuffman\\"))
            {
                Directory.CreateDirectory(_environment.WebRootPath + "\\ArchivosCompresosHuffman\\");
            }
            MetodosNecesarios _MetodosHuffman = new MetodosNecesarios();
            _MetodosHuffman.ProcesoCompresionHuffman(_DataHuffman.ArchivoComprimir, _environment.WebRootPath + "\\ArchivosCompresosHuffman\\", _DataHuffman.NuevoNombre);

        }
        /// <summary>
        /// Laboratorio 3.1, Proceso decompresion de huffman
        /// </summary>
        /// <param name="_ArchivoHUFF">Archivo compreso enviado a descomprimir</param>
        [HttpPost("DecompressHuffman")]
        public void DecompresionHuffman([FromForm]IFormFile _ArchivoHUFF) {
            if (!Directory.Exists(_environment.WebRootPath + "\\ArchivosDecompresosHuffman\\"))
            {
                Directory.CreateDirectory(_environment.WebRootPath + "\\ArchivosDecompresosHuffman\\");
            }
            MetodosNecesarios _MetodosHuffman = new MetodosNecesarios();
            _MetodosHuffman.ProcesoDecompresionHuffman(_ArchivoHUFF, _environment.WebRootPath + "\\ArchivosCompresosHuffman\\", _environment.WebRootPath + "\\ArchivosDecompresosHuffman\\");

        }
        /// <summary>
        /// Laboratorio 3.1, Proceso de escritura los datos de los archivos nombre original, nombre archivo comprimido, ruta archivo compreso, factor, razon, porcentaje. 
        /// </summary>
        [HttpPost("CompressionsHuffman")]
        public void Compressions() {
            if (!Directory.Exists(_environment.WebRootPath + "\\Compressions\\"))
            {
                Directory.CreateDirectory(_environment.WebRootPath + "\\Compressions\\");
            }
            MetodosNecesarios Escritura = new MetodosNecesarios();
            Escritura.ProcesoEscrituraDatosHuffman(_environment.WebRootPath + "\\Compressions\\");
        }
        /////////////////////////////////////////////
        /// <summary>
        /// Laboratorio 3.2, metodo de compresion LZW, metodo donde se envia el archivo a comprimir, y tambien se envia el nuevo nombre del archivo que se le colocara la extension .lzw
        /// </summary>
        /// <param name="_DataLzw">Archivo de entrada a comprimir y nuevo nombre del archivo</param>
        [HttpPost("CompressLzw")]
        public void CompresionLwz([FromForm]DataLzw _DataLzw) {
            if (!Directory.Exists(_environment.WebRootPath + "\\ArchivosCompresosLZW\\"))
            {
                Directory.CreateDirectory(_environment.WebRootPath + "\\ArchivosCompresosLZW\\");
            }
            MetodosNecesariosL _Metodoslzw = new MetodosNecesariosL();
            _Metodoslzw.ProcesoCompresionLZW(_DataLzw.ArchivoEntrada, _environment.WebRootPath + "\\ArchivosCompresosLZW\\", _DataLzw.NuevoNombre);

        }
        /// <summary>
        /// Laboratorio 3.2, metodo para descomprimir el archivo .lzw generado por el programa.
        /// </summary>
        /// <param name="_ArchivoLzw"></param>
        [HttpPost("DecompressLzw")]
        public void DecompresionLzw([FromForm]IFormFile _ArchivoLzw) {
            if (!Directory.Exists(_environment.WebRootPath + "\\ArchivosDecompresosLZW\\"))
            {
                Directory.CreateDirectory(_environment.WebRootPath + "\\ArchivosDecompresosLZW\\");
            }
            MetodosNecesariosL _Metodoslzw = new MetodosNecesariosL();
            _Metodoslzw.ProcesoDecompresionLZW(_ArchivoLzw, _environment.WebRootPath + "\\ArchivosDecompresosLZW\\");
        }
        /// <summary>
        /// Laboratorio 3.2, Proceso de escritura los datos de los archivos nombre original, nombre archivo comprimido, ruta archivo compreso, factor, razon, porcentaje. 
        /// </summary>
        [HttpPost("CompressionsLzw")]
        public void CompressionsLZW() {
            if (!Directory.Exists(_environment.WebRootPath + "\\Compressions\\"))
            {
                Directory.CreateDirectory(_environment.WebRootPath + "\\Compressions\\");
            }
            MetodosNecesariosL _MetodosLzw = new MetodosNecesariosL();
            _MetodosLzw.ProcesoEscrituraDatosLZW(_environment.WebRootPath + "\\Compressions\\");
        }
        /////////////////////////////////////////////
        /// <summary>
        /// Laboratorio 4, En este proceso se tendra los cifrados Ceaser, ZigZag, Espiral, Vertical
        /// </summary>
        /// <param name="_DataCipher">Clase que contiene todos los datos necesarios para la implementacion correcta de los cifrados</param>
        /// <param name="cifrado">Tipo de cifrado</param>
        [HttpPost("cipher/{cifrado}")]
        public void CifradosDatos([FromForm]DataCipher _DataCipher,string cifrado) {
            ZigZag _Zigzag = new ZigZag();
            MetodosNecesariosC _MetodosCipher = new MetodosNecesariosC();
            switch (cifrado.ToUpper())
            {
                case "CEASER":
                    if (!Directory.Exists(_environment.WebRootPath + "\\ArchivoCifradoCeaser\\"))
                    {
                        Directory.CreateDirectory(_environment.WebRootPath + "\\ArchivoCifradoCeaser\\");
                    }
                    _MetodosCipher.CifradoCeaser(_DataCipher.ArchivoCargado, _DataCipher.Llave,_DataCipher.NuevoNombre, _environment.WebRootPath + "\\ArchivoCifradoCeaser\\");
                    break;
                case "ZIGZAG":
                    if (!Directory.Exists(_environment.WebRootPath + "\\ArchivoCifradoZigZag\\"))
                    {
                        Directory.CreateDirectory(_environment.WebRootPath + "\\ArchivoCifradoZigZag\\");
                    }
                    _Zigzag.CifradoZigZag(_DataCipher.ArchivoCargado, _DataCipher.TamanoCarriles, _DataCipher.NuevoNombre, _environment.WebRootPath + "\\ArchivoCifradoZigZag\\");
                    break;
                case "ESPIRAL":
                    if (!Directory.Exists(_environment.WebRootPath + "\\ArchivoCifradoEspiral\\"))
                    {
                        Directory.CreateDirectory(_environment.WebRootPath + "\\ArchivoCifradoEspiral\\");
                    }
                    _MetodosCipher.CifradoEspiral(_DataCipher.ArchivoCargado, _DataCipher.n, _DataCipher.m, _DataCipher.NuevoNombre, _environment.WebRootPath + "\\ArchivoCifradoEspiral\\");
                    break;
                case "VERTICAL":
                    if (!Directory.Exists(_environment.WebRootPath + "\\ArchivoCifradoVertical\\"))
                    {
                        Directory.CreateDirectory(_environment.WebRootPath + "\\ArchivoCifradoVertical\\");
                    }
                    _MetodosCipher.CifradoVertical(_DataCipher.ArchivoCargado, _DataCipher.n, _DataCipher.m, _DataCipher.NuevoNombre, _environment.WebRootPath + "\\ArchivoCifradoVertical\\");
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// Laboratorio 4, En este proceso se tendra los decifrados Ceaser, ZigZag, Espiral, Vertical
        /// </summary>
        /// <param name="_DataDeCipher">Clase que contiene todos los datos necesarios para la implementacion correcta de los decifrados</param>
        /// <param name="decifrado">Tipo de decifrado</param>
        [HttpPost("decipher/{decifrado}")]
        public void DecifradoDatos([FromForm]DataCipher _DataDeCipher, string decifrado)
        {
            MetodosNecesariosC _MetodosDecipher = new MetodosNecesariosC();
            ZigZag _Zigzag = new ZigZag();
            switch (decifrado.ToUpper())
            {
                case "CEASER":
                    if (!Directory.Exists(_environment.WebRootPath + "\\ArchivoDecifradoCeaser\\"))
                    {
                        Directory.CreateDirectory(_environment.WebRootPath + "\\ArchivoDecifradoCeaser\\");
                    }
                    _MetodosDecipher.DecifradoCeaser(_DataDeCipher.ArchivoCargado, _DataDeCipher.Llave, _DataDeCipher.NuevoNombre, _environment.WebRootPath + "\\ArchivoDecifradoCeaser\\");
                    break;
                case "ZIGZAG":
                    if (!Directory.Exists(_environment.WebRootPath + "\\ArchivoDecifradoZigZag\\"))
                    {
                        Directory.CreateDirectory(_environment.WebRootPath + "\\ArchivoDecifradoZigZag\\");
                    }
                    _Zigzag.DescifradoZigZag(_DataDeCipher.ArchivoCargado, _DataDeCipher.TamanoCarriles, _DataDeCipher.NuevoNombre, _environment.WebRootPath + "\\ArchivoDecifradoZigZag\\");
                    break;
                case "ESPIRAL":
                    if (!Directory.Exists(_environment.WebRootPath + "\\ArchivoDecifradoEspiral\\"))
                    {
                        Directory.CreateDirectory(_environment.WebRootPath + "\\ArchivoDecifradoEspiral\\");
                    }
                    _MetodosDecipher.DecifradoEspiral(_DataDeCipher.ArchivoCargado, _DataDeCipher.n, _DataDeCipher.m, _DataDeCipher.NuevoNombre, _environment.WebRootPath + "\\ArchivoDecifradoEspiral\\");
                    break;
                case "VERTICAL":
                    if (!Directory.Exists(_environment.WebRootPath + "\\ArchivoDecifradoVertical\\"))
                    {
                        Directory.CreateDirectory(_environment.WebRootPath + "\\ArchivoDecifradoVertical\\");
                    }
                    _MetodosDecipher.DecifradoVertical(_DataDeCipher.ArchivoCargado, _DataDeCipher.n, _DataDeCipher.m, _DataDeCipher.NuevoNombre, _environment.WebRootPath + "\\ArchivoDecifradoVertical\\");
                    break;
                default:
                    break;
            }

        }
    }

   
}