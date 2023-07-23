using System;
using System.Collections.Generic;
using System.IO;

class Program
{
    static void Main()
    {
        List<List<int>> calories = new List<List<int>>();
        List<int> currentElfCalories = new List<int>();
        string filePath = Path.Combine(Directory.GetCurrentDirectory(), "input.txt");
        using (StreamReader reader = new StreamReader(filePath))
        
        {
            string line;
            while ((line = reader.ReadLine()) != null)
            {
                if (line.Trim() != "") 
                {
                    currentElfCalories.Add(int.Parse(line));
                }
                else 
                {
                    calories.Add(currentElfCalories);
                    currentElfCalories = new List<int>(); 
                }
            }
        }

        if (currentElfCalories.Count > 0)
        {
            calories.Add(currentElfCalories);
        }

      
        int maxCalories = 0;
        foreach (List<int> elfCalories in calories)
        {
            int elfTotalCalories = 0;
            foreach (int calorie in elfCalories)
            {
                elfTotalCalories += calorie;
            }
            maxCalories = Math.Max(maxCalories, elfTotalCalories);
        }

     
        Console.WriteLine(maxCalories);
    }
}
