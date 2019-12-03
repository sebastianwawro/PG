using GeneticSharp.Domain;
using GeneticSharp.Domain.Chromosomes;
using GeneticSharp.Domain.Crossovers;
using GeneticSharp.Domain.Fitnesses;
using GeneticSharp.Domain.Mutations;
using GeneticSharp.Domain.Populations;
using GeneticSharp.Domain.Selections;
using GeneticSharp.Domain.Terminations;
using System;
using System.Collections.Generic;
using System.Text;

namespace Projekt_Genetyczny
{
    class GeneticsMethods
    {
        public static wynikGA Genetic(int counter, Item[] dane, double back_volume)
        {


            double[] minValue = new double[counter];
            double[] maxValue = new double[counter];
            int[] totalBits = new int[counter];
            int[] fractionDigits = new int[counter];


            for (int i = 0; i < counter; i++)
            {
                minValue[i] = 0;
                maxValue[i] = 1;
                totalBits[i] = 16;
                fractionDigits[i] = 4;

            }

            var selection = new TournamentSelection(50, true);
            var crossover = new TwoPointCrossover();
            var mutation = new FlipBitMutation();
            var chromosome = new FloatingPointChromosome(minValue, maxValue, totalBits, fractionDigits);
            // var fitobj = new MyProblemFitness(dane, chromosome, back_volume);
            //var fitness = fitobj.Evaluate;
            var population = new Population(500, 500, chromosome);

            var fitness = new FuncFitness((c) =>
            {
                var fc = c as FloatingPointChromosome;
                //var _items = dane;
                //var _bvol = back_volume;
                /* var values = fc.ToFloatingPoints();
                 var x1 = values[0];
                 var y1 = values[1];
                 var x2 = values[2];
                 var y2 = values[3];
                 return Math.Sqrt(Math.Pow(x2 - x1, 2) + Math.Pow(y2 - y1, 2));

                  */

                //chromosome.ReplaceGenes(0, chromosome.GetGenes());
                double[] proc_wag = fc.ToFloatingPoints();
                double suma_spakowanych = 0;
                double wartosc_spakowanych = 0;
                for (int i = 0; i < proc_wag.Length; i++)
                {
                    suma_spakowanych += dane[i].item_volume * proc_wag[i];
                    wartosc_spakowanych += dane[i].item_value * proc_wag[i];
                }

                if (suma_spakowanych <= back_volume)
                {
                    double temp = (wartosc_spakowanych * (suma_spakowanych / back_volume));
                    if (temp < 0) System.Console.WriteLine("LOL ");
                }


                return (suma_spakowanych <= back_volume) ? (wartosc_spakowanych * (suma_spakowanych / back_volume)) : (-suma_spakowanych);




            });



            var ga = new GeneticAlgorithm(population, fitness, selection, crossover, mutation);
            ga.Termination = new GenerationNumberTermination(100);

            Console.WriteLine("GA running...");
            ga.MutationProbability = 0.05f;
            ga.CrossoverProbability = 0.20f;
            //ga.Selection.
            ga.Start();

            // FITNESS  Console.WriteLine("Best solution found has fitness = " + ga.BestChromosome.Fitness);
            wynikGA w = new wynikGA();
            w.ga = ga;
            w.ch = chromosome;
            w.ch.ReplaceGenes(0, ga.BestChromosome.GetGenes());

            return w;
        }

        public static wynikGA GeneticExt
            (int counter, 
            Item[] dane, 
            double back_volume, 
            int populationSize = 500, 
            ICrossover crossover = null,
            double crossoverChance = 0.20f,
            double mutationChance = 0.05f,
            int tournamentSize = 50)
        {


            double[] minValue = new double[counter];
            double[] maxValue = new double[counter];
            int[] totalBits = new int[counter];
            int[] fractionDigits = new int[counter];


            for (int i = 0; i < counter; i++)
            {
                minValue[i] = 0;
                maxValue[i] = 1;
                totalBits[i] = 16;
                fractionDigits[i] = 4;

            }

            ISelection selection = new TournamentSelection(tournamentSize > populationSize ? populationSize : tournamentSize, true);
            if (crossover == null) crossover = new TwoPointCrossover();
            IMutation mutation = new FlipBitMutation();
            var chromosome = new FloatingPointChromosome(minValue, maxValue, totalBits, fractionDigits);
            // var fitobj = new MyProblemFitness(dane, chromosome, back_volume);
            //var fitness = fitobj.Evaluate;
            IPopulation population = new Population(populationSize, populationSize, chromosome);

            IFitness fitness = new FuncFitness((c) =>
            {
                FloatingPointChromosome fc = c as FloatingPointChromosome;
                //var _items = dane;
                //var _bvol = back_volume;
                /* var values = fc.ToFloatingPoints();
                 var x1 = values[0];
                 var y1 = values[1];
                 var x2 = values[2];
                 var y2 = values[3];
                 return Math.Sqrt(Math.Pow(x2 - x1, 2) + Math.Pow(y2 - y1, 2));

                  */

                //chromosome.ReplaceGenes(0, chromosome.GetGenes());
                double[] proc_wag = fc.ToFloatingPoints();
                double suma_spakowanych = 0;
                double wartosc_spakowanych = 0;
                for (int i = 0; i < proc_wag.Length; i++)
                {
                    suma_spakowanych += dane[i].item_volume * proc_wag[i];
                    wartosc_spakowanych += dane[i].item_value * proc_wag[i];
                }

                if (suma_spakowanych <= back_volume)
                {
                    double temp = (wartosc_spakowanych * (suma_spakowanych / back_volume));
                    if (temp < 0) System.Console.WriteLine("LOL ");
                }


                return (suma_spakowanych <= back_volume) ? (wartosc_spakowanych * (suma_spakowanych / back_volume)) : (-suma_spakowanych);




            });



            GeneticAlgorithm ga = new GeneticAlgorithm(population, fitness, selection, crossover, mutation);
            ga.Termination = new GenerationNumberTermination(100);

            Console.WriteLine("GA running...");
            ga.MutationProbability = 0.05f;
            ga.CrossoverProbability = 0.20f;
            //ga.Selection.
            ga.Start();

            // FITNESS  Console.WriteLine("Best solution found has fitness = " + ga.BestChromosome.Fitness);
            wynikGA w = new wynikGA();
            w.ga = ga;
            w.ch = chromosome;
            w.ch.ReplaceGenes(0, ga.BestChromosome.GetGenes());

            return w;
        }
    }
}

/*
    public class MyProblemFitness : IFitness
    {
        public Item [] dane;
        public double back_volume;
        public FloatingPointChromosome chromosome;
        public MyProblemFitness(Item [] dane, FloatingPointChromosome chromosome, double back_volume) {
            this.dane = dane;
            this.chromosome = chromosome;
            this.back_volume = back_volume;
        }
        public double Evaluate(IChromosome ichromosome)
        {
            chromosome.ReplaceGenes(0, chromosome.GetGenes());
            double[] proc_wag = chromosome.ToFloatingPoints();
            double suma_spakowanych = 0;
            double wartosc_spakowanych = 0;
            for (int i=0; i<proc_wag.Length; i++)
            {
                suma_spakowanych += dane[i].item_volume * proc_wag[i];
                wartosc_spakowanych += dane[i].item_value * proc_wag[i];
            }
           // ichromosome.Fitness = suma_spakowanych <= back_volume ? (wartosc_spakowanych * (suma_spakowanych / back_volume)) : (-suma_spakowanych);

            if (suma_spakowanych <= back_volume )
            {
                double temp= (wartosc_spakowanych * (suma_spakowanych / back_volume));
                if (temp<0) System.Console.WriteLine("LOL ");
            }


            // return suma_spakowanych <= back_volume ? (wartosc_spakowanych * ( suma_spakowanych/back_volume) ) : (-suma_spakowanych);

            return (suma_spakowanych <= back_volume) ? 0 : -suma_spakowanych;

        }
    }
    */
