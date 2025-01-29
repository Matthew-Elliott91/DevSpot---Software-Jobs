using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Collections.Generic;
using DevSpot.Models;
using System.Text.Json.Serialization;
using System.Linq;
using System.Runtime.CompilerServices;

namespace DevSpot.Services
{
    public class JobPostingService
    {
        private readonly HttpClient _httpClient;

        public JobPostingService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<ApiJobPosting>> GetJobPostingsAsync()
        {
            var request = new HttpRequestMessage(HttpMethod.Post, "https://api.theirstack.com/v1/jobs/search");
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer",
                "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiJtLmVsbGlvdHQwOTEwOTFAZ21haWwuY29tIiwicGVybWlzc2lvbnMiOiJ1c2VyIn0.IkomR_jTXa-9pWMlWmMiFf1u3eAFyCO0bhcbTMRuXew");

            var requestBody = new
            {
                include_total_results = false,
                order_by = new[]
                {
                    new { desc = true, field = "date_posted" }
                },
                posted_at_max_age_days = 15,
                job_country_code_or = new[] { "GB" },
                job_title_or = new[]
                {
                    "Junior Software Developer",
                    "Junior Software Engineer",
                    "Junior Developer",
                    "Junior Programmer",
                    "Junior Software Programmer",
                    "Junior Web Developer",
                    "Junior Web Programmer",
                    "Junior Application Developer",
                    "Junior Application Programmer",
                    "Junior Full Stack Developer",
                    "Junior Frontend Developer",
                    "Junior Backend Developer",
                    "Junior Software Analyst",
                    "Junior Systems Developer",
                    "Junior Systems Programmer",
                    "Junior Cloud Developer",
                    "Junior Embedded Software Engineer",
                    "Junior Embedded Systems Programmer",
                    "Junior Mobile App Developer",
                    "Junior Game Developer",
                    "Junior Game Programmer",
                    "Graduate Software Developer",
                    "Graduate Software Engineer",
                    "Graduate Programmer",
                    "Entry-Level Software Developer",
                    "Entry-Level Software Engineer",
                    "Entry-Level Programmer",
                    "Associate Software Developer",
                    "Associate Software Engineer",
                    "Associate Programmer",
                    "IT Junior Developer",
                    "IT Junior Programmer",
                    "IT Junior Software Developer",
                    "IT Junior Software Engineer",
                    "IT Junior Software Programmer",
                    "IT Graduate Developer",
                    "IT Graduate Software Engineer",
                    "IT Entry-Level Developer",
                    "IT Entry-Level Programmer"
                },
                job_location_pattern_or = new[]
                {
                    "Leeds",
                    "Sheffield",
                    "Bradford",
                    "Kingston upon Hull",
                    "York",
                    "Huddersfield",
                    "Middlesbrough",
                    "Doncaster",
                    "Wakefield",
                    "Barnsley",
                    "Rotherham",
                    "Halifax",
                    "Harrogate",
                    "Beverley",
                    "Bridlington",
                    "Castleford",
                    "Dewsbury",
                    "Goole",
                    "Ilkley",
                    "Keighley",
                    "Pontefract",
                    "Scarborough",
                    "Selby",
                    "Skipton",
                    "Wetherby",
                    "South Yorkshire",
                    "Yorkshire",
                    "Nottingham",
                    "Nottinghamshire",
                    "Chesterfield"
                },
                page = 0,
                limit = 10,
                blur_company_data = true,
            };

            request.Content =
                new StringContent(JsonSerializer.Serialize(requestBody), Encoding.UTF8, "application/json");

            var response = await _httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();

            var responseContent = await response.Content.ReadAsStringAsync();
            var jobPostingsResponse = JsonSerializer.Deserialize<JobPostingsResponse>(responseContent,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            return jobPostingsResponse.Data;
        }

    }


    public class JobPostingsResponse
    {
        [JsonPropertyName("data")]
        public List<ApiJobPosting> Data { get; set; }
    }
}
