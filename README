Encapsulates Windows API functions for handling global hot keys.
Hoping to add Linux support in the future.

Example Usage:
HotKey hotKey = new HotKey();
hotKey.VirtualKey = (int)Keys.H;
hotKey.Alt = true;
hotKey.Control = true;
hotKey.HotKeyPressed += () => MessageBox.Show("ALT+CTRL+H pressed!");
hotKey.Register();