using FeedbackService.Data;
using FeedbackService.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Formatter;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Results;
using Microsoft.AspNetCore.OData.Routing.Controllers;

namespace FeedbackService.Controllers
{
    public class RatingsController : ODataController
    {
        private readonly DataContext _db;

        private readonly ILogger<RatingsController> _logger;

        public RatingsController(DataContext dbContext, ILogger<RatingsController> logger)
        {
            _logger = logger;
            _db = dbContext;
        }

        [EnableQuery(PageSize = 15)]
        public IQueryable<Rating> Get()
        {
            return _db.Ratings;
        }

        [EnableQuery]
        public SingleResult<Rating> Get([FromODataUri] Guid key)
        {
            var result = _db.Ratings.Where(c => c.Id == key);
            return SingleResult.Create(result);
        }
        [EnableQuery]
        public async Task<IActionResult> Post([FromBody] Rating rating)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _db.Ratings.Add(rating);
            await _db.SaveChangesAsync();
            return Created(rating);
        }
    }
}