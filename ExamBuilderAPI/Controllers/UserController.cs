using ExamBuilderAPI.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace ExamBuilderAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private IFirestoreHelper<User> _firestoreHelper;

        public UserController(IFirestoreHelper<User> firestoreHelper)
        {
            _firestoreHelper = firestoreHelper;
        }

        [HttpGet]
        public async Task<IActionResult> Get(string id)
        {
            var data = await this._firestoreHelper.Get("examBuilderUser", id);
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
        public async Task<IActionResult> Post(User user)
        {
            var data = await this._firestoreHelper.Post("examBuilderUser", user);
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
        public async Task<IActionResult> Put(User user)
        {
            var data = await this._firestoreHelper.Put("examBuilderUser", user);
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
            var data = await this._firestoreHelper.Delete("examBuilderUser", id);
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
