using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace mastermind
{
    public partial class FrmGame : Form
    {
        // Random Number Generator
        private static Random RNG;
        // The Count of guesses which is ZERO based index.
        private int Count { get; set; } = 0;
        // Contain a minus character for each correct number but not in correct position.
        private string minuses = string.Empty;
        // Contain a plus character for each correct number that is also in correct position.
        private string plusses = string.Empty;
        // Simple Label array for the letters A, B, C and D.
        private Label[] OrderColumn;
        // Simple TextBox array for the answer column.
        private TextBox[] AnswerColumn;
        // Simple int array that holds the answer.
        private int[] Answers;
        // This is a List object that will contain the Class Guess.
        private List<Guess> Guesses;

        public FrmGame()
        {
            InitializeComponent();

            Width = 640;

            Height = 360;

            RNG = new Random(DateTime.Now.Millisecond);

            OrderColumn = new Label[4] {
                new Label() { Location=new Point(5,60), Name="LBL_A", Text = "A" }
                ,
                new Label() { Location=new Point(5,90), Name="LBL_B", Text = "B" }
                ,
                new Label() { Location=new Point(5,120), Name="LBL_C", Text = "C" }
                ,
                new Label() { Location=new Point(5,150), Name="LBL_D", Text = "D" }
            };

            AnswerColumn = new TextBox[4] {
                new TextBox() { Location = new Point(540, 60),
                Name = "Answer_0",
                ReadOnly=true,
                TabIndex=99,
                TextAlign = HorizontalAlignment.Center,
                Width = 48 }
            ,
                new TextBox() { Location = new Point(540, 90),
                Name = "Answer_1",
                ReadOnly=true,
                TabIndex=100,
                TextAlign = HorizontalAlignment.Center,
                Width = 48 }
            ,
                new TextBox() { Location = new Point(540, 120),
                Name = "Answer_2",
                ReadOnly=true,
                TabIndex=101,
                TextAlign = HorizontalAlignment.Center,
                Width = 48 }
            ,
                new TextBox() { Location = new Point(540, 150),
                Name = "Answer_3",
                ReadOnly=true,
                TabIndex=102,
                TextAlign = HorizontalAlignment.Center,
                Width = 48 }
            };
        }
        private void FrmGame_Load(object sender, EventArgs e)
        {
            SetupGame();
        }
        private void NewGameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetupGame();
        }
        private void ExitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        private void SetupGame()
        {
            Controls.Clear();

            SetupMenu();

            Answers = new int[4] { RandomNumber(), RandomNumber(), RandomNumber(), RandomNumber() };

            Count = 0;

            Guesses = null;

            Guesses = new List<Guess>();

            for (int i = 0; i < 10; i++)
            {
                Guesses.Add(new Guess(i));
            }

            for (int i = 0; i < Guesses.Count; i++)
            {
                Controls.Add(Guesses[i].Results);
                for (int t = 0; t < Guesses[i].PlayerGuesses.Count; t++)
                {
                    Controls.Add(Guesses[i].PlayerGuesses[t]);
                }
            }

            for (int i = 0; i < AnswerColumn.Length; i++)
                Controls.Add(AnswerColumn[i]);

            for (int i = 0; i < Answers.Length; i++)
                AnswerColumn[i].Text = string.Empty;

#if DEBUG
            //for (int i = 0; i < Answers.Length; i++)
            //AnswerColumn[i].Text = Answers[i].ToString();
#endif

            for (int i = 0; i < OrderColumn.Length; i++)
                Controls.Add(OrderColumn[i]);

            Button proveMeWrong = new Button() { Location = new Point(20, 250), Name = "BTN_PROOF", Text = "Proof" };
            proveMeWrong.Click += new EventHandler(Proof);
            Controls.Add(proveMeWrong);

            Button exitButton = new Button() { Location = new Point(520,280), Name="BTN_EXIT", Text = "Exit" };
            exitButton.Click += new EventHandler(Exit);
            Controls.Add(exitButton);

            DisableAll();

            EnableGuess();

            SetFocus();
        }
        private void Proof(object sender, EventArgs e)
        {
            string results = string.Empty;

            int blank = 0;

            for (int i = 0; i < 4; i++)
            {
                if (string.IsNullOrEmpty(Guesses[Count].PlayerGuesses[i].Text))
                {
                    blank++;
                }
            }
            if (blank > 0) { MessageBox.Show("Please fill in the current attempt!", "Invalid"); SetFocus(); return; }

            // Valid Number and Correct Position returns minuses and plusses.
            results = ValidateGuess();

            if (results == "++++")
            {
                RevealAnswer();
                Winner();
                DisableAll();
                Guesses[Count].Results.Text = string.Empty;
                Button btn = (Button)Controls["BTN_PROOF"];
                btn.Enabled = false;
                Button exit = (Button)Controls["BTN_EXIT"];
                exit.Focus();
                results = string.Empty;
                return;
            }

            Guesses[Count].Results.Enabled = true;
            Guesses[Count].Results.Text = results;
            Guesses[Count].Results.Enabled = false;

            Count++;

            if (Count == 10)
            {
                RevealAnswer();
                Loser();
                DisableAll();
                Button btn = (Button)Controls["BTN_PROOF"];
                btn.Enabled = false;
                return;
            }

            DisableGuess();

            EnableGuess();

            SetFocus();
        }
        private static int RandomNumber()
        {
            return RNG.Next(1, 6);
        }
        private void SetupMenu()
        {
            #region Menu Strip

            MenuStrip MainMenu = new MenuStrip
            {
                Name = "MainMenu",
                Text = "File Menu",
                Font = new Font("Georgia", 12),
                Dock = DockStyle.None
            };
            MainMenuStrip = MainMenu;
            Controls.Add(MainMenuStrip);

            ToolStripMenuItem FileMenuOption = new ToolStripMenuItem()
            {
                Text = "File",
                Font = new Font("Georgia", 12)
            };
            // There is no Click Event for FileMenuOption
            // NO CLICK EVENT

            // Create New Menu Item NewGame
            ToolStripMenuItem NewGameMenuOption = new ToolStripMenuItem()
            {
                Text = "New Game",
                Font = new Font("Georgia", 12),
                ToolTipText = "Begin a new game of Mastermind."
            };
            // Click Event for NewGameMenuOption
            NewGameMenuOption.Click += new EventHandler(NewGameToolStripMenuItem_Click);

            // Create New Menu Item About
            ToolStripMenuItem AboutMenuOption = new ToolStripMenuItem()
            {
                Text = "About",
                Font = new Font("Georgia", 12),
                ToolTipText = "Click to read the about for the game Mastermind."
            };

            AboutMenuOption.Click += new EventHandler(ShowAbout);

            // Create New Menu Item ExitGame
            ToolStripMenuItem ExitMenuOption = new ToolStripMenuItem()
            {
                Text = "Exit",
                Font = new Font("Georgia", 12),
                ToolTipText = "Close the game and exit back to desktop."
            };
            // Click Event for ExitMenuOption
            ExitMenuOption.Click += new EventHandler(ExitToolStripMenuItem_Click);

            #endregion

            // Add menu options to the main menu.
            FileMenuOption.DropDownItems.Add(NewGameMenuOption);
            FileMenuOption.DropDownItems.Add(AboutMenuOption);
            FileMenuOption.DropDownItems.Add(ExitMenuOption);
            MainMenu.Items.Add(FileMenuOption);
        }
        private void ShowAbout(object sender, EventArgs e)
        {
            FrmAbout fa = new FrmAbout();
            fa.StartPosition = FormStartPosition.CenterParent;
            fa.ShowDialog();
        }
        private void DisableAll()
        {
            for (int i = 0; i < Guesses.Count; i++)
            {
                for (int t = 0; t < Guesses[i].PlayerGuesses.Count; t++)
                    Guesses[i].PlayerGuesses[t].Enabled = false;
            }
        }
        private void EnableGuess()
        {
            for (int i = 0; i < 4; i++)
                Guesses[Count].PlayerGuesses[i].Enabled = true;
        }
        private void DisableGuess()
        {
            for (int i = 0; i < 4; i++)
                Guesses[(Count - 1) < 0 ? 0 : (Count - 1)].PlayerGuesses[i].Enabled = false;
        }
        private void Winner()
        {
            MessageBox.Show("You guessed it in " + (Count + 1) + " " + ((Count + 1) == 1 ? "attempt" : "attempts") + "!", "Your Awesome");
        }
        private void Loser()
        {
            MessageBox.Show("You failed to guess it in 10 attempts!", "So Sad");
        }
        private void RevealAnswer()
        {
            for (int i = 0; i < AnswerColumn.Length; i++)
                AnswerColumn[i].Text = Answers[i].ToString();
        }
        private void SetFocus()
        {
            bool set = false;
            for (int i = 0; i < 4; i++)
            {
                if (string.IsNullOrEmpty(Guesses[Count].PlayerGuesses[i].Text))
                {
                    if (!set)
                    {
                        Guesses[Count].PlayerGuesses[i].Focus();
                        set = true;
                    }
                }
            }
        }
        private string ValidateGuess()
        {
            plusses = string.Empty;
            minuses = string.Empty;

            for (int i = 0; i < 4; i++)
            {
                if(ValidNumber(Convert.ToInt32(Guesses[Count].PlayerGuesses[i].Text)))
                    if (Convert.ToInt32(Guesses[Count].PlayerGuesses[i].Text) == Answers[i])
                    {
                        plusses += "+";
                    }
                    else
                    {
                        minuses += "-";
                    }
            }

            return minuses + plusses;
        }
        private bool ValidNumber(int number)
        {
            bool result = false;
            for (int i = 0; i < 4; i++)
            {
                if (number == Answers[i])
                    result = true;
            }            
            return result;
        }
        private void Exit(object sender, EventArgs e) { Application.Exit(); }
    }
    public class Guess
    {
        public List<TextBox> PlayerGuesses = new List<TextBox>();
        public TextBox Results;
        public Guess(int count)
        {
            PlayerGuesses.Add(new TextBox()
            {
                BorderStyle = BorderStyle.FixedSingle,
                Location = new Point(20 + (count * 50), 60),
                MaxLength = 1,
                Name = count + "_0",
                TextAlign = HorizontalAlignment.Center,
                TabIndex = 0 + (count * 5),
                Width = 48
            });
            PlayerGuesses.Add(new TextBox()
            {
                BorderStyle = BorderStyle.FixedSingle,
                Location = new Point(20 + (count * 50), 90),
                MaxLength = 1,
                Name = count + "_1",
                TextAlign = HorizontalAlignment.Center,
                TabIndex = 1 + (count * 5),
                Width = 48
            });
            PlayerGuesses.Add(new TextBox()
            {
                BorderStyle = BorderStyle.FixedSingle,
                Location = new Point(20 + (count * 50), 120),
                MaxLength = 1,
                Name = count + "_2",
                TextAlign = HorizontalAlignment.Center,
                TabIndex = 2 + (count * 5),
                Width = 48
            });
            PlayerGuesses.Add(new TextBox()
            {
                BorderStyle= BorderStyle.FixedSingle,
                Location = new Point(20 + (count * 50), 150),
                MaxLength = 1,
                Name = count + "_3",
                TextAlign = HorizontalAlignment.Center,
                TabIndex = 3 + (count * 5),
                Width = 48
            });

            Results = new TextBox() { BorderStyle = BorderStyle.FixedSingle, Enabled = false, Font = new Font("Courier", 16f, FontStyle.Bold), Height = 30, Location = new Point(20 + (count * 50), 190), Multiline = true, Name = "TB_Results_" + count, ReadOnly = true, TabIndex = 4 + (count * 5), TextAlign = HorizontalAlignment.Center, Width = 48 };

            PlayerGuesses[0].TextChanged += new EventHandler(ValidateEntry);
            PlayerGuesses[1].TextChanged += new EventHandler(ValidateEntry);
            PlayerGuesses[2].TextChanged += new EventHandler(ValidateEntry);
            PlayerGuesses[3].TextChanged += new EventHandler(ValidateEntry);
        }
        private void ValidateEntry(object sender, EventArgs e)
        {
            TextBox tb = (TextBox)sender;
            if (System.Text.RegularExpressions.Regex.IsMatch(tb.Text, "[^1-6]"))
            {
                tb.Text = tb.Text.Remove(tb.Text.Length - 1);
            }
        }
    }
}
