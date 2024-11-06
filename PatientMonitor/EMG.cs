using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatientMonitor
{
    class EMG
    {
        private double amplitude = 0.0;
        private double frequency = 0.0;
        private int harmonics = 0;


        public double Amplitude { get => amplitude; set => amplitude = value; }
        public double Frequency { get => frequency; set => frequency = value; }

        public int Harmonics { get => harmonics; set => harmonics = value; }

        public EMG(double amplitude, double frequency, int harmonics)
        {
            this.amplitude = amplitude;
            this.frequency = frequency;
            this.harmonics = harmonics;
        }
        public double NextSampling(double timeindex)
        {
            const double HzToBeatsPerMin = 60.0;
            double sample = 0.0;
            double stepIndex = 0.0;
            double SignalLength = 0.0;

            SignalLength = (double)(HzToBeatsPerMin / this.frequency);
            stepIndex = (double)(timeindex % SignalLength);
            if (stepIndex > SignalLength /2)
            {
                sample = 1;
            }
            else
            {
                sample = -1;
            }
            sample *= this.amplitude;
            return (sample);
        }
    }
}
