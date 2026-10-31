using System;

namespace LavarropasSimple
{
    class Program
    {
        static void Main()
        {
            try
            {
                Lavarropas lava = new Lavarropas("Whirlpool", "Registros");

                // Cada etapa se demora lo que dice el segundo numero.
                lava.AgregarEtapa("Llenar", 1);
                lava.AgregarEtapa("Lavar", 3);
                lava.AgregarEtapa("Enjuagar", 2);
                lava.AgregarEtapa("Centrifugar", 2);

                Console.WriteLine("Arranca el lavado");
                lava.Lavar();
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }
    }
}
