using API.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace API.Controllers
{
    public abstract class BaseTotalController<T> : BaseApiController where T : class
    {
        private readonly IUnitOfWork unitOfWork;

        public BaseTotalController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        [HttpDelete]
        public async Task<ActionResult> DeleteEntities([FromBody] List<int> ids)
        {
            if (ids == null || ids.Count == 0)
            {
                return BadRequest("No IDs provided.");
            }

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrEmpty(userId))
            {
                return BadRequest("No user ID was found in token.");
            }

            foreach (var id in ids)
            {
                var entity = await GetEntityByIdAsync(id);
                if (entity == null)
                {
                    return NotFound($"Entity with ID {id} not found.");
                }
                if (!IsUserAuthorized(entity, userId))
                {
                    return Unauthorized($"You are not authorized to delete entity with ID {id}.");
                }
                DeleteEntity(entity);
            }

            if (await unitOfWork.CompleteAsync())
            {
                return Ok();
            }

            return BadRequest("Failed to delete entities.");
        }

        protected abstract Task<T?> GetEntityByIdAsync(int id);
        protected abstract bool IsUserAuthorized(T entity, string userId);
        protected abstract void DeleteEntity(T entity);
    }
}
