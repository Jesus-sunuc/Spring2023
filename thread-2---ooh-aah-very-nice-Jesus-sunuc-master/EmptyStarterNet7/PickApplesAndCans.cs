using System;

namespace threads {
	public class PickApple
	{
		// Define a lock object to protect the shared data
		private object truckLock = new object();
		// Define the shared data	
		static int maxAppleInABag = 5;
		// The number of apples in the truck
		static int applesInTruck = 0;
		// The maximum number of apples in the truck
		static int maxAppleInTheTruck = 50;

		// Define the methods that will be called by the threads
		public void PickApples(int id)
		{
			while (true)
			{
				int appleToPick; // Define a variable to save the number of apples to pick
				lock (truckLock) // Lock the truck
				{
					if (applesInTruck == maxAppleInTheTruck)	// Check if the truck is full
					{
						break; 		// If the truck is full, break out of the loop
					}

					appleToPick = Math.Min(maxAppleInABag, maxAppleInTheTruck - applesInTruck); // Calculate the number of apples to pick
					applesInTruck += appleToPick; // Add the number of apples to pick to the truck
					Console.WriteLine($"Picker {id} has picked {appleToPick} apples. The truck has: {applesInTruck}"); // Print the number of apples picked and the number of apples in the truck
				}
			}
		}
		public void CanApples(int id) // Define the method that will be called by the canner threads
		{
			while (true)
			{
				int applesToCan; // Define a variable to save the number of apples to can
				lock (truckLock) // Lock the truck
				{
					if (applesInTruck == 0)	// Check if the truck is empty
					{
						break;	// If the truck is empty, break out of the loop
					}

					applesToCan = Math.Min(maxAppleInABag, applesInTruck); // Calculate the number of apples to can
					applesInTruck -= applesToCan; // Subtract the number of apples to can from the truck
				}
					Console.WriteLine($"Canner {id} has canned {applesToCan} apples. Apples in the truck: {applesInTruck}"); // Print the number of apples canned and the number of apples in the truck
			}
		}
	}
}