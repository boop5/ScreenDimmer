using System;
using System.Drawing;
using System.Windows.Forms;

namespace ScreenDimmer
{
    public class Tray : IDisposable
    {
        public Tray(Icon icon)
        {
            NotifyIcon = new NotifyIcon
            {
                Visible = true,
                ContextMenu = new ContextMenu(),
                Icon = icon
            };
        }

        ~Tray()
        {
            Dispose(false);
        }

        public event MouseEventHandler MouseClick
        {
            add => NotifyIcon.MouseClick += value;
            remove => NotifyIcon.MouseClick -= value;
        }

        public event MouseEventHandler MouseDoubleClick
        {
            add => NotifyIcon.MouseDoubleClick += value;
            remove => NotifyIcon.MouseDoubleClick -= value;
        }

        public event MouseEventHandler MouseDown
        {
            add => NotifyIcon.MouseDown += value;
            remove => NotifyIcon.MouseDown -= value;
        }

        public event MouseEventHandler MouseUp
        {
            add => NotifyIcon.MouseUp += value;
            remove => NotifyIcon.MouseUp -= value;
        }

        private NotifyIcon NotifyIcon { get; }

        public void CreateMenuItem(string name, Action clickedCallback) => CreateMenuItem(name, clickedCallback, () => true);

        public void CreateMenuItem(string name, Action clickedCallback, Func<bool> isEnabled)
        {
            var menuItem = new MenuItem
            {
                Index = 1,
                Name = name,
                Text = $"&{name}"
            };

            NotifyIcon.ContextMenu.Popup += (s, e) => menuItem.Enabled = isEnabled();
            menuItem.Click += (s, e) => clickedCallback();

            NotifyIcon.ContextMenu.MenuItems.Add(menuItem);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            NotifyIcon.Icon = null;
            NotifyIcon.Visible = false;

            if (disposing)
            {
                NotifyIcon.Dispose();
            }
        }
    }
}