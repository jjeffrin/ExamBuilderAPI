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
            var data = await this._firestoreHelper.Get("examBuilderUsers", id);
            if (data.Count() > 0)
            {
                return Ok(data);
            }
            else
            {
                return NotFound();
            }
        }

        [Route("login")]
        [HttpGet]
        public async Task<IActionResult> Login(string id, string username, string password)
        {
            var userToReturn = default(User);
            var data = await this._firestoreHelper.Get("examBuilderUsers", id);
            if (data.Count() > 0)
            {
                var userFound = false;
                foreach (var user in data)
                {
                    if (user.Username == username && user.Password == password)
                    {
                        userToReturn = user;
                        userFound = true;
                        break;
                    }
                    else
                    {
                        userFound = false;
                    }
                }
                if (userFound)
                {
                    return Ok(userToReturn);
                }
                else
                {
                    return NotFound();
                }
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post(User user)
        {
            var data = await this._firestoreHelper.Post("examBuilderUsers", user);
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
            var data = await this._firestoreHelper.Put("examBuilderUsers", user);
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
            var data = await this._firestoreHelper.Delete("examBuilderUsers", id);
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
