using System;
using System.Collections.Generic;
using System.Text;

namespace Projekt_Genetyczny
{
    class DataTransfer
    {
        public static void readFileWithData(String fileName, out int counterOut, out Item[] itemsOut, out double backVolumeOut)
        {
            //    float[][] population;
            string[] lines_tab;

            //       Tuple <int, double, double> [] item;

            Item[] item;


            double back_volume = 0; // pojemnosc plecaka



            int counter = 0;
            string line;

            System.IO.StreamReader file =
                new System.IO.StreamReader(fileName);
            while ((line = file.ReadLine()) != null)
            {
                //if (Experiments.doOutput) System.Console.WriteLine(line);

                counter++;
            }

            file.Close();
            if (Experiments.doOutput) System.Console.WriteLine("W pliku było {0} lini.", counter);

            System.IO.StreamReader file2 =
               new System.IO.StreamReader(fileName);



            lines_tab = new string[counter];

            //item = new Tuple<int, double, double>[counter];

            item = new Item[counter - 1];

            int i = 0;
            while ((line = file2.ReadLine()) != null)
            {


                lines_tab[i] = line.Replace(".", ",");

                i++;
            }


            for (int x = 1; x < (counter); x++)
            {

                string srodek = lines_tab[x].Substring(1, (lines_tab[x].Length - 2));
                int parse_result1;
                double parse__result2;
                double parse__result3;
                item[x - 1] = new Item();

                if (int.TryParse(srodek.Split("><")[0], out parse_result1)) item[x - 1].item_number = parse_result1;
                else { throw new Exception ("Błedne dane"); }

                if (double.TryParse(srodek.Split("><")[1], out parse__result2)) item[x - 1].item_volume = parse__result2;
                else { throw new Exception("Błedne dane"); }


                if (double.TryParse(srodek.Split("><")[2], out parse__result3)) item[x - 1].item_value = parse__result3;
                else { throw new Exception("Błedne dane"); }


            }


            file2.Close();

            double parse_result;

            if (double.TryParse(lines_tab[0].Substring(1, (lines_tab[0].Length - 2)), out parse_result)) back_volume = parse_result;
            else { throw new Exception("Błedne dane"); }

            counterOut = counter;
            itemsOut = item;
            backVolumeOut = back_volume;

            //  back_volume = double.Parse(lines_tab[0].Substring(1, (lines_tab[0].Length - 2)));
        }

        public static void displayAllContent(int counter, Item[] item, double back_volume)
        {

            if (Experiments.doOutput) System.Console.WriteLine("Objetosc plecaka: " + (back_volume.ToString("F")));


            for (int z = 0; z < counter - 1; z++)
            {
                if (Experiments.doOutput) System.Console.WriteLine("");
                if (Experiments.doOutput) System.Console.WriteLine("Indeks obiektu: " + item[z].item_number);
                if (Experiments.doOutput) System.Console.WriteLine("Waga obiektu: " + (item[z].item_volume.ToString("F")));
                if (Experiments.doOutput) System.Console.WriteLine("Wartość obiektu: " + (item[z].item_value.ToString("F")));

            }

            if (Experiments.doOutput) System.Console.WriteLine("");
            if (Experiments.doOutput) System.Console.WriteLine("");
        }

        public static void saveFileWithResults(String fileName, StringBuilder content)
        {
            using (System.IO.StreamWriter file3 =
            new System.IO.StreamWriter(fileName))
            {
                file3.Write(content.ToString().Replace(',', '.'));
            }
        }
    }
}
