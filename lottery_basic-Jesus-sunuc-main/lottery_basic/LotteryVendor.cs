using System.Collections.Generic;
namespace lottery_basic
{
    public class LotteryVendor
    {
        public  void purchaseTickets(LotteryPeriod period, int sellLimit){
            if (sellLimit<1) {   throw new System.ArgumentException("Parameter cannot be <1", "sellLimit");   }

            //need to buy/sell at least one more ticket
            List<LotteryTicket> listOfLotteryTicket = new();
            for (int i=0;i<sellLimit;i++)
            {
                listOfLotteryTicket.Add(new LotteryTicket());

                if(listOfLotteryTicket.Count == 1000)
                lock(period.soldTickets)
                {
                    foreach (LotteryTicket lotteryTicketInList in listOfLotteryTicket)
                    {
                        period.soldTickets.Push(lotteryTicketInList);
                    }
                    listOfLotteryTicket.Clear();
                }
            }
            System.Console.WriteLine("Done selling {0} tickets.",sellLimit);
        }
    }
}