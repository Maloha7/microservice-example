using LoyaltyProgram.Domain;
using Microsoft.AspNetCore.Mvc;

namespace LoyaltyProgram.Controllers
{
    [Route("/users")]
    public class UsersController : ControllerBase
    {
        private static readonly IDictionary<int, LoyaltyProgramUser>
            RegisteredUsers = new Dictionary<int, LoyaltyProgramUser>();

        [HttpPost("")]
        public ActionResult<LoyaltyProgramUser> CreateUser(
                [FromBody] LoyaltyProgramUser user)
        {
            if (user == null)
                return BadRequest();
            var newUser = RegisterUser(user);
            return Created(
                    new Uri($"/users/:{newUser.Id}", UriKind.Relative)
                    , newUser
                    );
        }

        [HttpPut("{userId:int}")]
        public LoyaltyProgramUser UpdateUser(
                int userId,
                [FromBody] LoyaltyProgramUser user)
            => RegisteredUsers[userId] = user;

        private LoyaltyProgramUser RegisterUser(LoyaltyProgramUser user)
        {
            throw new NotImplementedException();
        }
    }
}
