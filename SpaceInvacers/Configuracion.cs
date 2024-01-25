using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace SpaceInvacers
{
	class Configuracion
	{
		private static string ruta { get; set; } = "juego.cfg";
		private static Dictionary<string, string> valores { get; set; } = new Dictionary<string, string>();

		static Configuracion()
		{
			if (File.Exists(ruta)) valores = LeerArchivo(ruta);
			else File.Create(ruta);
		}

		public static string GetString(string clave)
		{
			if (valores.ContainsKey(clave))
				return valores[clave];

			return "";
		}

		public static int GetInt32(string clave)
		{
			if (valores.ContainsKey(clave))
				if (int.TryParse(valores[clave], out int valor))
					return valor;
			return -1;
		}

		public static void Guardar(string clave, string valor)
		{
			valores[clave] = valor;
			if (!File.Exists(ruta)) return;
			using (StreamWriter writer = new StreamWriter(ruta))
				foreach (var entry in valores)
					writer.WriteLine($"{entry.Key}: {entry.Value}");
		}

		public static void Guardar(string clave, int valor)
		{
			Guardar(clave, valor.ToString());
		}

		static Dictionary<string, string> LeerArchivo(string ruta)
		{
			Dictionary<string, string> valores = new Dictionary<string, string>();

			if (!File.Exists(ruta)) return valores;
			using (StreamReader reader = new StreamReader(ruta))
				while (!reader.EndOfStream) {
					string linea = reader.ReadLine();
					string[] partes = linea.Split(':');

					if (partes.Length == 2) {
						string clave = partes[0].Trim();
						string valor = partes[1].Trim();
						valores[clave] = valor;
					}
				}

			return valores;
		}
	}
}
