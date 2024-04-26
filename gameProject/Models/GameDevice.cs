

namespace gameProject.Models
{
    public class GameDevice
    {
        public int GameId { get; set; }

        public Games Game { get; set; } = default!;

        public int DeviceId { get; set; }

        public Devices Device { get; set; } = default!;
    }
}
