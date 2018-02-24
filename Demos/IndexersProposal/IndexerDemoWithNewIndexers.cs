using JJ.Demos.IndexersProposal;

namespace JJ.Demos.IndexersProposal.DemoWithNewIndexers
{
	// Caller

	internal class CallerOfSoundCalculator
	{
		public void DoSomething()
		{
			var calculator = new SoundCalculator();

			const int noteListIndex = 1;

			double frequency = calculator.Values[DimensionEnum.Frequency, noteListIndex];
			double volume = calculator.Values[DimensionEnum.Volume];

			frequency *= 2;
			volume *= 0.6;

			calculator.Values[DimensionEnum.Frequency, noteListIndex] = frequency;
			calculator.Values[DimensionEnum.Volume] = volume;
		}
	}

	// Implementation

	internal class SoundCalculator : ISoundCalculator
	{
		public double Values[int listIndex]
		{
			get; set;
		}

		public double Values[string name]
		{
			get; set;
		}

		public double Values[string name, int listIndex]
		{
			get; set;
		}

		public double Values[DimensionEnum dimensionEnum]
		{
			get; set;
		}

		public double Values[DimensionEnum dimensionEnum, int listIndex]
		{
			get; set;
		}

		public void Calculate(float[] buffer, int frameCount, double t0)
		{
			// ...
		}
	}

	// Interface

	internal interface ISoundCalculator
	{
		double Values[int listIndex];
		double Values[string name];
		double Values[string name, int listIndex];
		double Values[DimensionEnum dimensionEnum];
		double Values[DimensionEnum dimensionEnum, int listIndex];
		void Calculate(float[] buffer, int frameCount, double t0);
	}
}
