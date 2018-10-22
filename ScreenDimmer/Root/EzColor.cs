using System.Windows.Media;
using SchadLucas.Wpf.EzMvvm.Context;

namespace ScreenDimmer.Root
{
    public class EzColor : ObservableObject
    {
        private byte _r;
        private byte _g;
        private byte _b;

        public Brush Brush => new SolidColorBrush(new Color {R = R, G = G, B = B, A = 255});

        public byte B
        {
            get => _b;
            set => SetField(ref _b, value);
        }

        public byte G
        {
            get => _g;
            set => SetField(ref _g, value);
        }

        public byte R
        {
            get => _r;
            set => SetField(ref _r, value);
        }

        protected override bool SetField<T>(ref T field, T value, string property = null)
        {
            var result = base.SetField(ref field, value, property);

            OnPropertyChanged(() => Brush);

            return result;
        }
    }
}