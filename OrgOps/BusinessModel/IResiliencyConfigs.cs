namespace BusinessModel.Common
{
    /// <summary>
    /// Defines the <see cref="ResiliencyConfigs" />.
    /// </summary>
    public interface IResiliencyConfigs
    {
        /// <summary>
        /// Gets or sets the .
        /// </summary>
        public int FallBackEnabled { get; }

        /// <summary>
        /// Gets or sets the .
        /// </summary>
        public int RetryEnabled { get; }

        /// <summary>
        /// Gets or sets the .
        /// </summary>
        public int CircuitBreakerEnabled { get; }

        /// <summary>
        /// Gets or sets the .
        /// </summary>
        public int BulkHeadEnabled { get; }

        /// <summary>
        /// Gets or sets the .
        /// </summary>
        public int TimeOutEnabled { get; }

    }
}
