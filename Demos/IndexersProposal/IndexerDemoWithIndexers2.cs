using JJ.Demos.IndexersProposal;

namespace JJ.Demos.IndexersProposal.DemoWithIndexers2
{
	// Caller

	internal class CallerOfSoundCalculator
	{
		public void DoSomething()
		{
			ISoundCalculator calculator = new SoundCalculator();

			const int noteListIndex = 1;

			double frequency = calculator.Values[DimensionEnum.Frequency, noteListIndex];
			double volume = calculator.Values[DimensionEnum.Volume];

			frequency *= 2;
			volume *= 0.6;

			calculator.Values[DimensionEnum.Frequency, noteListIndex] = frequency;
			calculator.Values[DimensionEnum.Volume] = volume;
		}
	}

	// Implementations

	internal class SoundCalculator : ISoundCalculator
	{
		public ISoundCalculatorIndexers Values { get; } = new SoundCalculatorIndexers();

		public void Calculate(float[] buffer, int frameCount, double t0)
		{
			// ...
		}
	}

	internal class SoundCalculatorIndexers : ISoundCalculatorIndexers
{
		public double this[int listIndex]
		{
			get; set;
		}

		public double this[string name]
		{
			get; set;
		}

		public double this[string name, int listIndex]
		{
			get; set;
		}

		public double this[DimensionEnum dimensionEnum]
		{
			get; set;
		}

		public double this[DimensionEnum dimensionEnum, int listIndex]
		{
			get; set;
		}
	}

	// Interfaces

	internal interface ISoundCalculator
	{
		ISoundCalculatorIndexers Values { get; }
	}

	internal interface ISoundCalculatorIndexers
	{
		double this[int listIndex] { get; set; }
		double this[string name] { get; set; }
		double this[string name, int listIndex] { get; set; }
		double this[DimensionEnum dimensionEnum] { get; set; }
		double this[DimensionEnum dimensionEnum, int listIndex] { get; set; }
	}
}
