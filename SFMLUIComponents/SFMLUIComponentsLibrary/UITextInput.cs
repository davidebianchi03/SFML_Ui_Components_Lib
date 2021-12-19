using SFML.Graphics;
using SFML.System;
using SFML.Window;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

/*
 IMPORTANTE !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
    Questa classe funziona solo con tastiera con layout italiano
 */

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
        private int cursorPosition;
        public bool Enable { get; set; } = true;


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
            cursorPosition = 0;
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
            cursorPosition = Content.Length;
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

            string newContent = Content;



            /*if (ShowTextCursor)
            {
                newContent = Content.Insert(cursorPosition, "|");
            }
            else
            {
                newContent = Content;
            }*/

            text.DisplayedString = "";
            int textMaximumSize = 0;
            int charWidth = 0;
            while (text.GetGlobalBounds().Width < Size.X - 20)
            {
                text.DisplayedString += "A";
                textMaximumSize++;
                if (textMaximumSize == 1)
                {
                    charWidth = (int)text.GetLocalBounds().Width;
                }
            }

            int current_cursor_position = 0;
            text.DisplayedString = "";

            if (textMaximumSize >= newContent.Length)
            {
                textMaximumSize = newContent.Length;
            }

            if (cursorPosition < textMaximumSize)
            {
                text.DisplayedString = newContent.Substring(0, textMaximumSize);
                current_cursor_position = cursorPosition - 1;
                
            }
            else
            {
                try
                {
                    text.DisplayedString = newContent.Substring(cursorPosition - textMaximumSize, textMaximumSize + 1);
                }
                catch { text.DisplayedString = newContent.Substring(cursorPosition - textMaximumSize, textMaximumSize); }
                current_cursor_position = text.DisplayedString.Length - 1;
            }


            int textEndX = 0;
            int content_y = 0;
            if (TextAlignment == AlignmentLeft)
            {
                float content_height = text.GetLocalBounds().Height;
                content_y = (int)Position.Y + (int)Size.Y / 2 - (int)content_height / 2 - 5 + 2;
                text.Position = new Vector2f(Position.X + 5, content_y);
                textEndX = (int)Position.X - 5 + (int)text.GetGlobalBounds().Width - 5;
            }
            else if (TextAlignment == AlignmentCenter)
            {
                float content_width = text.GetLocalBounds().Width;
                float content_height = text.GetLocalBounds().Height;
                int content_x = (int)Position.X + (int)Size.X / 2 - (int)content_width / 2;
                content_y = (int)Position.Y + (int)Size.Y / 2 - (int)content_height / 2 - 5 + 2;
                text.Position = new Vector2f(content_x, content_y);
                textEndX = (int)Position.X + (int)content_width / 2;
            }
            else if (TextAlignment == AlignmentRight)
            {
                float content_width = text.GetLocalBounds().Width;
                float content_height = text.GetLocalBounds().Height;
                int content_x = (int)Position.X + (int)Size.X - (int)content_width;
                content_y = (int)Position.Y + (int)Size.Y / 2 - (int)content_height / 2 - 5 + 2;
                text.Position = new Vector2f(content_x - 5, content_y);
                textEndX = (int)Position.X + (int)content_width;
            }
            //draw cursor line
            string current_content = text.DisplayedString;
            text.DisplayedString = "";
            float cursor_x = 0;

            for (int i = 0; i < current_content.Length; i++)
            {
                text.DisplayedString += current_content[i];
                if (i == current_cursor_position)
                {//
                    cursor_x = text.GetGlobalBounds().Left + text.GetGlobalBounds().Width;
                }
            }

            if (ShowTextCursor)
            {
                //Draw cursor line
                RectangleShape cursorLine = new RectangleShape(new Vector2f(2, TextSize + 5));
                cursorLine.Position = new Vector2f(cursor_x, content_y);
                cursorLine.FillColor = ForegroundColor;
                window.Draw(cursorLine);
            }

            window.Draw(text);
        }

        private bool mousePressed = false;
        private bool ShowTextCursor = false;

        private void MouseRealesedCallback(object sender, SFML.Window.MouseButtonEventArgs e)
        {
            if (mousePressed == true)
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
                    cursorPosition = Content.Length;
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
                Thread.Sleep(750);
            }
            ShowTextCursor = false;
            text.DisplayedString = Content;
        }

        private void MousePressedCallback(object sender, SFML.Window.MouseButtonEventArgs e)
        {
            mousePressed = true;
        }

        private void KeyPressedCallback(object sender, SFML.Window.KeyEventArgs e)
        {
            //mapping di tutti i tasti della tastiera
            if (textInputSelected && Enable)
            {
                if (e.Code == Keyboard.Key.Backspace)
                {
                    //tasto cancella
                    //rimuovo la lettera all'ultima posizione
                    if (Content.Length > 0 && cursorPosition > 0)
                    {
                        Content = Content.Remove(cursorPosition - 1, 1);
                        cursorPosition--;
                    }
                }
                else if (e.Code == Keyboard.Key.Left)
                {
                    if (cursorPosition > 0)
                        cursorPosition--;
                }
                else if (e.Code == Keyboard.Key.Right)
                {
                    if (cursorPosition < Content.Length)
                        cursorPosition++;
                }
                else if (e.Code == Keyboard.Key.Enter)
                {
                    Content = Content.Insert(cursorPosition, "\n");
                    cursorPosition++;
                }
                else if (e.Code == Keyboard.Key.Space)
                {
                    Content = Content.Insert(cursorPosition, " ");
                    cursorPosition++;
                }
                //do nothing keys
                else if (e.Code == Keyboard.Key.Escape ||
                    e.Code == Keyboard.Key.LShift ||
                    e.Code == Keyboard.Key.RShift ||
                    e.Code == Keyboard.Key.LControl ||
                    e.Code == Keyboard.Key.RControl ||
                    e.Code == Keyboard.Key.LAlt ||
                    e.Code == Keyboard.Key.RAlt ||
                    e.Code == Keyboard.Key.LSystem ||
                    e.Code == Keyboard.Key.RSystem ||
                    e.Code == Keyboard.Key.Unknown
                    ) { }
                else if (e.Code == Keyboard.Key.Period)
                {
                    if (!Keyboard.IsKeyPressed(Keyboard.Key.LShift) && !Keyboard.IsKeyPressed(Keyboard.Key.RShift))
                    {
                        Content = Content.Insert(cursorPosition, ".");
                    }
                    else
                    {
                        Content = Content.Insert(cursorPosition, ":");
                    }
                    cursorPosition++;
                }
                else if (e.Code == Keyboard.Key.Backslash)
                {
                    if (!Keyboard.IsKeyPressed(Keyboard.Key.LShift) && !Keyboard.IsKeyPressed(Keyboard.Key.RShift))
                    {
                        Content = Content.Insert(cursorPosition, "\\");
                    }
                    else
                    {
                        Content = Content.Insert(cursorPosition, "|");
                    }
                    cursorPosition++;
                }
                else if (e.Code == Keyboard.Key.Comma)
                {
                    if (!Keyboard.IsKeyPressed(Keyboard.Key.LShift) && !Keyboard.IsKeyPressed(Keyboard.Key.RShift))
                    {
                        Content = Content.Insert(cursorPosition, ",");
                    }
                    else
                    {
                        Content = Content.Insert(cursorPosition, ";");
                    }
                    cursorPosition++;
                }
                else if (e.Code == Keyboard.Key.Dash)
                {
                    if (!Keyboard.IsKeyPressed(Keyboard.Key.LShift) && !Keyboard.IsKeyPressed(Keyboard.Key.RShift))
                    {
                        Content = Content.Insert(cursorPosition, "-");
                    }
                    else
                    {
                        Content = Content.Insert(cursorPosition, "_");
                    }
                    cursorPosition++;
                }
                else if (e.Code == Keyboard.Key.Delete)
                {
                    if (cursorPosition < Content.Length)
                    {
                        Content = Content.Remove(cursorPosition, 1);
                    }
                }
                else if ((int)e.Code >= 0 && (int)e.Code <= 25)//controllo se il tasto è una lettera
                {
                    string letter = e.Code.ToString();

                    //controllo le lettere maiuscole e minuscolo (tasto shift premuto)
                    if (!Keyboard.IsKeyPressed(Keyboard.Key.LShift) && !Keyboard.IsKeyPressed(Keyboard.Key.RShift))
                    {
                        letter = letter.ToLower();
                    }
                    Content = Content.Insert(cursorPosition, letter);
                    cursorPosition++;
                }
                else if ((int)e.Code >= 26 && (int)e.Code <= 35)//controllo se il tasto è un numero
                {
                    if (!Keyboard.IsKeyPressed(Keyboard.Key.LShift) && !Keyboard.IsKeyPressed(Keyboard.Key.RShift))
                    {
                        int number = (int)e.Code - 26;
                        Content = Content.Insert(cursorPosition, number.ToString());
                        cursorPosition++;
                    }
                    else
                    {
                        int number = (int)e.Code - 26;
                        switch (number)
                        {
                            case 0:
                                Content = Content.Insert(cursorPosition, "=");
                                break;

                            case 1:
                                Content = Content.Insert(cursorPosition, "!");
                                break;

                            case 2:
                                Content = Content.Insert(cursorPosition, "\"");
                                break;

                            case 3:
                                Content = Content.Insert(cursorPosition, "£");
                                break;

                            case 4:
                                Content = Content.Insert(cursorPosition, "$");
                                break;

                            case 5:
                                Content = Content.Insert(cursorPosition, "%");
                                break;

                            case 6:
                                Content = Content.Insert(cursorPosition, "&");
                                break;

                            case 7:
                                Content = Content.Insert(cursorPosition, "/");
                                break;

                            case 8:
                                Content = Content.Insert(cursorPosition, "(");
                                break;

                            case 9:
                                Content = Content.Insert(cursorPosition, ")");
                                break;
                        }
                        cursorPosition++;
                    }

                }
                else if (e.Code == Keyboard.Key.Tab)
                {
                    if (!Keyboard.IsKeyPressed(Keyboard.Key.LShift) && !Keyboard.IsKeyPressed(Keyboard.Key.RShift))
                    {
                        Content = Content.Insert(cursorPosition, "\t");
                        cursorPosition++;
                    }
                    else
                    {
                        //nella tabella ascii non esiste la tabulazione inversa
                        Console.Beep(1000, 250);
                    }

                }
                else if ((int)e.Code >= 75 && (int)e.Code <= 84)
                {
                    if (!Keyboard.IsKeyPressed(Keyboard.Key.LShift) && !Keyboard.IsKeyPressed(Keyboard.Key.RShift))
                    {
                        int number = (int)e.Code - 75;
                        Content = Content.Insert(cursorPosition, number.ToString());
                        cursorPosition++;
                    }
                    else
                    {
                        Console.Beep(1000, 250);
                    }
                }
                else if (e.Code == Keyboard.Key.Add)
                {
                    Content = Content.Insert(cursorPosition, "+");
                    cursorPosition++;
                }
                else if (e.Code == Keyboard.Key.Subtract)
                {
                    Content = Content.Insert(cursorPosition, "-");
                    cursorPosition++;
                }
                else if (e.Code == Keyboard.Key.Divide)
                {
                    Content = Content.Insert(cursorPosition, "/");
                    cursorPosition++;
                }
                else if (e.Code == Keyboard.Key.Multiply)
                {
                    Content = Content.Insert(cursorPosition, "*");
                    cursorPosition++;
                }
                else if ((int)e.Code == 48)
                {
                    if (!Keyboard.IsKeyPressed(Keyboard.Key.LShift) && !Keyboard.IsKeyPressed(Keyboard.Key.RShift) && !Keyboard.IsKeyPressed(Keyboard.Key.RAlt))
                    {
                        Content = Content.Insert(cursorPosition, "è");
                    }
                    else if (!Keyboard.IsKeyPressed(Keyboard.Key.LShift) && !Keyboard.IsKeyPressed(Keyboard.Key.RShift) && Keyboard.IsKeyPressed(Keyboard.Key.RAlt))
                    {
                        Content = Content.Insert(cursorPosition, "[");
                    }
                    else
                    {
                        Content = Content.Insert(cursorPosition, "é");
                    }
                    cursorPosition++;
                }
                else if ((int)e.Code == 55)
                {
                    if (!Keyboard.IsKeyPressed(Keyboard.Key.LShift) && !Keyboard.IsKeyPressed(Keyboard.Key.RShift) && !Keyboard.IsKeyPressed(Keyboard.Key.RAlt))
                    {
                        Content = Content.Insert(cursorPosition, "+");
                    }
                    else if (!Keyboard.IsKeyPressed(Keyboard.Key.LShift) && !Keyboard.IsKeyPressed(Keyboard.Key.RShift) && Keyboard.IsKeyPressed(Keyboard.Key.RAlt))
                    {
                        Content = Content.Insert(cursorPosition, "]");
                    }
                    else
                    {
                        Content = Content.Insert(cursorPosition, "*");
                    }
                    cursorPosition++;
                }
                else if ((int)e.Code == 54)
                {
                    if (!Keyboard.IsKeyPressed(Keyboard.Key.LShift) && !Keyboard.IsKeyPressed(Keyboard.Key.RShift) && !Keyboard.IsKeyPressed(Keyboard.Key.RAlt))
                    {
                        Content = Content.Insert(cursorPosition, "ò");
                    }
                    else if (!Keyboard.IsKeyPressed(Keyboard.Key.LShift) && !Keyboard.IsKeyPressed(Keyboard.Key.RShift) && Keyboard.IsKeyPressed(Keyboard.Key.RAlt))
                    {
                        Content = Content.Insert(cursorPosition, "@");
                    }
                    else
                    {
                        Content = Content.Insert(cursorPosition, "ç");
                    }
                    cursorPosition++;
                }
                else if ((int)e.Code == 51)
                {
                    if (!Keyboard.IsKeyPressed(Keyboard.Key.LShift) && !Keyboard.IsKeyPressed(Keyboard.Key.RShift) && !Keyboard.IsKeyPressed(Keyboard.Key.RAlt))
                    {
                        Content = Content.Insert(cursorPosition, "à");
                    }
                    else if (!Keyboard.IsKeyPressed(Keyboard.Key.LShift) && !Keyboard.IsKeyPressed(Keyboard.Key.RShift) && Keyboard.IsKeyPressed(Keyboard.Key.RAlt))
                    {
                        Content = Content.Insert(cursorPosition, "#");
                    }
                    else
                    {
                        Content = Content.Insert(cursorPosition, "°");
                    }
                    cursorPosition++;
                }
                else if ((int)e.Code == 52)
                {
                    if (!Keyboard.IsKeyPressed(Keyboard.Key.LShift) && !Keyboard.IsKeyPressed(Keyboard.Key.RShift))
                    {
                        Content = Content.Insert(cursorPosition, "ù");
                    }
                    else
                    {
                        Content = Content.Insert(cursorPosition, "§");
                    }
                    cursorPosition++;
                }
                else if ((int)e.Code == 46)
                {
                    if (!Keyboard.IsKeyPressed(Keyboard.Key.LShift) && !Keyboard.IsKeyPressed(Keyboard.Key.RShift))
                    {
                        Content = Content.Insert(cursorPosition, "'");
                    }
                    else
                    {
                        Content = Content.Insert(cursorPosition, "?");
                    }
                    cursorPosition++;
                }
                else if ((int)e.Code == 47)
                {
                    if (!Keyboard.IsKeyPressed(Keyboard.Key.LShift) && !Keyboard.IsKeyPressed(Keyboard.Key.RShift))
                    {
                        Content = Content.Insert(cursorPosition, "ì");
                    }
                    else
                    {
                        Content = Content.Insert(cursorPosition, "^");
                    }
                    cursorPosition++;
                }
                else
                {
                    Console.Beep(1000, 250);
                }
                KeyPressed?.Invoke(this, EventArgs.Empty);//richiamo l'evento    
            }

            //Console.WriteLine((int)e.Code);
        }

        public event EventHandler KeyPressed;

    }
}
