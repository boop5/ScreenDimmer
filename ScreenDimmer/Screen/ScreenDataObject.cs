using System.ComponentModel;
using System.Windows;
using SchadLucas.Wpf.EzMvvm.Context;
using ScreenDimmer.Root;

namespace ScreenDimmer.Screen
{
    public sealed class ScreenDataObject : ObservableObject
    {
        private double _dim;
        private EzColor _dye;
        private string _deviceName;
        private bool _isPrimary;

        private Rect _bounds;

        private Rect _workingArea;

        public Rect Bounds
        {
            get => _bounds;
            set => SetField(ref _bounds, value);
        }

        public string DeviceName
        {
            get => _deviceName;
            set => SetField(ref _deviceName, value);
        }

        public double Dim
        {
            get => _dim;
            set => SetField(ref _dim, value);
        }

        public double Dim100
        {
            get => _dim * 100;
            set
            {
                Dim = value / 100;
                OnPropertyChanged(() => Dim);
                OnPropertyChanged(() => Dim100);
            }
        }

        public EzColor Dye
        {
            get => _dye;
            set
            {
                if (_dye != null)
                {
                    _dye.PropertyChanged -= OnDyeChanged;
                }

                SetField(ref _dye, value);
                _dye.PropertyChanged += OnDyeChanged;
            }
        }

        public bool IsPrimary
        {
            get => _isPrimary;
            set => SetField(ref _isPrimary, value);
        }

        public Rect WorkingArea
        {
            get => _workingArea;
            set => SetField(ref _workingArea, value);
        }

        private void OnDyeChanged(object sender, PropertyChangedEventArgs e)
        {
            OnPropertyChanged(() => Dye);
        }
    }
}