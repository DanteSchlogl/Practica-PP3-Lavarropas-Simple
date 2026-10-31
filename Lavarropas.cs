using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;

namespace LavarropasSimple
{
    // Una etapa del lavado: el nombre y cuanto dura.
    class Etapa
    {
        public string Nombre { get; set; }
        public int Segundos { get; set; }

        public Etapa(string nombre, int segundos)
        {
            Nombre = nombre;
            Segundos = segundos;
        }
    }

    // El lavarropas hace todo: guarda las etapas, lava y anota en el archivo.
    class Lavarropas
    {
        private readonly string modelo;
        private readonly string carpeta;
        private readonly List<Etapa> etapas = new List<Etapa>();

        public string EtapaActual { get; set; }

        public Lavarropas(string modelo, string carpeta)
        {
            this.modelo = modelo;
            this.carpeta = carpeta;
            EtapaActual = "Apagado";
        }

        public void AgregarEtapa(string nombre, int segundos)
        {
            if (segundos <= 0)
            {
                throw new ArgumentException("La etapa " + nombre + " tiene que durar mas de 0 segundos.");
            }
            etapas.Add(new Etapa(nombre, segundos));
        }

        // Hace el lavado completo, una etapa atras de la otra.
        public void Lavar()
        {
            if (etapas.Count == 0)
            {
                throw new ArgumentException("El lavarropas no tiene ninguna etapa cargada.");
            }

            Registrar("Empieza el lavado");

            for (int i = 0; i < etapas.Count; i++)
            {
                Etapa etapa = etapas[i];
                EtapaActual = etapa.Nombre;
                Console.WriteLine("Etapa " + (i + 1) + " de " + etapas.Count + ": " + etapa.Nombre);

                // Aca esta el delay: espera lo que dura la etapa.
                Thread.Sleep(etapa.Segundos * 1000);

                Registrar(etapa.Nombre + " listo (" + etapa.Segundos + " s)");
                Console.WriteLine(etapa.Nombre + " listo");
            }

            EtapaActual = "Terminado";
            Registrar("Fin del lavado");
            Console.WriteLine("Fin del lavado");
        }

        // Anota una linea en el archivo del dia.
        private void Registrar(string mensaje)
        {
            try
            {
                Directory.CreateDirectory(carpeta);

                string nombre = "lavado_" + DateTime.Now.ToString("yyyy-MM-dd") + ".txt";
                string linea = DateTime.Now.ToString("HH:mm:ss") + " | " + modelo + " | " + mensaje;

                File.AppendAllText(Path.Combine(carpeta, nombre), linea + Environment.NewLine);
            }
            catch (Exception ex)
            {
                Console.WriteLine("No se pudo anotar en el archivo: " + ex.Message);
            }
        }
    }
}
