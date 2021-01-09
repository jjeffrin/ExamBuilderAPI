using ExamBuilderAPI.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace ExamBuilderAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExamController : ControllerBase
    {
        private IFirestoreHelper<Exam> _firestoreHelper;

        public ExamController(IFirestoreHelper<Exam> firestoreHelper)
        {
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
        public async Task<IActionResult> Post(Exam exam)
        {
            var data = await this._firestoreHelper.Post("examBuilderExams", exam);
            if (data != null)
            {
                return Ok(data);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPut]
        public async Task<IActionResult> Put(Exam exam)
        {
            var data = await this._firestoreHelper.Put("examBuilderExams", exam);
            if (data != null)
            {
                return Ok(data);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(string id)
        {
            var data = await this._firestoreHelper.Delete("examBuilderExams", id);
            if (data)
            {
                return Ok(true);
            }
            else
            {
                return NotFound();
            }
        }
    }
}
