using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TheWorld.Models;
using TheWorld.ViewModels;

namespace TheWorld.Controllers.Api
{
    [Authorize]
    [RouteAttribute("api/trips")]
    public class TripsController : Controller
    {
        private IWorldRepository _repository;
        private ILogger<TripsController> _logger;

        public TripsController(IWorldRepository repository, ILogger<TripsController> logger) {
            _repository = repository;
            _logger = logger;
        }

        //[HttpGet("api/trips")]
        //public JsonResult Get()
        //{
        //    return Json(new Trip() { Name = "My Trip" });
        //}

        [HttpGet()]
        public IActionResult Get()
        {
            try {
                //var results = _repository.GetAllTrips();
                var results = _repository.GetTripsByUsername(this.User.Identity.Name);
                return Ok(Mapper.Map<IEnumerable<TripViewModel>>(results));
            }
            catch (Exception ex) {
                _logger.LogError($"Failed to get all trips: {ex}");
                return BadRequest("Error occurred.");
            }
        }

        [HttpPost()]
        public async Task<IActionResult> Post([FromBody]TripViewModel vmTrip)
        {
            var trip = Mapper.Map<Trip>(vmTrip);

            trip.UserName = User.Identity.Name;

            _repository.AddTrip(trip);

            if (await _repository.SaveChangesAsync()) {
                return Created($"api/trips/{trip.Name}", Mapper.Map<TripViewModel>(trip));
            }

            _logger.LogError($"Failed to save changes to the database. {ModelState}");
            return BadRequest("Failed to save the trip.");
        }
    }
}