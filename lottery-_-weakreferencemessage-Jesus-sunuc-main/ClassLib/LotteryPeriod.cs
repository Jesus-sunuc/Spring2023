﻿using CommunityToolkit.Mvvm.Messaging;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;


namespace ClassLib
{
    public enum TicketSales
    {
        OK,
        CLOSED
    }

    // These next items are suggested coordination points (message types) between client/server (admin/vendor)
        public record ProgramStateChanged();
        public record VendorResultsNotAvailable();
        public record VendorResultsAvailable();
        public record TicketSaleCountUpdated(long SoldTickets);


    public class LotteryPeriod
    {
        int[] winTicket = { 1, 2, 3, 4, 5, 6 }; //Winning Ticket Values
        public ConcurrentStack<LotteryTicket> soldTickets = new ConcurrentStack<LotteryTicket>();
        public List<LotteryTicket> winningTicketsL = new List<LotteryTicket>();
        public List<LotteryTicket> losingTicketsL = new List<LotteryTicket>();
        private int _salesState;  //change to int
        public ReaderWriterLockSlim soldTicketsLock = new ReaderWriterLockSlim();
        public DateTime PeriodBeginTS { get; set; }

        public LotteryPeriod(decimal grandp)
        {
            this.PeriodBeginTS = DateTime.Now;
            GrandPrizeAmount = grandp;
        }

        public TicketSales SalesState
        {
            get { return (TicketSales)_salesState; }
            set
            {
                soldTicketsLock.EnterWriteLock();
                try
                {
                    System.Threading.Interlocked.Exchange(ref _salesState, (int)value);
                    //TODO - How will you send a message to the client/Vendors telling them the state has changed?
                    WeakReferenceMessenger.Default.Send(new ProgramStateChanged());
                    if ((TicketSales)_salesState !=TicketSales.OK);
                        //TODO - How will you send a message to the client/Vendors telling them that Vendor Results are not available?
                        WeakReferenceMessenger.Default.Send(new VendorResultsNotAvailable());
                }
                finally
                {
                    soldTicketsLock.ExitWriteLock();
                }
            }
        }

        public decimal GrandPrizeAmount { get; set; }

        public LotteryTicket WinningTicket { get; set; }

        public bool DrawWinningTicket()
        {
            if (SalesState == TicketSales.CLOSED)
            {
                WinningTicket = new LotteryTicket("WinningTicket");
                return true;
            }
            else
                return false;
        }
        public long GetVendorNumberOfTicketsSold(string vendor) =>
            winningTicketsL.Where(t => t.Player == vendor).Count() +
            losingTicketsL.Where(t => t.Player == vendor).Count();

        public IEnumerable<LotteryTicket> GetVendorWinningTickets(string vendor)
        {
            return winningTicketsL.Where(t => t.Player == vendor)
                .OrderBy(t => t.winLevel)
                .ThenBy(t => String.Join(" ",t.balls.Select(b=>b.ToString("00"))))
                .ThenBy(t => t.powerBall);
        }
        public IEnumerable<LotteryTicket> GetVendorLosingTickets(string vendor)
        {
            return losingTicketsL.Where(t => t.Player == vendor)
                .OrderBy(t => t.balls.ToString())
                .ThenBy(t => t.powerBall)
                .ToList();
        }
        public void ComputeWinners()
        {
            //TODO: ensure the state is set to ONLY let this work if drawing has started/ended

            LotteryTicket lt;
            if (SalesState == TicketSales.CLOSED)
            {
                while (!soldTickets.IsEmpty)
                {
                    try
                    {
                        if (soldTickets.TryPop(out lt))
                        {
                            CheckWinningTicket(lt);
                            if (lt.winLevel > 0)
                            {
                                winningTicketsL.Add(lt);
                            }
                            else
                            {
                                losingTicketsL.Add(lt);
                            }
                        }
                        //each ticket is moved from soldTickets to ( winningTickets or loosingTickets )
                    }
                    catch (System.InvalidOperationException)
                    {
                        //"All sold tickets have been 'computed'"
                        break;
                    }//leave the while 
                }

            }
            else
            {
                throw new ApplicationException("Period must be closed before winners can be computed");
            }
        }

        public int NumberMatchingWhiteBalls(LotteryTicket lt)
        {
            int numMatches = 0;
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    if (lt.balls[i] == WinningTicket.balls[j])
                    {
                        numMatches++;
                    }
                }//end for
            }
            return numMatches;
        }
        public void CheckWinningTicket(LotteryTicket lt)
        {
            int whiteMatches = NumberMatchingWhiteBalls(lt);
            if (lt.powerBall == WinningTicket.powerBall && whiteMatches == 5)
            {
                lt.winLevel = 1;
                lt.winAmtDollars = GrandPrizeAmount;
            }
            else if (whiteMatches == 5)
            {
                lt.winLevel = 2;
                lt.winAmtDollars = 1000000;//$1M
            }
            else if (lt.powerBall == WinningTicket.powerBall && whiteMatches == 4)
            {
                lt.winLevel = 3;
                lt.winAmtDollars = 50000;//$50k
            }
            else if (whiteMatches == 4)
            {
                lt.winLevel = 4;
                lt.winAmtDollars = 100;//$100
            }
            else if (lt.powerBall == WinningTicket.powerBall && whiteMatches == 3)
            {
                lt.winLevel = 5;
                lt.winAmtDollars = 100;//$100
            }
            else if (whiteMatches == 3)
            {
                lt.winLevel = 6;
                lt.winAmtDollars = 7;//$7
            }
            else if (lt.powerBall == WinningTicket.powerBall && whiteMatches == 2)
            {
                lt.winLevel = 7;
                lt.winAmtDollars = 7;//$7
            }
            else if (lt.powerBall == WinningTicket.powerBall && whiteMatches == 1)
            {
                lt.winLevel = 8;
                lt.winAmtDollars = 4;//$4
            }
            else if (lt.powerBall == WinningTicket.powerBall && whiteMatches == 0)
            {
                lt.winLevel = 9;
                lt.winAmtDollars = 4;//$4
            }
            else
            {
                lt.winLevel = 0;
                lt.winAmtDollars = 0;
            }

            lt.isGraded = true;
        }
    }
}
