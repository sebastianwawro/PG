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
            Experiments.testCommon(counter, items, backVolume, @"MacierzTestZwykly.txt");
            Experiments.testPopulationSize(counter, items, backVolume, @"MacierzTestPopulacje.txt");
            Experiments.testDifferentCrossovers(counter, items, backVolume, @"MacierzRozneCrossovery.txt");
            Experiments.testCrossoverChance(counter, items, backVolume, @"MacierzTestSzansaCrossovera.txt");
            Experiments.testMutationChance(counter, items, backVolume, @"MacierzTestSzansaMutacji.txt");
            Experiments.testTournamentSize(counter, items, backVolume, @"MacierzTestRozmiarTournamentu.txt");
            Experiments.testAIO(counter, items, backVolume, @"MacierzTestAIO.txt");
            // oczekiwanie na cud
            System.Console.WriteLine("kuniec");
            System.Console.ReadLine();
        }
    }
}