using BestAuth.Application.Abstracts;
using BestAuth.Domain.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BestAuth.Api.Controllers
{
    [Route("api/admin")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class AdminController(IRoleService userManagementService) : Controller
    {
        private readonly IRoleService _userManagementService = userManagementService;

        [HttpGet("users")]
        public async Task<IActionResult> GetUsers(CancellationToken ct)
        {
            var users = await _userManagementService.GetUsersWithRolesAsync(ct);
            return Ok(users);
        }

        [HttpPost("users/{userId:guid}/roles")]
        public async Task<IActionResult> AssignRole(Guid userId, UpdateUserRoleRequest request)
        {
            await _userManagementService.AssignRoleAsync(userId, request.RoleName);
            return NoContent();
        }

        [HttpDelete("users/{userId:guid}/roles/{roleName}")]
        public async Task<IActionResult> RemoveRole(Guid userId, string roleName)
        {
            await _userManagementService.RemoveRoleAsync(userId, roleName);
            return NoContent();
        }
    }
}
