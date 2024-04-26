

namespace gameProject.Models
{
    public class Devices : BaseEntity
    {
        [MaxLength(50)]
        public string Icon { get; set; } = string.Empty;
    }
}
