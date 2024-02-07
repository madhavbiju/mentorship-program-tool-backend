using Newtonsoft.Json;

namespace mentorship_program_tool.Models.GraphModel
{
    public class GraphUserData
    {
        [JsonProperty("displayName")]
        public string DisplayName { get; set; }

        [JsonProperty("givenName")]
        public string GivenName { get; set; }
        [JsonProperty("surname")]
        public string SurName { get; set; }

        [JsonProperty("familyName")]
        public string FamilyName { get; set; }

        [JsonProperty("userPrincipalName")]
        public string UserPrincipalName { get; set; }

        [JsonProperty("jobTitle")]
        public string JobTitle { get; set; }

        [JsonProperty("department")]
        public string Department { get; set; }

        [JsonProperty("id")]
        public string EmployeeId { get; set; }

        // Add other properties as needed
    };
}
