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
		private static string Ruta { get; set; } = "juego.cfg";
		private static Dictionary<string, string> valores { get; set; } = new Dictionary<string, string>();

		static Configuracion()
		{
			if (File.Exists(Ruta)) valores = LeerArchivo(Ruta);
			else File.Create(Ruta);
		}

		public static string GetString(string clave)
		{
			if (valores.ContainsKey(clave))
				return valores[clave];
			return "";
		}

		public static int GetInt32(string clave)
		{
			if (int.TryParse(GetString(clave), out int valor))
				return valor;
			return -1;
		}

		public static void Guardar(string clave, string valor)
		{
			valores[clave] = valor;
			if (!SePuedeAbrirArchivo(Ruta)) return;
			using (StreamWriter writer = new StreamWriter(Ruta))
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

			if (!SePuedeAbrirArchivo(ruta)) return valores;
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

		public static bool SePuedeAbrirArchivo()
		{
			return SePuedeAbrirArchivo(Ruta);
		}

		public static bool SePuedeAbrirArchivo(string ruta)
		{
			if (!File.Exists(ruta)) 
				return false;

			FileStream stream = null;

			try {
				stream = File.Open(ruta, FileMode.Open, FileAccess.ReadWrite, FileShare.None);
			} catch (IOException) {
				return false;
			}
			finally {
				if (stream != null)
					stream.Close();
			}

			return true;
		}
	}
}
