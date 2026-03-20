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
            
        }

        static bool inserisciSistemaEquazioni(float[,] coefficienti, float[] terminiNoti)
        {
            if (coefficienti.GetLength(0) != terminiNoti.Length)
                return false; // le matrici non hanno delle dimensioni coerenti

            return true;
            // riempimento delle matrici tramite input da console.
        }

        static void stampaSistemaEquazioni(float[,] coefficienti, float[] terminiNoti)
        {
            if (coefficienti.GetLength(0) != terminiNoti.Length)
                return; // le matrici non hanno delle dimensioni coerenti

            // stampa del sistema di equazioni.
        }
    }
}
