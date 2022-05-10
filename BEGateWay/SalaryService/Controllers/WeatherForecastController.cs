using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SalaryService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SalaryService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    
    public class SalaryController : ControllerBase
    {
        private IList<UserModel> _userModels;
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<SalaryController> _logger;

        public SalaryController(ILogger<SalaryController> logger)
        {
            _logger = logger;
            CreateDemoData();
        }
       
        [HttpGet]
        public IEnumerable<UserModel> Get()
        {
            return _userModels;

        }
        private void CreateDemoData()
        {
            _userModels = new List<UserModel>();

            for (int i = 0; i < 5; i++)
            {
                var index = i + 1;
                _userModels.Add(new UserModel
                {
                    AccountName = $"Quanag{index}",
                    CreatedDate = DateTime.Now.AddDays(-index),
                    Email = $"quahasyd{index}@gmail.com",
                    Id = Guid.NewGuid(),
                    Name = $"123{index}",
                    Salary= $"50000*{index}"
                });
            }
        }
    }
}
