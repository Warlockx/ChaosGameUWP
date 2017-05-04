using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.Input;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Xaml.Shapes;
using Microsoft.Graphics.Canvas.UI.Xaml;
using Vector2 = Microsoft.Graphics.Canvas.Numerics.Vector2;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace ChaosGameWin2D
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            InitializeComponent();
            CanvasControl.Draw += CanvasControl_Draw;
        }
        
        private readonly List<Vector2> _pointerPoints = new List<Vector2>();
        private readonly List<Vector2> _chaosPoints = new List<Vector2>();
        private void CanvasControl_Draw(CanvasControl sender, CanvasDrawEventArgs args)
        {
            foreach (Vector2 pointerPoint in _pointerPoints)
            {
                args.DrawingSession.FillEllipse(pointerPoint.X, pointerPoint.Y,5,5,Colors.Red);
                
            }
            foreach (Vector2 chaosPoint in _chaosPoints)
            {
                args.DrawingSession.FillEllipse(chaosPoint.X, chaosPoint.Y, 1, 1, Colors.Blue);
            }
        }

        private void CanvasControl_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            PointerPoint ptrPoint = e.GetCurrentPoint(CanvasControl);

            _pointerPoints.Add(ptrPoint.Position.ToVector2());
            CanvasControl.Invalidate();
        }


        private async void RenderCanvas_OnClick(object sender, RoutedEventArgs e)
        {

            if (_pointerPoints.Count > 1)
            {
                ChaosGameEngine chaosGameEngine = new ChaosGameEngine((float) CanvasControl.ActualHeight,
                    (float) CanvasControl.ActualWidth,
                    32315,
                    _pointerPoints);

               
                    for (int i = 0; i < 100000; i++)
                    {
                        _chaosPoints.Add(chaosGameEngine.RollNextPoint());
                    }
                CanvasControl.Invalidate();
            }

            else
            {
                MessageDialog dialog = new MessageDialog("Add Points First.");
                await dialog.ShowAsync();
            }
        
        }

        private void ClearCanvas_OnClick(object sender, RoutedEventArgs e)
        {
            _pointerPoints.Clear();
            _chaosPoints.Clear();
            CanvasControl.Invalidate();
        }
    }
}
