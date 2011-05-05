using System;
using System.Runtime.InteropServices;

namespace HotKeys
{
    internal static class NativeMethods
    {
        internal const int WM_HOTKEY = 0x0312;
        internal const int MOD_ALT = 0x1;
        internal const int MOD_CONTROL = 0x2;
        internal const int MOD_SHIFT = 0x4;
        internal const int MOD_WIN = 0x8;
    
        [DllImport("kernel32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        internal static extern ushort GlobalAddAtom(string lpString);

        [DllImport("kernel32.dll", SetLastError = true, ExactSpelling = true)]
        internal static extern ushort GlobalDeleteAtom(ushort nAtom);

        [DllImport("user32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool RegisterHotKey(IntPtr hWnd, int id, uint fsModifiers, uint vk);

        [DllImport("user32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool UnregisterHotKey(IntPtr hWnd, int id);
    }
}
