using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPIAssessment.Data;
using WebAPIAssessment.Dto;
using WebAPIAssessment.Models;

namespace WebAPIAssessment.Controllers
{
    [Route("api/")]
    [ApiController]
    public class FieldsController : ControllerBase
    {
        private readonly AssessmentDbContext FieldContext;
        private readonly AssessmentDbContext FormContext;
        private readonly AssessmentDbContext AocolumnContext;

        public FieldsController(AssessmentDbContext context, AssessmentDbContext formContext, AssessmentDbContext aocolumnContext)
        {
            FieldContext = context;
            FormContext = formContext;
            AocolumnContext = aocolumnContext;
        }
        // Get all records of field by Type
        // GET: api/Field/type/{type}
        [HttpGet]
        [Route("Field/type/{type}")]
        public async Task<IActionResult> GetFieldByType([FromRoute] string type)
        {
            try
            {
                var fields = await FieldContext.Fields.Where(x => x.Type == type).ToListAsync();
                if (fields.Count == 0)
                {
                    return NotFound($" Fields with type '{type}' was not found");
                }
                return Ok(fields);
            }
            catch
            {
                return StatusCode(500, $"Error Occured while Getting Field of Type '{type}'");
            }

        }

        //Get all record from field table by form name
        // GET: api/Field/FormName/{FormName
        [HttpGet]
        [Route("Field/FormName/{FormName}")]
        public async Task<IActionResult> GetFieldByFormName([FromRoute] string FormName)
        {
            try
            {
                var forms = await FormContext.Forms.Where(f => f.Name == FormName).ToListAsync();
                if (forms.Count == 0)
                {
                    return NotFound($" Form with Name '{FormName}' was not found");
                }
                List<Field> AllFields = new List<Field>();

                foreach (var form in forms)
                {
                    var fields = await FieldContext.Fields.Where(x => x.FormId == form.Id).ToListAsync();

                    AllFields.AddRange(fields);
                }
                if (!AllFields.Any())
                {
                    return NotFound($" Fields with Form Name '{FormName}' was not found");
                }
                return Ok(AllFields);

            }
            catch
            {
                return StatusCode(500, $"Error Occured while Getting Field with Form Name '{FormName}'");
            }

        }

        //Get all records from field table and name from form table by form Id
        // GET: api/Field/FormId/{FormId}
        [HttpGet]
        [Route("Field/FormId/{FormId}")]
        public async Task<ActionResult<List<Form>>> GetFieldByFormId([FromRoute] Guid FormId)
        {
            try
            {
                var form = await FormContext.Forms.FirstOrDefaultAsync(f => f.Id == FormId);
                if (form == null)
                {
                    return NotFound($" Form with Form Id '{FormId}' was not found");
                }
                var fields = await FieldContext.Fields.Include("Form").Where(x => x.FormId == FormId).ToListAsync();
                if (fields.Any())
                {
                    var response = fields.Select(field => new
                    {
                        Field = field,
                      //  FormName = form.Name
                    }).ToList();

                    return Ok(response);
                }
                return NotFound($" Fields with Form Id '{FormId}' was not found");
            }
            catch
            {
                return StatusCode(500, $"Error Occured while Getting Field of Form Id '{FormId}'");
            }

        }

        //Edit record in the field table by passing id
        // PATCH: api/Field/Id/{FieldId}
        [HttpPatch]
        [Route("Field/Id/{FieldId}")]
        public async Task<IActionResult> UpdateFieldById([FromRoute] Guid FieldId, [FromBody] EditField updatedField)
        {
            try
            {
                var existingField = await FieldContext.Fields.FindAsync(FieldId);

                if (existingField != null)
                {
                    existingField.Sequence = updatedField.Sequence ?? existingField.Sequence;
                    existingField.Type = updatedField.Type ?? existingField.Type;
                    existingField.TextAreaRows = updatedField.TextAreaRows ?? existingField.TextAreaRows;
                    existingField.TextAreaCols = updatedField.TextAreaCols ?? existingField.TextAreaCols;
                    existingField.Label = updatedField.Label ?? existingField.Label;
                    existingField.DisplayColumns = updatedField.DisplayColumns ?? existingField.DisplayColumns;
                    existingField.QuoteReadOnly = updatedField.QuoteReadOnly ?? existingField.QuoteReadOnly;
                    existingField.QuoteDisplay = updatedField.QuoteDisplay ?? existingField.QuoteDisplay;
                    existingField.QuoteDisabled = updatedField.QuoteDisabled ?? existingField.QuoteDisabled;
                    existingField.PolicyReadOnly = updatedField.PolicyReadOnly ?? existingField.PolicyReadOnly;
                    existingField.PolicyRequired = updatedField.PolicyRequired ?? existingField.PolicyRequired;
                    existingField.PolicyDisplay = updatedField.PolicyDisplay ?? existingField.PolicyDisplay;
                    existingField.PolicyDisabled = updatedField.PolicyDisabled ?? existingField.PolicyDisabled;
                    existingField.RequiredCondition = updatedField.RequiredCondition ?? existingField.RequiredCondition;
                    existingField.AmendablePostIssuance = updatedField.AmendablePostIssuance ?? existingField.AmendablePostIssuance;
                    existingField.AmendablePreRenewal = updatedField.AmendablePreRenewal ?? existingField.AmendablePreRenewal;
                    existingField.Default = updatedField.Default ?? existingField.Default;
                    existingField.Minimum = updatedField.Minimum ?? existingField.Minimum;
                    existingField.Maximum = updatedField.Maximum ?? existingField.Maximum;
                    existingField.Mask = updatedField.Mask ?? existingField.Mask;
                    existingField.Help = updatedField.Help ?? existingField.Help;
                    existingField.HelpText = updatedField.HelpText ?? existingField.HelpText;
                    existingField.DisplayController = updatedField.DisplayController ?? existingField.DisplayController;
                    existingField.Condition = updatedField.Condition ?? existingField.Condition;
                    existingField.Comment = updatedField.Comment ?? existingField.Comment;
                    existingField.DialogFileName = updatedField.DialogFileName ?? existingField.DialogFileName;
                    existingField.Auditable = updatedField.Auditable ?? existingField.Auditable;
                    existingField.AuditCondition = updatedField.AuditCondition ?? existingField.AuditCondition;
                    existingField.XslValue = updatedField.XslValue ?? existingField.XslValue;
                    existingField.RefTableId = updatedField.RefTableId ?? existingField.RefTableId;
                    existingField.TextDisplaySize = updatedField.TextDisplaySize ?? existingField.TextDisplaySize;
                    existingField.LinkText = updatedField.LinkText ?? existingField.LinkText;
                    existingField.AuditViewOnly = updatedField.AuditViewOnly ?? existingField.AuditViewOnly;
                    await FieldContext.SaveChangesAsync();
                    return Ok(existingField);
                }
                return NotFound($"Field with Id '{FieldId}' was not found.");
            }
            catch 
            {
                return StatusCode(500, "Error Occured");
            }
        }

        //Add records in Field
        // POST: api/Field/{FormName}
        [HttpPost]
        [Route("Field/{FormName}")]
        public async Task<IActionResult> AddFieldByFormName([FromRoute] string FormName, [FromBody] Field NewField)
        {

            try
            {
                var Form = await FormContext.Forms.FirstOrDefaultAsync(f => f.Name == FormName);

                if (Form == null)
                {
                    return NotFound($" Form with Name '{FormName}' was not found");
                }
                if (NewField.ColumnId == null)
                {
                    return BadRequest("ColumnId is required");
                }
                var Column = await AocolumnContext.Aocolumns.FirstOrDefaultAsync(c => c.Id == NewField.ColumnId);

                if (Column == null)
                {
                    return NotFound($" ColumnId '{NewField.ColumnId}' was not found");
                }

                NewField.Id = Guid.NewGuid();
                NewField.FormId = Form.Id;
                NewField.ColumnId = Column.Id;
                await FieldContext.Fields.AddAsync(NewField);
                await FieldContext.SaveChangesAsync();
                return Ok(NewField);
            }
            catch
            {
                return StatusCode(500, "Error Occured");
            }


        }

        //Delete record from field table by id
        // DELETE: api/Field/Id/{FieldId}/Delete
        [HttpDelete]
        [Route("Field/Id/{FieldId}/Delete")]
        public async Task<IActionResult> DeleteFieldById([FromRoute] Guid FieldId)
        {
            try
            {
                var field = await FieldContext.Fields.FindAsync(FieldId);
                if (field != null)
                {
                    FieldContext.Fields.Remove(field);
                    await FieldContext.SaveChangesAsync();

                    return Ok(field);
                }
                return NotFound("Field Not Found");
            }
            catch
            {
                return StatusCode(500, "Error Occured");
            }

        }

    }
}
