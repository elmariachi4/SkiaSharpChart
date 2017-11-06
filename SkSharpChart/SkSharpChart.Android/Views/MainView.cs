using Android.App;
using Android.OS;
using Android.Widget;
using MvvmCross.Droid.Views;
using SkiaSharp;
using SkiaSharp.Views.Android;

namespace SkSharpChart.Droid.Views
{
    //��������� ����� �� �������
    struct Point
    {
        public float X { get; set; }
        public float Y { get; set; }

        public Point(float x, float y)
        {
            X = x;
            Y = y;
        }
    }

    [Activity(Label = "Charts")]
    public class MainView : MvxActivity
    {
        
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.MainView);
            var skia = FindViewById<SKCanvasView>(Resource.Id.canvasView);
            skia.PaintSurface += (sender, e) => //�������, �������������� ��� ��������� �� Canvas
            {
                var info = e.Info;          //������ ���� � ������� �������: ������, ������, ���
                var surface = e.Surface;    // ������ � ������� ���������
                var canvas = surface.Canvas;//������ � canvas'�
                //����������� ������ � ����� ��� X
                var xCoordStart = new Point(info.Width / 20, info.Height - (info.Height / 3));
                var xCoordEnd = new Point(info.Width - (info.Width / 20), info.Height - (info.Height / 3));
                //����������� ������ � ����� ��� Y
                var yCoordStart = new Point(info.Width / 20, info.Height - (info.Height / 3));
                var yCoordEnd = new Point(info.Width / 20, info.Height / 95);

                canvas.Clear(SKColors.White);
                //���������� ������ SKPaint, �������� �� ������
                //���
                var axisBrush = new SKPaint
                {
                    Style = SKPaintStyle.Stroke,
                    StrokeWidth = 8,
                    Color = SKColor.FromHsv(0, 0, 75),                   
                };
                //����� �������
                var textBrush = new SKPaint
                {
                    Color = SKColors.SlateGray,
                    StrokeWidth = 2,
                    TextSize = 45f,
                    FakeBoldText = true,                   
                };
                //����� �� ������� ��������
                var messageBrush = new SKPaint
                {
                    Color = SKColors.White,
                    StrokeWidth = 2,
                    TextSize = 55,
                    FakeBoldText = true
                };
                //����� �������
                var chartBrush = new SKPaint
                {
                    Style = SKPaintStyle.Stroke,
                    Color = SKColors.Goldenrod,
                    StrokeWidth = 10
                };
                //������ ��������� ���
                canvas.DrawColor(SKColor.FromHsv(0, 0, 90));
                //��������! ��� ���� ������������ ����� ����� �� ���������� ����������� �������, � �������
                //������������ ����������� ������� �������� ����, � ������� �� ���������� ����������������.
                //� ���������� �����, ������������� �����-����� �����, ����� �������� ��� ���������. 
                while (i < 7) //������ 7 �������������� �����
                    canvas.DrawLine(xCoordStart.X, xCoordStart.Y - i * 150, xCoordEnd.X, xCoordEnd.Y - i++ * 150, axisBrush);

                canvas.DrawText("MARCH", xCoordStart.X, xCoordStart.Y + 80, textBrush);
                for(int k = 0; k < 6; k++)  //������ ������ �� X
                    canvas.DrawText((k + 12).ToString(), 200+ yCoordStart.X + 170 * k, yCoordStart.Y + 80, textBrush);
                for (int k = 1; k < 7; k++) //������ ������ �� Y
                    canvas.DrawText((20*k).ToString(),xCoordStart.X+20,xCoordStart.Y - k*150 + 60,textBrush);

                canvas.DrawPoints(SKPointMode.Polygon,points, chartBrush);
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
                canvas.DrawText("100 pts",rect.Location.X+150,rect.Location.Y + 80, messageBrush);
                messageBrush.FakeBoldText = false;
                canvas.DrawText("John Douglas", rect.Location.X + 90, rect.Location.Y + 150, messageBrush);
                //canvas.DrawLine(yCoordStart.X, yCoordStart.Y, yCoordEnd.X, yCoordEnd.Y, axisBrush);

            };
        }
    }
}
