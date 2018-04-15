using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PSTaskDialog;
using System.Diagnostics;
using System.Management;
using System.Runtime.InteropServices;

namespace Fire
{
    public partial class HotkeyHandler : Form
    {
        public HotkeyHandler()
        {
            InitializeComponent();
        }

        [DllImport("user32.dll", SetLastError = true)]
        static extern uint GetWindowThreadProcessId(IntPtr hWnd, out uint processId);

        [DllImport("user32.dll")]
        static extern IntPtr GetForegroundWindow();

        [DllImport("user32.dll")]
        static extern bool RegisterHotKey(IntPtr hWnd, int id, uint fsModifiers, uint vk);

        private void HotkeyHandler_Load(object sender, EventArgs e)
        {
            //                                       SHIFT  +  F4
            bool success = RegisterHotKey(Handle, 0, 0x0004, 0x73);
            if (!success)
            {
                MessageBox.Show("Error while registering hotkey.");
            }
        }

        protected override void WndProc(ref Message m)
        {
            if (m.Msg == 0x0312)
            {
                IntPtr activeWindow = GetForegroundWindow();
                GetWindowThreadProcessId(activeWindow, out uint pid);
                if (Process.GetCurrentProcess().Id != (int)pid)
                {
                    KillItWithFire((int)pid);
                }
            }
            base.WndProc(ref m);
        }

        void KillItWithFire(int pid)
        {
            CenterToScreen();
            DialogResult res = cTaskDialog.MessageBox(this,
                "Kill it with Fire!",
                "Are you sure?",
                $"Are you sure you want to kill process '{Process.GetProcessById(pid).ProcessName}' (PID {pid}) with Fire, killing all subprocesses that were spawned by it?" +
                "\n\nYou could lose unsaved work if any subprocesses containing edited documents were spawned by this process.",
                eTaskDialogButtons.YesNo,
                eSysIcons.Warning);

            if (res == DialogResult.Yes)
            {
                KillProcessAndChildren(pid);
            }
        }

        void KillProcessAndChildren(int pid)
        {
            ManagementObjectSearcher searcher = new ManagementObjectSearcher
                    ("Select * From Win32_Process Where ParentProcessID=" + pid);
            ManagementObjectCollection moc = searcher.Get();
            foreach (ManagementObject mo in moc)
            {
                KillProcessAndChildren(Convert.ToInt32(mo["ProcessID"]));
            }
            try
            {
                Process proc = Process.GetProcessById(pid);
                proc.Kill();
            }
            catch (ArgumentException)
            {
                // Process already exited.
            }
        }

        private void HotkeyHandler_Shown(object sender, EventArgs e)
        {
            Hide();
        }
    }
}
