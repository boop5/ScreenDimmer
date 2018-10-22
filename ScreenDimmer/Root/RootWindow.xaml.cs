using System.Windows.Input;
using SchadLucas.Wpf.EzMvvm;

namespace ScreenDimmer.Root
{
    public partial class RootWindow : IView
    {
        public RootWindow()
        {
            InitializeComponent();
        }

        private void OnLeftMouseButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }
    }
}