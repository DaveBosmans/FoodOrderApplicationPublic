using FoodOrderApplicationAPI.APIJSONClasses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodOrderApplicationAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CheckController : ControllerBase
    {


        //1. Public endpoint, dont need any authentication for this.
        [HttpGet("public")]
        public IActionResult Public()
        {
            return Ok(new
            {
                Message = "Hello from a public endpoint! You don't need to be authenticated to see this."
            });
        }

        [HttpGet("private")]
        [Authorize]
        public IActionResult Private()
        {
            return Ok(new
            {
                Message = "Hello from a private endpoint! You need to be authenticated to see this."
            });
        }

        //Please note, this endpoint wont work with the test token from Auth0 dashboard
        //the test token does not have scope information
        //so, this only works with a client that can include scope information.
        [HttpGet("private-scoped")]
        [Authorize("read:testperm1")]
        public IActionResult Scoped()
        {
            return Ok(new
            {
                Message = "Hello from a private endpoint! You need to be authenticated and have a scope of read:testperm1 to see this."
            });
        }


        [HttpGet]
        [Route("AuthZeroTestWithRole")]
        [Authorize(Policy = "TestRoleOrderApi")]
        public ActionResult<GeneralAPIResponse> ServerDetailsHi()
        {
            var generalAPIResponse = new GeneralAPIResponse();
            var tempString1 = "Okay, You have TestRoleOrderApi Role";
            generalAPIResponse.ListOfResponses.Add(tempString1);
            generalAPIResponse.dateTimeOfResponse = DateTime.Now;
            return generalAPIResponse;
        }


        [HttpGet]
        [Route("AdminRoleCheck")]
        [Authorize(Policy = "Admin")]
        public ActionResult<GeneralAPIResponse> CheckAdminRole()
        {
            var generalAPIResponse = new GeneralAPIResponse();
            var tempString1 = "Okay, You have Admin Role";
            generalAPIResponse.ListOfResponses.Add(tempString1);
            generalAPIResponse.dateTimeOfResponse = DateTime.Now;
            return generalAPIResponse;
        }


        [HttpGet]
        [Route("CustomerRoleCheck")]
        [Authorize(Policy = "Customer")]
        public ActionResult<GeneralAPIResponse> CheckCustomerRole()
        {
            var generalAPIResponse = new GeneralAPIResponse();
            var tempString1 = "Okay, You have Customer Role";
            generalAPIResponse.ListOfResponses.Add(tempString1);
            generalAPIResponse.dateTimeOfResponse = DateTime.Now;
            return generalAPIResponse;
        }

        [HttpGet]
        [Route("KitchenWorkerRoleCheck")]
        [Authorize(Policy = "KitchenWorker")]
        public ActionResult<GeneralAPIResponse> CheckKitchenWorkerRole()
        {
            var generalAPIResponse = new GeneralAPIResponse();
            var tempString1 = "Okay, You have KitchenWorker Role";
            generalAPIResponse.ListOfResponses.Add(tempString1);
            generalAPIResponse.dateTimeOfResponse = DateTime.Now;
            return generalAPIResponse;
        }

        [HttpGet]
        [Route("WaiterRoleCheck")]
        [Authorize(Policy = "Waiter")]
        public ActionResult<GeneralAPIResponse> CheckWaiterRole()
        {
            var generalAPIResponse = new GeneralAPIResponse();
            var tempString1 = "Okay, You have Waiter Role";
            generalAPIResponse.ListOfResponses.Add(tempString1);
            generalAPIResponse.dateTimeOfResponse = DateTime.Now;
            return generalAPIResponse;
        }

    }
}
