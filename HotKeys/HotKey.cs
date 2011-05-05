using System;
using System.Windows.Forms;

[assembly: CLSCompliant(true)]

namespace HotKeys
{
    public delegate void HotKeyPressedCallback();

    public sealed class HotKey
    {
        private bool alt;
        private bool control;
        private bool shift;
        private bool windows;
        private int virtualKey;
        private ushort id;

        public HotKey(int virtualKey = 0, bool alt = false, bool control = false, bool shift = false, bool windows = false)
        {
            Registered = false;
            Alt = alt;
            Control = control;
            Shift = shift;
            Windows = windows;
            VirtualKey = virtualKey;
        }

        public HotKey(Keys key, bool alt = false, bool control = false, bool shift = false, bool windows = false)
            : this((int)key, alt, control, shift, windows)
        {
        }

        public bool Registered
        {
            get;
            private set;
        }

        public bool Alt
        {
            get
            {
                return alt;
            }
            set
            {
                if (Registered)
                {
                    throw new InvalidOperationException();
                }
                Modifiers -= (Convert.ToInt32(alt) - Convert.ToInt32(alt = value)) * NativeMethods.MOD_ALT;
            }
        }

        public bool Control
        {
            get
            {
                return control;
            }
            set
            {
                if (Registered)
                {
                    throw new InvalidOperationException();
                }
                Modifiers -= (Convert.ToInt32(control) - Convert.ToInt32(control = value)) * NativeMethods.MOD_CONTROL;
            }
        }

        public bool Shift
        {
            get
            {
                return shift;
            }
            set
            {
                if (Registered)
                {
                    throw new InvalidOperationException();
                }
                Modifiers -= (Convert.ToInt32(shift) - Convert.ToInt32(shift = value)) * NativeMethods.MOD_SHIFT;
            }
        }

        public bool Windows
        {
            get
            {
                return windows;
            }
            set
            {
                if (Registered)
                {
                    throw new InvalidOperationException();
                }
                Modifiers -= (Convert.ToInt32(windows) - Convert.ToInt32(windows = value)) * NativeMethods.MOD_WIN;
            }
        }

        public int VirtualKey
        {
            get 
            { 
                return virtualKey; 
            }
            set
            {
                if (Registered)
                {
                    throw new InvalidOperationException();
                }
                virtualKey = value;
            }
        }

        public int Modifiers
        {
            get;
            private set;
        }

        public bool Register()
        {
            if (!Registered)
            {
                return Registered = ((id = HotKeyHandler.Instance.Add(this)) != 0);
            }
            throw new InvalidOperationException();
        }

        public bool Unregister()
        {
            if (Registered)
            {
                return Registered = !(HotKeyHandler.Instance.Remove(id));
            }
            throw new InvalidOperationException();
        }

        public void Press()
        {
            if (HotKeyPressed != null)
            {
                HotKeyPressed();
            }
        }

        public override int GetHashCode()
        {
            return Modifiers + (VirtualKey * 0x10);
        }

        public event HotKeyPressedCallback HotKeyPressed;
    }
}
