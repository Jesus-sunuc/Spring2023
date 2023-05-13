namespace threads {
    class Program
    {
        static int pickers = 3; // Define the number of picker threads
        static int canners = 5; // Define the number of canner threads

        public static void Main()
        {
            PickApple pa = new PickApple();
            // Create the picker threads
            Thread[] pickerThreads1 = new Thread[pickers];
            for (int i = 0; i < pickers; i++)
            {
                pickerThreads1[i] = new Thread(() => pa.PickApples(i));
                pickerThreads1[i].Start();
            }

            // Create the canner threads
            Thread[] cannerThreads = new Thread[canners];
            for (int i = 0; i < canners; i++)
            {
                cannerThreads[i] = new Thread(() => pa.CanApples(i));
                cannerThreads[i].Start();
            }

            // Wait for all the threads to finish
            foreach (Thread t in pickerThreads1)
            {
                t.Join();
            }
      
            foreach (Thread t in cannerThreads)
            {
                t.Join();
            }
        }
    }
}