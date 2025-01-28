using System.Security.AccessControl;
using DevSpot.Models;
using DevSpot.Repositories;
using DevSpot.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace DevSpot.Controllers
{
    public class JobPostingsController : Controller
    {
        private readonly IRepository<JobPosting> _jobPostingRepository;
        private readonly UserManager<IdentityUser> _userManager;

        public JobPostingsController(UserManager<IdentityUser> userManager, IRepository<JobPosting> repository)
        {
            _jobPostingRepository = repository;
            _userManager = userManager;
        }
        public async Task<IActionResult> Index()
        {

            ViewData["Title"] = "All Job Postings";
            var jobPostings = await _jobPostingRepository.GetAllAsync();
            return View(jobPostings);
        }

        public IActionResult Create()
        {
            ViewData["Title"] = "Create a Job Posting";
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(JobPostingViewModel jobPostingVm)
        {
           
            if (ModelState.IsValid)
            {
                var jobPosting = new JobPosting
                {
                    Title = jobPostingVm.Title,
                    Description = jobPostingVm.Description,
                    Company = jobPostingVm.Company,
                    Location = jobPostingVm.Location,
                    UserId = _userManager.GetUserId(User)

                };
                await _jobPostingRepository.AddAsync(jobPosting);
                return RedirectToAction(nameof(Index));
            }

            return View(jobPostingVm);
        }
    }
}


