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

            float[] soluzioni = new float[nIncognite];

            bool inserimentoRiuscito = false;
            while (!inserimentoRiuscito)
            {
                inserimentoRiuscito = inserisciSistemaEquazioni(coefficienti, terminiNoti);
                if (inserimentoRiuscito)
                {
                    Console.WriteLine();
                    Console.WriteLine("Hai inserito il seguente sistema di equazioni:");
                    stampaSistemaEquazioni(coefficienti, terminiNoti);

                    Console.WriteLine();
                    Console.Write("Confermi l'inserimento delle equazioni [S/N]? :");

                    if (Console.ReadLine().ToLower() != "s")
                    {
                        inserimentoRiuscito = false;
                    }
                }
                else
                {
                    Console.WriteLine("ERRORE DI PROGRAMMAZIONE: Le matrici non hanno una dimensione coerente.");
                }
            }

            soluzioni = risolvi(coefficienti, terminiNoti);

            if (soluzioni == null || soluzioni.Length == 0)
            {
                Console.WriteLine("Il sistema non è risolvibile");
            }
            else
            {
                Console.WriteLine("Soluzione del sistema:");
                for (int i = 0; i < soluzioni.Length; i++)
                {
                    Console.WriteLine($"x{i + 1} = {soluzioni[i]}");
                }
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
                    // del coefficiente stesso.

                    // se il coefficiente non è 0 (gestendo l'errore di approssimazione dei float)
                    // mostro il termine
                    if (Math.Abs(coefficiente) < 0.001)
                    {
                        // mostro il coefficiente solo se è diverso da 1
                        // (gestendo l'errore di approssimazione dei float)
                        if (Math.Abs(coefficiente - 1) < 0.001)
                        {
                            equazione += $"{segno}x{indiceIncognita + 1} ";
                        }
                        else
                        {
                            equazione += $"{segno}{coefficiente}x{indiceIncognita + 1} ";
                        }
                    }
                }

                equazione += "= " + terminiNoti[indiceEquazione];

                Console.WriteLine(equazione);
            }
        }

        static float[] risolvi(float[,] coefficienti, float[] terminiNoti)
        {
            // controllo degli errori sulle dimensioni delle matrici:
            // il sistema è risolvibile solo se coefficienti è quadrata
            // e se terminiNoti ha dimensione pari all'ordine di coerfficienti
            if (coefficienti != null && terminiNoti != null
                && coefficienti.GetLength(0) == coefficienti.GetLength(1)
                && coefficienti.GetLength(0) == terminiNoti.Length)
            {
                /*
                 per ogni incognita:
                    - Sostituisce i termini noti alla colonna dei coefficienti dell'incognita
                       calcolo di Dxi
                    - Calcola il rapporto del determinante della matrice con i coefficienti
                       sostituiti e del determinante della matrice dei coefficienti originale
                       xi = Dxi / D
                 */
                int numeroIncognite = terminiNoti.Length;
                float[] soluzione = new float[numeroIncognite];
                float denominatore = calcolaDeterminante(coefficienti);

                if (Math.Abs(denominatore) > 0.001) // se il denominatore non è 0, il sistema è risolvibile
                {
                    for (int i = 0; i < numeroIncognite; i++)
                    {
                        float[,] coefficientiMod = sostituisciColonna(coefficienti, terminiNoti, i);
                        float numeratore = calcolaDeterminante(coefficientiMod);
                        float soluzioneIncognita = numeratore / denominatore;
                        soluzione[i] = soluzioneIncognita;
                    }
                    return soluzione;
                }
            }

            return null;
        }

        static float[,] sostituisciColonna(float[,] matrice, float[] colonnaNuova, int indiceColonnaDaSostituire)
        {
            // creo una NUOVA matrice di dimensioni pari a quella originale,
            // per non sostituire la colonna sulla matrice originale, che deve
            // invece rimanere invariata, altrimenti i conti successivi avrebbero
            // errore.
            if (matrice != null)
            {
                float[,] risultato = new float[matrice.GetLength(0), matrice.GetLength(1)];

                for (int i = 0; i < risultato.GetLength(0); i++)
                {
                    for (int j = 0; j < risultato.GetLength(1); j++)
                    {
                        if (j == indiceColonnaDaSostituire)
                        {
                            // se la colonna è quella da sostituire,
                            // inserisco un valore preso dalla nuova colonna
                            if (colonnaNuova != null && i < colonnaNuova.Length)
                            {
                                risultato[i, j] = colonnaNuova[i];
                            }
                            else
                            {
                                risultato[i, j] = matrice[i, j];
                            }
                        }
                        else
                        {
                            // altrimenti, ricopio il valore vecchio
                            risultato[i, j] = matrice[i, j];
                        }
                    }
                }

                return risultato;
            }

            return null;
        }

        static float calcolaDeterminante(float[,] matrice)
        {
            // il calcolo del determinante ha senso solo su matrici quadrate
            if (matrice != null
                && matrice.GetLength(0) == matrice.GetLength(1))
            {
                if (matrice.GetLength(0) == 1)
                {
                    return matrice[0, 0];
                }
                else if (matrice.GetLength(0) == 2)
                {
                    return matrice[0, 0] * matrice[1, 1] - matrice[0, 1] * matrice[1, 0];
                }
                else if (matrice.GetLength(0) == 3)
                {
                    /*
                    return matrice[0, 0] * matrice[1, 1] * matrice[2, 2]
                         + matrice[0, 1] * matrice[1, 2] * matrice[2, 0]
                         + matrice[0, 2] * matrice[1, 0] * matrice[2, 1]
                         - matrice[0, 2] * matrice[1, 1] * matrice[2, 0]
                         - matrice[0, 0] * matrice[1, 2] * matrice[2, 1]
                         - matrice[0, 1] * matrice[1, 0] * matrice[2, 2]
                    ;
                    */

                    float risultato = 0;
                    for (int j = 0; j < 3; j++)
                    {
                        float diagonale1 = 1;
                        float diagonale2 = 1;
                        for (int i = 0; i < 3; i++)
                        {
                            diagonale1 *= matrice[i, (j + i + 3) % 3];
                            diagonale2 *= matrice[i, (j - i + 3) % 3];
                        }
                        risultato += diagonale1 - diagonale2;
                    }
                    return risultato;
                }
            }
            return 0f; // 0 è un valore valido, sarebbe meglio lanciare eccezione...
        }
    }
}
