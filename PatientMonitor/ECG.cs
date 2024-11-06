using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatientMonitor
{
    class ECG
    {
        private double amplitude = 0.0;
        private double frequency = 0;
        private int harmonics = 1;
        public double Amplitude { get => amplitude; set => amplitude = value; }
        public double Frequency { get => frequency; set => frequency = value; }

        public int Harmonics { get => harmonics; set => harmonics = value; }

        public ECG(double amplitude, double frequency, int harmonics)
        {
            this.amplitude = amplitude;
            this.frequency = frequency;
            this.harmonics = harmonics;
        }



        public double NextSample(double timeIndex)
        {
            const double HzToBeatsPerMin = 150.0;
            double sample = 0;

            switch (harmonics)
            {
                case 1:
                    sample = Math.Cos(2 * Math.PI * (frequency / HzToBeatsPerMin) * timeIndex);
                    break;
                case 2:
                    sample = Math.Cos(2 * Math.PI * (frequency / HzToBeatsPerMin) * timeIndex) + 0.5 * Math.Cos(2 * Math.PI * (2 * frequency / HzToBeatsPerMin) * timeIndex);
                    break;
                case 3:
                    sample = Math.Cos(2 * Math.PI * (frequency / HzToBeatsPerMin) * timeIndex) + 0.5 * Math.Cos(2 * Math.PI * (2 * frequency / HzToBeatsPerMin) * timeIndex) + 0.25 * Math.Cos(2 * Math.PI * (3 * frequency / HzToBeatsPerMin) * timeIndex);
                    break;

            }

            sample *= amplitude;

            return (sample);
        }
    }
}

