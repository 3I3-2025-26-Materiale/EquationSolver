using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EquationSolver
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Inserire il numero di incognite
            Console.Write("Inserire il numero di incognite: ");
            int nIncognite = int.Parse(Console.ReadLine()); // N

            // Inserire il numero di equazioni
            // Console.Write("Inserire il numero di equazioni: ");
            // int nEquazioni = int.Parse(Console.ReadLine()); // M
            int nEquazioni = nIncognite; // M

            // Il sistema di equazioni verrà rappresentato tramite
            // un prodotto matriciale A*X = B
            // Dove:
            // - A è la matrice dei coefficienti
            // - X è il vettore colonna delle incognite
            // - B è il vettore colonna dei termini noti
            // I vettori colonna saranno rappresentati tramite normali array.

            // A e B contengono coefficienti a scelta dell'utente, quindi sono
            // rappresentati come variabili. X, cioè l'array delle incognite,
            // non è a scelta dell'utente, quindi non ha una variabile associata,
            // ma sarà considerato direttamente come parte dell'algoritmo.

            float[,] coefficienti = new float[nEquazioni, nIncognite];
            float[] terminiNoti = new float[nEquazioni];

      

            bool inserimentoRiuscito = inserisciSistemaEquazioni(coefficienti, terminiNoti);
            if (inserimentoRiuscito)
            {
                Console.WriteLine();
                Console.WriteLine("Hai inserito il seguente sistema di equazioni:");
                stampaSistemaEquazioni(coefficienti, terminiNoti);
            }
            else
            {
                Console.WriteLine("ERRORE DI PROGRAMMAZIONE: Le matrici non hanno una dimensione coerente.");
            }

            Console.ReadLine();
        }

        static bool inserisciSistemaEquazioni(float[,] coefficienti, float[] terminiNoti)
        {
            if (coefficienti.GetLength(0) != terminiNoti.Length)
                return false; // le matrici non hanno delle dimensioni coerenti

            int nEquazioni = coefficienti.GetLength(0);
            int nIncognite = coefficienti.GetLength(1);

            // riempimento delle matrici tramite input da console.
            for (int indiceEquazione = 0; indiceEquazione < nEquazioni; indiceEquazione++)
            {
                Console.WriteLine($"Inserimento dell'equazione {indiceEquazione + 1}");

                // Inserire i coefficienti delle incognite
                for (int indiceIncognita = 0; indiceIncognita < nIncognite; indiceIncognita++)
                {
                    Console.Write($"Inserire il coefficiente di x{indiceIncognita + 1}: ");
                    float coefficiente = float.Parse(Console.ReadLine());
                    coefficienti[indiceEquazione, indiceIncognita] = coefficiente;
                }

                Console.Write("Inserire il termine noto: ");
                float termineNoto = float.Parse(Console.ReadLine());
                terminiNoti[indiceEquazione] = termineNoto;

                Console.WriteLine();
            }

            return true;
        }

        static void stampaSistemaEquazioni(float[,] coefficienti, float[] terminiNoti)
        {
            if (coefficienti.GetLength(0) != terminiNoti.Length)
                return; // le matrici non hanno delle dimensioni coerenti

            int nEquazioni = coefficienti.GetLength(0);
            int nIncognite = coefficienti.GetLength(1);

            for (int indiceEquazione = 0; indiceEquazione < nEquazioni; indiceEquazione++)
            {
                string equazione = "";
                for (int indiceIncognita = 0; indiceIncognita < nIncognite; indiceIncognita++)
                {
                    float coefficiente = coefficienti[indiceEquazione, indiceIncognita];

                    string segno = "";
                    if (coefficiente > 0 && indiceIncognita > 0)
                        segno = "+";
                    // se invece il coefficiente è negativo,
                    // il segno - viene aggiunto direttamente dalla conversione in stringa
                    // del coefficiente stess.

                    equazione += $"{segno}{coefficiente}x{indiceIncognita+1} ";
                }

                equazione += "= " + terminiNoti[indiceEquazione];

                Console.WriteLine(equazione);
            }
        }
    }
}
