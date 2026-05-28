using System.IO;
using System.Windows;
using System.Windows.Input;

namespace FitnessClub
{
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            Current.MainWindow = new MainWindow();
            Current.MainWindow.Loaded += (s, args) =>
            {
                try
                {
                    string cursorPath = Path.Combine(
                        AppDomain.CurrentDomain.BaseDirectory, "icon", "cursor.cur");

                    if (File.Exists(cursorPath))
                        Current.MainWindow.Cursor = new Cursor(File.OpenRead(cursorPath));
                }
                catch { }
            };
            Current.MainWindow.Show();
        }
    }
}