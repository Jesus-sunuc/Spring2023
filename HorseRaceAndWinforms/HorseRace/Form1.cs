namespace HorseRace
{
    public partial class Form1 : Form
    {
        private const int max = 100;
        private CancellationTokenSource cancel;
        private Task _raceTask;
        public Form1()
        {
            InitializeComponent();
            progressBar1.Maximum = max;
            progressBar2.Maximum = max;
            progressBar3.Maximum = max;
        }

        private async void button1_Click_1(object sender, EventArgs e)
        {
            button1.Enabled = false;
            button2.Enabled = false;
            button1.Text = "Race in progress...";
            ResetProgressBars();
            label4.Text = " ";

            try
            {
                cancel = new CancellationTokenSource();
                _raceTask = RunRace(cancel.Token);
                await _raceTask;
            }
            catch (OperationCanceledException)
            {
                // Race was cancelled
            }
            finally
            {
                button1.Enabled = true;
                button2.Enabled= true;
                button1.Text = "Start Race";
            }
        }

        private async Task RunRace(CancellationToken cancellationToken)
        {
            var winningHorse = -1;
            var winnerFound = false;

            while (!cancellationToken.IsCancellationRequested && !winnerFound)
            {
                // Wait for all horses to be ready
                await Task.Delay(1/1000);

                // Start lottery for each horse
                var horseTasks = new Task[3];
                for (int i = 0; i < 3; i++)
                {
                    var horseIndex = i + 1;
                    horseTasks[i] = Task.Run(() =>
                    {
                        if (new Random().Next() % 1000 == 0)
                        {
                            UpdateProgressBar(horseIndex);

                            if (progressBar1.Value == max && progressBar2.Value == max)
                            {
                                winningHorse = 0;
                                winnerFound = true;
                            }
                            else if (progressBar1.Value == max && progressBar3.Value == max)
                            {
                                winningHorse = 0;
                                winnerFound = true;
                            }
                            else if (progressBar2.Value == max && progressBar3.Value == max)
                            {
                                winningHorse = 0;
                                winnerFound = true;
                            }

                            if (progressBar1.Value == max && progressBar2.Value == max && progressBar3.Value == max)
                            {
                                winningHorse = 0;
                                winnerFound = true;
                            }
                            else if (progressBar1.Value == max)
                            {
                                winningHorse = 1;
                                winnerFound = true;
                            }
                            else if (progressBar2.Value == max)
                            {
                                winningHorse = 2;
                                winnerFound = true;
                            }
                            else if (progressBar3.Value == max)
                            {
                                winningHorse = 3;
                                winnerFound = true;
                            }
                        }
                    });
                }

                // Wait for all lottery tasks to finish
                await Task.WhenAll(horseTasks);
            }

            if (winningHorse == 0)
            {
                label4.Text = ("It's a tie!");
            }
            else
            {
                label4.Text = $"Horse {winningHorse} won the race!";
            }
        }

        private void UpdateProgressBar(int horseIndex)
        {
            switch (horseIndex)
            {
                case 1:
                    progressBar1.Invoke((MethodInvoker)(() =>
                    {
                        progressBar1.Increment(1);
                        label5.Text = $"{progressBar1.Value}%";
                    }));
                    break;
                case 2:
                    progressBar2.Invoke((MethodInvoker)(() =>
                    {
                        progressBar2.Increment(1);
                        label6.Text = $"{progressBar2.Value}%";
                    }));
                    break;
                case 3:
                    progressBar3.Invoke((MethodInvoker)(() =>
                    {
                        progressBar3.Increment(1);
                        label7.Text = $"{progressBar3.Value}%";
                    }));
                    break;
            }
        }

        private void ResetProgressBars()
        {
            progressBar1.Value = 0;
            progressBar2.Value = 0;
            progressBar3.Value = 0;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            cancel?.Cancel();
            _raceTask?.Wait();
            ResetProgressBars();
            label4.Text = " ";
            label5.Text = "0%";
            label6.Text = "0%";
            label7.Text = "0%";
        }
    }
}