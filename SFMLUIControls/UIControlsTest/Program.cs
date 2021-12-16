using SFML.Graphics;
using SFML.Window;
using SFMLUIControls;
using System;
using System.Threading;

namespace UIControlsTest
{
    class Program {
        public static void Main(string[] args)
        {
            VideoMode v = new VideoMode(1000, 500);
            RenderWindow w = new RenderWindow(v,"Hello world");
            w.Closed += (s, e) => { w.Close(); };
            UIButton btn = new UIButton(w,"hello test", new Font("E:\\bianchi\\Minecraft.ttf"));
            btn.Size = new SFML.System.Vector2f(100, 50);
            btn.FillColor = Color.White;
            btn.Position = new SFML.System.Vector2f(150, 150);
            btn.textColor = Color.Red;
            btn.textSize = 16;
            btn.ButtonPressed += Btn_ButtonPressed;
            btn.BorderThickness = 3;
            btn.BorderColor = Color.Blue;

            UITextInput text_input = new UITextInput(new Font("E:\\bianchi\\Minecraft.ttf"),w);
            text_input.Content = "Hello world";
            text_input.Size = new SFML.System.Vector2f(150, 50);
            text_input.Position = new SFML.System.Vector2f(100, 50);
            text_input.BackgroundColor = Color.White;
            text_input.ForegroundColor = Color.Blue;
            text_input.TextAlignment = UITextInput.AlignmentLeft;
            text_input.TextSize = 16;
            text_input.BorderColor = Color.Green;
            text_input.BorderThickness = 2;

            while (w.IsOpen)
            {
                w.DispatchEvents();
                w.Clear();//->anti-flickering
                btn.draw();
                text_input.draw();
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