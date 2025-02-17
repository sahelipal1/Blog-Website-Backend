using Blog_website.BAL.IServices;
using Blog_website.DAL.Entity;
using Blog_website.DAL.Entity.DTO;
using Blog_website.Repo.IRepository;
using Microsoft.AspNetCore.Mvc;
using static Blog_website.DAL.Entity.DTO.UserDTO;

[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly IUserServices _userService;

    // Inject the UserService into the controller
    public UserController(IUserServices userService)
    {
        _userService = userService;
    }

    // POST api/user
    [HttpPost("AddUser")]
    public async Task<IActionResult> AddUser([FromBody] UserDTO.ReqDTO nuserDto)
    {
        if (nuserDto == null)
        {
            return BadRequest("User data is required.");
        }

        try
        {
            // Call the UserService to add the user
            var result = await _userService.AddUser(nuserDto);

            if (result != null) // Check if result is a valid UserDTO
            {
                return Ok(new { message = "User added successfully.", user = result });
            }
            else
            {
             
                return StatusCode(500, "There was an error adding the user.");
            }
        }
        catch (Exception ex)
        {
            // Log the error (you can use a logger here)
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }

    }
    [HttpPut("UpdateUser/{id}")]
   [Produces("application/json")]
    public async Task<IActionResult> UpdateUser(int id, [FromBody] ReqDTO userDto)
    {
        if (userDto == null)
        {
            return BadRequest("Invalid user data.");
        }

        try
        {
            // Call the UpdateUser method from UserService
            var updatedUser = await _userService.UpdateUser(id, userDto);

            // Return the updated user
            return Ok(updatedUser);
        }
        catch (KeyNotFoundException ex)
        {
            // User not found
            return NotFound(ex.Message);
        }
        catch (Exception ex)
        {
            // Other errors (e.g., database issues)
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }
    [HttpGet("FetchAllUsers")]
    [Produces("application/json")]
    public async Task<ActionResult<List<User>>> GetAllUsers()
    {
        var users = await _userService.GetAllUsersAsync();
        return Ok(users);
    }

    [HttpGet("GetUsersById/{id}")] // Correct route syntax
    [Produces("application/json")]

    public async Task<ActionResult<User>> GetByIdAsync(int id)
    {
        var user = await _userService.GetByIdAsync(id); // Corrected method call without re-declaring parameter type

        if (user == null)
        {
            return NotFound("User not found.");
        }

        return Ok(user);
    }


    [HttpDelete("DeleteUser/{id}")]
    public async Task<IActionResult> DeleteUser(int id)
    {
        var result = await _userService.DeleteUserAsync(id);
        if (!result)
        {
            return NotFound(new { Message = "User not found or already Deleted." });
        }
        return Ok(new { Message = "User deleted successfully (soft delete)." });
    }

}
