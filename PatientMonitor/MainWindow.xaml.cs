using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;


using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.DataVisualization.Charting;
using System.Windows.Controls.DataVisualization.Charting.Compatible;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;


namespace PatientMonitor
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ObservableCollection<KeyValuePair<int, double>> dataPoints;
        private DispatcherTimer timer;
        private int index = 0;
        Patient patient;
        MonitorConstants.Parameter parameter = MonitorConstants.Parameter.ECG;

        public MainWindow()
        {
            InitializeComponent();
            sliderECG.Value = 0;
            ComboBoxHarmonics.Text = "1";
            TextBoxFrequency.Text = " ";


            dataPoints = new ObservableCollection<KeyValuePair<int, double>>();
            lineSeriesECG.ItemsSource = dataPoints;
            patient = new Patient(100, (double)1.0, 1, "John Doe", DateTime.Now, 1);

            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(0.025); // Set timer to tick every second
            timer.Tick += Timer_Tick;

        }
        private void Timer_Tick(object sender, EventArgs e)
        {
            // Generate a new data point
            dataPoints.Add(new KeyValuePair<int, double>(index++, patient.NextSample(index, parameter)));

            // Optional: Remove old points to keep the chart clean
            if (dataPoints.Count > 200) // Maximum number of points
            {
                dataPoints.RemoveAt(0); // Remove the oldest point
            }
        }

        private void Slider_IsEnabledChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            Slider slider = sender as Slider;
            if (slider.IsEnabled) slider.ValueChanged += sliderECG_ValueChanged;
            else slider.ValueChanged -= sliderECG_ValueChanged;
        }

        private void sliderECG_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            switch (parameter)
            {

                case (MonitorConstants.Parameter.ECG):
                    patient.ECGAmplitude = sliderECG.Value;
                    break;
                case (MonitorConstants.Parameter.EMG):
                    patient.EMGAmplitude = sliderECG.Value;
                    break;
                case (MonitorConstants.Parameter.EEG):
                    patient.EEGAmplitude = sliderECG.Value;
                    break;
                case (MonitorConstants.Parameter.Resp):
                    patient.RespAmplitude = sliderECG.Value;
                    break;
                default:
                    break;


            }

        }





        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }



        private void ButtonUpdatePatient_Click(object sender, RoutedEventArgs e)
        {

        }


        private void TextBoxFrequency_TextChanged(object sender, TextChangedEventArgs e)
        {

        }



        private void TextBoxName_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void textBoxPatientName_TextChanged(object sender, TextChangedEventArgs e)
        {
            // textBoxPatientName.Text = "";
        }

        private void textBoxPatientName_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = int.TryParse(e.Text, out _);
        }

        private void textBoxPatientAge_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {

            e.Handled = !int.TryParse(e.Text, out int result);
        }

        private void datePickerMonitor_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            // Überprüfen, ob ein gültiges Datum ausgewählt wurde
            if (!datePickerMonitor.SelectedDate.HasValue)
            {
                // Zeigt eine Warnung, wenn kein Datum ausgewählt ist oder ein ungültiges Datum eingegeben wurde
                MessageBox.Show("Please select a valid date!", "Invalid Date", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else
            {
                // Optional: Bestätigung, dass das Datum korrekt ist
                DateTime selectedDate = datePickerMonitor.SelectedDate.Value;
                // Hier kann weitere Logik hinzugefügt werden, z.B. das gewählte Datum verwenden
            }
        }

        private void datePickerMonitor_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {

        }





        private void textBoxPatientName_GotFocus(object sender, RoutedEventArgs e)
        {
            textBoxPatientName.Text = "";
        }

        private void ComboBoxHarmonics_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (patient != null && ComboBoxHarmonics.SelectedIndex >= 0)

            {
                patient.ECGHarmonics = ComboBoxHarmonics.SelectedIndex + 1;
            }
        }

        private void textBoxPatientAge_GotFocus(object sender, RoutedEventArgs e)
        {
            textBoxPatientAge.Text = " ";
        }

        private void buttonStartParameter_Click(object sender, RoutedEventArgs e)
        {
            sliderECG.IsEnabled = true;
            ComboBoxHarmonics.IsEnabled = true;
            TextBoxFrequency.IsEnabled = true;
            comboBoxParameters.IsEnabled = true;
            timer.Start();

        }

        private void buttonQuit_Click(object sender, RoutedEventArgs e)
        {
            timer.Stop();
        }

        private void buttonCreatePatient_Click(object sender, RoutedEventArgs e)
        {
            if (textBoxPatientName.Text == "Enter Name here" || string.IsNullOrWhiteSpace(textBoxPatientName.Text))
            {
                MessageBox.Show("Please enter a name!");
                return;
            }

            if (textBoxPatientAge.Text == "Enter Age here" || !int.TryParse(textBoxPatientAge.Text, out int age))
            {
                MessageBox.Show("Please enter a valid age!");
                return;
            }

            if (!datePickerMonitor.SelectedDate.HasValue)
            {
                MessageBox.Show("Please select a date!");
                return;
            }

            double frequency = 1;

            if (!int.TryParse(ComboBoxHarmonics.Text, out int harmonics))
            {
                MessageBox.Show("Please select a valid harmonics value.");
                return;
            }

            buttonStartParameter.IsEnabled = true;
            ButtonUpdatePatient.IsEnabled = true;

            patient = new Patient(sliderECG.Value, frequency, harmonics, textBoxPatientName.Text, datePickerMonitor.SelectedDate.Value, age);
        }




        private void TextBoxFrequency_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                if (patient != null)
                {
                    // Überprüfen, ob der Text in 'textBoxFrequency' in eine Zahl konvertiert werden kann
                    if (int.TryParse(TextBoxFrequency.Text, out int parameterFreq))
                    {
                        // Begrenze die Frequenz auf einen gültigen Bereich
                        if (parameterFreq > 0 && parameterFreq <= 150)
                        {
                            // Setze die Frequenz basierend auf dem aktuellen Parameter
                            switch (parameter)
                            {
                                case MonitorConstants.Parameter.ECG:
                                    patient.ECGFrequency = (double)parameterFreq;
                                    break;
                                case MonitorConstants.Parameter.EEG:
                                    patient.EEGFrequency = (double)parameterFreq;
                                    break;
                                case MonitorConstants.Parameter.EMG:
                                    patient.EMGFrequency = (double)parameterFreq;
                                    break;
                               case MonitorConstants.Parameter.Resp:
                                    patient.RespFrequency = (double)parameterFreq;
                                      break;
                                default:
                                    break;
                            }
                        }
                        else
                        {
                            MessageBox.Show("Bitte geben Sie einen Wert zwischen 0 und 150 ein.");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Ungültige Eingabe! Bitte geben Sie eine gültige Zahl ein.");
                    }
                }
                else
                {
                    MessageBox.Show("Kein Patient ausgewählt.");
                }
            }
        }


        private void TextBoxFrequency_KeyDown_1(object sender, KeyEventArgs e)
        {

        }

        private void comboBoxParameters_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            parameter = (MonitorConstants.Parameter)comboBoxParameters.SelectedIndex;
            TextBoxFrequency.IsEnabled = true;
            sliderECG.IsEnabled = true;
            if (parameter == MonitorConstants.Parameter.ECG)
            {
                ComboBoxHarmonics.IsEnabled = true;
            }
            else
            {
               
                ComboBoxHarmonics.IsEnabled = false;
            }
            switch (parameter)
            {
                case MonitorConstants.Parameter.ECG: patient.setActiveParameters(patient.ECGFrequency, patient.ECGAmplitude, patient.ECGHarmonics);
                    break;
            
                case MonitorConstants.Parameter.EMG: patient.setActiveParameters(patient.EMGFrequency, patient.EMGAmplitude, -1);
                    break;

                case MonitorConstants.Parameter.Resp: patient.setActiveParameters(patient.RespFrequency, patient.RespAmplitude, -1); 
                    break;

                case MonitorConstants.Parameter.EEG: patient.setActiveParameters(patient.EEGFrequency , patient.EEGAmplitude, -1);
                    break;

                default: 
                    break;
            }
            sliderECG.Value = patient.ActiveAmplitude;
            TextBoxFrequency.Text = patient.ActiveFrequency.ToString();

        }

        private void comboBoxParameters_IsEnabledChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            ComboBox combo = sender as ComboBox;
            if (combo.IsEnabled)
                combo.SelectionChanged += comboBoxParameters_SelectionChanged;
            else
                combo.SelectionChanged -= comboBoxParameters_SelectionChanged;
        }

        private void buttonStartParameter_IsEnabledChanged(object sender, DependencyPropertyChangedEventArgs e)
        {

        }
    }
}