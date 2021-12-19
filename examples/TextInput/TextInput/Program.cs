using SFML.Graphics;
using SFML.Window;
using SFMLUIComponentsLibrary;
using SFMLUIControls;

namespace TextInput
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
            UITextInput text_input = new UITextInput(f,window);
            text_input.Content = "Hello world";
            text_input.Size = new SFML.System.Vector2f(150, 50);
            text_input.Position = new SFML.System.Vector2f(100, 50);
            text_input.BackgroundColor = Color.Black;
            text_input.ForegroundColor = Color.White;
            text_input.TextAlignment = UITextInput.AlignmentLeft;
            text_input.TextSize = 16;
            text_input.BorderColor = Color.White;
            text_input.BorderThickness = 2;


            while (window.IsOpen)
            {
                window.DispatchEvents();
                window.Clear();

                text_input.draw();
                window.Display();
                Thread.Sleep(33);
            }
        }
    }
}