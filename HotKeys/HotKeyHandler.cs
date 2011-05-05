using System.Collections.Generic;
using System.Windows.Forms;

namespace HotKeys
{
    internal sealed class HotKeyHandler : NativeWindow
    {
        private readonly Dictionary<ushort, HotKey> hotKeys;
        private static HotKeyHandler instance;

        private HotKeyHandler()
        {
            CreateHandle(new CreateParams());
            hotKeys = new Dictionary<ushort, HotKey>();
        }

        ~HotKeyHandler()
        {
            foreach (ushort id in hotKeys.Keys)
            {
                Remove(id);
            }
            DestroyHandle();
        }

        internal static HotKeyHandler Instance
        {
            get 
            { 
                return instance ?? (instance = new HotKeyHandler()); 
            }
        }

        internal ushort Add(HotKey hotKey)
        {
            ushort id = NativeMethods.GlobalAddAtom(hotKey.GetHashCode().ToString());
            if (id != 0 && NativeMethods.RegisterHotKey(Handle, id, (uint)hotKey.Modifiers, (uint)hotKey.VirtualKey))
            {
                hotKeys.Add(id, hotKey);
                return id;
            }
            return 0;
        }

        internal bool Remove(ushort id)
        {
            NativeMethods.GlobalDeleteAtom(id);
            return (NativeMethods.UnregisterHotKey(Handle, id) && hotKeys.Remove(id));
        }

        protected override void WndProc(ref Message m)
        {
            if (m.Msg == NativeMethods.WM_HOTKEY)
            {
                HotKey hotKey;
                if (hotKeys.TryGetValue((ushort)m.WParam, out hotKey))
                {
                    hotKey.Press();
                }
            }
            base.WndProc(ref m);
        }
    }
}
