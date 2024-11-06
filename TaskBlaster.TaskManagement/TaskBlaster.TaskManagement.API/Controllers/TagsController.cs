using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskBlaster.TaskManagement.Models.Dtos;
using TaskBlaster.TaskManagement.Models.InputModels;
using TaskBlaster.TaskManagement.API.Services.Interfaces;

namespace TaskBlaster.TaskManagement.API.Controllers;

[Authorize]
[Route("[controller]")]
[ApiController]
public class TagsController : ControllerBase
{
    private readonly ITagService _tagService;

    public TagsController(ITagService tagService)
    {
        _tagService = tagService;
    }

    /// <summary>
    /// Gets all tags
    /// </summary>
    /// <returns>A list of all tags</returns>
    [HttpGet("")]
    public async Task<ActionResult<IEnumerable<TagDto>>> GetAllTags()
    {
        var tags = await _tagService.GetAllTagsAsync();
        return Ok(tags);
    }

    /// <summary>
    /// Create a new tag
    /// </summary>
    /// <param name="inputModel">An input model used to populate the new tag</param>
    [HttpPost("")]
    public async Task<ActionResult> CreateNewTag([FromBody] TagInputModel inputModel)
    {
        await _tagService.CreateNewTagAsync(inputModel);
        return CreatedAtAction(nameof(GetAllTags), new { id = inputModel.Name }, inputModel);
    }
}
