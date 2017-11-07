using System;
using System.Collections.Generic;
using Android.App;
using Android.OS;
using MvvmCross.Droid.Views;
using SkiaSharp;
using SkiaSharp.Views.Android;
using System.Linq;

namespace SkSharpChart.Droid.Views
{
    public static class ChartData
    {
        public static SKPaint AxisBrush = new SKPaint
        {
            Style = SKPaintStyle.Stroke,
            StrokeWidth = 6,
            Color = SKColor.FromHsv(0, 0, 75),
        }; 

        public static SKPaint MessageBrush = new SKPaint
        {
            IsAntialias = true,
            Color = SKColors.White,
            StrokeWidth = 2,
            TextSize = 55,
            FakeBoldText = true,
            Typeface = SKTypeface.FromFile("Assets/HelveticaNeue.ttf")
        };

        public static SKPaint ChartBrush = new SKPaint
        {
            Style = SKPaintStyle.Stroke,
            Color = SKColors.Goldenrod,
            StrokeWidth = 10,
        };
    }

    [Activity(Label = "Charts")]
    public class MainView : MvxActivity
    {
        
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.MainView);
            var skia = FindViewById<SKCanvasView>(Resource.Id.canvasView);
            skia.PaintSurface += (sender, e) => 
            {
                var info = e.Info;         
                var surface = e.Surface;    
                var canvas = surface.Canvas;
                var tf = SKTypeface.FromFile("Assets/HelveticaNeue.ttf");
                SKPaint TextBrush = new SKPaint
                {
                    IsAntialias = true,
                    Style = SKPaintStyle.Fill,
                    Color = SKColors.SlateGray,
                    TextSize = info.Width/24,
                    FakeBoldText = true,
                    Typeface = tf
                };

                var xCoordStart = new SKPoint(info.Width / 20, info.Height - info.Height / 3);
                var xCoordEnd = new SKPoint(info.Width - info.Width / 20, info.Height - info.Height / 3);
                var yCoordStart = new SKPoint(info.Width / 20, info.Height - info.Height / 3);
                var yCoordEnd = new SKPoint(info.Width / 20, info.Height / 95);
                canvas.Clear(SKColor.FromHsv(0, 0, 90));

                var pieceWidth = info.Width / 8;
                var sideMargin = pieceWidth / 2;

                SKPoint[] points =
                {
                    new SKPoint(xCoordStart.X, xCoordStart.Y - pieceWidth),
                    new SKPoint(xCoordStart.X + pieceWidth, xCoordStart.Y - pieceWidth),
                    new SKPoint(xCoordStart.X + 2 * pieceWidth, xCoordStart.Y - 4 * pieceWidth),
                    new SKPoint(xCoordStart.X + 3 * pieceWidth, xCoordStart.Y - 3 * pieceWidth)
                };

                for (int i = 0; i < 7; i++)
                    canvas.DrawLine(xCoordStart.X, xCoordStart.Y - i * pieceWidth, xCoordEnd.X, xCoordEnd.Y - i * pieceWidth, ChartData.AxisBrush);
                
                var testSize = info.Width / 20;

                var testMargin = info.Width / 5.4f;
                // *** Labels ***
                canvas.DrawText("MARCH", xCoordStart.X, xCoordStart.Y + sideMargin, TextBrush);
                for(int k = 0; k < 6; k++) 
                    canvas.DrawText((k + 12).ToString(), yCoordStart.X + pieceWidth * k, yCoordStart.Y + testSize, TextBrush);
                for (int k = 1; k < 6; k++) 
                    canvas.DrawText((20*k).ToString(), xCoordStart.X ,xCoordStart.Y - k * pieceWidth + TextBrush.TextSize, TextBrush);
                //*** End labels ***
                canvas.DrawPoints(SKPointMode.Polygon, points, ChartData.ChartBrush);
                for (int j = 1; j < points.Length; j++)
                {
                    canvas.DrawCircle(points[j].X, points[j].Y, 18, new SKPaint { StrokeWidth = 10, Color = SKColors.Goldenrod, Style = SKPaintStyle.Stroke });
                    canvas.DrawCircle(points[j].X, points[j].Y, 13, new SKPaint { Color = SKColors.White, Style = SKPaintStyle.Fill });
                }
                canvas.DrawCircle(points[2].X, points[2].Y, 18, new SKPaint { StrokeWidth = 10, Color = SKColors.White, Style = SKPaintStyle.Stroke });
                canvas.DrawCircle(points[2].X, points[2].Y, 13, new SKPaint { Color = SKColors.Green, Style = SKPaintStyle.Fill });
                SKRect rect = new SKRect
                {
                    Size = new SKSize(500, 250),
                    Location = new SKPoint(points[2].X-250, points[2].Y-300)
                };
                canvas.DrawRoundRect(rect,10,10, new SKPaint { Style = SKPaintStyle.Fill, Color = SKColors.Green });
                SKPath triangle = new SKPath();
                triangle.MoveTo(points[2].X,points[2].Y-25);
                triangle.LineTo(points[2].X-35,points[2].Y-50);

                triangle.LineTo(points[2].X+35,points[2].Y-50);
                triangle.LineTo(points[2].X, points[2].Y - 25);
                triangle.Close();
                
                canvas.DrawPath(triangle,new SKPaint { Color = SKColors.Green, Style = SKPaintStyle.Fill});
                canvas.DrawText("100 pts",rect.Location.X+150,rect.Location.Y + 80, ChartData.MessageBrush);
                ChartData.MessageBrush.FakeBoldText = false;
                canvas.DrawText("John Douglas", rect.Location.X + 90, rect.Location.Y + 150, ChartData.MessageBrush);
                //canvas.DrawLine(yCoordStart.X, yCoordStart.Y, yCoordEnd.X, yCoordEnd.Y, axisBrush);*/
            };
        }
    }
}
