

namespace gameProject.Controllers
{
    public class GamesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ICategoryservice _categoryservice;
        private readonly IDeviceService _deviceService;
        private readonly IGameService _gameService;

        public GamesController(ApplicationDbContext context,
            ICategoryservice categoryservice,
            IDeviceService deviceService,
            IGameService gameService)
        {
            _context = context;
            _categoryservice = categoryservice;
            _deviceService = deviceService;
            _gameService = gameService;
        }

        public IActionResult Index()
        {
            var games = _gameService.GetAll();
            return View(games);
        }
        public IActionResult Details(int id)
        {
            var game = _gameService.GetById(id);

            if (game is null)
            {
                return NotFound();
            }
            return View(game);

        }

        [HttpGet]
        public IActionResult Create()
        {
            CreateGameViewModel viewmodel = new()
            {

                Categories = _categoryservice.GetSelectLists(),

                Devices = _deviceService.GetSelectLists()
            };
            return View(viewmodel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateGameViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.Categories = _categoryservice.GetSelectLists();
                model.Devices = _deviceService.GetSelectLists();
                return View(model);
            }
            else
            {
                //save in database
                await _gameService.Create(model);
                return RedirectToAction(nameof(Index));
            }
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var game = _gameService.GetById(id);

            if (game is null)
            {
                return NotFound();
            }

            UpdateGameViewModel viewModel = new()
            {
                Id = id,
                Name = game.Name,
                Description = game.Description,
                CategoryId = game.CategoryId,
                SelectedDevices = game.Device.Select(d => d.DeviceId).ToList(),
                Categories = _categoryservice.GetSelectLists(),
                Devices = _deviceService.GetSelectLists(),
                CurrentCover = game.Cover
            };

            return View(viewModel);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(UpdateGameViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.Categories = _categoryservice.GetSelectLists();
                model.Devices = _deviceService.GetSelectLists();
                return View(model);
            }
            else
            {
                //save in database
                var game = await _gameService.Update(model);
                if (game is null)
                {
                    return BadRequest();
                }
                return RedirectToAction(nameof(Index));
            }
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var isDeleted = _gameService.Delete(id);
            return isDeleted ? Ok() : BadRequest();
        }
    }
}
