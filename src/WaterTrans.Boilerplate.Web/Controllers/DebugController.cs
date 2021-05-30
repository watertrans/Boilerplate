using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using WaterTrans.Boilerplate.Domain.Abstractions;
using WaterTrans.Boilerplate.Persistence;
using WaterTrans.Boilerplate.Web.Filters;

namespace WaterTrans.Boilerplate.Web.Controllers
{
    [ApiController]
    [ApiVersion("1")]
    [DebugOnlyFilter]
    public class DebugController : ControllerBase
    {
        private readonly IDBSettings _dbSettings;

        public DebugController(IDBSettings dbSettings)
        {
            _dbSettings = dbSettings;
        }

        [HttpPost]
        [Route("api/v{version:apiVersion}/debug/database/initialize")]
        [SwaggerOperationFilter(typeof(AnonymousOperationFilter))]
        public IActionResult Initialize()
        {
            var setup = new DataSetup(_dbSettings);
            setup.Initialize();
            return new OkResult();
        }

        [HttpPost]
        [Route("api/v{version:apiVersion}/debug/database/loadInitialData")]
        [SwaggerOperationFilter(typeof(AnonymousOperationFilter))]
        public IActionResult LoadInitialData()
        {
            var setup = new DataSetup(_dbSettings);
            setup.LoadInitialData();
            return new OkResult();
        }

        [HttpPost]
        [Route("api/v{version:apiVersion}/debug/database/cleanup")]
        [SwaggerOperationFilter(typeof(AnonymousOperationFilter))]
        public IActionResult Cleanup()
        {
            var setup = new DataSetup(_dbSettings);
            setup.Cleanup();
            return new OkResult();
        }
    }
}
