using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

class Program
{
    static int screenWidth = 100;
    static int screenHeight = 50;
    static char snakeChar = 'O';

    static List<int> snakeX = new List<int>() { 0 };
    static List<int> snakeY = new List<int>() { 0 };

    static bool isGameOver = false;

    static void Main()
    {
        Console.CursorVisible = false;
        Console.WindowHeight = screenHeight + 1;
        Console.WindowWidth = screenWidth + 1;

        InitializeGame();

        while (!isGameOver)
        {
            if (!isGameOver)
            {
                DrawGame();
                Thread.Sleep(100);
            }
        }

        Console.Clear();
        Console.WriteLine("Game Over!");
        Console.WriteLine("Press any key to exit...");
        Console.ReadKey();
    }

    static void InitializeGame()
    {
        snakeX[0] = screenWidth / 2;
        snakeY[0] = screenHeight / 2;
    }

    static void DrawGame()
    {
        Console.Clear();

        for (int i = 0; i < snakeX.Count; i++)
        {
            Console.SetCursorPosition(snakeX[i], snakeY[i]);
            Console.Write(snakeChar);
        }
    }
}
