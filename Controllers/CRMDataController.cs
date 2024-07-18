using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Formatter;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Results;
using ODataEpplus.Context;
using ODataEpplus.Models;

namespace ODataEpplus.Controllers
{
    public class CRMDataController : Controller
    {
        private readonly DataContext _context;

        public CRMDataController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        [EnableQuery]
        public IQueryable<CRM> Get()
        {
            return _context.CRMs;
        }

        [HttpGet]
        [EnableQuery]
        public SingleResult<CRM> Get([FromODataUri] int key)
        {
           var result = _context.CRMs.Where(e => e.Id == key);
            return SingleResult.Create(result);
        }
    }
}
