using System;
using System.Collections.Generic;
using System.Linq;

public enum Shape
{
    Rock,
    Paper,
    Scissors,
}

public struct Round
{
    public bool Win { get; }
    public bool Draw { get; }
    public Shape Shape { get; }

    public Round(bool win, bool draw, Shape shape)
    {
        Win = win;
        Draw = draw;
        Shape = shape;
    }

    public int GetPoints()
    {
        int score = 0;

        if (Win)
        {
            score += 6;
        }

        if (Draw)
        {
            score += 3;
        }

        switch (Shape)
        {
            case Shape.Rock:
                score += 1;
                break;
            case Shape.Paper:
                score += 2;
                break;
            case Shape.Scissors:
                score += 3;
                break;
        }

        return score;
    }
}

public static class RPSHelper
{
    private static Shape FromChar(char s)
    {
        return s switch
        {
            'A' or 'X' => Shape.Rock,
            'B' or 'Y' => Shape.Paper,
            'C' or 'Z' => Shape.Scissors,
            _ => throw new Exception("Invalid input"),
        };
    }

    public static int GetScoreWithFirstStrategy(string input)
    {
        var matchRounds = input.Split(new[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);
        return matchRounds.Select(round => GetScoreFirstStrat((FromChar(round[0]), FromChar(round[2])))).Sum();
    }

    public static int GetScoreWithSecondStrategy(string input)
    {
        var matchRounds = input.Split(new[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);
        return matchRounds.Select(round => GetScoreSecondStrat((FromChar(round[0]), FromChar(round[2])))).Sum();
    }

    public static int GetScoreFirstStrat((Shape, Shape) input)
    {
        return input switch
        {
            (Shape.Scissors, Shape.Scissors) or
            (Shape.Paper, Shape.Paper) or
            (Shape.Rock, Shape.Rock) => new Round(false, true, input.Item2).GetPoints(),
            (Shape.Scissors, Shape.Paper) or
            (Shape.Rock, Shape.Scissors) or
            (Shape.Paper, Shape.Rock) => new Round(false, false, input.Item2).GetPoints(),
            (Shape.Paper, Shape.Scissors) or
            (Shape.Rock, Shape.Paper) or
            (Shape.Scissors, Shape.Rock) => new Round(true, false, input.Item2).GetPoints(),
            _ => throw new Exception("Invalid input"),
        };
    }

    public static int GetScoreSecondStrat((Shape, Shape) input)
    {
        return input switch
        {
            (Shape.Rock, Shape.Rock) => new Round(false, false, Shape.Scissors).GetPoints(),
            (Shape.Paper, Shape.Rock) => new Round(false, false, Shape.Rock).GetPoints(),
            (Shape.Scissors, Shape.Rock) => new Round(false, false, Shape.Paper).GetPoints(),
            (Shape.Scissors, Shape.Scissors) => new Round(true, false, Shape.Rock).GetPoints(),
            (Shape.Rock, Shape.Scissors) => new Round(true, false, Shape.Paper).GetPoints(),
            (Shape.Paper, Shape.Scissors) => new Round(true, false, Shape.Scissors).GetPoints(),
            (Shape.Rock, Shape.Paper) or
            (Shape.Paper, Shape.Paper) or
            (Shape.Scissors, Shape.Paper) => new Round(false, true, input.Item1).GetPoints(),
            _ => throw new Exception("Invalid input"),
        };
    }
}

public class Program
{
    public static void Main()
    {
        string input = System.IO.File.ReadAllText("inputday2.txt");

        int totalScoreFirstStrat = RPSHelper.GetScoreWithFirstStrategy(input);
        int totalScoreSecondStrat = RPSHelper.GetScoreWithSecondStrategy(input);

        Console.WriteLine("Part 1: " + totalScoreFirstStrat);
        Console.WriteLine("Part 2: " + totalScoreSecondStrat);
    }
}
