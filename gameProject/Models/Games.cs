
namespace gameProject.Models
{
    public class Games : BaseEntity
    {

        [MaxLength(1000)]
        public string Description { get; set; } = string.Empty;
        [MaxLength(200)]
        public string Cover { get; set; } = string.Empty;

        public int CategoryId { get; set; }
        public Category Category { get; set; } = default!;

        public ICollection<GameDevice> Device { get; set; } = new List<GameDevice>();


    }
}
