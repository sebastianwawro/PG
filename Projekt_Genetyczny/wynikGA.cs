using GeneticSharp.Domain;
using GeneticSharp.Domain.Chromosomes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Projekt_Genetyczny
{
    struct wynikGA
    {
        public GeneticAlgorithm ga { set; get; }
        public FloatingPointChromosome ch { set; get; }
    }
}
