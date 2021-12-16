using SFML.Graphics;
using SFML.Window;
using SFMLUIComponentsLibrary;
using SFMLUIControls;
using System;
using System.Threading;

namespace UIControlsTest
{
    class Program
    {
        static UITextInput text_input;
        public static void Main(string[] args)
        {
            VideoMode v = new VideoMode(1000, 500);
            UIWindow w = new UIWindow(v, "Hello world");
            w.Closed += (s, e) => { w.Close(); };
            UIButton btn = new UIButton(w, "hello test", new Font("C:\\Users\\Davide\\Desktop\\cursorspeed\\cursorspeedc#\\ResizingWindow\\ResizingWindow\\dependencies\\font\\Roboto.ttf"));
            btn.Size = new SFML.System.Vector2f(100, 50);
            btn.FillColor = Color.White;
            btn.Position = new SFML.System.Vector2f(150, 150);
            btn.textColor = Color.Red;
            btn.textSize = 16;
            btn.ButtonPressed += Btn_ButtonPressed;
            btn.BorderThickness = 3;
            btn.BorderColor = Color.Blue;

            text_input = new UITextInput(new Font("C:\\Users\\Davide\\Desktop\\cursorspeed\\cursorspeedc#\\ResizingWindow\\ResizingWindow\\dependencies\\font\\Roboto.ttf"), w);
            text_input.Content = "Hello world";
            text_input.Size = new SFML.System.Vector2f(150, 50);
            text_input.Position = new SFML.System.Vector2f(100, 50);
            text_input.BackgroundColor = Color.White;
            text_input.ForegroundColor = Color.Blue;
            text_input.TextAlignment = UITextInput.AlignmentLeft;
            text_input.TextSize = 16;
            text_input.BorderColor = Color.Green;
            text_input.BorderThickness = 2;
            text_input.KeyPressed += Text_input_InsertingText;

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

        private static void Text_input_InsertingText(object? sender, EventArgs e)
        {
            //Console.WriteLine("è stato modificato il testo");
        }

        private static void Btn_ButtonPressed(object? sender, EventArgs e)
        {
            Console.WriteLine("Il contenuto della textbox è: " + text_input.Content);
        }
    }
}