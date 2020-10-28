namespace BusinessModel.Common
{
    /// <summary>
    /// Defines the <see cref="ResiliencyConfigs" />.
    /// </summary>
    public class ResiliencyConfigs
    {
        /// <summary>
        /// Gets or sets the .
        /// </summary>
        public bool FallBackEnabled { get; }

        /// <summary>
        /// Gets or sets the .
        /// </summary>
        public bool RetryEnabled { get; }

        /// <summary>
        /// Gets or sets the .
        /// </summary>
        public bool CircuitBreakerEnabled { get; }

        /// <summary>
        /// Gets or sets the .
        /// </summary>
        public bool BulkHeadEnabled { get; }

        /// <summary>
        /// Gets or sets the .
        /// </summary>
        public bool TimeOutEnabled { get; }

    }
}
