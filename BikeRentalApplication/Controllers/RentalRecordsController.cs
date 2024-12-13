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
using Microsoft.AspNetCore.Authorization;

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
        [Authorize]
        public async Task<IActionResult> GetRentalRecord(State? state)
        {
            var data = await _rentalRecordService.GetRentalRecords(state);
            return Ok(data);
        }

        // GET: api/RentalRecords/5
        [HttpGet("{id}")]
        [Authorize]
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
        [Authorize(Roles = "Admin,Manager")]
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

        //[HttpGet("Get-overdue")]
        //public async Task<IActionResult> GetOverDueRentals()
        //{
        //    try { 
        //        var data = await _rentalRecordService.GetOverDueRentals();
        //        return Ok(data);
        //    } catch (Exception ex) {
        //        return BadRequest(ex.Message);
        //    }
        //}

        [HttpGet("Get-overdue")]
        [Authorize]
        public async Task<IActionResult> GetOverDueRentalsOfUser(string? nicNo)
        {
            try
            {
                var data = await _rentalRecordService.GetOverDueRentalsOfUser(nicNo);
                return Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        // PUT: api/RentalRecords/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin,Manager")]
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
        [Authorize(Roles = "Admin,Manager")]
        public async Task<IActionResult> CompleteRentalRecord(Guid id , RentalRecPutRequest rentalRecPutRequest)
        {
            var data = await _rentalRecordService.CompleteRentalRecord(id , rentalRecPutRequest);
            return Ok(data);
        }

        // POST: api/RentalRecords
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Authorize(Roles = "Admin,Manager")]
        public async Task<IActionResult> PostRentalRecord(RentalRecRequest rentalRecRequest)
        {
            try
            {
                var data = await _rentalRecordService.PostRentalRecord(rentalRecRequest);
                return Ok(data);
            }
            catch (Exception ex) { 
                return BadRequest(ex.Message);
            }
            
        }

        [HttpPost("Review")]
        public async Task<IActionResult> PostReview(RatingRequest ratingRequest)
        {
            var data = await _rentalRecordService.PostReview(ratingRequest);
            return Ok(data);
        }

        [HttpGet("Search")]
        public async Task<IActionResult> Search(string searchText)
        {
            try
            {
                var data = await _rentalRecordService.Search(searchText);
                return Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("Get-Range")]
        public async Task<IActionResult> GetRecordsByRange(DateTime Start, DateTime End)
        {
            try
            {
                var data = await _rentalRecordService.GetRecordsByRange(Start, End);
                return Ok(data);
            }
            catch (Exception ex) { 
                return BadRequest(ex.Message);
            }
        }
        // DELETE: api/RentalRecords/5
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteRentalRecord(Guid id)
        //{
        //    var data = await _rentalRecordService.DeleteRentalRecord(id);
        //    return Ok(data);
        //}

        //private bool RentalRecordExists(Guid id)
        //{
        //    return _context.RentalRecords.Any(e => e.Id == id);
        //}
    }
}
