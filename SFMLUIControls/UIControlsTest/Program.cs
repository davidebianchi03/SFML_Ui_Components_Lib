using SFML.Graphics;
using SFML.Window;
using SFMLUIControls;

namespace UIControlsTest
{
    class Program {
        public static void Main(string[] args)
        {
            VideoMode v = new VideoMode(1000, 500);
            RenderWindow w = new RenderWindow(v,"Hello world");
            w.Closed += (s, e) => { w.Close(); };
            UIButton btn = new UIButton(w,"hello test", new Font("C:\\Users\\Davide\\Desktop\\cursorspeed\\cursorspeedc#\\ResizingWindow\\ResizingWindow\\dependencies\\font\\Roboto.ttf"));
            btn.Size = new SFML.System.Vector2f(100, 50);
            btn.FillColor = Color.White;
            btn.Position = new SFML.System.Vector2f(150, 150);
            btn.textColor = Color.Red;
            btn.textSize = 16;
            btn.ButtonPressed += Btn_ButtonPressed;
            btn.BorderThickness = 3;
            btn.BorderColor = Color.Blue;

            while (w.IsOpen)
            {
                w.DispatchEvents();
                w.Clear();//->anti-flickering
                btn.drawButton();
                w.Display();
                Thread.Sleep(33);
            }
        }

        private static void Btn_ButtonPressed(object? sender, EventArgs e)
        {
            Console.WriteLine("è stato premuto il pulsante");
        }
    }
}