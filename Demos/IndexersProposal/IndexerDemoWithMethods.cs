using JJ.Demos.IndexersProposal;

namespace JJ.Demos.IndexersProposal.DemoWithMethods
{
    // Caller

    internal class CallerOfSoundCalculator
    {
        public void DoSomething()
        {
            var calculator = new SoundCalculator();

            const int noteListIndex = 1;

            double frequency = calculator.GetValue(DimensionEnum.Frequency, noteListIndex);
            double volume = calculator.GetValue(DimensionEnum.Volume);

            frequency *= 2;
            volume *= 0.6;

            calculator.SetValue(DimensionEnum.Frequency, noteListIndex, frequency);
            calculator.SetValue(DimensionEnum.Volume, volume);
        }
    }

    // Implementation

    internal class SoundCalculator : ISoundCalculator
    {
        public double GetValue(int listIndex) => ...;
        public void SetValue(int listIndex, double value) => ...;

        public double GetValue(string name) => ...;
        public void SetValue(string name, double value) => ...;

        public double GetValue(string name, int listIndex) => ...;
        public void SetValue(string name, int listIndex, double value) => ...;

        public double GetValue(DimensionEnum dimensionEnum) => ...;
        public void SetValue(DimensionEnum dimensionEnum, double value) => ...;

        public double GetValue(DimensionEnum dimensionEnum, int listIndex) => ...;
        public void SetValue(DimensionEnum dimensionEnum, int listIndex, double value) => ...;

        public void Calculate(float[] buffer, int frameCount, double t0)
        {
            // ...
        }
    }

    // Interface

    internal interface ISoundCalculator
    {
        double GetValue(int listIndex);
        void SetValue(int listIndex, double value);

        double GetValue(string name);
        void SetValue(string name, double value);

        double GetValue(string name, int listIndex);
        void SetValue(string name, int listIndex, double value);

        double GetValue(DimensionEnum dimensionEnum);
        void SetValue(DimensionEnum dimensionEnum, double value);

        double GetValue(DimensionEnum dimensionEnum, int listIndex);
        void SetValue(DimensionEnum dimensionEnum, int listIndex, double value);

        void Calculate(float[] buffer, int frameCount, double t0);
    }
}
