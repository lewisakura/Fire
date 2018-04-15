using System;
using System.Threading;
using System.Windows.Forms;

namespace Fire
{
    internal class Program
    {
        [STAThread]
        private static void Main()
        {
            var m = new Mutex(true, "f89ae4f2-5042-43f4-8e56-2e42b2717b05");
            if (!m.WaitOne(0, true))
            {
                MessageBox.Show("Fire is already running.", "Fire", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Environment.Exit(0);
            }

            Application.EnableVisualStyles();

            var exitItem = new MenuItem
            {
                Index = 0,
                Text = "E&xit"
            };

            var menu = new ContextMenu();
            menu.MenuItems.Add(exitItem);

            var icon = new NotifyIcon
            {
                Text = "Fire",
                ContextMenu = menu,
                Icon = Properties.Resources.FlameIcon,
                Visible = true
            };

            exitItem.Click += delegate
            {
                icon.Dispose();
                Environment.Exit(0);
            };

            Application.Run(new HotkeyHandler());

            Console.WriteLine("Fire initialized.");
        }

    }
}
