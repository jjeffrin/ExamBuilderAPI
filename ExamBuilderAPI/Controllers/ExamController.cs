using ExamBuilderAPI.Models;
using Google.Cloud.Firestore;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExamBuilderAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExamController : ControllerBase
    {
        private readonly ILogger<ExamController> _logger;
        private IFirestoreHelper<Exam> _firestoreHelper;

        public ExamController(ILogger<ExamController> logger, IFirestoreHelper<Exam> firestoreHelper)
        {
            _logger = logger;
            _firestoreHelper = firestoreHelper;
        }

        [HttpGet]
        public async Task<IActionResult> Get(string id)
        {
            var data = await this._firestoreHelper.Get("examBuilderExams", id);
            if (data.Count() > 0)
            {
                return Ok(data);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPost]
        public async Task<Exam> Post(Exam exam)
        {
            return await this._firestoreHelper.Post("examBuilderExams", exam);
        }

        [HttpPut]
        public async Task<Exam> Put(Exam exam)
        {
            return await this._firestoreHelper.Put("examBuilderExams", exam);
        }

        //[HttpDelete]
        //public async Task<IActionResult> Delete(string id)
        //{
        //    var data = await this._firestoreHelper.Delete("examBuilderExams", id);
        //    if (data == null)
        //    {
        //        return Ok(true);
        //    }
        //    else
        //    {
        //        return NotF
        //    }
        //}
    }
}
