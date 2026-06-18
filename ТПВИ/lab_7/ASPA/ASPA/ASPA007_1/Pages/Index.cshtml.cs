using DAL_Celebrity;
using DAL_Celebrity_MSSQL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ASPA007_1.Pages
{
    public class IndexModel : PageModel
    {
        private readonly IRepository<Celebrity, Lifeevent> _repo;

        public List<Celebrity> Celebrities { get; set; } = new();

        public IndexModel(IRepository<Celebrity, Lifeevent> repo)
        {
            _repo = repo;
        }

        public void OnGet()
        {
            Celebrities = _repo.GetAllCelebrities();
        }
        public IActionResult OnPostDelete(int id)
        {
            // 1. Сначала можно найти знаменитость, чтобы узнать имя файла фото
            var celebrity = _repo.GetCelebrityById(id);

            if (celebrity != null)
            {
                // 2. Удаляем запись из базы данных через репозиторий
                _repo.DelCelebrity(id);
            }

            return RedirectToPage(); // Перезагружаем страницу, чтобы список обновился
        }
    }
}
