using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MathQuiz
{
    public partial class Form1 : Form
    {
        Random randomizer = new Random();

        // Addition variables
        int addend1;
        int addend2;

        // Subtraction variables
        int minuend;
        int subtrahend;

        // Multiplication variables
        int multiplicand;
        int multiplier;

        // Division variables
        int dividend;
        int divisor;

        // Remaining time
        int timeLeft;


        public Form1()
        {
            InitializeComponent();

            // Display current date
            dateLabel.Text = DateTime.Now.ToString("d MMMM yyyy");
        }

        public void StartTheQuiz()
        {
            // Generate and display the addition problem
            addend1 = randomizer.Next(51);
            addend2 = randomizer.Next(51);
            plusLeftLabel.Text = addend1.ToString();
            plusRightLabel.Text = addend2.ToString();
            sum.Value = 0;

            // Generate and display the subtraction problem
            minuend = randomizer.Next(1, 101);
            subtrahend = randomizer.Next(1, minuend);
            minusLeftLabel.Text = minuend.ToString();
            minusRightLabel.Text = subtrahend.ToString();
            difference.Value = 0;

            // Generate and display the multiplication problem
            multiplicand = randomizer.Next(2, 11);
            multiplier = randomizer.Next(2, 11);
            timesLeftLabel.Text = multiplicand.ToString();
            timesRightLabel.Text = multiplier.ToString();
            product.Value = 0;

            // Generate and display the division problem
            divisor = randomizer.Next(2, 11);
            int temporaryQuotient = randomizer.Next(2, 11);
            dividend = divisor * temporaryQuotient;
            dividedLeftLabel.Text = dividend.ToString();
            dividedRightLabel.Text = divisor.ToString();
            quotient.Value = 0;

            // Start the timer.
            timeLeft = 30;
            timeLabel.Text = "30 seconds";
            timer1.Start();
        }

        private void startButton_Click(object sender, EventArgs e)
        {
            StartTheQuiz();
            startButton.Enabled = false;
        }

        private bool CheckTheAnswer()
        {
            if ((addend1 + addend2 == sum.Value)
                && (minuend - subtrahend == difference.Value)
                && (multiplicand * multiplier == product.Value)
                && (dividend / divisor == quotient.Value))
                return true;
            else
                return false;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (CheckTheAnswer())
            {
                // If All answers were correct, congratulate the player
                timer1.Stop();
                MessageBox.Show("You got all the answers right!",
                                "Congratulations!");
                startButton.Enabled = true;
            }
            else if (timeLeft > 0)
            {
                // If any answers are false, continue counting down
                timeLeft = timeLeft - 1;
                timeLabel.Text = timeLeft + " seconds";

                // Make the time label red during the last 5 seconds of the quiz
                if (timeLeft <= 5)
                {
                    timeLabel.BackColor = Color.Red;
                }
            }
            else
            {
                // If time runs out, end quiz
                timer1.Stop();
                timeLabel.Text = "Time's up!";
                MessageBox.Show("You didn't finish in time.", "Sorry!");
                sum.Value = addend1 + addend2;
                difference.Value = minuend - subtrahend;
                product.Value = multiplicand * multiplier;
                quotient.Value = dividend / divisor;
                startButton.Enabled = true;
                timeLabel.BackColor = Color.White;
            }
        }

        private void answer_Enter(object sender, EventArgs e)
        {
            // Select whole answer in the NumercUpDown control
            NumericUpDown answerBox = sender as NumericUpDown;

            if (answerBox != null)
            {
                int lengthOfAnswer = answerBox.Value.ToString().Length;
                answerBox.Select(0, lengthOfAnswer);
            }

        }

        private void correct_Answer(object sender, EventArgs e)
        {
            SoundPlayer correctAnswerSound = new SoundPlayer(@"C:\Users\johne\OneDrive\Desktop\College Files\Fall 2023\CSE 325 - .Net\Week 2\MathQuiz Assignment\MathQuiz\MathQuiz\Correct Answer.wav");
            correctAnswerSound.Play();
        }

        private void check_Answer_Addition(object sender, EventArgs e)
        {
            // If the correct answer was input, play the correct answer noise
            if (sum.Value == (addend1 + addend2)) 
            { 
                correct_Answer(sender, e); 
            }
        }

        private void check_Answer_Subtraction(object sender, EventArgs e)
        {
            // If the correct answer was input, play the correct answer noise
            if (difference.Value == (minuend - subtrahend))
            {
                correct_Answer(sender, e);
            }
        }

        private void check_Answer_Multiplication(object sender, EventArgs e)
        {
            // If the correct answer was input, play the correct answer noise
            if (product.Value == (multiplicand * multiplier))
            {
                correct_Answer(sender, e);
            }
        }

        private void check_Answer_Division(object sender, EventArgs e)
        {
            // If the correct answer was input, play the correct answer noise
            if (quotient.Value == (dividend / divisor))
            {
                correct_Answer(sender, e);
            }
        }
    }
}
