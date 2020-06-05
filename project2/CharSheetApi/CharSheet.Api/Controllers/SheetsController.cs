using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Security;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using CharSheet.Api.Services;
using CharSheet.Api.Models;

namespace CharSheet.Api.Controllers
{
    [ApiController]
    [Route("/api/sheets")]
    public class SheetsController : ControllerBase
    {
        private readonly ILogger<SheetsController> _logger;
        private readonly IBusinessService _service;

        public SheetsController(ILogger<SheetsController> logger, IBusinessService service)
        {
            this._logger = logger;
            this._service = service;
        }

        #region Action Methods
        [HttpGet("")]
        [Authorize]
        /// <summary>
        /// Get all sheets by the user.
        /// </summary>
        /// <returns>All sheets by the user.</returns>
        public async Task<ActionResult<IEnumerable<SheetModel>>> GetSheets()
        {
            try
            {
                var identity = HttpContext.User.Identity as ClaimsIdentity;
                var userId = Guid.Parse(identity.Claims.First(claim => claim.Type == "Id").Value);
                return Ok(await _service.GetSheets(userId));
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpGet("{id}")]
        /// <summary>
        /// Get a sheet.
        /// </summary>
        /// <param name="id">Sheet id.</param>
        /// <returns>A sheet.</returns>
        public async Task<ActionResult<SheetModel>> GetSheets(Guid? id)
        {
            try
            {
                this._logger.LogInformation("Finding sheet by id", id);
                // Find by id.
                if (id != null)
                    return Ok(await _service.GetSheet(id));
                return NotFound();
            }
            catch
            {
                this._logger.LogError("Sheet not found", id);
                return NotFound();
            }
        }

        [HttpPost("")]
        [Authorize]
        /// <summary>
        /// Create a sheet if the model state is valid.
        /// </summary>
        /// <param name="sheetModel">Sheet model containing all properties of the new sheet object.</param>
        /// <returns>New sheet model of the sheet.</returns>
        public async Task<ActionResult<SheetModel>> CreateSheet(SheetModel sheetModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var identity = HttpContext.User.Identity as ClaimsIdentity;
                    var userId = Guid.Parse(identity.Claims.First(claim => claim.Type == "Id").Value);
                    sheetModel = await _service.CreateSheet(sheetModel, userId);
                    return CreatedAtAction(nameof(GetSheets), new { id = sheetModel.SheetId }, sheetModel);
                }
                catch
                {
                    return BadRequest();
                }
            }
            return BadRequest();
        }

        [HttpPut("{id}")]
        [Authorize]
        /// <summary>
        /// Update a sheet's properties.
        /// </summary>
        /// <param name="id">Sheet id of sheet being updated.</param>
        /// <param name="sheetModel">Sheet model containing all properties to be updated.</param>
        /// <returns>Sheet model of the sheet with updated properties.</returns>
        public async Task<ActionResult<SheetModel>> UpdateSheet(Guid? id, SheetModel sheetModel)
        {
            if (ModelState.IsValid)
            {
                if (id == null)
                    return BadRequest();
                var identity = HttpContext.User.Identity as ClaimsIdentity;
                var userId = Guid.Parse(identity.Claims.First(claim => claim.Type == "Id").Value);
                sheetModel.SheetId = (Guid)id;
                try
                {
                    return await _service.UpdateSheet(sheetModel, userId);
                }
                catch (SecurityException)
                {
                    return Unauthorized();
                }
                catch
                {
                    return BadRequest();
                }
            }
            return BadRequest();
        }

        [HttpDelete("{id}")]
        [Authorize]
        /// <summary>
        /// Delete a sheet.
        /// </summary>
        /// <param name="id">Id of the sheet to be deleted.</param>
        /// <returns></returns>
        public async Task<ActionResult> DeleteSheet(Guid? id)
        {
            if (id == null)
                return BadRequest();
            try
            {
                var identity = HttpContext.User.Identity as ClaimsIdentity;
                var userId = Guid.Parse(identity.Claims.First(claim => claim.Type == "Id").Value);
                await _service.DeleteSheet(id, userId);
                return Ok();
            }
            catch (SecurityException)
            {
                return Unauthorized();
            }
            catch
            {
                return BadRequest();
            }
        }
        #endregion
    }
}
