using System;
using System.Collections.Generic;

namespace JJ.Demos.CollectionConversion.Helpers
{
    internal static class MockFactory
    {
        public static IList<Entity> CreateMockEntities() => throw new NotImplementedException();

        public static IList<ViewModel> CreateMockViewModels() => throw new NotImplementedException();
    }
}
