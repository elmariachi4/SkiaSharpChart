using System.Threading.Tasks;
using MvvmCross.Core.ViewModels;
using System.Windows.Input;
using SkiaSharp;
namespace SkSharpChart.Core.ViewModels
{
    public class MainViewModel : MvxViewModel
    {
        public MainViewModel()
        {
            PaintCommand = new MvxCommand<SKPaintSurfaceEventArgs>(OnPainting);
        }

        public ICommand PaintCommand { get; private set; }
        private void OnPainting(SKPaintSurfaceEventArgs e)

        {

        }
    }
}