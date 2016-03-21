using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

namespace Exchange_UI
{
    class Win32Support
    {
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr FindWindow(string className, string windowName);
        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling =true)]
        public static extern IntPtr GetWindow(HandleRef hWnd, int nCmd);
        [DllImport("user32.dll")]
        public static extern IntPtr SetParent(IntPtr child, IntPtr parent);
        [DllImport("user32.dll", EntryPoint ="GetDCEx", CharSet = CharSet.Auto, ExactSpelling =true)]
        public static extern IntPtr GetDCEx(IntPtr hWnd, IntPtr hrgnClip, int flags);
        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling =true)]
        public static extern bool SetWindowPos(HandleRef hWnd, HandleRef hWndInsertAfter, int x, int y, int cx, int cy, int flags);
        [DllImport("user32.dll")]
        public static extern int ReleaseDC(IntPtr window, IntPtr handle);
    }
}
