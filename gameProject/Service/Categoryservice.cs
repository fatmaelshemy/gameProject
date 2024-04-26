

namespace gameProject.Service
{
    public class Categoryservice : ICategoryservice
    {
        private readonly ApplicationDbContext _context;

        public Categoryservice(ApplicationDbContext context)
        {
            _context = context;
        }
        public IEnumerable<SelectListItem> GetSelectLists()
        {
            IEnumerable<SelectListItem> selectedItem = _context.Categories.Select(
                    c => new SelectListItem { Value = c.Id.ToString(), Text = c.Name })
                    .OrderBy(c => c.Text)
                    .AsNoTracking()
                    .ToList();

            return selectedItem;
        }
    }
}
