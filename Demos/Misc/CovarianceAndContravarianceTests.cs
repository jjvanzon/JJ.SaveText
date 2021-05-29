using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace JJ.Demos.Misc
{
    [TestClass]
    public class CovarianceAndContravarianceTests
    {
        private class Base
        { }

        // ReSharper disable once ClassNeverInstantiated.Local
        private class DerivedClass : Base
        { }

        [TestMethod]
        public void Test_ContraVariance_Func()
        {
            Func<object, object> baseFunc = b => b;
            // ReSharper disable once NotAccessedVariable
            Func<DerivedClass, object> derivedFunc = d => d;

            //baseFunc = derivedFunc; // Does not compile.
            // ReSharper disable once RedundantAssignment
            derivedFunc = baseFunc;
        }
    }
}
