using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

class Program
{
    static int screenWidth = 100;
    static int screenHeight = 50;
    static char snakeChar = 'O';
    static char foodChar = '@';

    static List<int> snakeX = new List<int>() { 1, 0 };
    static List<int> snakeY = new List<int>() { 0, 0 };

    static int foodX;
    static int foodY;

    static bool isGameOver = false;

    static int score = 0;

    static int delay = 100;

    static void Main()
    {
        Console.CursorVisible = false;
        Console.WindowHeight = screenHeight + 2;
        Console.WindowWidth = screenWidth + 15;

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
            CheckFood();

            if (!isGameOver)
            {
                DrawGame();
                Thread.Sleep(delay);
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

        foodX = new Random().Next(0, screenWidth);
        foodY = new Random().Next(0, screenHeight);
    }

    static void DrawGame()
    {
        Console.Clear();

        // Rysowanie górnej granicy planszy
        for (int i = 0; i <= screenWidth + 1; i++)
        {
            Console.SetCursorPosition(i, 0);
            Console.Write("-");
        }

        // Rysowanie dolnej granicy planszy
        for (int i = 0; i <= screenWidth + 1; i++)
        {
            Console.SetCursorPosition(i, screenHeight + 1);
            Console.Write("-");
        }

        // Rysowanie lewej granicy planszy
        for (int i = 1; i <= screenHeight; i++)
        {
            Console.SetCursorPosition(0, i);
            Console.Write("|");
        }

        // Rysowanie prawej granicy planszy
        for (int i = 1; i <= screenHeight; i++)
        {
            Console.SetCursorPosition(screenWidth + 1, i);
            Console.Write("|");
        }

        Console.SetCursorPosition(foodX, foodY);
        Console.Write(foodChar);

        for (int i = 0; i < snakeX.Count; i++)
        {
            Console.SetCursorPosition(snakeX[i], snakeY[i]);
            Console.Write(snakeChar);
        }

        Console.SetCursorPosition(screenWidth +3, 0);
        Console.Write($"Score: {score}");
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
        if (snakeX[0] <= 0 || snakeX[0] >= screenWidth +1 || snakeY[0] <= 0 || snakeY[0] >= screenHeight +1)
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

    static void CheckFood()
    {
        if (snakeX[0] == foodX && snakeY[0] == foodY)
        {
            // Snake ate the food
            snakeX.Add(0);
            snakeY.Add(0);

            // Update highscore
            score += 10;

            // Increase speed
            delay = Math.Max(10, delay - 5);

            // Generate new food
            foodX = new Random().Next(0, screenWidth);
            foodY = new Random().Next(0, screenHeight);
        }
    }
}
