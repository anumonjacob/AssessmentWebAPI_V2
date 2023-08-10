using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.EntityFrameworkCore;
using WebAPIAssessment.Data;
using WebAPIAssessment.Models;

namespace WebAPIAssessment.Controllers
{
    [Route("api/")]
    [ApiController]
    public class AocolumnsController : ControllerBase
    {
        private readonly AssessmentDbContext PasDbContext;

        public AocolumnsController(AssessmentDbContext pasdbcontext)
        {
            this.PasDbContext = pasdbcontext;
        }

        // GET: api/Columns
        [HttpGet]
        [Route("Columns")]
        public async Task<ActionResult<IEnumerable<Aocolumn>>> GetAocolumns()
        {

            try
            {
                var form = await PasDbContext.Aocolumns.ToListAsync();
                if (form.Count == 0)
                {
                    return NotFound($" Column not found");
                }
                return form;
            }
            catch
            {
                return StatusCode(500, $"Error Occured while Getting Column");
            }
            
        }

        // GET: api/Column/Id/{Id}
        [HttpGet]
        [Route("Column/Id/{Id}")]
        public async Task<ActionResult<Aocolumn>> GetAocolumn([FromRoute] Guid Id)
        {
            try
            {
                var aocolumn = await PasDbContext.Aocolumns.FindAsync(Id);

                if (aocolumn == null)
                {
                    return NotFound($" Column Id '{Id}' was not found");
                }

                return aocolumn;
            }
            catch
            {
                return StatusCode(500, $"Error Occured while Getting Column of Form Id '{Id}'");
            }
        }

    }
}
