namespace APIAutomation.Interfaces
{
    using Framework.Common.Api.Utilities;

    using RestSharp;

    public interface IRequestBuilder
    {
        /// <summary>
        /// The build request.
        /// </summary>
        /// <param name="arguments">
        /// The arguments.
        /// </param>
        /// <returns>
        /// The <see cref="IRestRequest"/>.
        /// </returns>
        RestRequest BuildRequest(RequestArguments arguments);
    }
}
