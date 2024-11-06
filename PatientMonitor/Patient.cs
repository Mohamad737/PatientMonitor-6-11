using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatientMonitor
{
    internal class Patient
    {
        ECG ecg;
        EMG emg;
        EEG eeg;
        Resp resp;
        private string patientName;
        private DateTime dateOfStudy;
        private int age;
        MonitorConstants.Parameter parameter;
        private double activeFrequency = 0.0;
        private double activeAmplitude = 0.0;
        private int activeECGHarmonics = 0;

        public double ECGAmplitude { get => ecg.Amplitude; set => ecg.Amplitude = value; }
        public double ECGFrequency { get => ecg.Frequency; set => ecg.Frequency = value; }

        public int ECGHarmonics { get => ecg.Harmonics; set => ecg.Harmonics = value; }

        public string PatientName { get => patientName; set => patientName = value; }

        public DateTime DateOfStudy { get => dateOfStudy; set => dateOfStudy = value; }
        public int Age { get => age; set => age = value; }
        public double EMGAmplitude { get => emg.Amplitude; set => emg.Amplitude = value; }
        public double EMGFrequency { get => emg.Frequency; set => emg.Frequency = value; }

        public double EEGAmplitude { get => eeg.Amplitude; set => eeg.Amplitude = value; }
        public double EEGFrequency { get => eeg.Frequency; set => eeg.Frequency = value; }
        public double RespAmplitude { get => resp.Amplitude; set => resp.Amplitude = value; }
        public double RespFrequency { get => resp.Frequency; set => resp.Frequency = value; }

        public void setActiveParameters(double activeFrequency, double activeAmplitude, int activeECGHarmonics)
        {
            this.activeFrequency = activeFrequency;
            this.activeAmplitude = activeAmplitude;
            this.activeECGHarmonics = activeECGHarmonics;
        }
        public double ActiveAmplitude { get => activeAmplitude; }
        public double ActiveFrequency { get => activeFrequency; }
        public int ActiveECGHarmonics { get => activeECGHarmonics; }




        public Patient(double ampltude, double frequency, int harmonics, string patientName, DateTime dateOfStudy, int age)
        {
            this.patientName = patientName;
            this.dateOfStudy = dateOfStudy;
            this.age = age;
            ecg = new ECG(ampltude, frequency, harmonics);
            emg = new EMG(ampltude, frequency, harmonics);
            eeg = new EEG(ampltude, frequency, harmonics);
            resp = new Resp(ampltude, frequency, harmonics);
        }
        public double NextSample(double timeIndex, MonitorConstants.Parameter parameter)
        {
            double nextSample = 0.0;
            switch (parameter)
            {
                case MonitorConstants.Parameter.ECG:
                    nextSample = ecg.NextSample(timeIndex);
                    break;

                case MonitorConstants.Parameter.EMG:
                    nextSample = emg.NextSampling(timeIndex);
                    break;

                case MonitorConstants.Parameter.EEG:
                    nextSample = eeg.NextSample(timeIndex);
                    break;
                default:
                    break;
                case MonitorConstants.Parameter.Resp:
                    nextSample = resp.NextSample(timeIndex);
                    break;
            }
            return (nextSample);
        }
    }
}