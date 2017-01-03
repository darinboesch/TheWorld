using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TheWorld.Models;
using TheWorld.Services;
using TheWorld.ViewModels;

namespace TheWorld.Controllers.Api
{
    [Authorize]
    [Route("/api/trips/{tripName}/stops")]
    public class StopsController : Controller
    {
        private IWorldRepository _repository;
        private GeoCoordsService _geoService;
        private ILogger<StopsController> _logger;

        public StopsController(IWorldRepository repository,
                               GeoCoordsService geoService,
                               ILogger<StopsController> logger)
        {
            _repository = repository;
            _geoService = geoService;
            _logger = logger;
        }

        [HttpGet()]
        public IActionResult Get(string tripName) {
            try {
                //var trip = _repository.GetTripByName(tripName);
                var trip = _repository.GetUserTripByName(tripName, User.Identity.Name);
                return Ok(
                    Mapper.Map<IEnumerable<StopViewModel>>(trip.Stops.OrderBy(s => s.Order).ToList())
                );
            }
            catch (Exception ex) {
                _logger.LogError("Failed to get stops: {0}", ex);
            }

            return BadRequest("Failed to get stops.");
        }

        [HttpPost()]
        public async Task<IActionResult> Post(string tripName, [FromBody] StopViewModel vm) {
            try {
                if (ModelState.IsValid) {
                    var newStop = Mapper.Map<Stop>(vm);

                    // lookup geocodes
                    var result = await _geoService.GetCoordsAsync(newStop.Name);

                    if (!result.Success) {
                        _logger.LogError(result.Message);
                    }
                    else {
                        newStop.Latitude = result.Latitude;
                        newStop.Longitude = result.Longitude;

                        // save to the db
                        _repository.AddStop(tripName, newStop, User.Identity.Name);

                        if (await _repository.SaveChangesAsync()) {
                            return Created(
                                $"/api/trips/{tripName}/stops/{newStop.Name}",
                                Mapper.Map<StopViewModel>(newStop)
                            );
                        }
                    }
                }
            }
            catch (Exception ex) {
                _logger.LogError("Failed to save new Stop: {0}", ex);
            }

            return BadRequest("Failed to save new stop.");
        }
    }
}