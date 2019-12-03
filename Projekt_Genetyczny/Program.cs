using GeneticSharp.Domain;
using GeneticSharp.Domain.Chromosomes;
using GeneticSharp.Domain.Crossovers;
using GeneticSharp.Domain.Fitnesses;
using GeneticSharp.Domain.Mutations;
using GeneticSharp.Domain.Populations;
using GeneticSharp.Domain.Selections;
using GeneticSharp.Domain.Terminations;
using System;
using System.Text;
using System.Threading;

// W wersji polskiej programu visual studio, należy w danych wejściowych kropki zamienić na przecinki



namespace Projekt_Genetyczny
{
    class Program
    {
        static void Main(string[] args)
        {
            int counter;
            Item[] items;
            double backVolume;
            DataTransfer.readFileWithData(@"Dane.txt", out counter, out items, out backVolume);
            DataTransfer.displayAllContent(counter, items, backVolume);
            Thread testCommonThread = new Thread(p =>
            {
                Experiments.testCommon(counter, items, backVolume, @"MacierzTestZwykly.txt");
            });
            Thread testPopulationSizeThread = new Thread(p =>
            {
                Experiments.testPopulationSize(counter, items, backVolume, @"MacierzTestPopulacje.txt"); ;
            });
            Thread testDifferentCrossoversThread = new Thread(p =>
            {
                Experiments.testDifferentCrossovers(counter, items, backVolume, @"MacierzRozneCrossovery.txt");
            });
            Thread testCrossoverChanceThread = new Thread(p =>
            {
                Experiments.testCrossoverChance(counter, items, backVolume, @"MacierzTestSzansaCrossovera.txt");
            });
            Thread testMutationChanceThread = new Thread(p =>
            {
                Experiments.testMutationChance(counter, items, backVolume, @"MacierzTestSzansaMutacji.txt");
            });
            Thread testTournamentSizeThread = new Thread(p =>
            {
                Experiments.testTournamentSize(counter, items, backVolume, @"MacierzTestRozmiarTournamentu.txt");
            });
            Thread testAIOThread = new Thread(p =>
            {
                Experiments.testAIO(counter, items, backVolume, @"MacierzTestAIO.txt");
            });

            testCommonThread.Start();
            testPopulationSizeThread.Start();
            testDifferentCrossoversThread.Start();
            testCrossoverChanceThread.Start();
            testMutationChanceThread.Start();
            testTournamentSizeThread.Start();
            testAIOThread.Start();

            testCommonThread.Join();
            testPopulationSizeThread.Join();
            testDifferentCrossoversThread.Join();
            testCrossoverChanceThread.Join();
            testMutationChanceThread.Join();
            testTournamentSizeThread.Join();
            testAIOThread.Join();

            // oczekiwanie na cud
            System.Console.WriteLine("kuniec");
            System.Console.ReadLine();
        }
    }
}