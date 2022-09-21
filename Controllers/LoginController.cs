using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System;

namespace university.Controllers
{
    [Route("api/[controller]")]
    public class LoginController : ControllerBase
    {
        public LoginController(Database db)
        {
            Db = db;
        }


        // POST api/login
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]User body)
        {
            //Console.WriteLine(body.username);
            //Console.WriteLine(body.password);
            await Db.Connection.OpenAsync();
            var query = new Login(Db);
            var result = await query.GetPassword(body.username);
            Console.WriteLine(result.identity);
   
            if (result is null || ! BCrypt.Net.BCrypt.Verify(body.password, result.password))
            {
                // authentication failed
                return new OkObjectResult(false);
            }
            else
            {
                // authentication successful
                return new OkObjectResult(result.identity);
                
            }
            
        }

    

        public Database Db { get; }
    }
}