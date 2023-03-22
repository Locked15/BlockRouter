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
    public class ModelController : Controller
    {
        private readonly IRepository<Model> _repository;

        public ModelController(IRepository<Model> modelRepository)
        {
            _repository = modelRepository;
        }

        #region Actions.

        [HttpPost]
        [Route("Create")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> Create([FromBody] Model model)
        {
            try
            {
                var result = await _repository.Create(model);
                if (result)
                    return Ok(model);
                else
                    return StatusCode(StatusCodes.Status422UnprocessableEntity, model);
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
        public async Task<IActionResult> Update([FromBody] Model model)
        {
            try
            {
                var result = await _repository.Update(model);
                if (result)
                    return Ok(model);
                else
                    return StatusCode(StatusCodes.Status422UnprocessableEntity, model);
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
