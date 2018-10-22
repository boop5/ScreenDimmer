using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Input;
using Microsoft.Win32;
using SchadLucas.Wpf.EzMvvm.Commands;
using SchadLucas.Wpf.EzMvvm.Context;
using ScreenDimmer.Configuration;
using ScreenDimmer.Properties;
using ScreenDimmer.Screen;
using Application = System.Windows.Application;

namespace ScreenDimmer.Root
{
    internal class RootContext : ViewModel
    {
        private readonly ScreenService _screenService;
        private readonly ConfigurationService _configuration;
        private ICommand _closeCommand;
        private Visibility _visibility = Visibility.Hidden;
        private Tray _tray;

        private IEnumerable<ScreenDataObject> _screens = new ScreenDataObject[0];

        public RootContext(ScreenService screenService, ConfigurationService configuration)
        {
            _screenService = screenService;
            _configuration = configuration;
            SystemEvents.DisplaySettingsChanged += OnDisplaySettingsChanged;
            UpdateScreens();
            ApplyScreenSettingsFromConfiguration();
            SetupTrayIcon();
        }

        public ICommand CloseCommand => _closeCommand ?? (_closeCommand = new EzCommand(Close));

        public IEnumerable<ScreenDataObject> Screens
        {
            get => _screens;
            set => SetField(ref _screens, value);
        }

        public Visibility Visibility
        {
            get => _visibility;
            set => SetField(ref _visibility, value);
        }

        private Func<bool> CanShowWindow => () => Visibility != Visibility.Visible;

        private void ApplyScreenSettingsFromConfiguration()
        {
            foreach (var screen in _screens)
            {
                screen.Dim = _configuration.GetDim(screen);
                screen.Dye = _configuration.GetDye(screen);
            }
        }

        private void Close(object obj)
        {
            HideWindow();
        }

        private void HideWindow()
        {
            Visibility = Visibility.Collapsed;
        }

        private void OnDimChanged(ScreenDataObject screen, double dim)
        {
            _screenService.Dim(screen, dim);
            _configuration.UpdateScreenSettings(screen);
        }

        private void OnDisplaySettingsChanged(object sender, EventArgs e)
        {
            UpdateScreens();
        }

        private void OnDyeChanged(ScreenDataObject screen, EzColor dye)
        {
            _screenService.Dye(screen, dye.R, dye.G, dye.B);
            _configuration.UpdateScreenSettings(screen);
        }

        private void OnScreenPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (sender is ScreenDataObject sdo)
            {
                switch (e.PropertyName)
                {
                    case nameof(ScreenDataObject.Dim):
                        OnDimChanged(sdo, sdo.Dim);
                        break;
                    case nameof(ScreenDataObject.Dye):
                        OnDyeChanged(sdo, sdo.Dye);
                        break;
                }
            }
        }

        private void SetupTrayIcon()
        {
            _tray = new Tray(Resources.TrayIcon);
            _tray.CreateMenuItem("Open", ShowWindow, CanShowWindow);
            _tray.CreateMenuItem("Exit", Application.Current.Shutdown);
            _tray.MouseDoubleClick += (s, args) =>
            {
                if (args.Button == MouseButtons.Left)
                {
                    ShowWindow();
                }
            };
        }

        private void ShowWindow()
        {
            Visibility = Visibility.Visible;
        }

        private void UpdateScreens()
        {
            foreach (var screen in Screens)
            {
                screen.PropertyChanged -= OnScreenPropertyChanged;
            }

            _screenService.Update();
            Screens = _screenService.Screens;

            foreach (var screen in Screens)
            {
                screen.PropertyChanged += OnScreenPropertyChanged;
            }
        }
    }
}