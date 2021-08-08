using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GroceryStoreAPI.Controllers
{

    [ApiController]
    public class ApiControllerBase : ControllerBase
    {
        public IActionResult ErrorResult => new StatusCodeResult(StatusCodes.Status500InternalServerError);
        
    }
}
