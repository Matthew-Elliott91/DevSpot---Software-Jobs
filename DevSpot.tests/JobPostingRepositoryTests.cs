using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevSpot.Data;
using DevSpot.Models;
using DevSpot.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace DevSpot.tests
{
    public class JobPostingRepositoryTests
    {
        private readonly DbContextOptions<ApplicationDbContext> _options;

        public JobPostingRepositoryTests()
        {
            _options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

        }

        private ApplicationDbContext CreateDbContext() => new ApplicationDbContext(_options);
        [Fact]
        public async Task AddAsync_ShouldAddJobPosting()
        {
            // Db Context
           var db = CreateDbContext();
            //job posting repository
            var repository = new JobPostingRepository(db);
            //job posting
            var jobPosting = new JobPosting
            {
                Title = "Software Developer",
                Description = "Develop software",
                Company = "DevSpot",
                Location = "Remote",
                PostedDate = DateTime.Now,
                IsApproved = true,
                UserId = "Test"

            };
            // execute add async
            await repository.AddAsync(jobPosting);
            // result?
            var result = await db.JobPostings.SingleOrDefaultAsync(jobPosting => jobPosting.Title == "Software Developer");
            // assert
            Assert.NotNull(result);
            Assert.Equal(jobPosting.Title, result.Title);
        }

        [Fact]
        public async Task GetByIdAsync_ShouldGetJobPosting()
        {
            var db = CreateDbContext();
            var repository = new JobPostingRepository(db);
            var jobPosting = new JobPosting
            {
                Title = "Software Developer",
                Description = "Develop software",
                Company = "DevSpot",
                Location = "Remote",
                PostedDate = DateTime.Now,
                IsApproved = true,
                UserId = "Test1"
            };

            //execute get by id async
            await db.JobPostings.AddAsync(jobPosting);
            await db.SaveChangesAsync();
            
            //create the correct outcome called result
            var result = await repository.GetByIdAsync(jobPosting.Id);
            //assert
            Assert.NotNull(result);
            Assert.Equal(jobPosting.Title, result.Title);
        }

        [Fact]
        public async Task GetByIdAsync_ShouldThrowKeyNotFoundException()
        {
            var db = CreateDbContext();
            var repository = new JobPostingRepository(db);
            var jobPosting = new JobPosting
            {
                Title = "Software Developer2",
                Description = "Develop software",
                Company = "DevSpot",
                Location = "Remote",
                PostedDate = DateTime.Now,
                IsApproved = true,
                UserId = "Test2"
            };

            await db.JobPostings.AddAsync(jobPosting);
            await db.SaveChangesAsync();

           

            await Assert.ThrowsAsync<KeyNotFoundException>(() => repository.GetByIdAsync(999));
        }

        [Fact]
        public async Task DeleteAsync_ShouldDeleteJobPosting()
        {
            var db = CreateDbContext();
            var repository = new JobPostingRepository(db);
            var jobPosting = new JobPosting
            {
                Title = "Software Developer3",
                Description = "Develop software",
                Company = "DevSpot",
                Location = "Remote",
                PostedDate = DateTime.Now,
                IsApproved = true,
                UserId = "Test3"
            };

            await db.JobPostings.AddAsync(jobPosting);
            await db.SaveChangesAsync();
            await repository.DeleteAsync(jobPosting.Id);

            var result = await db.JobPostings.SingleOrDefaultAsync(jobPosting => jobPosting.Title == "Software Developer3");
            Assert.Null(result);

        }
        [Fact]
        public async Task GetAllAsync_ShouldGetAllJobPostings()
        {
            var db = CreateDbContext();
            var repository = new JobPostingRepository(db);
            var jobPosting = new JobPosting
            {
                Title = "Software Developer4",
                Description = "Develop software",
                Company = "DevSpot",
                Location = "Remote",
                PostedDate = DateTime.Now,
                IsApproved = true,
                UserId = "Test4"
            };
            var jobPosting2 = new JobPosting
            {
                Title = "Software Developer5",
                Description = "Develop software",
                Company = "DevSpot",
                Location = "Remote",
                PostedDate = DateTime.Now,
                IsApproved = true,
                UserId = "Test5"
            };
            await db.JobPostings.AddRangeAsync(jobPosting, jobPosting2);
            await db.SaveChangesAsync();
            
            var result = await repository.GetAllAsync();
            Assert.NotNull(result);
            Assert.Equal(2, result.Count());
        }
        [Fact]
        public async Task UpdateAsync_ShouldUpdateJobPosting()
        {
            var db = CreateDbContext();
            var repository = new JobPostingRepository(db);
            var jobPosting = new JobPosting
            {
                Title = "Software Developer6",
                Description = "Develop software",
                Company = "DevSpot",
                Location = "Remote",
                PostedDate = DateTime.Now,
                IsApproved = true,
                UserId = "Test6"
            };
            await db.JobPostings.AddAsync(jobPosting);
            await db.SaveChangesAsync();
            jobPosting.Title = "Software Developer7";
            await repository.UpdateAsync(jobPosting);
            var result = await db.JobPostings.SingleOrDefaultAsync(jobPosting => jobPosting.Title == "Software Developer7");
            Assert.NotNull(result);
            Assert.Equal(jobPosting.Title, result.Title);
        }
    }
}
