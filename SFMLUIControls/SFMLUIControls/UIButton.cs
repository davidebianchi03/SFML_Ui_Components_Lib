using SFML.Graphics;
using SFML.System;
using System;
using System.Threading;

namespace SFMLUIControls
{
    public class UIButton
    {
        public string content { get; set; }//define button content
        public Color textColor { get; set; }//define text color

        public int textSize { get; set; }

        public Vector2f Size { get; set; }

        public  Vector2f Position { get; set; }

        public int TextSize { get; set; }

        public Color FillColor { get; set; }

        private RenderWindow window;

        private int last_time_button_pressed_and_realesed;

        public Font Font { get; set; }

        //constructors
        public UIButton(RenderWindow window, Font Font)
        {
            content = "";
            textColor = Color.Black;
            this.Font = Font;
            Size = new Vector2f(0,0);
            Position = new Vector2f(0, 0);
            TextSize = 0;
            FillColor = Color.White;
            this.window = window;
            last_time_button_pressed_and_realesed = DateTime.Now.Millisecond;
        }



        public UIButton(RenderWindow window, string content, Font Font)
        {
            this.content = content;
            textColor = Color.Black;
            this.Font = Font;
            Size = new Vector2f(0, 0);
            Position = new Vector2f(0, 0);
            TextSize = 0;
            FillColor = Color.White;
            this.window = window;
            last_time_button_pressed_and_realesed = DateTime.Now.Millisecond;
        }

        public UIButton(RenderWindow window,string content,Color textColor,Font Font)
        {
            this.content = content;
            this.textColor = textColor;
            this.Font = Font;
            Size = new Vector2f(0, 0);
            Position = new Vector2f(0, 0);
            TextSize = 0;
            FillColor = Color.White;
            this.window = window;
            last_time_button_pressed_and_realesed = DateTime.Now.Millisecond;
        }

        public void drawButton()
        {
            Text text_to_show = new Text(content, Font);
            text_to_show.CharacterSize = (uint)textSize;
            text_to_show.FillColor = textColor;
            float content_width = text_to_show.GetLocalBounds().Width;
            float content_height = text_to_show.GetLocalBounds().Height;

            int content_x = (int)Position.X + (int)Size.X/2 - (int)content_width / 2;
            int content_y = (int)Position.Y + (int)Size.Y/2 - (int)content_height / 2 - 5;
            text_to_show.Position = new Vector2f(content_x,content_y);

            //text_to_show.Position = Position;

            RectangleShape shape = new RectangleShape();
            shape.Size = Size;
            shape.Position = Position;
            shape.FillColor = FillColor;

            window.Draw(shape);
            window.Draw(text_to_show);
            window.MouseButtonReleased += MouseButtonReleasedCallback;
        }

        

        private void MouseButtonReleasedCallback(object sender, SFML.Window.MouseButtonEventArgs e)
        {
            //controllo se il mouse è stato premuto nello spazio del pulsante e se non è appena stato premuto (l'evento viene richiamato molte volte)
            int mouse_x = e.X;
            int mouse_y = e.Y;
            if(mouse_x >= Position.X && mouse_y >= Position.Y && mouse_x <= Position.X + Size.X && mouse_y <= Position.Y + Size.Y
                && (Math.Abs(DateTime.Now.Millisecond -last_time_button_pressed_and_realesed))>100)
            {
                last_time_button_pressed_and_realesed = DateTime.Now.Millisecond;
                ButtonPressed?.Invoke(this, EventArgs.Empty);
                //faccio partire l'animazione del click
                Thread animation_thread = new Thread(ButtonPressedAnimation);
                animation_thread.Start();
            }
        }

        public event EventHandler ButtonPressed;//evento pulsante premuto

        //animazione pulsante premuto
        private void ButtonPressedAnimation()
        {
            Vector2f original_size = Size;
            Vector2f original_position = Position;
            Vector2f new_size = new Vector2f(original_size.X-4, original_size.Y - 4);
            Vector2f new_position = new Vector2f(original_position.X + 2, original_position.Y +2);
            Size = new_size;
            Position = new_position;
            Thread.Sleep(250);
            Size = original_size;
            Position = original_position;
        }
    }
}
