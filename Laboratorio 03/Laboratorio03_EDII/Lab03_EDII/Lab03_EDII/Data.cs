using System.Collections.Generic;

namespace Lab03_EDII
{
    public class Data
    {
		private static Data instancia = null;
		public static Data Instance
		{
			get
			{
				if (instancia == null) instancia = new Data();
				return instancia;
			}
		}
		public List<Compresiones> historialArchivosComprimidos = new List<Compresiones>();
	}
}
