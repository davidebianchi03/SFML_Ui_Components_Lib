using SFML.Graphics;
using SFML.Window;
using SFMLUIComponentsLibrary;
using SFMLUIControls;

namespace Button
{
    class Program
    {
        public static void Main(string[] args)
        {
            VideoMode mode = new VideoMode(1000, 500);
            UIWindow window = new UIWindow(mode, "Simple window test");
            window.Closed += (obj, e) => { window.Close(); };
            string font_path = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.ToString() + "\\OpenSans.ttf";
            Font f = new Font(font_path);
            UIButton example_button = new UIButton(window, f);
            example_button.content = "Press me";
            example_button.textSize = 16;
            example_button.BorderColor = Color.White;
            example_button.textColor = Color.White;
            example_button.BorderThickness = 2;
            example_button.FillColor = Color.Black;
            example_button.Size = new SFML.System.Vector2f(250, 50);
            example_button.Position = new SFML.System.Vector2f(100, 100);
            example_button.ButtonPressed += ButtonPressedCallBack;

            while (window.IsOpen)
            {
                window.DispatchEvents();
                window.Clear();

                example_button.draw();
                window.Display();
            }
        }

        private static void ButtonPressedCallBack(object? sender, EventArgs e)
        {
            Console.WriteLine("Button has been pressed");
        }
    }
}