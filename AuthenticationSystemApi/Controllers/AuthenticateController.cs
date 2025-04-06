using AuthenticationSystemApi.Entities;
using AuthenticationSystemApi.Extensions;
using AuthenticationSystemApi.Middlewares;
using AuthenticationSystemApi.Models;
using AuthenticationSystemApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Persistence.Entities.Requests;

namespace AuthenticationSystemApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthenticateController : Controller
    {
        private readonly IPersonServices personService;
        private readonly IUserServices userService;
        private readonly ITokenServices tokenService;
        private readonly IProjectServices projectService;

        public AuthenticateController(IPersonServices personService, IUserServices userService, ITokenServices tokenService, IProjectServices projectService)
        {
            this.personService = personService ?? throw new ArgumentNullException(nameof(personService));
            this.userService = userService ?? throw new ArgumentNullException(nameof(userService));
            this.tokenService = tokenService ?? throw new ArgumentNullException(nameof(tokenService));
            this.projectService = projectService ?? throw new ArgumentNullException(nameof(projectService));   

        }

        /// <summary>
        /// Get a Person with the userId
        /// </summary>
        /// <returns>Information of the Person</returns>
        [HttpGet]
        [ServiceFilter(typeof(ApiProcessingFilter))]
        [Route("user/{userId}/person")]
        [CacheResponse(300)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Person))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetPerson([FromHeader] string Authorization, string userId)
        {
            var person = await personService.GetPerson(userId);
            return person.GetStatusCode();
        }

        /// <summary>
        /// Get a User with the ProjectId, Username and Password
        /// </summary>
        /// <returns>Information of the User</returns>
        [HttpPost]
        [ServiceFilter(typeof(ApiProcessingFilter))]
        [Route("project/{projectId}/user")]
        [CacheResponse(300)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(User))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> IsUserAuthenticated([FromHeader] string Authorization,[FromBody] Credentials credentials, string projectId)
        {
            var user = await userService.GetUser(credentials, projectId);
            return user.GetStatusCode();
        }

        [HttpPost]
        [Route("create/user")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(User))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> CreateUser([FromBody] User user)
        {
            var affected = await userService.CreateUser(user);
            if (!affected)
                return BadRequest();

            return Created();
        }

        [HttpGet]
        [Route("users")]
        [CacheResponse(300)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(User))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetUsers([FromQuery] int skip = 0, [FromQuery] int take = 10)
        {
            var users = await userService.GetUsers(skip, take);
            return users.GetStatusCode();
        }

        [HttpPost]
        [Route("create/project")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Project))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> CreateProject([FromBody] Project project)
        {
            var affected = await projectService.CreateProject(project);
            if (!affected)
                return BadRequest();
            return Created();
        }

        [HttpGet]
        [Route("projects")]
        [CacheResponse(300)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Project))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetProjects([FromQuery] int skip = 0, [FromQuery] int take = 10)
        {
            var projects = await projectService.GetProjects(skip, take);
            return Ok(projects);
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("jwt")]
        [CacheResponse(1800)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Authorization))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetJwt()
        {
            var jwt = tokenService.GetJwt();
            return jwt.GetStatusCode();
        }
    }
}
