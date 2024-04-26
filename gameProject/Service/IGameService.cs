using gameProject.Models;

namespace gameProject.Service
{
    public interface IGameService
    {
        public IEnumerable<Games> GetAll();
        public Games? GetById(int id);
        public Task Create(CreateGameViewModel gamemodel);

        public Task<Games?> Update(UpdateGameViewModel gamemodel);

        bool Delete(int id);
    }
}
