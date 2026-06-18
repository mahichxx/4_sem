using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ASPA007_1.Pages
{
    public class AddModel : PageModel
    {
        [BindProperty] public string FullName { get; set; } = "";
        [BindProperty] public string Nationality { get; set; } = "";
        [BindProperty] public IFormFile Photo { get; set; } = null!;

        public IActionResult OnPost()
        {
            if (Photo == null)
                return Page();

            // Создаём временную папку
            var tempFolder = Path.Combine("wwwroot", "temp");
            if (!Directory.Exists(tempFolder))
                Directory.CreateDirectory(tempFolder);

            // Генерируем уникальное имя файла
            var tempFileName = Guid.NewGuid().ToString() + Path.GetExtension(Photo.FileName);
            var tempPath = Path.Combine(tempFolder, tempFileName);

            // Сохраняем файл во временную папку
            using (var fs = new FileStream(tempPath, FileMode.Create))
            {
                Photo.CopyTo(fs);
            }

            // Сохраняем только текстовые данные
            TempData["FullName"] = FullName;
            TempData["Nationality"] = Nationality;
            TempData["TempPhoto"] = tempFileName;

            return RedirectToPage("Confirm");
        }
    }
}
