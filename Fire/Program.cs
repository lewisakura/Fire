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
            var m = new Mutex(true, "c332199a-cd13-47cc-a05b-93276fe80fef");
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
