using System;
using System.Collections.Generic;
using System.Threading;

namespace SnakeGame
{
    class Program
    {
        static int width = 30;
        static int height = 20;
        static int score = 0;
        static bool gameOver = false;
        static int headX, headY;
        static List<int> tailX = new List<int>();
        static List<int> tailY = new List<int>();
        static int fruitX, fruitY;
        static Random random = new Random();
        static ConsoleKey direction = ConsoleKey.RightArrow;
        static int gameSpeed = 100; // Controle a velocidade do jogo aqui (quanto menor, mais rápido)

        static void Main(string[] args)
        {
            Setup();

            while (!gameOver)
            {
                Draw();
                Input();
                Logic();
                Thread.Sleep(gameSpeed);
            }

            Console.SetCursorPosition(width / 2 - 5, height / 2);
            Console.Write("Game Over!");
            Console.ReadLine();
        }

        static void Setup()
        {
            Console.CursorVisible = false;
            Console.SetWindowSize(width + 1, height + 1);
            headX = width / 2;
            headY = height / 2;
            tailX.Add(headX - 1);
            tailY.Add(headY);
            fruitX = random.Next(0, width);
            fruitY = random.Next(0, height);
        }

        static void Draw()
        {
            Console.Clear();

            for (int i = 0; i < width + 2; i++)
                Console.Write("#");

            Console.WriteLine();

            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    if (j == 0)
                        Console.Write("#");

                    if (i == headY && j == headX)
                        Console.Write("O");
                    else if (i == fruitY && j == fruitX)
                        Console.Write("F");
                    else
                    {
                        bool printTail = false;
                        for (int k = 0; k < tailX.Count; k++)
                        {
                            if (tailX[k] == j && tailY[k] == i)
                            {
                                Console.Write("o");
                                printTail = true;
                            }
                        }

                        if (!printTail)
                            Console.Write(" ");
                    }

                    if (j == width - 1)
                        Console.Write("#");
                }
                Console.WriteLine();
            }

            for (int i = 0; i < width + 2; i++)
                Console.Write("#");

            Console.WriteLine();
            Console.WriteLine("Score: " + score);
        }

        static void Input()
        {
            if (Console.KeyAvailable)
            {
                ConsoleKeyInfo key = Console.ReadKey(true);

                switch (key.Key)
                {
                    case ConsoleKey.LeftArrow:
                        if (direction != ConsoleKey.RightArrow)
                            direction = ConsoleKey.LeftArrow;
                        break;

                    case ConsoleKey.RightArrow:
                        if (direction != ConsoleKey.LeftArrow)
                            direction = ConsoleKey.RightArrow;
                        break;

                    case ConsoleKey.UpArrow:
                        if (direction != ConsoleKey.DownArrow)
                            direction = ConsoleKey.UpArrow;
                        break;

                    case ConsoleKey.DownArrow:
                        if (direction != ConsoleKey.UpArrow)
                            direction = ConsoleKey.DownArrow;
                        break;
                }
            }
        }

        static void Logic()
        {
            int prevX = tailX[0];
            int prevY = tailY[0];
            int prev2X, prev2Y;
            tailX[0] = headX;
            tailY[0] = headY;

            for (int i = 1; i < tailX.Count; i++)
            {
                prev2X = tailX[i];
                prev2Y = tailY[i];
                tailX[i] = prevX;
                tailY[i] = prevY;
                prevX = prev2X;
                prevY = prev2Y;
            }

            switch (direction)
            {
                case ConsoleKey.LeftArrow:
                    headX--;
                    break;

                case ConsoleKey.RightArrow:
                    headX++;
                    break;

                case ConsoleKey.UpArrow:
                    headY--;
                    break;

                case ConsoleKey.DownArrow:
                    headY++;
                    break;
            }

            if (headX < 0)
                headX = width - 1;
            else if (headX >= width)
                headX = 0;

            if (headY < 0)
                headY = height - 1;
            else if (headY >= height)
                headY = 0;

            for (int i = 0; i < tailX.Count; i++)
            {
                if (tailX[i] == headX && tailY[i] == headY)
                    gameOver = true;
            }

            if (headX == fruitX && headY == fruitY)
            {
                score += 10;
                fruitX = random.Next(0, width);
                fruitY = random.Next(0, height);
                tailX.Add(tailX[tailX.Count - 1]);
                tailY.Add(tailY[tailY.Count - 1]);
            }
        }
    }
}
