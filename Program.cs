﻿using System;
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

        ConsoleKeyInfo keyInfo;

        InitializeGame();

        while (!isGameOver)
        {
            if (Console.KeyAvailable)
            {
                keyInfo = Console.ReadKey();
                ChangeDirection(keyInfo.Key);
            }

            MoveSnake();
            CheckCollision();

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
    static void MoveSnake()
    {
        for (int i = snakeX.Count - 1; i > 0; i--)
        {
            snakeX[i] = snakeX[i - 1];
            snakeY[i] = snakeY[i - 1];
        }

        switch (snakeDirection)
        {
            case Direction.Up:
                snakeY[0]--;
                break;
            case Direction.Down:
                snakeY[0]++;
                break;
            case Direction.Left:
                snakeX[0]--;
                break;
            case Direction.Right:
                snakeX[0]++;
                break;
        }
    }
    enum Direction
    {
        Up,
        Down,
        Left,
        Right
    }

    static Direction snakeDirection = Direction.Right;

    static void ChangeDirection(ConsoleKey key)
    {
        switch (key)
        {
            case ConsoleKey.UpArrow:
                if (snakeDirection != Direction.Down)
                    snakeDirection = Direction.Up;
                break;
            case ConsoleKey.DownArrow:
                if (snakeDirection != Direction.Up)
                    snakeDirection = Direction.Down;
                break;
            case ConsoleKey.LeftArrow:
                if (snakeDirection != Direction.Right)
                    snakeDirection = Direction.Left;
                break;
            case ConsoleKey.RightArrow:
                if (snakeDirection != Direction.Left)
                    snakeDirection = Direction.Right;
                break;
        }
    }

    static void CheckCollision()
    {
        if (snakeX[0] < 0 || snakeX[0] >= screenWidth || snakeY[0] < 0 || snakeY[0] >= screenHeight)
        {
            isGameOver = true;
        }

        for (int i = 1; i < snakeX.Count; i++)
        {
            if (snakeX[0] == snakeX[i] && snakeY[0] == snakeY[i])
            {
                isGameOver = true;
            }
        }
    }
}
