using GraphQL;
using GraphQL.Client.Http;
using GraphQL.Client.Serializer.Newtonsoft;

namespace TwitterClient.Services
{
    public class GraphQlCallServices
    {
        private readonly IConfiguration Configuration;
        public GraphQlCallServices(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public async Task<T> sendRequest<T>(string input, string token = "")
        {
            var graphQLClient = new GraphQLHttpClient(Configuration["baseUrl"], new NewtonsoftJsonSerializer());
            var Request = new GraphQLRequest(input);
            if (!string.IsNullOrEmpty(token))
                graphQLClient.HttpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
            var graphQLResponse = await graphQLClient.SendQueryAsync<T>(Request);
            if (graphQLResponse.Data is not null)
                return graphQLResponse.Data;
            return default;

        }
    }
}
