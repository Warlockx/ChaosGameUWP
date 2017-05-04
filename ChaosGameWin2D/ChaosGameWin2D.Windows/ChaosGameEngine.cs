using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.UI;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Imaging;
using Microsoft.Graphics.Canvas.Numerics;

namespace ChaosGameWin2D
{
    public class ChaosGameEngine
    {
        private float _height;
        private float _width;
        private readonly Random _random;
        private List<Vector2> _edges;
        private Vector2 _currentPosition;
        public ChaosGameEngine(float height,float width, int seed, List<Vector2> edges)
        {
            _height = height;
            _width = width;
            _random = new Random(seed);
            _edges = edges;
            _currentPosition = new Vector2 {X = 0, Y = 0};
            _currentPosition = RollNextPoint();
        }


        public Vector2 RollNextPoint()
        {
            int pointTo = _random.Next(0,_edges.Count);
            Vector2 nextVector2 = new Vector2
            {
                X = (_edges[pointTo].X + _currentPosition.X) / 2,
                Y = (_edges[pointTo].Y + _currentPosition.Y) / 2
            };

            _currentPosition = nextVector2;
            return nextVector2;
        }
    }
}
