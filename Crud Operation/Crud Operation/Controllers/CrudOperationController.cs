using Crud_Operation.CommonLayer.Model;
using Crud_Operation.ServiceLayer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Crud_Operation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CrudOperationController : ControllerBase
    {
        public readonly ICrudOperationSL _crudOperationSL;

        public CrudOperationController(ICrudOperationSL crudOperationSL)
        {
            _crudOperationSL = crudOperationSL;
        }
        [HttpPost]
        [Route(template:"CreateRecord")]
        public async Task<IActionResult> CreateRecord(CreateRecordRequest request)
        {
            CreateRecordResponse response = null;
            try
            {
                response = await _crudOperationSL.CreateRecord(request);
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ex.Message;
            }
            return Ok(response);
        }
        [HttpGet]
        [Route(template: "ReadRecords")]
        public async Task<IActionResult> ReadRecords()
        {
            ReadRecordResponse response = null;
            try
            {
                response = await _crudOperationSL.ReadRecord();
            }
            catch(Exception ex)
            {

            }
            return Ok(response);
        }
        [HttpPut]
        [Route(template: "UpdateRecords")]
        public async Task<IActionResult> UpdateRecords(UpdateRecordRequest request)
        {
            UpdateRecordResponse response = null;
            try
            {
                response = await _crudOperationSL.UpdateRecord(request);
            }
            catch (Exception ex)
            {

            }
            return Ok(response);
        }

        [HttpDelete]
        [Route(template: "DeleteRecords")]
        public async Task<IActionResult> DeleteRecords(DeleteRecordRequest request)
        {
            DeleteRecordResponse response = null;
            try
            {
                response = await _crudOperationSL.DeleteRecord(request);
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ex.Message;
            }
            return Ok(response);
        }

    }
}
