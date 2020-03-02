using Repository;
using Repository.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CustomerListApp
{
    public class CustomerListApplication
    {
        /// <summary>
        /// Sends an array of all costa another metods and shows the sum of them
        /// </summary>
        public void Start()
        {
            List<Order> orders = OrderRepository.GetOrders();
            int count = orders.Count;
            string[] costs = new string[count];

            for (int i = 0; i < count; i++)
            {
                costs[i] = orders[i].Cost;
            }
            int sumOfOrdersCosts = GetSumOfOrdersCost(costs);
            Console.WriteLine("Total cost of all orders is: " + sumOfOrdersCosts);

            Console.Write("\nPress any key to Exit ...");
            Console.ReadKey();
        }

        /// <summary>
        /// Calculates the sum of all array values and returns the sum
        /// </summary>
        /// <param name="costs"></param>
        /// <returns>Sum of all array values</returns>
        public int GetSumOfOrdersCost(string[] costs)
        {
            int sumOfOrdersCosts = 0;

            foreach (var cost in costs)
            {
                sumOfOrdersCosts += int.Parse(cost);
            }

            return sumOfOrdersCosts;
        }

    }
}
