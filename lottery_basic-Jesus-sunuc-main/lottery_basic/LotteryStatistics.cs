namespace lottery_basic
{
    public class LotteryStatistics
    {
        
         public void compileStats(LotteryPeriod p){
             while(p.soldTickets.Count>0){
                    LotteryTicket lt = new LotteryTicket();
                    lock(p.soldTickets){
                        try{
                            lt=p.soldTickets.Pop();
                        }
                        catch (System.InvalidOperationException){ break;}//leave the while
                    }
                    if (p.isGrandPrizeWinner(lt)>0) { //GRAND PRIZE 
                        lock(p.flagGrandPrizeWinners){
                            p.numGrandPrizeWinners++;
                        }
                    } else if (p.isWin1(lt)>0){//1st Prize
                        lock(p.flagWin1){
                            p.numAlt1Winners++;
                        }
                    } else if (p.isWin2(lt)>0){
                        lock(p.flagWin2){
                            p.numAlt2Winners++;
                        }
                    }  else if (p.isWin3(lt)>0){// Prize
                        lock(p.flagWin3){
                            p.numAlt3Winners++;
                        }
                    } else if (p.isWin4(lt)>0){
                        lock(p.flagWin4){
                            p.numAlt4Winners++;
                        }
                    }  else if (p.isWin5(lt)>0){// Prize
                        lock(p.flagWin5){
                            p.numAlt5Winners++;
                        }
                    } else if (p.isWin6(lt)>0){
                        lock(p.flagWin6){
                            p.numAlt6Winners++;
                        }
                    }  else if (p.isWin7(lt)>0){//Prize
                        lock(p.flagWin7){
                            p.numAlt7Winners++;
                        }
                    } else if (p.isWin8(lt)>0){
                        lock(p.flagWin8){
                            p.numAlt8Winners++;
                        }
                    }//otherwise, it is a losing ticket
             }//end while
         }//end CompileStats()
    }
}
