using System.Collections.Generic;

namespace lottery_basic
{
    public class LotteryPeriod
    {
        int[] winTicket={1,2,3,4,5,6}; //Winning Ticket Values
        public Stack<LotteryTicket> soldTickets = new Stack<LotteryTicket>();
        public int numGrandPrizeWinners=0;
        public object flagGrandPrizeWinners=new object();
        public object flagWin1 = new object();
        public object flagWin2 = new object();
        public object flagWin3 = new object();
        public object flagWin4 = new object();
        public object flagWin5 = new object();
        public object flagWin6 = new object();
        public object flagWin7 = new object();
        public object flagWin8 = new object();
        public int numAlt1Winners=0; public int numAlt2Winners=0; public int numAlt3Winners=0;
        public int numAlt4Winners=0; public int numAlt5Winners=0; public int numAlt6Winners=0;
        public int numAlt7Winners=0; public int numAlt8Winners=0; 
        
        

        public int isGrandPrizeWinner(LotteryTicket lt ){
            if ( ! (lt.powerBall==winTicket[5]) ) //powerball
                return 0; //not a winning ticket
            int[] iBallsMatch={0,0,0,0,0}; //flag array.  Set to 1 if Match!
            for (int i=0;i<5;i++)
            {       iBallsMatch[i]=0; //0 if lost, 1 if winner
                    for(int j=0;j<5;j++)
                    {       if (lt.balls[i]==winTicket[j])
                            {       iBallsMatch[i]=1; //this ticket is OK
                            }
                    }//end for
            }
            if (iBallsMatch[0]+iBallsMatch[1]+iBallsMatch[2]+iBallsMatch[3]+iBallsMatch[4]==5)
                    return 1; //Winner
            else
                    return 0; //not a winner
         }//end GrandPrizeWinner

        public int isWin1(LotteryTicket lt ){
            //NO POWERBALL  if ( ! (lt.powerBall==winTicket[5]) ) //powerball
            //NO POWERBALL      return 0; //not a winning ticket
            int[] iBallsMatch={0,0,0,0,0}; //flag array.  Set to 1 if Match!
            for (int i=0;i<5;i++)
            {       iBallsMatch[i]=0; //0 if lost, 1 if winner
                    for(int j=0;j<5;j++)
                    {       if (lt.balls[i]==winTicket[j])
                            {       iBallsMatch[i]=1; //this ticket is OK
                            }
                    }//end for
            }
            if (iBallsMatch[0]+iBallsMatch[1]+iBallsMatch[2]+iBallsMatch[3]+iBallsMatch[4]==5)
                    return 1; //Winner
            else
                    return 0; //not a winner
         }//end Win1

        public int isWin2(LotteryTicket lt ){
            if ( ! (lt.powerBall==winTicket[5]) ) //powerball
                return 0; //not a winning ticket
            int[] iBallsMatch={0,0,0,0,0}; //flag array.  Set to 1 if Match!
            for (int i=0;i<5;i++)
            {       iBallsMatch[i]=0; //0 if lost, 1 if winner
                    for(int j=0;j<5;j++)
                    {       if (lt.balls[i]==winTicket[j])
                            {       iBallsMatch[i]=1; //this ticket is OK
                            }
                    }//end for
            }
            if (iBallsMatch[0]+iBallsMatch[1]+iBallsMatch[2]+iBallsMatch[3]+iBallsMatch[4]==4)
                    return 1; //Winner
            else
                    return 0; //not a winner
         }
         
        public int isWin3(LotteryTicket lt ){
            //NO POWERBALL if ( ! (lt.powerBall==winTicket[5]) ) //powerball
            //NO POWERBALL    return 0; //not a winning ticket
            int[] iBallsMatch={0,0,0,0,0}; //flag array.  Set to 1 if Match!
            for (int i=0;i<5;i++)
            {       iBallsMatch[i]=0; //0 if lost, 1 if winner
                    for(int j=0;j<5;j++)
                    {       if (lt.balls[i]==winTicket[j])
                            {       iBallsMatch[i]=1; //this ticket is OK
                            }
                    }//end for
            }
            if (iBallsMatch[0]+iBallsMatch[1]+iBallsMatch[2]+iBallsMatch[3]+iBallsMatch[4]==4)
                    return 1; //Winner
            else
                    return 0; //not a winner
         }
         
        public int isWin4(LotteryTicket lt ){
            if ( ! (lt.powerBall==winTicket[5]) ) //powerball
                return 0; //not a winning ticket
            int[] iBallsMatch={0,0,0,0,0}; //flag array.  Set to 1 if Match!
            for (int i=0;i<5;i++)
            {       iBallsMatch[i]=0; //0 if lost, 1 if winner
                    for(int j=0;j<5;j++)
                    {       if (lt.balls[i]==winTicket[j])
                            {       iBallsMatch[i]=1; //this ticket is OK
                            }
                    }//end for
            }
            if (iBallsMatch[0]+iBallsMatch[1]+iBallsMatch[2]+iBallsMatch[3]+iBallsMatch[4]==3)
                    return 1; //Winner
            else
                    return 0; //not a winner
         }
         
        public int isWin5(LotteryTicket lt ){
            //IGNORE POWERBALL if ( ! (lt.powerBall==winTicket[5]) ) //powerball
            //IGNORE POWERBALL     return 0; //not a winning ticket
            int[] iBallsMatch={0,0,0,0,0}; //flag array.  Set to 1 if Match!
            for (int i=0;i<5;i++)
            {       iBallsMatch[i]=0; //0 if lost, 1 if winner
                    for(int j=0;j<5;j++)
                    {       if (lt.balls[i]==winTicket[j])
                            {       iBallsMatch[i]=1; //this ticket is OK
                            }
                    }//end for
            }
            if (iBallsMatch[0]+iBallsMatch[1]+iBallsMatch[2]+iBallsMatch[3]+iBallsMatch[4]==3)
                    return 1; //Winner
            else
                    return 0; //not a winner
         }        
         public int isWin6(LotteryTicket lt ){
            if ( ! (lt.powerBall==winTicket[5]) ) //powerball
                return 0; //not a winning ticket
            int[] iBallsMatch={0,0,0,0,0}; //flag array.  Set to 1 if Match!
            for (int i=0;i<5;i++)
            {       iBallsMatch[i]=0; //0 if lost, 1 if winner
                    for(int j=0;j<5;j++)
                    {       if (lt.balls[i]==winTicket[j])
                            {       iBallsMatch[i]=1; //this ticket is OK
                            }
                    }//end for
            }
            if (iBallsMatch[0]+iBallsMatch[1]+iBallsMatch[2]+iBallsMatch[3]+iBallsMatch[4]==2)
                    return 1; //Winner
            else
                    return 0; //not a winner
         }
        public int isWin7(LotteryTicket lt ){
            if ( ! (lt.powerBall==winTicket[5]) ) //powerball
                return 0; //not a winning ticket
            int[] iBallsMatch={0,0,0,0,0}; //flag array.  Set to 1 if Match!
            for (int i=0;i<5;i++)
            {       iBallsMatch[i]=0; //0 if lost, 1 if winner
                    for(int j=0;j<5;j++)
                    {       if (lt.balls[i]==winTicket[j])
                            {       iBallsMatch[i]=1; //this ticket is OK
                            }
                    }//end for
            }
            if (iBallsMatch[0]+iBallsMatch[1]+iBallsMatch[2]+iBallsMatch[3]+iBallsMatch[4]==1)
                    return 1; //Winner
            else
                    return 0; //not a winner
         }    
        public int isWin8(LotteryTicket lt ){
            if ( lt.powerBall==winTicket[5] ) //powerball
                    return 1; //Winner
            else
                    return 0; //not a winner
         }    
    }
}