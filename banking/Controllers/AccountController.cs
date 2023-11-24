using AutoMapper;
using banking.Models;
using banking.Models.DTO;
using banking.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace banking.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ISQLrepository sqlrepository;

        public AccountController(IMapper mapper, ISQLrepository sqlrepository)
        {

            _mapper = mapper;
            this.sqlrepository = sqlrepository;
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] NewUserAccountDTO user)
        {
            // Validate the user input



            var userdomainModel = _mapper.Map<User>(user);

            // Check if the user name already exists
           

            userdomainModel= await sqlrepository.CreateAccountAsync(userdomainModel);
            if (userdomainModel!= null)
            {
                return Conflict("User name already exists");
            }
            var uservalues = _mapper.Map<UserDTO>(userdomainModel);
            return Ok(uservalues);

        }

        [HttpPost]
        [Route("{username}/{password}")]
        public async Task<IActionResult> Login( string username,string password)
        {


            var userdomainModel = await sqlrepository.ValidateAccountAsync(username, password);
            if (userdomainModel == null)
            {
                return Conflict("Invalid User");
            }
            var uservalues = _mapper.Map<UserDTO>(userdomainModel);
            return Ok(uservalues);


        }

        [HttpGet("{id:Guid}")]
        public async Task<IActionResult> CheckBalance(Guid id)
        {
            // Find the user with the matching id



            var UserdomainModel = await sqlrepository.GetUserAccountIdAsync(id);
            // Check if the user exists
            if (UserdomainModel == null)
            {
                return NotFound("Account not found");
            }

            var uservalues = _mapper.Map<AccountDTO>(UserdomainModel);
            // Return the balance of the user
            return Ok(uservalues);
        }

        [HttpPost("{Id:Guid}/{amount:double}")]
        public IActionResult TransferFunds(Guid Id, double amount)
        {
            string message = String.Empty;

            var accountdomainModel = sqlrepository.Transfer(Id, amount, out message);

            if (!String.IsNullOrEmpty(message) && message.ToUpper() != "SUCCESS")
            {
                if(message=="Account does not exist")
                {
                    return NotFound(message);
                }
                else
                {
                    return Ok(message);
                }
               
            }

            var uservalues = _mapper.Map<AccountDTO>(accountdomainModel);
            // Return the balance of the user
            return Ok(uservalues);
        }

        [HttpGet]
        public IActionResult Logout()
        {
            // Find the user with the matching id



            
            // Return the balance of the user
            return Ok("User Logged Out successfully");
        }
    }
}
