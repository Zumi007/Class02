using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp6
{
    /* Készíts egy TERMEK osztályt mely a raktárban található termékeket írja le!
Egy terméket az alábbiak jellemzik:
- Név
- Ára
- Készlet

Az adattagokat csak tagfüggvényeken keresztül érhetjük el. Lekérdezni vagy módosítani csak rajtuk keresztül lehet. 
Készíts Konstruktort az adattagok első beállításához!
     */
    class TERMEK
    {
        private string nev;
        private double ara;
        private int keszlet;

        public  void Set_Nev(string _nev)
        {
            nev = _nev;

        }

        public void Set_Ar(double _ar)
        {
            ara = _ar;

        }

        public string Get_Nev()
        {
            return nev;
        }

        public double Get_Ar()
        {
            return ara;
        }

        public int Get_Keszlet()
        {
            return keszlet;
        }

        public TERMEK(string _nev, double _ara, int _keszlet) {
            nev = _nev;
            ara = _ara;
            keszlet = _keszlet;
        }

        public TERMEK(string line)
        {
            string[] splitLine = line.Split(';');

            nev = splitLine[0];
            ara = Double.Parse(splitLine[1]);
            keszlet = Int32.Parse(splitLine[2]);
        }
        
        public bool Kivet(int mennyiseg)
        {
            if(keszlet - mennyiseg > 0)
            {
                keszlet -= mennyiseg;
                return true;
            }
            return false;       
        }

        /*Az alábbi adokat kézzel másold ki egy Termekek.txt fájlba, majd írj egy programot mely soronként beolvassa az adatokat, mindegyikből készít egy TERMEK típusú példányt melyet egy listában letárol.
A lista tartalmát írja ki a Képernyőre!*/
    }

    internal class Program
    {
       static List<TERMEK> lista = new List<TERMEK>();
        
        static void Main(string[] args)
        {
            using (StreamReader olvaso = new StreamReader(@"c:\Temp\Termekek.txt"))
            {
                while(!olvaso.EndOfStream) 
                {
                    //string sor = olvaso.ReadLine();
                    //string[] adat = sor.Split(';');
                    TERMEK termek = new TERMEK(olvaso.ReadLine());
                    lista.Add(termek);
                }
            }

            Console.WriteLine($"A termékek: \n");

            foreach (TERMEK sor in lista)
            {
                Console.WriteLine($"{sor.Get_Nev()}\t{sor.Get_Ar()}\t{sor.Get_Keszlet()} ");
            }

            TERMEK minTermek = null;
            double min = int.MaxValue;

            foreach (TERMEK item in lista)
            { 
                if (item.Get_Ar() < min)
                {
                    min = item.Get_Ar();
                    minTermek = item;
                }
            }

            Console.WriteLine($"A legolcsóbb termék {minTermek.Get_Nev()} ({minTermek.Get_Ar()})");
            Console.WriteLine($"A raktár készlet: {RaktarKeszlet().ToString("C2")}");

            foreach (TERMEK item in lista)
            {
                item.Set_Ar(item.Get_Ar() * 1.10);
            }

            Console.WriteLine($"A raktár készlet: {RaktarKeszlet().ToString("C2")}");

            Console.ReadKey();

        }

        static double RaktarKeszlet()
        {
            double sum = 0;

            foreach (TERMEK item in lista)
            {
                sum += item.Get_Ar() * item.Get_Keszlet();
            }

            return sum;
        }
    }
}
