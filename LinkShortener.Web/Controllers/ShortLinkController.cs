using System.Runtime.CompilerServices;
using System.Text.Json;
using LinkShortener.Infrastructure.Services;
using LinkShortener.Web.Services;
using Microsoft.AspNetCore.Mvc;

namespace LinkShortener.Web.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ShortLinkController(IApiKeyValidation apiKeyValidation,
    ILinkConverterCommand commandService, 
    IShortenerService shortenerService): ControllerBase
{
    
    [HttpGet("link")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> GetLink(
        [FromQuery] string hash,
        [FromQuery] int option,
        [EnumeratorCancellation]  CancellationToken cancellationToken = default)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        
        var link = await shortenerService.GetLink(hash, cancellationToken);
        if (link is null)
        {
            return NotFound();
        }
        
        var formattedLink = await commandService.ShareLink(link, option);
        var response = JsonSerializer.Serialize(formattedLink);
        return Content(response);
    }
    
    /// <summary>
    /// Create a new resource with a shorten uri
    /// </summary>
    /// <param name="token">Client api key</param>
    /// <param name="url">Url origin</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpPost("link")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> PostLink(
        [FromHeader(Name="X-Api-Key")] string apikey, 
        [FromQuery] string originalUrl, 
        [FromQuery] string userName, 
        [EnumeratorCancellation] CancellationToken cancellationToken = default)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        
        bool isValid = apiKeyValidation.IsValidApiKey(apikey);
        if (!isValid)
            return Unauthorized();

        var shortLink= await shortenerService.AddShortLink(originalUrl, userName, cancellationToken);
        
        return Content(shortLink);
    }
    
    [HttpPut("link")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> PutLink(
        [FromQuery] string token, 
        [FromQuery] string url_key, 
        [EnumeratorCancellation] CancellationToken cancellationToken = default)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        await shortenerService.UpdateLink(token, url_key, cancellationToken);
        
        return Ok();
    }

    [HttpDelete("link")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> DeleteLink(
        [FromQuery] string apikey, 
        [FromQuery] string url, 
        CancellationToken cancellationToken = default)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        
        bool isValid = apiKeyValidation.IsValidApiKey(apikey);
        if (!isValid)
            return Unauthorized();
        
        await shortenerService.RemoveLink(url, cancellationToken);
        return Ok();
    }
}