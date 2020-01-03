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
    public partial class FrmAbout : Form
    {
        /// <summary>
        /// Public instantiation method.
        /// </summary>
        public FrmAbout()
        {
            InitializeComponent();
        }
        
        /// <summary>
        /// MEthod to close the form.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BTN_CLOSE_Click(object sender, EventArgs e)
        {
            Close();
        }
        
        /// <summary>
        /// The Form Load event.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FrmAbout_Load(object sender, EventArgs e)
        {
            TB_README.Text = "Mastermind or Master Mind is a code-breaking game for two players. The modern game with pegs was invented in 1970 by Mordecai Meirowitz, an Israeli postmaster and telecommunications expert. "
                +
                LineBreak()
                +
                "It resembles an earlier pencil and paper game called Bulls and Cows that may date back a century or more."
                +
                LineBreak()
                +
                "This is recreation of the game Mastermind or Master Mind which is a code-breaking game for two players. In this case it is one player vs the computer."
                +
                LineBreak()
                +
                "As the player you will have ten chances to guess the random value of a passcode. The passcode is made up of 4-digits with a range of 1 to 6."
                +
                LineBreak()
                +"After you have entered in your guess, press the proof button and it will compare too see if you have guessed correctly."
                +
                LineBreak()
                +
                "For every number you have right and in a proper position, you will get a PLUS symbol. For every correct number but not in the correct position you will get a MINUS symbol.";
        }

        /// <summary>
        /// This allows a line break and feed.
        /// </summary>
        /// <returns></returns>
        private string LineBreak() { return Environment.NewLine + Environment.NewLine; }
    }
}
