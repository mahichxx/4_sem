using ASPA007_1;
using DAL_Celebrity;
using DAL_Celebrity_MSSQL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Options;

namespace ASPA007_1.Pages
{
    public class ConfirmModel : PageModel
    {
        private readonly IRepository<Celebrity, Lifeevent> _repo;
        private readonly CelebritiesConfig _cfg;

        public string FullName { get; set; } = "";
        public string Nationality { get; set; } = "";
        public string TempPhoto { get; set; } = "";

        public ConfirmModel(IRepository<Celebrity, Lifeevent> repo, IOptions<CelebritiesConfig> cfg)
        {
            _repo = repo;
            _cfg = cfg.Value;
        }

        public void OnGet()
        {
            FullName = TempData["FullName"]?.ToString() ?? "";
            Nationality = TempData["Nationality"]?.ToString() ?? "";
            TempPhoto = TempData["TempPhoto"]?.ToString() ?? "";

            TempData.Keep();
        }

        public IActionResult OnPost()
        {
            FullName = TempData["FullName"]?.ToString() ?? "";
            Nationality = TempData["Nationality"]?.ToString() ?? "";
            TempPhoto = TempData["TempPhoto"]?.ToString() ?? "";

            if (string.IsNullOrEmpty(TempPhoto))
                return RedirectToPage("Add");

            // Путь к временному файлу
            var tempPath = Path.Combine("wwwroot", "temp", TempPhoto);

            // Путь к постоянной папке
            var finalFolder = _cfg.PhotosFolder;
            if (!Directory.Exists(finalFolder))
                Directory.CreateDirectory(finalFolder);

            var finalPath = Path.Combine(finalFolder, TempPhoto);

            // Переносим файл
            System.IO.File.Move(tempPath, finalPath);

            // Добавляем в БД
            var c = new Celebrity
            {
                FullName = FullName,
                Nationality = Nationality,
                ReqPhotoPath = "/photos/" + TempPhoto
            };

            _repo.AddCelebrity(c);

            return RedirectToPage("Index");
        }
    }
}
