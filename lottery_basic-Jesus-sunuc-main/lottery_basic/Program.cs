using System;
using System.Collections.Generic;
using System.Threading; 

namespace lottery_basic
{
    class Program
    {
        static LotteryPeriod p = new LotteryPeriod();
        static int vTicketsToSell = 40000000; 
        static int vThreadCount=4; //VENDOR Thread Count

            
        static void Main(string[] args)
        {
            //Stopwatch to measure how long N threads take to sell _____ tickets
            System.Diagnostics.Stopwatch timer = new System.Diagnostics.Stopwatch();
            timer.Start();

            List<Thread> vTList = new List<Thread>();
            for (int i = 1; i < 5; i++)
            { 
                Thread thread = new Thread(tLotteryVendor);
                vTList.Add(thread);
                thread.Start();
            }

            foreach (Thread thread in vTList)
            {
                thread.Join();
            }

            //Stopwatch stop
            timer.Stop();
            Console.WriteLine("Elapsed time for {0} threads is: {1}", vThreadCount,timer.Elapsed);
            Console.WriteLine("Array has {0:n0} tickets sold.",p.soldTickets.Count);

            //Phase 3 - check for winners
            Thread thr4 = new Thread(tStatsCompilers);
            thr4.Start();
            Thread thr5 = new Thread(tStatsCompilers);
            thr5.Start();
            thr4.Join(); thr5.Join();
            Console.WriteLine("Winners are:  GRAND={0:n0} 1st={1:n0}  2nd={2:n0}  3rd={3:n0} 4th={4:n0}"+
                " 5th={5:n0}  6th={6:n0}  7th={7:n0}  8th={8:n0}",p.numGrandPrizeWinners,p.numAlt1Winners, p.numAlt2Winners,
                 p.numAlt3Winners, p.numAlt4Winners, p.numAlt5Winners, p.numAlt6Winners, p.numAlt7Winners, p.numAlt8Winners);
            
                        
        }
        static void tLotteryVendor(){
            LotteryVendor lv = new LotteryVendor();
            int ticketLimit=vTicketsToSell/vThreadCount;
            lv.purchaseTickets(p,ticketLimit);
            Console.WriteLine($"Thread finished with {ticketLimit:n0} tickets.");
        }

        static void tStatsCompilers(){
            LotteryStatistics ls = new LotteryStatistics();
            ls.compileStats(p);
            Console.WriteLine("Thread finished compiling stats");
        }
    }
}
