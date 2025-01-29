using DevSpot.Constants;
using DevSpot.Models;
using DevSpot.Repositories;
using DevSpot.Services;
using DevSpot.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace DevSpot.Controllers
{
    [Authorize]
    public class JobPostingsController : Controller
    {
        private readonly IRepository<JobPosting> _jobPostingRepository;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly JobPostingService _jobPostingService;

        public JobPostingsController(UserManager<IdentityUser> userManager, IRepository<JobPosting> repository, JobPostingService jobPostingService)
        {
            _jobPostingRepository = repository;
            _userManager = userManager;
            _jobPostingService = jobPostingService;
        }

        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            ViewData["Title"] = "All Job Postings";
            var jobPostings = await _jobPostingRepository.GetAllAsync();
            return View(jobPostings);
        }

        [AllowAnonymous]
        public async Task<IActionResult> ExternalJobPostings()
        {
            ViewData["Title"] = "Yorkshire Job Postings";
            var apiJobPostings = await _jobPostingService.GetJobPostingsAsync();
            var jobPostings = _jobPostingService.MapToJobPostings(apiJobPostings);
            return View(jobPostings);
        }


        [Authorize(Roles = "Admin, Employer")]
        public async Task<IActionResult> MyJobPostings()
        {
            ViewData["Title"] = "My Job Postings";
            var allJobPostings = await _jobPostingRepository.GetAllAsync();
            var userId = _userManager.GetUserId(User);
            var filteredJobPostings = allJobPostings.Where(j => j.UserId == userId);
            return View(filteredJobPostings);
        }

        [Authorize(Roles = "Admin, Employer")]
        public IActionResult Create()
        {
            ViewData["Title"] = "Create a Job Posting";
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Admin, Employer")]
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

        [HttpDelete]
        [Authorize(Roles = "Admin, Employer")]
        public async Task<IActionResult> Delete(int id)
        {
            var jobPosting = await _jobPostingRepository.GetByIdAsync(id);
            if (jobPosting == null)
            {
                return NotFound();
            }
            var currentUserId = _userManager.GetUserId(User);
            if (!User.IsInRole(Roles.Admin) && currentUserId != jobPosting.UserId)
            {
                return Forbid();
            }

            await _jobPostingRepository.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
