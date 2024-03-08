using BettingApi.ApiContracts;
using BettingApi.BusinessLogic;
using BettingApi.Mappings;
using BettingApi.Models.DTO;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace BettingApi.Controllers
{
    [ApiController]
    //[Authorize] //Uncomment when authorization are implemented
    public class MatchController : Controller
    {
        private readonly ILogger<MatchController> _logger;
        private readonly IBettingLogic _bettingLogic;

        public MatchController(
            ILogger<MatchController> logger,
            IBettingLogic bettingLogic)
        {
            _logger = logger;
            _bettingLogic = bettingLogic;
        }

        [HttpGet(Routes.MatchRoutes.Heartbeat)]
        [SwaggerOperation(Summary = "Returns tik or tok in order to chech controller availability (useful when authentication is implemented)")]
        [SwaggerResponse(StatusCodes.Status200OK, "Request Successfull")]
        public IActionResult Heartbeat() => Ok(DateTime.UtcNow.Ticks % 2 == 0 ? "Tik" : "Tok");


        [HttpPost(Routes.MatchRoutes.CreateMatch)]
        [SwaggerOperation(Summary = "Creates a match")]
        [SwaggerResponse(StatusCodes.Status200OK, "Request Successfull", typeof(MatchResponseDto))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Bad Request")]
        public async Task<ActionResult<MatchResponseDto>> CreateMatch([FromBody] CreateMatchRequestDto createMatchRequestDto)
        {
            var (success, message) = createMatchRequestDto.IsCreateMatchValid();
            if (!success)
            {
                return BadRequest(message);
            }

            var matchToBeCreated = MatchMappings.MapCreateMatchRequestDtoToMatch(createMatchRequestDto);
            if (matchToBeCreated == null)
            {
                return BadRequest("Could not map item");
            }

            var created = await _bettingLogic.CreateMatch(matchToBeCreated);
            if (created == null)
            {
                return Conflict(matchToBeCreated);
            }
            var response = MatchMappings.MapMatchToMatchResponseDto(created);
            return Ok(response);
        }

        [HttpGet(Routes.MatchRoutes.GetMatch)]
        [SwaggerOperation(Summary = "Gets a match")]
        [SwaggerResponse(StatusCodes.Status200OK, "Request Successfull", typeof(MatchResponseDto))]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Match not found")]
        public async Task<ActionResult<MatchResponseDto>> GetMatch([FromRoute] int id)
        {
            var match = await _bettingLogic.GetMatch(id);
            if (match == null)
            {
                return NotFound();
            }

            var response = MatchMappings.MapMatchToMatchResponseDto(match);
            return Ok(response);
        }

        [HttpPut(Routes.MatchRoutes.UpdateMatch)]
        [SwaggerOperation(Summary = "Updates a match")]
        [SwaggerResponse(StatusCodes.Status200OK, "Request Successfull", typeof(MatchResponseDto))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Bad Request")]
        public async Task<ActionResult<MatchResponseDto>> UpdateMatch(
            [FromBody] UpdateMatchRequestDto updateMatchRequestDto,
            [FromRoute] int id)
        {
            var (success, message) = updateMatchRequestDto.IsUpdateMatchValid();
            if (!success)
            {
                return BadRequest(message);
            }

            var match = await _bettingLogic.GetMatch(id);
            if (match == null)
            {
                return NotFound();
            }

            var matchToBeUpdated = MatchMappings.MapUpdateMatchRequestDtoToMatch(match,updateMatchRequestDto, id);
            if (matchToBeUpdated == null)
            {
                return BadRequest("Could not map item");
            }

            var updated = _bettingLogic.UpdateMatch(matchToBeUpdated);
            if (updated == null)
            {
                return Conflict(matchToBeUpdated);
            }
            var response = MatchMappings.MapMatchToMatchResponseDto(updated);
            return Ok(response);
        }

        [HttpDelete(Routes.MatchRoutes.DeleteMatch)]
        [SwaggerOperation(Summary = "Deletes a match")]
        [SwaggerResponse(StatusCodes.Status204NoContent, "Request Successfull")]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Not found")]
        [SwaggerResponse(StatusCodes.Status409Conflict, "Something went wrong")]
        public async Task<IActionResult> DeleteMatch([FromRoute] int id)
        {
            var match = await _bettingLogic.GetMatch(id);
            if (match == null)
            {
                return NotFound();
            }

            var success = _bettingLogic.DeleteMatch(match);
            if (!success)
            {
                return Conflict();
            }

            return NoContent();
        }

        [HttpGet(Routes.MatchRoutes.GetMatches)]
        [SwaggerOperation(Summary = "Gets a list of matches")]
        [SwaggerResponse(StatusCodes.Status200OK, "Request Successfull", typeof(List<object>))]
        public async Task<ActionResult<List<object>>> GetMatches(
            [FromQuery] int skip = 0,
            [FromQuery] int take = 20,
            [FromQuery] string? descriptionContains = null,
            [FromQuery] bool sortAscending = true)
        {
            if (take > 1000)
            {
                return BadRequest("Please provide valid paging limit");
            }

            var matches = await _bettingLogic.GetMatches(
                skip: skip,
                take: take,
                descriptionContains: descriptionContains,
                sortAscending: sortAscending);

            var response = MatchMappings.MapToGetMatchesResponse(matches);

            return Ok(response);
        }
    }
}
