using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlayerOrder2
{
    class Program
    {
        const double minProba = 0.05;

        static void SearchOrder(int N, int n)
        {
            double probaMax = -1;
            object mutexProbaMax = new object();

            for (int i = 0; i < N; i++)
            {
                var diceA = new Dice("A");
                var diceB = new Dice("B");
                double pAB = diceA.ProbaVictory(diceB);
                if (Math.Abs(pAB) > minProba)
                {
                    if (pAB < 0)
                    {
                        var tmp = diceA;
                        diceA = diceB;
                        diceB = tmp;
                        pAB = -pAB; // pAB>0
                    }
                    for (int j = 0; j < n; j++)
                    {
                        var diceC = new Dice("C");
                        double pBC = diceB.ProbaVictory(diceC);
                        double pCA = diceC.ProbaVictory(diceA);
                        if (pBC > minProba && pCA > minProba)
                        {
                            double s = pAB + pBC + pCA;
                            if (s > probaMax)
                            {
                                Console.WriteLine(diceA);
                                Console.WriteLine(diceB);
                                Console.WriteLine(diceC);
                                Console.WriteLine($"A>B: {pAB}");
                                Console.WriteLine($"B>C: {pBC}");
                                Console.WriteLine($"C>A: {pCA}");
                                probaMax = s;
                                Console.WriteLine($"Sum proba: {probaMax}");
                            }
                        }
                    }
                }
                if (i % 1000 == 0)
                    Console.WriteLine($"i={i}");
            }
        }

        static void Main(string[] args)
        {
            CultureInfo.DefaultThreadCurrentCulture = CultureInfo.InvariantCulture;
            CultureInfo.DefaultThreadCurrentUICulture = CultureInfo.InvariantCulture;
            try
            {
                Dice.SetFaces(4);
                SearchOrder(10000, 100000);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }
}
