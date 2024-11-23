

namespace JoshuaMackaySTFCCodingTest
{
    /// <summary>
    /// A Calculator for orders from 'Coffee Snobs'.
    /// </summary>
    public class OrderCostCalculator
    {
        /// <summary>
        /// A dictionary holding the price of drink extras as a float keyed against the type of extra as a string. 
        /// </summary>
        private readonly Dictionary<string, float> drinkExtras = new()
        {
            { "milk", 0.53f},
            { "sugar", 0.17f},
            { "cream", 0.73f },
            { "sprinkles", 0.29f},
        };

        /// <summary>
        /// A dictionary holding the price of cakes as a float keyed against the type of cake as a string. 
        /// </summary>
        private readonly Dictionary<string, float> cakes = new()
        {
            { "muffins", 2.03f},
            { "flapjacks", 2.59f},
            { "panettones", 2.88f},
        };

        /// <summary>
        /// A list of supported beverages
        /// </summary>
        private readonly List<Beverage> possibleBeverages = new()
        {
            new DecafCoffee(),
            new RegularCoffee(),
        };

        /// <summary>
        /// Calculates the overall cost of an order, given in a comma-separated string of the form 
        /// "1 x regular + milk + sugar, 2 x decaf + cream, 5 x muffins".
        /// </summary>
        /// <param name="order">A comma-separated string representing the order in the format 
        /// "1 x regular + milk + sugar, 2 x decaf + cream, 5 x muffins" </param>
        /// <returns> A string giving the final bill in dollars.</returns>
        public string CalculateCost(string order)
        {
            // Holds the total cost
            float totalCost = 0;

            // Split the csv order into individual orders
            List<string> separatedOrder = order.Split(',').ToList();

            // Calculate the cost for each individual order, and update totalCost
            foreach (string individualOrder in separatedOrder)
            {
                totalCost += CalculateIndividualCost(individualOrder);
            }

            // Make the cost a 2dp decimal to fit format of currency while avoiding floating point inaccuracies
            decimal decimalCost = Decimal.Round((decimal)totalCost, 2);

            // Create result string
            string result = $"Final bill is ${decimalCost}";

            // If the total cost is a whole number of dollars, add .00 to the string
            if (decimal.Remainder(decimalCost, 1m) == 0)
            {
                result += ".00";
            }
            // If the total cost would have a single dp, add a 0 at the end to fit format of currency correctly.
            else if (decimal.Remainder(decimalCost, 0.1m) == 0)
            {
                result += "0";
            }
            

            return result;
        }

        /// <summary>
        /// Calculates the cost of an individual item in the format of
        /// "1 x regular + milk + sugar" or " 1 x muffin".
        /// </summary>
        /// <param name="order"> The string representing the individual order.</param>
        /// <returns>A float representing the cost of the individual order.</returns>
        private float CalculateIndividualCost(string order) {

            // Check the order contains an 'x' character to seperate the number of items ordered from the order contents
            if (!order.Contains('x'))
            {
                Console.WriteLine($"Invalid format for item {order}. Item skipped.");
                return 0;
            }

            // Cost of the order
            float orderCost = 0;

            //Get number of items ordered
            int numberOfItemsSeparatorIndex = order.IndexOf('x');
            string numberOfItemsString = order[..numberOfItemsSeparatorIndex];
            if (!int.TryParse(numberOfItemsString, out int numberOfItems))
            {
                Console.WriteLine($"Invalid format for item {order}. Item skipped.");
                return 0;
            }

            // Get list of item contents
            string itemContents = order[(numberOfItemsSeparatorIndex + 1)..];
            List<string> itemContentsList = itemContents
                .Split('+')
                .Select(c => c.Trim().ToLower())
                .ToList();

            // Check if order is a beverage
            Beverage? beverageOrdered = possibleBeverages.SingleOrDefault(b => b.getDescription() == itemContentsList[0]);

            
            if (beverageOrdered is null)// Not a beverage
            { 
                
                // If not a beverage, then there should only be one item
                if (itemContentsList.Count != 1)
                {
                    Console.WriteLine($"Invalid format or unsupported beverage for item {order}. Item skipped.");
                }
                try{
                    // Get cake from order list

                    string cake = itemContentsList[0];

                    // Check if missing plural

                    if (!cake.EndsWith('s'))
                    {
                        cake += 's';
                    }

                    // Retrieve cake price from dictionary
                    orderCost = cakes[cake];
                }
                catch (KeyNotFoundException)
                {
                    Console.WriteLine($"Unsupported item type of {order}. Item skipped");
                    return 0;
                }
            }
            else // Is a beverage
            {
                // Get cost from the beverage object
                orderCost = beverageOrdered.getCost();

                //If no extras, multiply the beverage cost by the number of items and return
                if(itemContentsList.Count == 1)
                { 
                    return orderCost * numberOfItems;
                }
                
                // Go through the extras and add to cost
                itemContentsList.RemoveAt(0);
                foreach (string drinkExtra in itemContentsList) {
                    try
                    {
                        orderCost += drinkExtras[drinkExtra];
                    }
                    catch (KeyNotFoundException)
                    {
                        Console.WriteLine($"Unsupported drink extra of {drinkExtra} in {order}. Item skipped");
                        return 0;
                    }
                }

            }
            // Multiply the cost by the number of items ordered and return
            orderCost *= numberOfItems;

            return orderCost;

        }
        
    }
}
