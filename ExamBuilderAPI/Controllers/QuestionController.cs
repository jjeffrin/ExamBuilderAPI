using ExamBuilderAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExamBuilderAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuestionController : ControllerBase
    {
        private readonly ILogger<QuestionController> _logger;

        public QuestionController(ILogger<QuestionController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<Question> Get()
        {
            var question = new Question() { Content = "How far is the moon ?", TypeCode = 1, TypeDescription = "fill-in", Options = new string[] { "2000kms", "5000kms" } };
            return new Question[] { question };
        }        
    }
}
