using System;
using System.Collections.Generic;
using System.Threading;

namespace HorseRacing
{
    class Program
    {
        //Todo: 1) Create 3 threads (one per horse)
        //Todo: 2) establish 3 AutoResetEvents  (one for each child thread).
        //Todo: 3) The main thread will then send a pulse out to all 3 threads by calling .Set()
        //Todo: 4) Each Horse thread will wait for a signal from main, and upon hearing it:
        //      4a) run a lottery 1/1000
        //      4b) if lottery won, then advance their horse 1 space (put a '-' on the screen)
        //Todo: 5) The first horse to travel 50 spaces is pronounced the winner
        //Todo: 6) 2 - All kids die appropriately  (hint - how did we do this in Matrix??)

        static Semaphore[] semaphores = new Semaphore[3];
        static Thread[] horseThreads = new Thread[3];
        static AutoResetEvent[] resetEvents = new AutoResetEvent[3];
        static int[] horsePositions = new int[3];
        static bool gameOver = false;

        public static void DrawScreen()
        {
            Console.SetCursorPosition(0, 6);
            Console.WriteLine($"HorseA: {new string('-', horsePositions[0])}");
            Console.WriteLine($"HorseB: {new string('-', horsePositions[1])}");
            Console.WriteLine($"HorseC: {new string('-', horsePositions[2])}");
        }

        public static void GiveEachHorseAChanceToRun(object indexObj)
        {
            int index = (int)indexObj;

            while (!gameOver)
            {
                resetEvents[index].WaitOne();

                if (new Random().Next() % 1000 == 0)
                {
                    horsePositions[index]++;
                }

                semaphores[index].Release();

                if (horsePositions[index] > 49)
                {
                    gameOver = true;
                }
            }
        }

        static void Main(string[] args)
        {
            Console.SetCursorPosition(0, 0);
            Console.WriteLine("Welcome to the Racing Program:");
            DrawScreen();
            List<Thread> threads = new List<Thread>();

            for (int i = 0; i < 3; i++)
            {
                semaphores[i] = new Semaphore(0, 1);
                horseThreads[i] = new Thread(new ParameterizedThreadStart(GiveEachHorseAChanceToRun));
                resetEvents[i] = new AutoResetEvent(false);
                horsePositions[i] = 0;
            }

            for (int i = 0; i < 3; i++)
            {
                horseThreads[i].Start(i);
            }

            while (!gameOver)
            {
                resetEvents[0].Set();
                resetEvents[1].Set();
                resetEvents[2].Set();

                semaphores[0].WaitOne();
                semaphores[1].WaitOne();
                semaphores[2].WaitOne();

                DrawScreen();
                Thread.Sleep(1/100);
                foreach (Thread r in threads)
                {
                    r.Start();
                }
            }

            int winnerIndex = -1;

            for (int i = 0; i < 3; i++)
            {
                if (horsePositions[i] > 49)
                {
                    if (winnerIndex == -1)
                    {
                        winnerIndex = i;
                    }
                    else
                    {
                        Console.WriteLine("There is a tie between two or more horses.");
                        return;
                    }
                }


            }

            Console.WriteLine($"The winner of this race was Horse{(char)('A' + winnerIndex)}.");

          

            foreach (Thread r in threads)
            {
                r.Join();
            }
        }
    }
}
