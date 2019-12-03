using GeneticSharp.Domain.Crossovers;
using System;
using System.Collections.Generic;
using System.Text;

namespace Projekt_Genetyczny
{
    class Experiments
    {
        private static int repeatCount = 20;
        private static Boolean showCurrent = false;
        public static Boolean doOutput = false;

        public static List<Wynik> testCommon(int counter, Item[] item, double back_volume, String fileName)
        {
            List<Wynik> wyniki = new List<Wynik>();

            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append("Wartość spakowanych itemków\tWaga spakowanych itemków\tCzas pakowania\tCzy poprawne\r\n");
            for (int rp = 0; rp< repeatCount*8; rp++)
            {
                Wynik wynik = new Wynik();
                if (showCurrent) if (Experiments.doOutput) System.Console.WriteLine("testCommon  " + rp);
                Boolean isOK = true;
                wynikGA w = GeneticsMethods.Genetic(counter - 1, item, back_volume);



                double[] wn = w.ch.ToFloatingPoints();
                double pack_items_volume = 0;
                double pack_items_value = 0;
                for (int j = 0; j < wn.Length; j++)
                {
                    pack_items_volume += wn[j] * item[j].item_volume;
                    pack_items_value += wn[j] * item[j].item_value;
                    if (item[j].item_volume < 0) isOK = false;
                }



                //  if (Experiments.doOutput) System.Console.WriteLine("Objetosc plecaka: " + (back_volume.ToString("F")));
                if (Experiments.doOutput) System.Console.WriteLine("Objetosc spakowanych rzeczy: " + (pack_items_volume.ToString("F")));
                wynik.items_volume = pack_items_volume;
                if (Experiments.doOutput) System.Console.WriteLine("Czas: " + w.ga.TimeEvolving.TotalSeconds);
                wynik.time = w.ga.TimeEvolving.TotalSeconds;
                /*
                for (int j = 0; j < wn.Length; j++) System.Console.Write(item[j].item_volume + "\t");
                if (Experiments.doOutput) System.Console.WriteLine("");
                for (int j = 0; j < wn.Length; j++) System.Console.Write(wn[j] + "\t");
                if (Experiments.doOutput) System.Console.WriteLine("");
                for (int j = 0; j < wn.Length; j++) System.Console.Write(MathF.Round((float)(item[j].item_volume * wn[j]), 2) + "\t");
                */
                // if (Experiments.doOutput) System.Console.WriteLine("");
                // if (Experiments.doOutput) System.Console.WriteLine("CrossoverProbability: " + w.ga.CrossoverProbability.ToString("F"));
                //  if (Experiments.doOutput) System.Console.WriteLine("GenerationsNumber: " + w.ga.GenerationsNumber.ToString("F"));
                // if (Experiments.doOutput) System.Console.WriteLine("MutationProbability: " + w.ga.MutationProbability.ToString("F"));
                if (Experiments.doOutput) System.Console.WriteLine("Wartosc: " + pack_items_value.ToString("F"));
                wynik.items_value = pack_items_value;
                if (pack_items_volume > back_volume) { wynik.czypoprawny = false; if (Experiments.doOutput) System.Console.WriteLine("niepoprawny"); }
                else { wynik.czypoprawny = true; if (Experiments.doOutput) System.Console.WriteLine("poprawny"); }
                wyniki.Add(wynik);
                if (Experiments.doOutput) System.Console.WriteLine("");
                if (Experiments.doOutput) System.Console.WriteLine("");
                stringBuilder.Append(System.Math.Round(pack_items_value, 4) + "\t" + System.Math.Round(pack_items_volume, 4) + "\t" + System.Math.Round(w.ga.TimeEvolving.TotalSeconds, 4) + "\t" + (isOK ? "1" : "0") + "\r\n");

            }
            DataTransfer.saveFileWithResults(fileName, stringBuilder);
            return wyniki;
        }

        public static List<Wynik> testPopulationSize(int counter, Item[] item, double back_volume, String fileName)
        {
            List<Wynik> wyniki = new List<Wynik>();

            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append("Wartość spakowanych itemków\tWaga spakowanych itemków\tCzas pakowania\tCzy poprawne\tRozmiar populacji\r\n");
            for (int iPop = 5; iPop < 500; iPop += 5)
            {
                for (int rp = 0; rp < repeatCount; rp++)
                {
                    Wynik wynik = new Wynik();
                    if (showCurrent) if (Experiments.doOutput) System.Console.WriteLine("testPopulationSize  " + iPop + "  " + rp);
                    Boolean isOK = true;
                    wynikGA w = GeneticsMethods.GeneticExt(counter - 1, item, back_volume, iPop);



                    double[] wn = w.ch.ToFloatingPoints();
                    double pack_items_volume = 0;
                    double pack_items_value = 0;
                    for (int j = 0; j < wn.Length; j++)
                    {
                        pack_items_volume += wn[j] * item[j].item_volume;
                        pack_items_value += wn[j] * item[j].item_value;
                        if (item[j].item_volume < 0) isOK = false;
                    }
                    if (Experiments.doOutput) System.Console.WriteLine("Rozmiar populacji: " + iPop);
                    if (Experiments.doOutput) System.Console.WriteLine("Objetosc spakowanych rzeczy: " + (pack_items_volume.ToString("F")));
                    wynik.items_volume = pack_items_volume;
                    if (Experiments.doOutput) System.Console.WriteLine("Czas: " + w.ga.TimeEvolving.TotalSeconds);
                    wynik.time = w.ga.TimeEvolving.TotalSeconds;
                    if (Experiments.doOutput) System.Console.WriteLine("Wartosc: " + pack_items_value.ToString("F"));
                    wynik.items_value = pack_items_value;
                    if (pack_items_volume > back_volume) { wynik.czypoprawny = false; if (Experiments.doOutput) System.Console.WriteLine("niepoprawny"); }
                    else { wynik.czypoprawny = true; if (Experiments.doOutput) System.Console.WriteLine("poprawny"); }
                    wyniki.Add(wynik);
                    if (Experiments.doOutput) System.Console.WriteLine("");
                    if (Experiments.doOutput) System.Console.WriteLine("");
                    if (pack_items_volume > back_volume) isOK = false;
                    stringBuilder.Append(System.Math.Round(pack_items_value, 4) + "\t" + System.Math.Round(pack_items_volume, 4) + "\t" + System.Math.Round(w.ga.TimeEvolving.TotalSeconds, 4) + "\t" + (isOK ? "1" : "0") + "\t" + iPop + "\r\n");

                }
            }
            DataTransfer.saveFileWithResults(fileName, stringBuilder);
            return wyniki;
        }

        public static List<Wynik> testDifferentCrossovers(int counter, Item[] item, double back_volume, String fileName)
        {
            List<Wynik> wyniki = new List<Wynik>();
            List<ICrossover> crossovers = new List<ICrossover>();
            crossovers.Add(new CycleCrossover());
            crossovers.Add(new CutAndSpliceCrossover());
            crossovers.Add(new OnePointCrossover());
            crossovers.Add(new OrderBasedCrossover());
            crossovers.Add(new OrderedCrossover());
            crossovers.Add(new PartiallyMappedCrossover());
            crossovers.Add(new PositionBasedCrossover());
            crossovers.Add(new ThreeParentCrossover());
            crossovers.Add(new TwoPointCrossover());
            crossovers.Add(new UniformCrossover());

            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append("Wartość spakowanych itemków\tWaga spakowanych itemków\tCzas pakowania\tCzy poprawne\tTyp crossovera\r\n");
            foreach (ICrossover crossover in crossovers)
            {
                for (int rp = 0; rp < repeatCount; rp++)
                {
                    if (showCurrent) if (Experiments.doOutput) System.Console.WriteLine("testDifferentCrossovers  " + rp);
                    Boolean isOK = true;
                    if (Experiments.doOutput) System.Console.WriteLine("Typ krosołwera: " + crossover.GetType().Name);

                    wynikGA w = new wynikGA
                    {
                        ga = null,
                        ch = null
                    };

                    try
                    {
                        w = GeneticsMethods.GeneticExt(counter - 1, item, back_volume, crossover: crossover);
                    }
                    catch (System.ArgumentException e)
                    {
                        if (Experiments.doOutput) System.Console.WriteLine("BUMMMMMM");
                        //if (Experiments.doOutput) System.Console.WriteLine("BUMMMMMM " + crossover.GetType().Name + " " + e.ToString());
                        continue;
                    }
                    catch (System.Exception e)
                    {
                        if (Experiments.doOutput) System.Console.WriteLine("BUMMMMMM");
                        //if (Experiments.doOutput) System.Console.WriteLine("BUMMMMMM " + crossover.GetType().Name + " " + e.ToString());
                        continue;
                    }

                    Wynik wynik = new Wynik();


                    double[] wn = w.ch.ToFloatingPoints();
                    double pack_items_volume = 0;
                    double pack_items_value = 0;
                    for (int j = 0; j < wn.Length; j++)
                    {
                        pack_items_volume += wn[j] * item[j].item_volume;
                        pack_items_value += wn[j] * item[j].item_value;
                        if (item[j].item_volume < 0) isOK = false;
                    }
                    if (Experiments.doOutput) System.Console.WriteLine("Objetosc spakowanych rzeczy: " + (pack_items_volume.ToString("F")));
                    wynik.items_volume = pack_items_volume;
                    if (Experiments.doOutput) System.Console.WriteLine("Czas: " + w.ga.TimeEvolving.TotalSeconds);
                    wynik.time = w.ga.TimeEvolving.TotalSeconds;
                    if (Experiments.doOutput) System.Console.WriteLine("Wartosc: " + pack_items_value.ToString("F"));
                    wynik.items_value = pack_items_value;
                    if (pack_items_volume > back_volume) { wynik.czypoprawny = false; if (Experiments.doOutput) System.Console.WriteLine("niepoprawny"); }
                    else { wynik.czypoprawny = true; if (Experiments.doOutput) System.Console.WriteLine("poprawny"); }
                    if (Experiments.doOutput) System.Console.WriteLine("");
                    if (Experiments.doOutput) System.Console.WriteLine("");
                    stringBuilder.Append(System.Math.Round(pack_items_value, 4) + "\t" + System.Math.Round(pack_items_volume, 4) + "\t" + System.Math.Round(w.ga.TimeEvolving.TotalSeconds, 4) + "\t" + (isOK ? "1" : "0") + "\t" + crossover.GetType().Name + "\r\n");

                    wyniki.Add(wynik);
                }
            }
            DataTransfer.saveFileWithResults(fileName, stringBuilder);
            return wyniki;
        }

        public static List<Wynik> testCrossoverChance(int counter, Item[] item, double back_volume, String fileName)
        {
            List<Wynik> wyniki = new List<Wynik>();

            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append("Wartość spakowanych itemków\tWaga spakowanych itemków\tCzas pakowania\tCzy poprawne\tSzansa crossovera\r\n");
            for (double iCrossChance = 0.05f; iCrossChance < 1.0f; iCrossChance += 0.05f)
            {
                for (int rp = 0; rp < repeatCount; rp++)
                {
                    Wynik wynik = new Wynik();
                    if (showCurrent) if (Experiments.doOutput) System.Console.WriteLine("testCrossoverChance  " + iCrossChance + "  " + rp);
                    Boolean isOK = true;
                    wynikGA w = GeneticsMethods.GeneticExt(counter - 1, item, back_volume, crossoverChance: iCrossChance);



                    double[] wn = w.ch.ToFloatingPoints();
                    double pack_items_volume = 0;
                    double pack_items_value = 0;
                    for (int j = 0; j < wn.Length; j++)
                    {
                        pack_items_volume += wn[j] * item[j].item_volume;
                        pack_items_value += wn[j] * item[j].item_value;
                        if (item[j].item_volume < 0) isOK = false;
                    }
                    if (Experiments.doOutput) System.Console.WriteLine("Szansa na krosołwer: " + iCrossChance);
                    if (Experiments.doOutput) System.Console.WriteLine("Objetosc spakowanych rzeczy: " + (pack_items_volume.ToString("F")));
                    wynik.items_volume = pack_items_volume;
                    if (Experiments.doOutput) System.Console.WriteLine("Czas: " + w.ga.TimeEvolving.TotalSeconds);
                    wynik.time = w.ga.TimeEvolving.TotalSeconds;
                    if (Experiments.doOutput) System.Console.WriteLine("Wartosc: " + pack_items_value.ToString("F"));
                    wynik.items_value = pack_items_value;
                    if (pack_items_volume > back_volume) { wynik.czypoprawny = false; if (Experiments.doOutput) System.Console.WriteLine("niepoprawny"); }
                    else { wynik.czypoprawny = true; if (Experiments.doOutput) System.Console.WriteLine("poprawny"); }
                    wyniki.Add(wynik);
                    if (Experiments.doOutput) System.Console.WriteLine("");
                    if (Experiments.doOutput) System.Console.WriteLine("");
                    if (pack_items_volume > back_volume) isOK = false;
                    stringBuilder.Append(System.Math.Round(pack_items_value, 4) + "\t" + System.Math.Round(pack_items_volume, 4) + "\t" + System.Math.Round(w.ga.TimeEvolving.TotalSeconds, 4) + "\t" + (isOK ? "1" : "0") + "\t" + iCrossChance + "\r\n");
                    

                }
            }
            DataTransfer.saveFileWithResults(fileName, stringBuilder);
            return wyniki;
        }

        public static List<Wynik> testMutationChance(int counter, Item[] item, double back_volume, String fileName)
        {
            List<Wynik> wyniki = new List<Wynik>();

            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append("Wartość spakowanych itemków\tWaga spakowanych itemków\tCzas pakowania\tCzy poprawne\tSzansa mutacji\r\n");
            for (double iMutChance = 0.05f; iMutChance < 1.0f; iMutChance += 0.01f)
            {
                for (int rp = 0; rp < repeatCount; rp++)
                {
                    Wynik wynik = new Wynik();
                    if (showCurrent) if (Experiments.doOutput) System.Console.WriteLine("testMutationChance  " + iMutChance + "  " + rp);
                    Boolean isOK = true;
                    wynikGA w = GeneticsMethods.GeneticExt(counter - 1, item, back_volume, mutationChance: iMutChance);



                    double[] wn = w.ch.ToFloatingPoints();
                    double pack_items_volume = 0;
                    double pack_items_value = 0;
                    for (int j = 0; j < wn.Length; j++)
                    {
                        pack_items_volume += wn[j] * item[j].item_volume;
                        pack_items_value += wn[j] * item[j].item_value;
                        if (item[j].item_volume < 0) isOK = false;
                    }
                    if (Experiments.doOutput) System.Console.WriteLine("Szansa mutacji: " + iMutChance);
                    if (Experiments.doOutput) System.Console.WriteLine("Objetosc spakowanych rzeczy: " + (pack_items_volume.ToString("F")));
                    wynik.items_volume = pack_items_volume;
                    if (Experiments.doOutput) System.Console.WriteLine("Czas: " + w.ga.TimeEvolving.TotalSeconds);
                    wynik.time = w.ga.TimeEvolving.TotalSeconds;
                    if (Experiments.doOutput) System.Console.WriteLine("Wartosc: " + pack_items_value.ToString("F"));
                    wynik.items_value = pack_items_value;
                    if (pack_items_volume > back_volume) { wynik.czypoprawny = false; if (Experiments.doOutput) System.Console.WriteLine("niepoprawny"); }
                    else { wynik.czypoprawny = true; if (Experiments.doOutput) System.Console.WriteLine("poprawny"); }
                    wyniki.Add(wynik);
                    if (Experiments.doOutput) System.Console.WriteLine("");
                    if (Experiments.doOutput) System.Console.WriteLine("");
                    if (pack_items_volume > back_volume) isOK = false;
                    stringBuilder.Append(System.Math.Round(pack_items_value, 4) + "\t" + System.Math.Round(pack_items_volume, 4) + "\t" + System.Math.Round(w.ga.TimeEvolving.TotalSeconds, 4) + "\t" + (isOK ? "1" : "0") + "\t" + iMutChance + "\r\n");
                    

                }
            }
            DataTransfer.saveFileWithResults(fileName, stringBuilder);
            return wyniki;
        }

        public static List<Wynik> testTournamentSize(int counter, Item[] item, double back_volume, String fileName)
        {
            List<Wynik> wyniki = new List<Wynik>();

            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append("Wartość spakowanych itemków\tWaga spakowanych itemków\tCzas pakowania\tCzy poprawne\tRozmiar tournamentu\r\n");
            for (int iTurSize = 2; iTurSize < 50; iTurSize += 1)
            {
                for (int rp = 0; rp < repeatCount; rp++)
                {
                    Wynik wynik = new Wynik();
                    if (showCurrent) if (Experiments.doOutput) System.Console.WriteLine("testTournamentSize  " + iTurSize + "  " + rp);
                    Boolean isOK = true;
                    wynikGA w = GeneticsMethods.GeneticExt(counter - 1, item, back_volume, tournamentSize: iTurSize);



                    double[] wn = w.ch.ToFloatingPoints();
                    double pack_items_volume = 0;
                    double pack_items_value = 0;
                    for (int j = 0; j < wn.Length; j++)
                    {
                        pack_items_volume += wn[j] * item[j].item_volume;
                        pack_items_value += wn[j] * item[j].item_value;
                        if (item[j].item_volume < 0) isOK = false;
                    }
                    if (Experiments.doOutput) System.Console.WriteLine("Rozmiar tournamentu: " + iTurSize);
                    if (Experiments.doOutput) System.Console.WriteLine("Objetosc spakowanych rzeczy: " + (pack_items_volume.ToString("F")));
                    wynik.items_volume = pack_items_volume;
                    if (Experiments.doOutput) System.Console.WriteLine("Czas: " + w.ga.TimeEvolving.TotalSeconds);
                    wynik.time = w.ga.TimeEvolving.TotalSeconds;
                    if (Experiments.doOutput) System.Console.WriteLine("Wartosc: " + pack_items_value.ToString("F"));
                    wynik.items_value = pack_items_value;
                    if (pack_items_volume > back_volume) { wynik.czypoprawny = false; if (Experiments.doOutput) System.Console.WriteLine("niepoprawny"); }
                    else { wynik.czypoprawny = true; if (Experiments.doOutput) System.Console.WriteLine("poprawny"); }
                    wyniki.Add(wynik);
                    if (Experiments.doOutput) System.Console.WriteLine("");
                    if (Experiments.doOutput) System.Console.WriteLine("");
                    if (pack_items_volume > back_volume) isOK = false;
                    stringBuilder.Append(System.Math.Round(pack_items_value, 4) + "\t" + System.Math.Round(pack_items_volume, 4) + "\t" + System.Math.Round(w.ga.TimeEvolving.TotalSeconds, 4) + "\t" + (isOK ? "1" : "0") + "\t" + iTurSize + "\r\n");
                    
                }
            }
            DataTransfer.saveFileWithResults(fileName, stringBuilder);
            return wyniki;
        }

        public static List<Wynik> testAIO(int counter, Item[] item, double back_volume, String fileName)
        {
            List<Wynik> wyniki = new List<Wynik>();
            List<ICrossover> crossovers = new List<ICrossover>();
            crossovers.Add(new CycleCrossover());
            crossovers.Add(new CutAndSpliceCrossover());
            crossovers.Add(new OnePointCrossover());
            crossovers.Add(new OrderBasedCrossover());
            crossovers.Add(new OrderedCrossover());
            crossovers.Add(new PartiallyMappedCrossover());
            crossovers.Add(new PositionBasedCrossover());
            crossovers.Add(new ThreeParentCrossover());
            crossovers.Add(new TwoPointCrossover());
            crossovers.Add(new UniformCrossover());

            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append("Wartość spakowanych itemków\tWaga spakowanych itemków\tCzas pakowania\tCzy poprawne\tTyp crossovera\tRozmiar populacji\tSzansa na crossover\tSzansa na mutację\tRozmiar tournamentu\r\n");
            foreach (ICrossover crossover in crossovers)
            {
                for (int iPop = 5; iPop<500; iPop+=5)
                    for (double iCrossChance = 0.05f; iCrossChance < 1.0f; iCrossChance += 0.05f)
                        for (double iMutChance = 0.05f; iMutChance < 1.0f; iMutChance += 0.01f)
                            for (int iTurSize = 2; iTurSize < 50; iTurSize += 1)
                                for (int rp = 0; rp < repeatCount; rp++)
                                {
                                    if (showCurrent) if (Experiments.doOutput) System.Console.WriteLine("testDifferentCrossovers  " + rp);
                                    Boolean isOK = true;
                                    if (Experiments.doOutput) System.Console.WriteLine("Typ krosołwera: " + crossover.GetType().Name);

                                    wynikGA w = new wynikGA
                                    {
                                        ga = null,
                                        ch = null
                                    };

                                    try
                                    {
                                        w = GeneticsMethods.GeneticExt(counter - 1, item, back_volume, iPop, crossover, iCrossChance, iMutChance, iTurSize);
                                    }
                                    catch (System.ArgumentException e)
                                    {
                                        if (Experiments.doOutput) System.Console.WriteLine("BUMMMMMM");
                                        //if (Experiments.doOutput) System.Console.WriteLine("BUMMMMMM " + crossover.GetType().Name + " " + e.ToString());
                                        continue;
                                    }
                                    catch (System.Exception e)
                                    {
                                        if (Experiments.doOutput) System.Console.WriteLine("BUMMMMMM");
                                        //if (Experiments.doOutput) System.Console.WriteLine("BUMMMMMM " + crossover.GetType().Name + " " + e.ToString());
                                        continue;
                                    }

                                    Wynik wynik = new Wynik();


                                    double[] wn = w.ch.ToFloatingPoints();
                                    double pack_items_volume = 0;
                                    double pack_items_value = 0;
                                    for (int j = 0; j < wn.Length; j++)
                                    {
                                        pack_items_volume += wn[j] * item[j].item_volume;
                                        pack_items_value += wn[j] * item[j].item_value;
                                        if (item[j].item_volume < 0) isOK = false;
                                    }
                                    if (Experiments.doOutput) System.Console.WriteLine("Objetosc spakowanych rzeczy: " + (pack_items_volume.ToString("F")));
                                    wynik.items_volume = pack_items_volume;
                                    if (Experiments.doOutput) System.Console.WriteLine("Czas: " + w.ga.TimeEvolving.TotalSeconds);
                                    wynik.time = w.ga.TimeEvolving.TotalSeconds;
                                    if (Experiments.doOutput) System.Console.WriteLine("Wartosc: " + pack_items_value.ToString("F"));
                                    wynik.items_value = pack_items_value;
                                    if (pack_items_volume > back_volume) { wynik.czypoprawny = false; if (Experiments.doOutput) System.Console.WriteLine("niepoprawny"); }
                                    else { wynik.czypoprawny = true; if (Experiments.doOutput) System.Console.WriteLine("poprawny"); }
                                    if (Experiments.doOutput) System.Console.WriteLine("");
                                    if (Experiments.doOutput) System.Console.WriteLine("");
                                    stringBuilder.Append(System.Math.Round(pack_items_value, 4) + "\t" + System.Math.Round(pack_items_volume, 4) + "\t" + System.Math.Round(w.ga.TimeEvolving.TotalSeconds, 4) + "\t" + (isOK ? "1" : "0") + "\t" + crossover.GetType().Name + "\t" + iPop + "\t" + iCrossChance + "\t" + iMutChance + "\t" + iTurSize + "\r\n");

                                    wyniki.Add(wynik);
                                }
            }
            DataTransfer.saveFileWithResults(fileName, stringBuilder);
            return wyniki;
        }
    }
}
