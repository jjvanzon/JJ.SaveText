
using JJ.Framework.Exceptions;
using JJ.Framework.Business;
using JJ.Data.Canonical;

namespace JJ.Business.Canonical
{
	public static class BusinessToCanonicalExtensions
	{
		public static VoidResultDto ToCanonical(this VoidResult source)
		{
			if (source == null) throw new NullException(() => source);

			var dest = new VoidResultDto
			{
				Successful = source.Successful,
				Messages = source.Messages
			};

			return dest;
		}

		public static ResultDto<T> ToCanonical<T>(this Result<T> source)
		{
			if (source == null) throw new NullException(() => source);

			var dest = new ResultDto<T>
			{
				Successful = source.Successful,
				Messages = source.Messages,
				Data = source.Data,
			};

			return dest;
		}
	}
}
