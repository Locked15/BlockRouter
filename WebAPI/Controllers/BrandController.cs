using BlockRouter.WebAPI.Controllers.Repositories.Base;
using BlockRouter.WebAPI.Models.Entities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace BlockRouter.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BrandController : Controller
    {
        private readonly IRepository<Brand> _repository;

        public BrandController(IRepository<Brand> brandRepository)
        {
            _repository = brandRepository;
        }

        #region Actions.

        [HttpPost]
        [Route("Create")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> Create([FromBody] Brand brand)
        {
            try
            {
                var result = await _repository.Create(brand);
                if (result)
                    return Ok(brand);
                else
                    return StatusCode(StatusCodes.Status422UnprocessableEntity, brand);
            }
            catch (Exception ex)
            {
                Log.Error($"Error!\n{ex.Message}.");
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet]
        [Route("Read")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> Read()
        {
            try
            {
                var result = await _repository.ReadAll();
                return Ok(result);
            }
            catch (Exception ex)
            {
                Log.Error($"Error!\n{ex.Message}.");
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet]
        [Route("Read/ID/{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> Read(int id)
        {
            try
            {
                var result = await _repository.Read(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                Log.Error($"Error!\n{ex.Message}.");
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet]
        [Route("Read/Name/{name}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> Read(string name)
        {
            try
            {
                var result = await _repository.Read(name);
                return Ok(result);
            }
            catch (Exception ex)
            {
                Log.Error($"Error!\n{ex.Message}.");
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPost]
        [Route("Update")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> Update([FromBody] Brand brand)
        {
            try
            {
                var result = await _repository.Update(brand);
                if (result)
                    return Ok(brand);
                else
                    return StatusCode(StatusCodes.Status422UnprocessableEntity, brand);
            }
            catch (Exception ex)
            {
                Log.Error($"Error!\n{ex.Message}.");
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpDelete]
        [Route("Delete/{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var result = await _repository.Delete(id);
                if (result)
                    return Ok(id);
                else
                    return StatusCode(StatusCodes.Status404NotFound);
            }
            catch (Exception ex)
            {
                Log.Error($"Error!\n{ex.Message}.");
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        #endregion
    }
}
