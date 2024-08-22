using ColorBackend.DataAccess;
using ColorBackend.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ColorBackend.Controllers
{
    /// <summary>
    /// API Controller to manage CRUD operations for Color entities.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class ColorController : ControllerBase
    {
        private readonly ColorDA colorDA_ = new();

        /// <summary>
        /// Creates a new Color entity.
        /// </summary>
        /// <param name="objBody">The Color entity to create.</param>
        /// <returns>A response containing the created Color entity.</returns>
        [Authorize]
        [HttpPost]
        public IActionResult Create([FromBody] Color objBody)
        {
            try
            {
                objBody = colorDA_.Create(objBody);

                var createdResponse = new ApiResponse<Color>
                {
                    IsSuccess = true,
                    ReturnedCode = 201,
                    Message = "Color successfully created",
                    Result = objBody
                };
                return CreatedAtAction(nameof(GetById), new { id = objBody.ID }, createdResponse);
            }
            catch (Exception ex)
            {
                var errorResponse = new ApiResponse<object>
                {
                    IsSuccess = false,
                    ReturnedCode = 500,
                    Message = $"Internal server error: {ex.Message}",
                    Result = null
                };
                return StatusCode(500, errorResponse);
            }
        }

        /// <summary>
        /// Updates an existing Color entity.
        /// </summary>
        /// <param name="id">The ID of the Color entity to update.</param>
        /// <param name="objBody">The updated Color entity.</param>
        /// <returns>A response indicating the outcome of the update operation.</returns>
        [Authorize]
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] Color objBody)
        {
            try
            {
                if (id != objBody.ID)
                {
                    var badRequestResponse = new ApiResponse<object>
                    {
                        IsSuccess = false,
                        ReturnedCode = 400,
                        Message = "The ID in the URL does not match the ID in the request body",
                        Result = null
                    };
                    return BadRequest(badRequestResponse);
                }

                colorDA_.Update(objBody);
                return Ok(new ApiResponse<object>
                {
                    IsSuccess = true,
                    ReturnedCode = 200,
                    Message = "Color successfully updated",
                    Result = null
                });
            }
            catch (Exception ex)
            {
                var errorResponse = new ApiResponse<object>
                {
                    IsSuccess = false,
                    ReturnedCode = 500,
                    Message = $"Internal server error: {ex.Message}",
                    Result = null
                };
                return StatusCode(500, errorResponse);
            }
        }

        /// <summary>
        /// Deletes a Color entity by ID.
        /// </summary>
        /// <param name="id">The ID of the Color entity to delete.</param>
        /// <returns>A response indicating the outcome of the delete operation.</returns>
        [Authorize]
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                colorDA_.Delete(id);
                return Ok(new ApiResponse<object>
                {
                    IsSuccess = true,
                    ReturnedCode = 200,
                    Message = "Color successfully deleted",
                    Result = null
                });
            }
            catch (Exception ex)
            {
                var errorResponse = new ApiResponse<object>
                {
                    IsSuccess = false,
                    ReturnedCode = 500,
                    Message = $"Internal server error: {ex.Message}",
                    Result = null
                };
                return StatusCode(500, errorResponse);
            }
        }

        /// <summary>
        /// Retrieves a Color entity by ID.
        /// </summary>
        /// <param name="id">The ID of the Color entity to retrieve.</param>
        /// <returns>A response containing the requested Color entity.</returns>
        [Authorize]
        [HttpGet("{id}")]
        public ActionResult<ApiResponse<Color>> GetById(int id)
        {
            try
            {
                Color objS = colorDA_.GetByID(id);
                if (objS == null)
                {
                    var notFoundResponse = new ApiResponse<object>
                    {
                        IsSuccess = false,
                        ReturnedCode = 404,
                        Message = $"Color with ID {id} not found",
                        Result = null
                    };
                    return NotFound(notFoundResponse);
                }

                var successResponse = new ApiResponse<Color>
                {
                    IsSuccess = true,
                    ReturnedCode = 200,
                    Message = "Color successfully retrieved",
                    Result = objS
                };
                return Ok(successResponse);
            }
            catch (Exception ex)
            {
                var errorResponse = new ApiResponse<object>
                {
                    IsSuccess = false,
                    ReturnedCode = 500,
                    Message = $"Internal server error: {ex.Message}",
                    Result = null
                };
                return StatusCode(500, errorResponse);
            }
        }

        /// <summary>
        /// Retrieves all Color entities.
        /// </summary>
        /// <returns>A response containing a list of all Color entities.</returns>
        [Authorize]
        [HttpGet]
        public ActionResult<ApiResponse<List<Color>>> GetAll()
        {
            try
            {
                List<Color> list = colorDA_.GetAll();
                var response = new ApiResponse<List<Color>>
                {
                    IsSuccess = true,
                    ReturnedCode = 200,
                    Message = "Colors successfully retrieved",
                    Result = list
                };
                return Ok(response);
            }
            catch (Exception ex)
            {
                var response = new ApiResponse<object>
                {
                    IsSuccess = false,
                    ReturnedCode = 500,
                    Message = $"Internal server error: {ex.Message}",
                    Result = null
                };
                return StatusCode(500, response);
            }
        }
    }
}
