namespace Clock {
    using System.Timers;

    public partial class MainPage : ContentPage {
        private Timer timer = new Timer(200);


        public MainPage() {
            InitializeComponent();
            timer.Elapsed += UpdateClock;
            timer.Start();
        }

        private void UpdateClock(object? _, ElapsedEventArgs __) => ClockView.Invalidate();

    }

    public class GraphicsDrawable : IDrawable { //Размеры ячейки 50х80 (x*y)
        private int currentX = 0;

        public void Draw(ICanvas canvas, RectF dirtyRect) {
            canvas.StrokeColor = Colors.Black;
            canvas.FillColor = Colors.Black;
            canvas.StrokeSize = 3;

            DateTime now = DateTime.Now;

            int hours = now.Hour;
            int minutes = now.Minute;
            int seconds = now.Second;

            DrawDigit(canvas, hours / 10);
            DrawDigit(canvas, hours % 10);
            DrawDelimeter(canvas);
            DrawDigit(canvas, minutes / 10);
            DrawDigit(canvas, minutes % 10);
            DrawDelimeter(canvas);
            DrawDigit(canvas, seconds / 10);
            DrawDigit(canvas, seconds % 10);
            ResetPosition();
        }

        private void NextCell() => currentX += 50;
        private void ResetPosition() => currentX = 0;


        private void DrawDelimeter(ICanvas canvas) {
            canvas.FillRectangle(currentX + 20, 20, 10, 10);
            canvas.FillRectangle(currentX + 20, 50, 10, 10);
            NextCell();
        }

        private void DrawDigit(ICanvas canvas, int digit) {
            switch (digit) {
                case 0:
                    DrawLines(canvas, ZeroLines);
                    break;
                case 1:
                    DrawLines(canvas, OneLines);
                    break;
                case 2:
                    DrawLines(canvas, TwoLines);
                    break;
                case 3:
                    DrawLines(canvas, ThreeLines);
                    break;
                case 4:
                    DrawLines(canvas, FourLines);
                    break;
                case 5:
                    DrawLines(canvas, FiveLines);
                    break;
                case 6:
                    DrawLines(canvas, SixLines);
                    break;
                case 7:
                    DrawLines(canvas, SevenLines);
                    break;
                case 8:
                    DrawLines(canvas, EightLines);
                    break;
                case 9:
                    DrawLines(canvas, NineLines);
                    break;
                default: break;
            }
            NextCell();
        }

        private void DrawLines(ICanvas canvas, LineType[] types) {
            foreach (var type in types) {
                var path = new PathF();

                switch (type) {
                    case LineType.TopHorLine:
                        path.MoveTo(currentX + 10, 10);
                        path.LineTo(currentX + 40, 10);
                        break;
                    case LineType.MidHorLine:
                        path.MoveTo(currentX + 10, 40);
                        path.LineTo(currentX + 40, 40);
                        break;
                    case LineType.LowHorLine:
                        path.MoveTo(currentX + 10, 70);
                        path.LineTo(currentX + 40, 70);
                        break;
                    case LineType.RightTopVertLine:
                        path.MoveTo(currentX + 40, 10);
                        path.LineTo(currentX + 40, 40);
                        break;
                    case LineType.LeftTopVertLine:
                        path.MoveTo(currentX + 10, 10);
                        path.LineTo(currentX + 10, 40);
                        break;
                    case LineType.RightLowVertLine:
                        path.MoveTo(currentX + 40, 40);
                        path.LineTo(currentX + 40, 70);
                        break;
                    case LineType.LeftLowVertLine:
                        path.MoveTo(currentX + 10, 40);
                        path.LineTo(currentX + 10, 70);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException(nameof(type), type, null);
                }
                canvas.DrawPath(path);
                path.Dispose();
            }
        }

        private enum LineType {
            TopHorLine, MidHorLine, LowHorLine,
            RightTopVertLine, LeftTopVertLine, RightLowVertLine, LeftLowVertLine
        }

        private LineType[] ZeroLines = {
            LineType.TopHorLine, LineType.LowHorLine, LineType.LeftLowVertLine, LineType.LeftTopVertLine,
            LineType.RightLowVertLine, LineType.RightTopVertLine
        };
        private LineType[] OneLines = {
            LineType.RightLowVertLine, LineType.RightTopVertLine
        };
        private LineType[] TwoLines = {
            LineType.TopHorLine, LineType.LowHorLine, LineType.MidHorLine, 
            LineType.RightTopVertLine, LineType.LeftLowVertLine
        };
        private LineType[] ThreeLines = {
            LineType.TopHorLine, LineType.LowHorLine, LineType.MidHorLine,
            LineType.RightLowVertLine, LineType.RightTopVertLine
        };
        private LineType[] FourLines = {
            LineType.MidHorLine, LineType.LeftTopVertLine, LineType.RightLowVertLine, LineType.RightTopVertLine
        };
        private LineType[] FiveLines = {
            LineType.TopHorLine, LineType.LowHorLine, LineType.MidHorLine,
            LineType.LeftTopVertLine, LineType.RightLowVertLine
        };
        private LineType[] SixLines = {
            LineType.TopHorLine, LineType.LowHorLine, LineType.MidHorLine,
            LineType.LeftLowVertLine, LineType.LeftTopVertLine, LineType.RightLowVertLine
        };
        private LineType[] SevenLines = {
            LineType.RightLowVertLine, LineType.RightTopVertLine, LineType.TopHorLine
        };
        private LineType[] EightLines = {
            LineType.TopHorLine, LineType.LowHorLine, LineType.MidHorLine, 
            LineType.LeftLowVertLine, LineType.LeftTopVertLine, LineType.RightLowVertLine, LineType.RightTopVertLine
        };
        private LineType[] NineLines = {
            LineType.TopHorLine, LineType.LowHorLine, LineType.MidHorLine,
            LineType.LeftTopVertLine, LineType.RightLowVertLine, LineType.RightTopVertLine
        };
    }
}
