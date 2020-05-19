using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Lab03_EDII.Lzw;


    
namespace Lab03_EDII.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ManejoArchivoController : ControllerBase
    {
        public static IWebHostEnvironment _environment;
        byte[] dataByte;
        byte[] DataComprimida;
        public ManejoArchivoController(IWebHostEnvironment environment)
        {
            _environment = environment;
        }

        public class FileUploadFromApi
        {
            public IFormFile files { get; set; }
        }
        [HttpGet("compressHuffman")]
        public async Task<string> Compresion([FromForm] FileUploadFromApi objFile)
        {
            if (objFile.files != null)
            {
                EscrituraD dataEscritura = new EscrituraD();
                var CapturarArchivo = new StringBuilder();
                using (var Lector = new StreamReader(objFile.files.OpenReadStream()))
                {
                    while (Lector.Peek() >= 0)
                    {
                        CapturarArchivo.AppendLine(Lector.ReadLine());
                    }
                }
                string ArchivoConvertidoEnBytes = CapturarArchivo.ToString();
                /*if (ArchivoConvertidoEnBytes.Contains("\r\n"))
                {
                    ArchivoConvertidoEnBytes = ArchivoConvertidoEnBytes.Replace("\r\n", "");
                }*/
                dataEscritura.ComprimirData(ArchivoConvertidoEnBytes, objFile.files.FileName);
                try
                {
                    if (objFile.files.Length > 0)
                    {
                        if (!Directory.Exists(_environment.WebRootPath + "\\ArchivosComprimir\\"))
                        {
                            Directory.CreateDirectory(_environment.WebRootPath + "\\ArchivosComprimir\\");
                        }
                        using (FileStream fileStream = System.IO.File.Create(_environment.WebRootPath + "\\ArchivosComprimir\\" + objFile.files.FileName))
                        {

                            objFile.files.CopyTo(fileStream);
                            fileStream.Flush();
                            return "\\ArchivoEntrada\\" + objFile.files.FileName;

                        }

                    }

                    else
                    {
                        return "Failed";
                    }
                }
                catch (Exception ex)
                {
                    return ex.Message.ToString();
                } 
            }
            else
            {
                return "No se ha agregado ningun archivo para comprimir, Utiliza la Herramienta Postman para enviar un Archivo";
            }

        }

        [HttpGet("descompressHuffman")]
        public async Task<string> Descompresion([FromForm] FileUploadFromApi objFile) {
            if (objFile.files !=  null)
            {
                EscrituraD dataEscritura = new EscrituraD();
                var CapturarArchivo = new StringBuilder();
                using (var Lector = new StreamReader(objFile.files.OpenReadStream()))
                {
                    while (Lector.Peek() >= 0)
                    {
                        CapturarArchivo.AppendLine(Lector.ReadLine());
                    }
                }

                string ArchivoConvertidoEnBytes = CapturarArchivo.ToString();

                if (ArchivoConvertidoEnBytes.Contains("\r\n"))
                {
                    ArchivoConvertidoEnBytes = CapturarArchivo.ToString();
                }
                dataEscritura.DescomprimirData(ArchivoConvertidoEnBytes, objFile.files.FileName);
                try
                {
                    if (objFile.files.Length > 0)
                    {
                        if (!Directory.Exists(_environment.WebRootPath + "\\ArchivosDesComprimidos\\"))
                        {
                            Directory.CreateDirectory(_environment.WebRootPath + "\\ArchivosDesComprimidos\\");
                        }
                        using (FileStream fileStream = System.IO.File.Create(_environment.WebRootPath + "\\ArchivosDesComprimidos\\" + objFile.files.FileName))
                        {

                            objFile.files.CopyTo(fileStream);
                            fileStream.Flush();
                            return "\\ArchivoSalida\\" + objFile.files.FileName;

                        }

                    }

                    else
                    {
                        return "Failed";
                    }
                }
                catch (Exception ex)
                {
                    return ex.Message.ToString();
                }

            }
            else
            {
                return "No se ha agregado ningun archivo para descomprimir, Utiliza la Herramienta Postman para enviar un Archivo";
            }


        }

        [HttpGet("compressLzw")]
        public async Task<string> CompressLZW([FromForm] FileUploadFromApi objFile)
        {
            if (objFile.files != null)
            {
                try
                {
                    if (objFile.files.Length > 0)
                    {
                        if (!Directory.Exists(_environment.WebRootPath + "\\ArchivosAComprimirLZW\\"))
                        {
                            Directory.CreateDirectory(_environment.WebRootPath + "\\ArchivosAComprimirLZW\\");
                        }
                        using (FileStream fileStream = System.IO.File.Create(_environment.WebRootPath + "\\ArchivosAComprimirLZW\\" + objFile.files.FileName))
                        {
                            CompresionLZW CompresionLzw = new CompresionLZW();
                            objFile.files.CopyTo(fileStream);
                            fileStream.Flush();
                            fileStream.Close();
                            CompresionLzw.ComprimirLZW(objFile.files.FileName, _environment.WebRootPath + "\\ArchivosAComprimirLZW\\");
                            return "\\ArchivosAComprimirLZW\\" + objFile.files.FileName;

                        }

                    }

                    else
                    {
                        return "Failed";
                    }
                }
                catch (Exception ex)
                {
                    return ex.Message.ToString();
                }

            }
            else
            {
                return "No se ha agregado ningun archivo para comprimir, Utiliza la Herramienta Postman para enviar un Archivo";
            }


        }

        [HttpGet("descompressLzw")]
        public async Task<string> DescompressLZW([FromForm] FileUploadFromApi objFile)
        {
            if (objFile.files != null)
            {
                try
                {
                    if (objFile.files.Length > 0)
                    {
                        if (!Directory.Exists(_environment.WebRootPath + "\\ArchivosADescomprimirLZW\\"))
                        {
                            Directory.CreateDirectory(_environment.WebRootPath + "\\ArchivosADescomprimirLZW\\");
                        }
                        using (FileStream fileStream = System.IO.File.Create(_environment.WebRootPath + "\\ArchivosADescomprimirLZW\\" + objFile.files.FileName))
                        {
                            DescompresionLZW DescompresionLzw = new DescompresionLZW();
                            objFile.files.CopyTo(fileStream);
                            fileStream.Flush();
                            fileStream.Close();
                            DescompresionLzw.DescomprimirLZW(objFile.files.FileName,_environment.WebRootPath + "\\ArchivosADescomprimirLZW\\");
                            return "\\ArchivosADescomprimirLZW\\" + objFile.files.FileName;
                        }

                    }

                    else
                    {
                        return "Failed";
                    }
                }
                catch (Exception ex)
                {
                    return ex.Message.ToString();
                }

            }
            else
            {
                return "No se ha agregado ningun archivo para descomprimir, Utiliza la Herramienta Postman para enviar un Archivo";
            }


        }
    }
}