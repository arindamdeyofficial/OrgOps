using BusinessModel.Infra.Interface;

namespace BusinessModel.Infra.Implementation
{
    public class AzureActiveDirectoryClientApp : IAzureActiveDirectoryClientApp
    {
        /// <summary>
        /// Gets or sets the ClientId.
        /// </summary>
        public string ClientId { get; set; }

    }
}
