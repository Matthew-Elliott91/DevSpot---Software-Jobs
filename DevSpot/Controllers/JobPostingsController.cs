using System.Security.AccessControl;
using DevSpot.Models;
using DevSpot.Repositories;
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
            var jobPostings = await _jobPostingRepository.GetAllAsync();
            return View(jobPostings);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(JobPosting jobPosting)
        {
           return RedirectToAction(nameof(Index));
        }
    }
}
