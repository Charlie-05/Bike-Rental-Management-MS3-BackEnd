using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BikeRentalApplication.Database;
using BikeRentalApplication.Entities;
using BikeRentalApplication.IServices;
using BikeRentalApplication.DTOs.RequestDTOs;

namespace BikeRentalApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RentalRecordsController : ControllerBase
    {
        private readonly IRentalRecordService _rentalRecordService;

        public RentalRecordsController(IRentalRecordService rentalRecordService)
        {
            _rentalRecordService = rentalRecordService;
        }

        // GET: api/RentalRecords
        [HttpGet]
        public async Task<IActionResult> GetRentalRecord(State? state)
        {
            var data = await _rentalRecordService.GetRentalRecords(state);
            return Ok(data);
        }

        // GET: api/RentalRecords/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetRentalRecord(Guid id)
        {
            try
            {
                var data = await _rentalRecordService.GetRentalRecord(id);
                return Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("get-payment{id}")]
        public async Task<IActionResult> GetRentalRecordPayment(Guid id)
        {
            try
            {
                var data = await _rentalRecordService.GetPayment(id);
                return Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("Get-overdue")]
        public async Task<IActionResult> GetOverDueRentals(string? nicNo)
        {
            try { 
                var data = await _rentalRecordService.GetOverDueRentals(nicNo);
                return Ok(data);
            } catch (Exception ex) {
                return BadRequest(ex.Message);
            }
        }

        // PUT: api/RentalRecords/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRentalRecord(Guid id, RentalRecord rentalRecord)
        {
            try
            {
                var data = await _rentalRecordService.UpdateRentalRecord(id, rentalRecord);
                return Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpPut("complete-record")]
        public async Task<IActionResult> CompleteRentalRecord(Guid id , RentalRecPutRequest rentalRecPutRequest)
        {
            var data = await _rentalRecordService.CompleteRentalRecord(id , rentalRecPutRequest);
            return Ok(data);
        }

        // POST: api/RentalRecords
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<IActionResult> PostRentalRecord(RentalRecRequest rentalRecRequest)
        {
            var data = await _rentalRecordService.PostRentalRecord(rentalRecRequest);
            return Ok(data);
        }

        // DELETE: api/RentalRecords/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRentalRecord(Guid id)
        {
            var data = await _rentalRecordService.DeleteRentalRecord(id);
            return Ok(data);
        }

        //private bool RentalRecordExists(Guid id)
        //{
        //    return _context.RentalRecords.Any(e => e.Id == id);
        //}
    }
}
