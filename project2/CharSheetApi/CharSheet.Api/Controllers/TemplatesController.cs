using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using CharSheet.Api.Services;
using CharSheet.Api.Models;

namespace CharSheet.Api.Controllers
{
    [ApiController]
    [Route("/api/templates")]
    public class TemplatesController : ControllerBase
    {
        private readonly IBusinessService _service;

        public TemplatesController( IBusinessService service)
        {
            this._service = service;
        }

        #region Action Methods
        [HttpGet("")]
        public async Task<ActionResult<IEnumerable<TemplateModel>>> GetTemplates(Boolean user = false)
        {
            try
            {
                if (user)
                {
                    var identity = HttpContext.User.Identity as ClaimsIdentity;
                    var userId = Guid.Parse(identity.Claims.First(claim => claim.Type == "Id").Value);
                    return Ok(await _service.GetTemplates(userId));
                }
                return Ok(await _service.GetTemplates());
            }
            catch
            {
                return BadRequest();
            }
        }
        [HttpGet("{id}")]
        public async Task<ActionResult> GetTemplate(Guid? id)
        {
            try
            {
                return Ok(await _service.GetTemplate(id));
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpPost("")]
        [Authorize]
        public async Task<ActionResult<TemplateModel>> CreateTemplate(TemplateModel templateModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var identity = HttpContext.User.Identity as ClaimsIdentity;
                    var userId = Guid.Parse(identity.Claims.First(claim => claim.Type == "Id").Value);
                    templateModel = await _service.CreateTemplate(templateModel, userId);
                    return CreatedAtAction(nameof(GetTemplates), new { id = templateModel.TemplateId }, templateModel);
                }
                else
                {
                    throw new InvalidOperationException("Model state invalid.");
                }
            }
            catch
            {
                return BadRequest();
            }
        }
        #endregion
    }
}