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
                job_title_or = new[] { "Junior Software Developer", "Junior Software Engineer" },
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

        public List<JobPosting> MapToJobPostings(List<ApiJobPosting> apiJobPostings)
        {
            return apiJobPostings.Select(apiJobPosting => new JobPosting
            {
                Title = apiJobPosting.Title,
                Company = apiJobPosting.Company,
                Location = apiJobPosting.Location,
                DatePosted = apiJobPosting.DatePosted,
                Description = apiJobPosting.Description,
                SalaryString = apiJobPosting.SalaryString,
                Remote = apiJobPosting.Remote,
                Hybrid = apiJobPosting.Hybrid,
                JobUrl = apiJobPosting.Url,
                CompanyDomain = apiJobPosting.CompanyDomain,
                CompanyObject = new Company
                {
                    Name = apiJobPosting.CompanyObject?.Name,
                    Domain = apiJobPosting.CompanyObject?.Domain,
                    Logo = apiJobPosting.CompanyObject?.Logo,
                    IsRecruitingAgency = apiJobPosting.CompanyObject?.IsRecruitingAgency
                }
            }).ToList();
        }
    }


    public class JobPostingsResponse
    {
        [JsonPropertyName("data")]
        public List<ApiJobPosting> Data { get; set; }
    }
}
