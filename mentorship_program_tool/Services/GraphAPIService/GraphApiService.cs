using mentorship_program_tool.Data;
using mentorship_program_tool.Models.GraphModel;

namespace mentorship_program_tool.Services.GraphAPIService
{
    public class GraphApiService
    {
        private readonly HttpClient _httpClient;
        private readonly AppDbContext _dbContext;

        public GraphApiService(HttpClient httpClient, AppDbContext dbContext)
        {
            _httpClient = httpClient;
            _dbContext = dbContext;
            _httpClient.BaseAddress = new Uri("https://graph.microsoft.com/v1.0/");
        }

        public async Task<GraphUserData> GetUserDetailsAsync(string accessToken)
        {
            // Microsoft Graph API endpoint for user details
            var graphApiEndpoint = "me?$select=department,id,givenName,surName,userPrincipalName,jobTitle";

            // Create a request to the Graph API
            var request = new HttpRequestMessage(HttpMethod.Get, graphApiEndpoint);

            // Set the Authorization header with the access token
            request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", accessToken);

            // Send the request and process the response
            var response = await _httpClient.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                var userGraphData = Newtonsoft.Json.JsonConvert.DeserializeObject<GraphUserData>(await response.Content.ReadAsStringAsync());

                // Insert values into AzureModel table



                return userGraphData;
            }

            // Handle error cases if needed
            return null;
        }
    }
}
