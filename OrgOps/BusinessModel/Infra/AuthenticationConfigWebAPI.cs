using BusinessModel.Infra.Interface;

namespace BusinessModel.Infra.Implementation
{
    public class AuthenticationConfigWebAPI : IAuthenticationConfigWebAPI
    {
        /// <summary>
        /// Gets or sets the ClientId.
        /// </summary>
        public string Instance { get; set; }

        /// <summary>
        /// Gets or sets the TenantId.
        /// </summary>
        public string Domain { get; set; }

        /// <summary>
        /// Gets or sets the Authority.
        /// </summary>
        public string TenantId { get; set; }

        /// <summary>
        /// Gets or sets the Authority.
        /// </summary>
        public string ClientId { get; set; }

        /// <summary>
        /// Gets or sets the Authority.
        /// </summary>
        public string ClientSecret { get; set; }

        /// <summary>
        /// Gets or Sets SubscriptionId
        /// </summary>
        public string SubscriptionId { get; set; }
    }
}
