using System;
using System.Diagnostics;
using System.Threading;
using System.Windows.Forms;

namespace Fire
{
    internal class Program
    {
        [STAThread]
        private static void Main(string[] args)
        {
            if (args.Length > 0)
            {
                if (args[0] == "/pid")
                {
                    try
                    {
                        var pid = int.Parse(args[1]);
                        Fire.KillProcessAndChildren(pid);
                    }
                    catch (Exception)
                    {
                        // suppress errors, CLI is meant to be silent
                        Environment.Exit(-1);
                    }
                }
                Environment.Exit(0);
            }

            var m = new Mutex(true, "c332199a-cd13-47cc-a05b-93276fe80fef");
            if (!m.WaitOne(0, true))
            {
                MessageBox.Show("Fire is already running.", "Fire", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Environment.Exit(0);
            }

            Application.EnableVisualStyles();

            var aboutItem = new MenuItem
            {
                Index = 0,
                Text = "About"
            };

            var exitItem = new MenuItem
            {
                Index = 1,
                Text = "E&xit"
            };

            var menu = new ContextMenu();
            menu.MenuItems.Add(aboutItem);
            menu.MenuItems.Add(exitItem);

            var hotkeyHandler = new HotkeyHandler();

            var icon = new NotifyIcon
            {
                Text = "Fire",
                ContextMenu = menu,
                Icon = Properties.Resources.FlameIcon,
                Visible = true
            };

            aboutItem.Click += delegate
            {
                hotkeyHandler.ShowAboutDialog();
            };

            exitItem.Click += delegate
            {
                icon.Dispose();
                Environment.Exit(0);
            };

            Application.Run(hotkeyHandler);

            Console.WriteLine("Fire initialized.");
        }

    }
}
