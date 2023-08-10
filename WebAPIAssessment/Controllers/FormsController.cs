using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.SqlServer.Server;
using WebAPIAssessment.Data;
using WebAPIAssessment.Models;

namespace WebAPIAssessment.Controllers
{
    [Route("api/")]
    [ApiController]
    public class FormsController : ControllerBase
    {
        private readonly AssessmentDbContext FormContext;

        public FormsController(AssessmentDbContext context)
        {
            FormContext = context;
        }

        // GET: api/Forms
        [HttpGet]
        [Route("Forms")]
        public async Task<ActionResult<IEnumerable<Form>>> GetForms()
        {
            try
            {
                var form =  await FormContext.Forms.ToListAsync();
                if (form.Count == 0)
                {
                    return NotFound($" Form not found");
                }
                return form;
            }
            catch
            {
                return StatusCode(500, $"Error Occured while Getting Form");
            }
           
        }

        // GET: api/Form/Id/{Id}
        [HttpGet]
        [Route("Form/Id/{Id}")]
        public async Task<ActionResult<Form>> GetForm([FromRoute] Guid Id)
        {
            try
            {
                var form = await FormContext.Forms.FindAsync(Id);

                if (form == null)
                {
                    return NotFound($" Form with Id '{Id}' was not found");
                }

                return form;
            }
            catch
            {
                return StatusCode(500, $"Error Occured while Getting Form of Form Id '{Id}'");
            }
          
            
        }

        
    }
}
