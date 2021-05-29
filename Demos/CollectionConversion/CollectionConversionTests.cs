using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using JJ.Demos.CollectionConversion.Helpers;
// ReSharper disable HeuristicUnreachableCode
// ReSharper disable UnusedVariable
#pragma warning disable 162

namespace JJ.Demos.CollectionConversion
{
    [TestClass]
    public class CollectionConversionTests
    {
        [TestMethod]
        public void Test_CollectionConversion_CompareSetsOfIDs_IDsToDeleteContains()
        {
            // The demo code will throw exceptions, so just return.
            return;

            IRepository repository = null;
            IList<Entity> entities = MockFactory.CreateMockEntities();
            IList<ViewModel> viewModels = MockFactory.CreateMockViewModels();

            IList<Entity> newEntities = CollectionConversionHelper.ConvertCollection_CompareSetsOfIDs_IDsToDeleteContains(viewModels, entities, repository);
            foreach (Entity newEntity in newEntities)
            {
                // You might link the new entities to their parent here.
            }
        }

        [TestMethod]
        public void Test_CollectionConversion_CompareSetsOfIDs_GetAndDelete()
        {
            // The demo code will throw exceptions, so just return.
            return;

            IRepository repository = null;
            IList<Entity> entities = MockFactory.CreateMockEntities();
            IList<ViewModel> viewModels = MockFactory.CreateMockViewModels();

            IList<Entity> newEntities = CollectionConversionHelper.ConvertCollection_CompareSetsOfIDs_GetAndDelete(viewModels, entities, repository);
            foreach (Entity newEntity in newEntities)
            {
                // You might link the new entities to their parent here.
            }
        }

        [TestMethod]
        public void Test_CollectionConversion_MaintainListsOfSomeOperations()
        {
            // The demo code will throw exceptions, so just return.
            return;

            IRepository repository = null;
            IList<Entity> entities = MockFactory.CreateMockEntities();
            IList<ViewModel> viewModels = MockFactory.CreateMockViewModels();

            ConversionResult result = CollectionConversionHelper.ConvertCollection(viewModels, entities, repository);
            foreach (Entity insertedEntity in result.InsertedEntities)
            {
                // You might link the new entities to their parent here.
            }
        }

        [TestMethod]
        public void Test_CollectionConversion_MaintainListsOfSomeOperations_ExplicitTryGet()
        {
            // The demo code will throw exceptions, so just return.
            return;

            IRepository repository = null;
            IList<Entity> entities = MockFactory.CreateMockEntities();
            IList<ViewModel> viewModels = MockFactory.CreateMockViewModels();

            IList<Entity> newEntities = CollectionConversionHelper.ConvertCollection_MaintainListsOfSomeOperations_ExplicitTryGet(viewModels, entities, repository);
            foreach (Entity newEntity in newEntities)
            {
                // You might link the new entities to their parent here.
            }
        }
    }
}
