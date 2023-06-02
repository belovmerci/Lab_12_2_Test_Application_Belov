using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using static System.Random;

namespace Lab_12_2_Test_Application_Belov
{
    public partial class Form1 : Form
    {
        private int currentQuestion = 1;
        private int totalQuestions = 0;
        private int correctAnswers = 0;
        private string correctAnswer;

        List<string> answerVariants;
        StreamReader sr = new StreamReader("testtext.txt");
        Random rand = new Random();
        Button[] buttons;

        public Form1()
        {
            InitializeComponent();
            buttons = new Button[]{ btnAnswer1, btnAnswer2, btnAnswer3, btnAnswer4 };

            using (var reader = File.OpenText(@"testtext.txt"))
            {
                while (reader.ReadLine() != null) { totalQuestions++; }
            }
            totalQuestions /= 5;

            // Initialize the progress bar
            progressBar.Minimum = 0;
            progressBar.Maximum = totalQuestions;
            progressBar.Value = 0;

            // Display the first question
            ShowQuestion(currentQuestion);
        }

        private void ShowQuestion(int questionNumber)
        {
            // Format (5 lines per question)
            // 1. Question
            // 2. Right answer
            // 3. Wrong answer
            // 4. Wrong answer
            // 5. Wrong answer
            lblQuestionText.Text = sr.ReadLine();
            correctAnswer = sr.ReadLine();
            answerVariants = new List<string>();
            answerVariants.Add(correctAnswer);
            for (int i = 0; i < 3; i++) answerVariants.Add(sr.ReadLine());

            // Display the question and answer variants
            // Ideally randomize for fairer tests
            int indexTemp;
            for (int i = 3; i > -1; i--)
            {
                indexTemp = rand.Next(0, i);
                buttons[i].Text = answerVariants[indexTemp];
                answerVariants.Remove(answerVariants[indexTemp]);
            }
        }

        private void CheckAnswer(Button answerButton)
        {
            // Increase the progress bar value and the correct answer counter
            progressBar.Value++;
            if (answerButton.Text == correctAnswer) correctAnswers++;

            // End test or show next question
            if (currentQuestion != totalQuestions)
            {
                currentQuestion++;
                ShowQuestion(currentQuestion);
            }
            else
            {
                MessageBox.Show($"Вы ответили на {correctAnswers} вопросов из {totalQuestions}");
                Close();
            }
        }

        // Click handlers for the answer buttons
        private void btnAnswer1_Click(object sender, EventArgs e) { CheckAnswer(btnAnswer1); }
        private void btnAnswer2_Click(object sender, EventArgs e) { CheckAnswer(btnAnswer2); }
        private void btnAnswer3_Click(object sender, EventArgs e) { CheckAnswer(btnAnswer3); }
        private void btnAnswer4_Click(object sender, EventArgs e) { CheckAnswer(btnAnswer4); }
        }
    }