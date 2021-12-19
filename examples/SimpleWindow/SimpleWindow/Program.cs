using SFML.Window;
using SFMLUIComponentsLibrary;

namespace SimpleWindow
{
    class Program
    {
        public static void Main(string[] args)
        {
            VideoMode mode = new VideoMode(1000, 500);
            UIWindow window = new UIWindow(mode, "Simple window test");
            window.Closed += (obj, e) => { window.Close(); };

            while (window.IsOpen)
            {
                window.DispatchEvents();
                window.Clear();
                window.Display();
            }
        }


    }
}