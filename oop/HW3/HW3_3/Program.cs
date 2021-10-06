using System;

namespace HW3_3
{
    class Program
    {
        static void Main(string[] args)
        {
            const string PROMPT = "Pick one:\n" +
                "1) Convert usd to uah\n" +
                "2) Convert eur to uah\n" +
                "3) Convert uah to usd\n" +
                "4) Convert uah to eur\n" +
                "5) Quit";

            Converter converter = new Converter(26.3, 30.5);

            while (true)
            {
                Console.WriteLine(PROMPT);
                string option = Console.ReadLine();
                if (option == "1")
                {
                    Console.Write("usd: ");
                    double usd = Console.Read();
                    Console.ReadLine();
                    Console.Write("uah: ");
                    Console.WriteLine("{0:F}", converter.UsdToUah(usd));
                }
                else if (option == "2")
                {
                    Console.Write("eur: ");
                    double eur = Console.Read();
                    Console.ReadLine();
                    Console.Write("uah: ");
                    Console.WriteLine("{0:F}", converter.EurToUah(eur));
                }
                else if (option == "3")
                {
                    Console.Write("uah: ");
                    double uah = Console.Read();
                    Console.ReadLine();
                    Console.Write("usd: ");
                    Console.WriteLine("{0:F}", converter.UahToUsd(uah));
                }
                else if (option == "4")
                {
                    Console.Write("uah: ");
                    double uah = Console.Read();
                    Console.ReadLine();
                    Console.Write("eur: ");
                    Console.WriteLine("{0:F}", converter.UahToEur(uah));
                }
                else if (option == "5")
                {
                    Console.WriteLine("Bye!");
                    break;
                }
                else
                {
                    Console.WriteLine("Wrong option!");
                }
            }
        }
    }

    class Converter
    {
        private double usd;
        private double eur;

        public Converter(double usd, double eur)
        {
            this.usd = usd;
            this.eur = eur;
        }

        public double UahToUsd(double uah)
        {
            return uah / usd;
        }

        public double UahToEur(double uah)
        {
            return uah / eur;
        }

        public double UsdToUah(double usd)
        {
            return usd * this.usd;
        }

        public double EurToUah(double eur)
        {
            return eur * this.eur;
        }
    }
}
