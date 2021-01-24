using Hahn.ApplicatonProcess.December2020.Data.Entities;
using Hahn.ApplicatonProcess.December2020.Domain.Services;
using Hahn.ApplicatonProcess.December2020.Web.Extensions;
using Hahn.ApplicatonProcess.December2020.Web.Localize;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Hahn.ApplicatonProcess.December2020.Web.Controllers
{
    [ApiController]
    [Route("api/applicant")]
    [Produces("application/json")]
    public class ApplicantController : ControllerBase
    {
        private readonly ILogger<ApplicantController> _logger;
        private readonly IStringLocalizer<Resource> _localizer;
        private readonly IApplicantService _applicantService;
        public ApplicantController(ILogger<ApplicantController> logger, IStringLocalizer<Resource> localizer, IApplicantService applicantService)
        {
            _logger = logger;
            _localizer = localizer;
            _applicantService = applicantService;
        }


        /// <summary>
        /// creates applicant.
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     POST api/applicant
        ///     {        
        ///         "id": 0,
        ///         "name": "sdsd",
        ///         "familyName": "dsdsd",
        ///         "address": "string",
        ///         "countryOfOrigin": "string",
        ///         "eMailAdress": "string",
        ///         "age": 0,
        ///         "hired": false    
        ///     }
        /// </remarks>
        /// <param name="request"></param>      

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Applicant request)
        {
            _logger.LogInformation("Execution started of applicant post action");
            _logger.LogInformation(_localizer["Entity is not valid"]);

            var result = await _applicantService.CreateAsync(request);

            _logger.LogInformation("Execution ended of applicant post action");

            return result.ToCreatedResult($"api/applicant/{result}");
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> Get(int id)
        {
            _logger.LogInformation("Execution started of applicant get action");
            var result = await _applicantService.GetByIdAsync(id);
            _logger.LogInformation("Execution ended of applicant get action");
            return result.ToOkResult();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            _logger.LogInformation("Execution started of applicant delete action");
            await _applicantService.DeleteAsync(id);
            _logger.LogInformation("Execution ended of applicant delete action");
            return NoContent();
        }

        /// <summary>
        /// update applicant.
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     PUT api/applicant
        ///     {        
        ///         "id": 0,
        ///         "name": "sdsd",
        ///         "familyName": "dsdsd",
        ///         "address": "string",
        ///         "countryOfOrigin": "string",
        ///         "eMailAdress": "string",
        ///         "age": 0,
        ///         "hired": true    
        ///     }
        /// </remarks>
        /// <param name="id"></param>  
        /// <param name="request"></param>    

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Applicant request)
        {
            _logger.LogInformation("Execution started of applicant put action");
            request.ID = id;
            var result = await _applicantService.UpdateAsync(request);
            _logger.LogInformation("Execution started of applicant put action");
            return result.ToOkResult();
        }


        [HttpGet]
        public async Task<ActionResult> List()
        {
            var result = await _applicantService.ListAsync();
            return result.ToOkResult();
        }

    }
}
