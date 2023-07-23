using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode
{
    class Program
    {
        static void Main(string[] args)
        {
            List<List<int>> calories = new List<List<int>>();
            List<int> currentElfCalories = new List<int>();

            using (StreamReader reader = new StreamReader("input.txt"))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    if (!string.IsNullOrWhiteSpace(line)) 
                    {
                        currentElfCalories.Add(int.Parse(line));
                    }
                    else
                    {
                        calories.Add(currentElfCalories);
                        currentElfCalories = new List<int>(); 
                    }
                }

                
                if (currentElfCalories.Count > 0)
                {
                    calories.Add(currentElfCalories);
                }
            }

           
            calories = calories.OrderByDescending(elfCalories => elfCalories.Sum()).ToList();

          
            List<List<int>> topCalories = calories.Take(3).ToList();

            
            int totalCalories = topCalories.Sum(elfCalories => elfCalories.Sum());

           
            Console.WriteLine(totalCalories);
        }
    }
}
