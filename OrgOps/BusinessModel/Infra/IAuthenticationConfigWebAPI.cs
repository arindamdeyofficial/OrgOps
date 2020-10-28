

namespace BusinessModel.Infra.Interface
{
    public interface IAuthenticationConfigWebAPI
    {
        /// <summary>
        /// Gets or sets the ClientId.
        /// </summary>
        string Instance { get; set; }

        /// <summary>
        /// Gets or sets the TenantId.
        /// </summary>
        string Domain { get; set; }

        /// <summary>
        /// Gets or sets the Authority.
        /// </summary>
        string TenantId { get; set; }

        /// <summary>
        /// Gets or sets the Authority.
        /// </summary>
        string ClientId { get; set; }

        /// <summary>
        /// Gets or sets the Authority.
        /// </summary>
        string ClientSecret { get; set; }

        /// <summary>
        /// Gets or Sets SubscriptionId
        /// </summary>
        string SubscriptionId { get; set; }
    }
}
