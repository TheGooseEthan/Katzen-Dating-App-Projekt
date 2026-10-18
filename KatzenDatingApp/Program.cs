
using System;
using System.IO;
using System.Drawing;
using System.Windows.Forms;
using Microsoft.Web.WebView2.WinForms;

namespace KatzenDatingApp
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();

            string htmlPath = Path.Combine(
                AppContext.BaseDirectory,
                "index.html"
            );

            if (!File.Exists(htmlPath))
            {
                MessageBox.Show(
                    "index.html was not found:\n" + htmlPath,
                    "Katzen Dating App"
                );
                return;
            }

            Form window = new Form
            {
                Text = "Katzen Dating App",

                // Exact starting content area: 1280 x 720
                ClientSize = new Size(1280, 720),

                // Open as a normal, windowed application
                WindowState = FormWindowState.Normal,
                StartPosition = FormStartPosition.CenterScreen
            };

            var browser = new WebView2
            {
                Dock = DockStyle.Fill
            };

            window.Controls.Add(browser);

            window.Shown += async (sender, e) =>
            {
                try
                {
                    await browser.EnsureCoreWebView2Async();

                    browser.CoreWebView2.Navigate(
                        new Uri(htmlPath).AbsoluteUri
                    );
                }
                catch (Exception ex)
                {
                    MessageBox.Show(
                        ex.Message,
                        "Browser startup error"
                    );

                    window.Close();
                }
            };

            Application.Run(window);
        }
    }
}
