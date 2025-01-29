using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace DevSpot.Models
{
    public class ApiJobPosting
    {

        [JsonPropertyName("job_title")]
        public string Title { get; set; }

        [JsonPropertyName("url")]
        public string Url { get; set; }

        [JsonPropertyName("date_posted")]
        public DateTime DatePosted { get; set; }

        [JsonPropertyName("company")]
        public string Company { get; set; }
        [JsonPropertyName("location")]
        public string Location { get; set; }
        [JsonPropertyName("postal_code")]
        public string PostalCode { get; set; }

        [JsonPropertyName("remote")]
        public bool Remote { get; set; }

        [JsonPropertyName("hybrid")]
        public bool Hybrid { get; set; }

        [JsonPropertyName("salary_string")]
        public string SalaryString { get; set; }

        [JsonPropertyName("company_domain")]
        public string CompanyDomain { get; set; }


        [JsonPropertyName("description")]
        public string Description { get; set; }
        public string? ShortDescription
        {
            get
            {
                if (Description.Length <= 100)
                    return Description;
                return Description.Substring(0, 100) + "...";
            }
        }

        [JsonPropertyName("company_object")]
        public ApiCompany CompanyObject { get; set; }
       


    }

   

    public class ApiCompany
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("domain")]
        public string Domain { get; set; }

        [JsonPropertyName("logo")]
        public string Logo { get; set; }

        [JsonPropertyName("is_recruiting_agency")]
        public bool? IsRecruitingAgency { get; set; }

        
    }
 


}
