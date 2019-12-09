using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Linq;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;

namespace Day7 {
    public class SIFReader {
        public string input;
        public List<Dictionary<int, List<int>>> layers;

        public SIFReader() {
            input = File.ReadAllLines("input.txt")[0];
            layers = new List<Dictionary<int, List<int>>>();
        }

        public void DivideIntoLayers(int width, int height) {
            while (input.Length != 0) {
                Dictionary<int, List<int>> newLayer = new Dictionary<int, List<int>>();
                for (int i = 1; i <= height; i++) {
                    newLayer.Add(i, new List<int>());
                    newLayer[i].AddRange(input.Take(width).Select(s => Int32.Parse(s.ToString())));
                    input = input.Remove(0, width);
                    layers.Add(newLayer);
                }
            }
            layers.Sort(new ILowestAmountOfZeroesComparer());
            var lowestLayer = layers[0];
            Console.WriteLine(lowestLayer.SelectMany(item => item.Value).Count(i => i == 1) * lowestLayer.SelectMany(item => item.Value).Count(i => i == 2));
        }

        public void DivideIntoLayersWithoutSort(int width, int height) {
            while (input.Length != 0) {
                Dictionary<int, List<int>> newLayer = new Dictionary<int, List<int>>();
                for (int i = 1; i <= height; i++) {
                    newLayer.Add(i, new List<int>());
                    newLayer[i].AddRange(input.Take(width).Select(s => Int32.Parse(s.ToString())));
                    input = input.Remove(0, width);
                    layers.Add(newLayer);
                }
            }
        }

        public void DecodeMessage(int width, int height) {
            DivideIntoLayersWithoutSort(width, height);
            Dictionary<int, List<int>> image = new Dictionary<int, List<int>>();
            for (int i = 1; i <= height; i++) {
                image.Add(i, new List<int>());
                for (int j = 0; j < width; j++) {
                    image[i].Add(2);
                }
            }
            foreach (var layer in layers) {
                foreach (var line in layer.Keys) {
                    for (int pixelNumber=0;pixelNumber< layer[line].Count;pixelNumber++) { 
                        if (image[line][pixelNumber] == 2) {
                            image[line][pixelNumber] = layer[line][pixelNumber];
                        }
                    }
                }
            }

            foreach (var key in image.Keys) {
                foreach (var pixel in image[key]) {
                    if (pixel == 1) {
                        Console.BackgroundColor = ConsoleColor.White;
                        Console.ForegroundColor = ConsoleColor.White;
                    } else {
                        Console.BackgroundColor = ConsoleColor.Black;
                        Console.ForegroundColor = ConsoleColor.Black;
                    }
                    Console.Write(pixel + " ");
                }
                Console.Write("\n");
            }
        }
    }

    internal class ILowestAmountOfZeroesComparer : IComparer<Dictionary<int, List<int>>> {
        public int Compare([AllowNull] Dictionary<int, List<int>> layer1, [AllowNull] Dictionary<int, List<int>> layer2) {            
            return layer1.SelectMany(item => item.Value).Count(x => x == 0) - layer2.SelectMany(item => item.Value).Count(x => x == 0);
        }
    }
}

