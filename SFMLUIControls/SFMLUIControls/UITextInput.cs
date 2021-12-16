using SFML.Graphics;
using SFML.System;
using SFML.Window;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SFMLUIControls
{
    public class UITextInput
    {
        public Font Font { get; set; }//font utilizzato nella text input
        public Vector2f Size { get; set; }//dimensione dell'input del testo
        public Vector2f Position { get; set; }//posizione dell'input del testo
        public Color BackgroundColor { get; set; }//colore dello sfondo
        public Color ForegroundColor { get; set; }//colore del testo
        public int BorderThickness { get; set; }
        public Color BorderColor { get; set; }
        public string Content { get; set; }//testo contenuto nell'input del testo
        private RenderWindow window;//text input target window
        public int TextAlignment { get; set; }//allineamento del testo all'interno dell'input
        public const int AlignmentLeft = 1;//allineamento del testo a sinistra
        public const int AlignmentCenter = 2;//allineamento del testo in centro
        public const int AlignmentRight = 3;//allineamento del testo a destra
        private Text text;

        public int TextSize { get; set; }

        private bool textInputSelected;


        //Costruttori
        public UITextInput(Font Font, RenderWindow window)
        {
            this.Font = Font;
            this.window = window;
            Size = new Vector2f();
            Position = new Vector2f();
            BackgroundColor = Color.White;
            ForegroundColor = Color.Black;
            BorderThickness = 0;
            BorderColor = Color.Black;
            Content = "";
            TextSize = 12;
            textInputSelected = false;
            window.MouseButtonPressed += MousePressedCallback;
            window.MouseButtonReleased += MouseRealesedCallback;
            window.KeyReleased += KeyPressedCallback;
            text = new Text();
        }

        public UITextInput(Font Font, RenderWindow window, string Content)
        {
            this.Font = Font;
            this.window = window;
            this.Content = Content;
            Size = new Vector2f();
            Position = new Vector2f();
            BackgroundColor = Color.White;
            ForegroundColor = Color.Black;
            BorderThickness = 0;
            BorderColor = Color.Black;
            TextSize = 12;
            textInputSelected = false;
            window.MouseButtonPressed += MousePressedCallback;
            window.MouseButtonReleased += MouseRealesedCallback;
            text = new Text();
            text.DisplayedString = Content;
        }

        //Metodo per disegnare l'input del testo
        public void draw()
        {
            //disegno il container
            RectangleShape container = new RectangleShape(Size);
            container.Position = Position;
            container.FillColor = BackgroundColor;
            container.OutlineColor = BorderColor;
            container.OutlineThickness = BorderThickness;
            window.Draw(container);
            //disegno il testo
            text.Font = Font;
            text.CharacterSize = (uint)TextSize;
            text.FillColor = ForegroundColor;


            if(TextAlignment == AlignmentLeft)
            {
                float content_height = text.GetLocalBounds().Height;
                int content_y = (int)Position.Y + (int)Size.Y / 2 - (int)content_height / 2 - 5 + 2;
                text.Position = new Vector2f(Position.X + 5, content_y);
            }
            else if (TextAlignment == AlignmentCenter)
            {
                float content_width = text.GetLocalBounds().Width;
                float content_height = text.GetLocalBounds().Height;
                int content_x = (int)Position.X + (int)Size.X / 2 - (int)content_width / 2;
                int content_y = (int)Position.Y + (int)Size.Y / 2 - (int)content_height / 2 - 5 + 2;
                text.Position = new Vector2f(content_x, content_y);
            }
            else if (TextAlignment == AlignmentRight)
            {
                float content_width = text.GetLocalBounds().Width;
                float content_height = text.GetLocalBounds().Height;
                int content_x = (int)Position.X + (int)Size.X  - (int)content_width;
                int content_y = (int)Position.Y + (int)Size.Y / 2 - (int)content_height / 2 - 5 + 2;
                text.Position = new Vector2f(content_x - 5, content_y);
            }

            window.Draw(text);
        }

        private bool mousePressed = false;
        private bool ShowTextCursor = false;

        private void MouseRealesedCallback(object sender, SFML.Window.MouseButtonEventArgs e)
        {
            if(mousePressed == true)
            {
                int mouse_x = e.X;
                int mouse_y = e.Y;
                mousePressed = false;
                if (mouse_x >= Position.X && mouse_y >= Position.Y && mouse_x <= Position.X + Size.X && mouse_y <= Position.Y + Size.Y)
                {
                    //mouse premuto e rilasciato nel container del text input
                    textInputSelected = true;
                    Thread TextCursorThread = new Thread(KeyboardCursorAnimation);
                    TextCursorThread.Start();
                }
                else
                {
                    textInputSelected = false;
                }
            }
        }

        private void KeyboardCursorAnimation()
        {
            while (textInputSelected && window.IsOpen)
            {
                ShowTextCursor = !ShowTextCursor;
                if (ShowTextCursor)
                {
                    text.DisplayedString = Content + "|";
                }
                else
                {
                    text.DisplayedString = Content;
                }
                Thread.Sleep(500);
            }
            text.DisplayedString = Content;
        }

        private void MousePressedCallback(object sender, SFML.Window.MouseButtonEventArgs e)
        {
            mousePressed = true;
        }

        private void KeyPressedCallback(object sender, SFML.Window.KeyEventArgs e)
        {
            
            if (textInputSelected)
            {
                if (e.Code == Keyboard.Key.BackSpace)
                {
                    //tasto cancella
                    //rimuovo la lettera all'ultima posizione
                    if (Content.Length > 0)
                    {
                        Content = Content.Remove(Content.Length - 1);
                    }
                }
                else if (e.Code == Keyboard.Key.Space)
                {
                    Content += " ";
                }
                else
                {
                    string letter = e.Code.ToString();

                    //controllo le lettere maiuscole e minuscolo (tasto shift premuto)
                    if (!Keyboard.IsKeyPressed(Keyboard.Key.LShift) || !Keyboard.IsKeyPressed(Keyboard.Key.RShift))
                    {
                        letter = letter.ToLower();
                    }

                    Content += letter;
                    //Console.WriteLine(letter);
                }
            }
        }

    }
}
