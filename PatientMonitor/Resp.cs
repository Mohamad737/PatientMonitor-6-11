using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatientMonitor
{
    class Resp
    {
        private double amplitude = 0.0;
        private double frequency = 0.0;
        private int harmonics = 0;


        public double Amplitude { get => amplitude; set => amplitude = value; }
        public double Frequency { get => frequency; set => frequency = value; }

        public int Harmonics { get => harmonics; set => harmonics = value; }

        public Resp(double amplitude, double frequency, int harmonics)
        {
            this.amplitude = amplitude;
            this.frequency = frequency;
            this.harmonics = harmonics;
        }
        public double NextSample(double timeIndex)
        {
            const double HzToBeatsPerMin = 15.0;
            double sample = 0.0;

            // Berechnung der Signallänge basierend auf der Frequenz und HzToBeatsPerMin
            double SignalLength = HzToBeatsPerMin / (frequency / 10);

            // Berechnung des Zeitwerts innerhalb der aktuellen Signallänge
            double stepIndex = (timeIndex * 0.025) % SignalLength;

            // Berechnung des Signals basierend auf der Position in der Signallänge
            if (stepIndex < SignalLength / 2)
            {
                // Erste Hälfte der Signallänge: gespiegelte exponentielle Abnahme
                sample = -amplitude * (Math.Exp(-5 * stepIndex / (SignalLength / 2)) - 1);
            }
            else
            {
                // Zweite Hälfte der Signallänge: normale exponentielle Abnahme
                sample = amplitude * (Math.Exp(-5 * (stepIndex - SignalLength / 2) / (SignalLength / 2)) - 1);
            }

            return sample;
        }

    }
}
