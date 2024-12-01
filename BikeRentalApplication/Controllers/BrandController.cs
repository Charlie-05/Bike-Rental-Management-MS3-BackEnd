using BikeRentalApplication.DTOs.RequestDTOs;
using BikeRentalApplication.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BikeRentalApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BrandController : ControllerBase
    {
        private readonly IBrandService _brandService;
        public BrandController(IBrandService brandService) { 
            _brandService = brandService;
        }
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult>AddBrand(BrandRequest request)
        {
            var data = await _brandService.AddBrand(request);
            return Ok(data);
        }
        [HttpGet]
        public async Task<IActionResult> GetBrands()
        {
            var data = await _brandService.GetAllBrands();
            return Ok(data);
        }
    }
}
