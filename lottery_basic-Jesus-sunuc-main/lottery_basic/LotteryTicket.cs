namespace lottery_basic 
{
    public class LotteryTicket
    {
        public int[] balls = new int[5];
        public int    powerBall;

        //ToDo: public Ticket(int[] specifiedNumbers){
        //      constructor with specified ticket choices
        public LotteryTicket(){ //No specified numbers - Constructor
            //RULE: first 5 balls are 1-69 inclusive, no duplicates in balls 1-5
            //RULE: 6th ball is 1-26 inclusive
//            var rnd = new System.Random(System.Environment.TickCount);               
            var rnd = new System.Random();               

            bool duplicate = false;
            for (int i=0; i<5; i++) {
                int x = rnd.Next(1,70);//1-69 inclusive
                //is this tryBall already in my ticket?  (no dups allowed)
                for (int j=0;j<i;j++) {
                    if (balls[j]==x) { 
                        duplicate = true;
                    }
                }
                if (duplicate == false) {
                    balls[i]= x;
                }
                else {
                    //force myself through loop again
                    i--; 
                    duplicate=false;
                }
            }//end of for() first 5 balls
            System.Array.Sort(balls);
            //get powerball
            powerBall=rnd.Next(1,27);//1-26 inclusive
        }
    }//end LotteryTicket
}