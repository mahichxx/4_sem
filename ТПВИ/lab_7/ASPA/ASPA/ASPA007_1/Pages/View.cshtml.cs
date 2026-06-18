using DAL_Celebrity;
using DAL_Celebrity_MSSQL;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ASPA007_1.Pages
{
    public class ViewModel : PageModel
    {
        private readonly IRepository<Celebrity, Lifeevent> _repo;

        public Celebrity? Celebrity { get; set; }

        public ViewModel(IRepository<Celebrity, Lifeevent> repo)
        {
            _repo = repo;
        }

        public void OnGet(int id)
        {
            Celebrity = _repo.GetCelebrityById(id);
        }
    }
}
