using JJ.Framework.Testing;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace JJ.Demos.Misc
{
    [TestClass]
    public class CSharp7Tests
    {
        [TestMethod]
        public void Test_CSharp7_Discards()
        {
            (int x, _, _) = GetTuple();

            AssertHelper.AreEqual(10, () => x);
        }

        private (int, int, int) GetTuple()
        {
            var x = (10, 20, 30);

            return x;
        }
    }
}
