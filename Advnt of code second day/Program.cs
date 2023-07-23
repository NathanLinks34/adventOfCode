using System;
using System.Collections.Generic;
using System.IO;

namespace RockPaperScissorsTournament
{
    class Program
    {
        static Dictionary<char, int> PLAY_D = new Dictionary<char, int>
        {
            { 'X', 1 }, { 'Y', 2 }, { 'Z', 3 }, { 'A', 1 }, { 'B', 2 }, { 'C', 3 }
        };

        static HashSet<Tuple<int, int>> WIN_D = new HashSet<Tuple<int, int>>
        {
            new Tuple<int, int>(1, 3), new Tuple<int, int>(2, 1), new Tuple<int, int>(3, 2)
        };

        static Dictionary<char, char> WIN_D_PT2 = new Dictionary<char, char>
        {
            { 'A', 'B' }, { 'B', 'C' }, { 'C', 'A' }
        };

        static Dictionary<char, char> LOSE_D_PT2 = new Dictionary<char, char>();
        static Dictionary<char, Dictionary<char, char>> ACTION_D = new Dictionary<char, Dictionary<char, char>>();

        static void Main(string[] args)
        {
            // Part 1
            List<string> matchRounds = LoadStrategyGuide();
            int totalScore = GetMyTotal(matchRounds);
            Console.WriteLine("Part 1: " + totalScore);

            // Part 2
            InitializePart2Data();
            List<string> alteredRounds = GetAlteredRounds(matchRounds);
            int totalScorePt2 = GetMyTotal(alteredRounds);
            Console.WriteLine("Part 2: " + totalScorePt2);
        }

        static List<string> LoadStrategyGuide()
        {
            List<string> result = new List<string>();
            string filePath = "inputday2.txt";

            using (StreamReader reader = new StreamReader(filePath))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    result.Add(line.Trim());
                }
            }

            return result;
        }

        static int RoundScore(string matchRound)
        {
            char elf = matchRound[0];
            char me = matchRound[matchRound.Length - 1];
            int elfPlay = PLAY_D[elf];
            int mePlay = PLAY_D[me];
            return 6 * (WIN_D.Contains(new Tuple<int, int>(mePlay, elfPlay)) ? 1 : 0) + 3 * (mePlay == elfPlay ? 1 : 0) + mePlay;
        }

        static int GetMyTotal(List<string> matchRounds)
        {
            int totalScore = 0;
            foreach (string match in matchRounds)
            {
                totalScore += RoundScore(match);
            }
            return totalScore;
        }

        static void InitializePart2Data()
        {
            foreach (var kvp in WIN_D_PT2)
            {
                LOSE_D_PT2.Add(kvp.Value, kvp.Key);
            }

            foreach (char c in "XYZ")
            {
                Dictionary<char, char> action = new Dictionary<char, char> { { c, c } };
                ACTION_D.Add(c, action);
            }
            ACTION_D['X'].Add('A', LOSE_D_PT2['A']);
            ACTION_D['X'].Add('B', LOSE_D_PT2['B']);
            ACTION_D['X'].Add('C', LOSE_D_PT2['C']);
            ACTION_D['Z'].Add('A', WIN_D_PT2['A']);
            ACTION_D['Z'].Add('B', WIN_D_PT2['B']);
            ACTION_D['Z'].Add('C', WIN_D_PT2['C']);
        }

        static string AlterRound(string matchRound)
        {
            char elf = matchRound[0];
            char me = matchRound[matchRound.Length - 1];
            return $"{elf} {ACTION_D[me][elf]}";
        }

        static List<string> GetAlteredRounds(List<string> matchRounds)
        {
            List<string> alteredRounds = new List<string>();
            foreach (string match in matchRounds)
            {
                alteredRounds.Add(AlterRound(match));
            }
            return alteredRounds;
        }
    }
}
