using JJ.Demos.IndexersProposal;

namespace JJ.Demos.IndexersProposal.DemoWithIndexers
{
    // Caller

    internal class CallerOfSoundCalculator
    {
        public void DoSomething()
        {
            ISoundCalculator calculator = new SoundCalculator();

            const int noteListIndex = 1;

            double frequency = calculator.ValueByDimensionEnumAndListIndex[DimensionEnum.Frequency, noteListIndex];
            double volume = calculator.ValueByDimensionEnum[DimensionEnum.Volume];

            frequency *= 2;
            volume *= 0.6;

            calculator.ValueByDimensionEnumAndListIndex[DimensionEnum.Frequency, noteListIndex] = frequency;
            calculator.ValueByDimensionEnum[DimensionEnum.Volume] = volume;
        }
    }

    // Implementations

    internal class SoundCalculator : ISoundCalculator
    {
        public IIndexerByListIndex ValueByListIndex { get; } = new SoundCalculatorIndexerByListIndex();
        public IIndexerByName ValueByName { get; } = new SoundCalculatorIndexerByName();
        public IIndexerByNameAndListIndex ValueByNameAndListIndex { get; } = new SoundCalculatorIndexerByNameAndListIndex();
        public IIndexerByDimensionEnum ValueByDimensionEnum { get; } = new SoundCalculatorIndexerByDimensionEnum();
        public IIndexerByDimensionEnumAndListIndex ValueByDimensionEnumAndListIndex { get; } = new SoundCalculatorIndexerByDimensionEnumAndListIndex();

        public void Calculate(float[] buffer, int frameCount, double t0)
        {
            // ...
        }
    }

    internal class SoundCalculatorIndexerByListIndex : IIndexerByListIndex
    {
        public double this[int listIndex]
        {
            get; set;
        }
    }

    internal class SoundCalculatorIndexerByName : IIndexerByName
    {
        public double this[string name]
        {
            get; set;
        }
    }

    internal class SoundCalculatorIndexerByNameAndListIndex : IIndexerByNameAndListIndex
    {
        public double this[string name, int listIndex]
        {
            get; set;
        }
    }

    internal class SoundCalculatorIndexerByDimensionEnum : IIndexerByDimensionEnum
    {
        public double this[DimensionEnum dimensionEnum]
        {
            get; set;
        }
    }

    internal class SoundCalculatorIndexerByDimensionEnumAndListIndex : IIndexerByDimensionEnumAndListIndex
    {
        public double this[DimensionEnum dimensionEnum, int listIndex]
        {
            get; set;
        }
    }

    // Interfaces

    internal interface ISoundCalculator
    {
        IIndexerByListIndex ValueByListIndex { get; }
        IIndexerByName ValueByName { get; }
        IIndexerByNameAndListIndex ValueByNameAndListIndex { get; }
        IIndexerByDimensionEnum ValueByDimensionEnum { get; }
        IIndexerByDimensionEnumAndListIndex ValueByDimensionEnumAndListIndex { get; }
        void Calculate(float[] buffer, int frameCount, double t0);
    }

    internal interface IIndexerByListIndex
    {
        double this[int listIndex] { get; set; }
    }

    internal interface IIndexerByName
    {
        double this[string name] { get; set; }
    }

    internal interface IIndexerByNameAndListIndex
    {
        double this[string name, int listIndex] { get; set; }
    }

    internal interface IIndexerByDimensionEnum
    {
        double this[DimensionEnum dimensionEnum] { get; set; }
    }

    internal interface IIndexerByDimensionEnumAndListIndex
    {
        double this[DimensionEnum dimensionEnum, int listIndex] { get; set; }
    }
}