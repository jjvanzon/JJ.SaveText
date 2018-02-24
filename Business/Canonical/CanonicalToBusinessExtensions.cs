
using JJ.Framework.Business;
using JJ.Data.Canonical;
using JJ.Framework.Exceptions;

namespace JJ.Business.Canonical
{
	public static class CanonicalToBusinessExtensions
	{
		public static VoidResult ToBusiness(this VoidResultDto source)
		{
			if (source == null) throw new NullException(() => source);

			var dest = new VoidResult
			{
				Successful = source.Successful,
			};

			if (source.Messages != null)
			{
				dest.Messages = source.Messages;
			}

			return dest;
		}
	}
}
