
public enum RatingEnum
{
    One = 1, Two = 2, Three = 3, Four = 4, Five = 5
}
namespace FeedbackService.Models
{
    public class Rating : BaseModel
    {
        public Guid TitleId { get; set; }
        public Guid ProfileId { get; set; }
        public RatingEnum? RatingValue { get; set; }
        public string? Comment { get; set; }
    }
}