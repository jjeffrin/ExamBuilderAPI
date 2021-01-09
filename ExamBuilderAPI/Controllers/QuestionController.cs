using ExamBuilderAPI.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace ExamBuilderAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuestionController : ControllerBase
    {
        private IFirestoreHelper<Question> _firestoreHelper;

        public QuestionController(IFirestoreHelper<Question> firestoreHelper)
        {
            _firestoreHelper = firestoreHelper;
        }

        [HttpGet]
        public async Task<IActionResult> Get(string id)
        {
            var data = await this._firestoreHelper.Get("examBuilderQuestions", id);
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
        public async Task<IActionResult> Post(Question question)
        {
            var data = await this._firestoreHelper.Post("examBuilderQuestions", question);
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
        public async Task<IActionResult> Put(Question question)
        {
            var data = await this._firestoreHelper.Put("examBuilderQuestions", question);
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
            var data = await this._firestoreHelper.Delete("examBuilderQuestions", id);
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
