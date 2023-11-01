using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

class Program
{
    static void Main(string[] args)
    {
        Console.WindowHeight = 16;
        Console.WindowWidth = 32;
        int screenwidth = Console.WindowWidth;
        int screenheight = Console.WindowHeight;
        Random randomnummer = new Random();
        int score = 0;

        int screenmiddle = screenwidth / 2;

        // SNAKE
        int snekposX = screenwidth / 2;
        int snekposY = screenheight / 2;
        List<int> sneklijfX = new List<int> { snekposX - 1, snekposX - 2, snekposX - 3 };
        List<int> sneklijfY = new List<int> { snekposY, snekposY, snekposY };
        int sneksnelheid = 100;
        int snekraketi = 0;
        int snekhoofd = 1;

        // BARRIER
        int barriereposX = randomnummer.Next(1, screenwidth - 1);
        int barriereposY = randomnummer.Next(1, screenheight - 1);

        DateTime tijd = DateTime.Now;
        DateTime tijd2 = DateTime.Now;
        string movement = "RIGHT";
        string movement2 = "RIGHT";
        int kogelsnelheid = 0;

        while (true)
        {
            Console.Clear();
            if (sneklijfX[0] == screenwidth - 1)
                break;

            #region STATS
            TimeSpan span = DateTime.Now - tijd;
            int ms = span.Milliseconds;
            if (ms > sneksnelheid)
            {
                tijd = DateTime.Now;
                snekraketi++;
                // START Movement
                for (int i = sneklijfX.Count() - 1; i >= 0; i--)
                {
                    if (sneklijfX[i] == snekposX && sneklijfY[i] == snekposY)
                        break;
                }
                if (snekraketi == snekhoofd)
                {
                    if (movement == "RIGHT")
                        snekposX++;
                    if (movement == "LEFT")
                        snekposX--;
                    if (movement == "DOWN")
                        snekposY++;
                    if (movement == "UP")
                        snekposY--;
                    snekraketi = 0;
                    sneklijfX.Add(snekposX);
                    sneklijfY.Add(snekposY);
                    if (sneklijfX.Count() > snekhoofd)
                    {
                        sneklijfX.RemoveAt(0);
                        sneklijfY.RemoveAt(0);
                    }
                }
                // END Movement
            }
            if (Console.KeyAvailable)
            {
                var key = Console.ReadKey(true).Key;
                movement2 = movement;
                switch (key)
                {
                    case ConsoleKey.UpArrow:
                        if (movement != "DOWN")
                            movement = "UP";
                        break;
                    case ConsoleKey.DownArrow:
                        if (movement != "UP")
                            movement = "DOWN";
                        break;
                    case ConsoleKey.LeftArrow:
                        if (movement != "RIGHT")
                            movement = "LEFT";
                        break;
                    case ConsoleKey.RightArrow:
                        if (movement != "LEFT")
                            movement = "RIGHT";
                        break;
                    case ConsoleKey.Spacebar:
                        if (kogelsnelheid == 0)
                        {
                            kogelsnelheid = 1;
                            movement2 = movement;
                        }
                        break;
                }
            }
            #endregion

            #region SNAKE
            for (int i = 0; i < sneklijfX.Count(); i++)
            {
                Console.SetCursorPosition(sneklijfX[i], sneklijfY[i]);
                if (i == sneklijfX.Count() - 1)
                {
                    Console.Write("0");
                }
                else
                {
                    Console.Write("o");
                }
            }
            #endregion

            #region BARRIER
            if (barriereposX == snekposX && barriereposY == snekposY)
            {
                score++;
                snekhoofd++;
                barriereposX = randomnummer.Next(1, screenwidth - 1);
                barriereposY = randomnummer.Next(1, screenheight - 1);
                if (sneksnelheid >= 10)
                {
                    sneksnelheid--;
                }
            }
            else
            {
                Console.SetCursorPosition(barriereposX, barriereposY);
                Console.Write("X");
            }
            #endregion

            #region ROCKET
            if (kogelsnelheid != 0)
            {
                tijd2 = DateTime.Now;
                TimeSpan span2 = tijd2 - tijd;
                int ms2 = span2.Milliseconds;
                if (ms2 > 50)
                {
                    if (movement2 == "RIGHT")
                        kogelsnelheid++;
                    if (movement2 == "LEFT")
                        kogelsnelheid--;
                    tijd = DateTime.Now;
                }
                if (kogelsnelheid > screenwidth - 1)
                    kogelsnelheid = 0;
                if (kogelsnelheid < 1)
                    kogelsnelheid = screenwidth - 1;
                Console.SetCursorPosition(kogelsnelheid, screenheight - 1);
                Console.Write("|");
                Console.SetCursorPosition(kogelsnelheid, screenheight - 2);
                Console.Write("|");
                if (barriereposX == kogelsnelheid && barriereposY == screenheight - 1)
                {
                    barriereposX = randomnummer.Next(1, screenwidth - 1);
                    barriereposY = randomnummer.Next(1, screenheight - 1);
                    kogelsnelheid = 0;
                }
            }
            #endregion

            Thread.Sleep(50);
        }
        Console.SetCursorPosition(screenmiddle - 5, screenheight / 2);
        Console.WriteLine("Je hebt verloren, Score: " + score);
        Console.SetCursorPosition(screenmiddle - 15, (screenheight / 2) + 1);
        return;
    }
}
