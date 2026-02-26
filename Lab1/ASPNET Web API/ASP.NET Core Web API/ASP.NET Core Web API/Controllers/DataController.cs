using ASP.NET_Core_Web_API.DataAccess;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ASP.NET_Core_Web_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DataController : ControllerBase
    {

        readonly MyDataCRUD _myDataCRUDAccess;
        public DataController(MyDataCRUD myDataCRUDAccess)
        {
            _myDataCRUDAccess = myDataCRUDAccess;
        }

        [HttpGet]
        public IEnumerable<string> Get()
        {
            return _myDataCRUDAccess.Read();
        }

        [HttpGet("{id:int}")]
        public string Get(int id)
        {
            return _myDataCRUDAccess.Read(id);
        }
        //[HttpGet("{id}")]
        //public string Get(string id)
        //{
        //    return "Data not found.";
        //}


        [HttpGet("[action]")]
        public void CreateData()
        {
            _myDataCRUDAccess.Create();
        }

        //[HttpGet("{name}")]
        //public string Name(string name)
        //{
        //    return "your name" + name;
        //}
        [HttpGet("{name}")]
        public string Name([FromQuery] string name)
        {
            return "your name" + name;
        }

    }
}
