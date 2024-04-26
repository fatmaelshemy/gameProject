
using gameProject.Models;

namespace gameProject.Service
{
    public class GameService : IGameService
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _WebHostEnvironment;
        private readonly string ImagePath;

        public GameService(ApplicationDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _WebHostEnvironment = webHostEnvironment;
            ImagePath = $"{_WebHostEnvironment.WebRootPath}{FileSettings.PathImage}";
        }
        public IEnumerable<Games> GetAll()
        {
            var games = _context.Games
                .Include(g => g.Category)

                .Include(g => g.Device)
                .ThenInclude(d => d.Device)
                .AsNoTracking()
                .ToList();
            return games;
        }

        public Games? GetById(int id)
        {
            var game = _context.Games
               .Include(g => g.Category)

               .Include(g => g.Device)
               .ThenInclude(d => d.Device)
               .AsNoTracking()
               .SingleOrDefault(g => g.Id == id);


            return game;
        }



        public async Task Create(CreateGameViewModel gamemodel)
        {



            Games game = new Games()
            {
                Name = gamemodel.Name,
                Description = gamemodel.Description,
                CategoryId = gamemodel.CategoryId,
                Cover = await SaveCover(gamemodel.Cover),
                Device = gamemodel.SelectedDevices.Select(d => new GameDevice { DeviceId = d }).ToList()
            };
            _context.Add(game);
            _context.SaveChanges();
        }

        public async Task<Games?> Update(UpdateGameViewModel model)
        {
            var game = _context.Games
           .Include(g => g.Device)
           .SingleOrDefault(g => g.Id == model.Id);

            if (game is null)
                return null;

            var hasNewCover = model.Cover is not null;

            var cover2 = Path.Combine(ImagePath, game.Cover);

            game.Name = model.Name;
            game.Description = model.Description;
            game.CategoryId = model.CategoryId;
            game.Device = model.SelectedDevices.Select(d => new GameDevice { DeviceId = d }).ToList();

            if (hasNewCover)
            {
                game.Cover = await SaveCover(model.Cover!);
            }

            var effectedRows = _context.SaveChanges();

            if (effectedRows > 0)
            {
                //if (hasNewCover)
                //{

                //    File.Delete(cover2);
                //}

                return game;
            }
            else
            {
                var cover = Path.Combine(ImagePath, game.Cover);
                File.Delete(cover);

                return null;
            }
        }



        private async Task<string> SaveCover(IFormFile cover)
        {
            var coverName = $"{Guid.NewGuid()}{Path.GetExtension(cover.FileName)}";

            var path = Path.Combine(ImagePath, coverName);

            using var stream = File.Create(path);
            await cover.CopyToAsync(stream);

            return coverName;
        }

        public bool Delete(int id)
        {
            var isDeleted = false;

            var game = _context.Games.Find(id);

            if (game is null)
                return isDeleted;

            _context.Remove(game);
            var effectedRows = _context.SaveChanges();

            if (effectedRows > 0)
            {
                isDeleted = true;

                var cover = Path.Combine(ImagePath, game.Cover);
                File.Delete(cover);
            }

            return isDeleted;
        }
    }
}
