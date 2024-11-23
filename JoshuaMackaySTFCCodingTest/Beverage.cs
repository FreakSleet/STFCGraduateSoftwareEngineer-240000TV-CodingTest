namespace JoshuaMackaySTFCCodingTest
{
    /// <summary>
    /// An abstract Beverage class with a cost and description.
    /// </summary>
    public abstract class Beverage
    {
        /// <summary>
        /// The cost of the beverage.
        /// </summary>
        private readonly float cost;
        /// <summary>
        /// A description of the type of beverage.
        /// </summary>
        private readonly string description;

        /// <summary>
        /// Creates an instance of the <see cref="Beverage"/> class.
        /// </summary>
        /// <param name="cost">The cost of the beverage. </param>
        /// <param name="description"> The name of the type of beverage. </param>
        public Beverage(float cost, string description)
        {
            this.cost = cost;
            this.description = description.ToLower();
        }

        /// <summary>
        /// Gets the cost of the beverage.
        /// </summary>
        /// <returns> The cost in dollars as a float.</returns>
        public float getCost()
        {
            return this.cost;
        }

        /// <summary>
        /// Gets the type of beverage.
        /// </summary>
        /// <returns>A string describing the type of beverage. </returns>
        public  string getDescription()
        {
            return this.description;
        }
    }
}