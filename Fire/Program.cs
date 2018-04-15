using System;
using System.Threading;
using System.Windows.Forms;

namespace Fire
{
    class Program
    {
        static void Main(string[] args)
        {
            Mutex m = new Mutex(true, "f89ae4f2-5042-43f4-8e56-2e42b2717b05");
            if (!m.WaitOne(0, true))
            {
                MessageBox.Show("Fire is already running.", "Fire", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Environment.Exit(0);
            }

            Application.EnableVisualStyles();

            Program inst = new Program();

            MenuItem exitItem = new MenuItem
            {
                Index = 0,
                Text = "E&xit"
            };

            ContextMenu menu = new ContextMenu();
            menu.MenuItems.Add(exitItem);

            NotifyIcon icon = new NotifyIcon
            {
                Text = "Fire",
                ContextMenu = menu,
                Icon = Properties.Resources.FlameIcon,
                Visible = true
            };

            exitItem.Click += delegate (object Sender, EventArgs e)
            {
                icon.Dispose();
                Environment.Exit(0);
            };

            Application.Run(new HotkeyHandler());

            Console.WriteLine("Fire initialized.");
        }

    }
}
