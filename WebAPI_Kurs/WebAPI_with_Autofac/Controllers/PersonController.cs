using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAPI_with_Autofac.Interface;

namespace WebAPI_with_Autofac.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonController : ControllerBase
    {
        private readonly IPersonBusiness _personBusiness;

        public PersonController(IPersonBusiness personBusiness)
        {
            _personBusiness = personBusiness;
        }

        [HttpGet]
        [Route("personList")]
        public List<string> GetPersonList()
        {
            return _personBusiness.GetPersonList();
        }
    }
}
