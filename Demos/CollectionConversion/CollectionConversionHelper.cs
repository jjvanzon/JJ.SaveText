using System.Linq;
using System.Collections.Generic;
using JJ.Demos.CollectionConversion.Helpers;

namespace JJ.Demos.CollectionConversion
{
	/// <summary>
	/// This class demonstrates different ways to convert one collection to another,
	/// including updating, deleting and inserting items.
	/// 
	/// See the architecture document sections about 'Collection Conversion'.
	/// 
	/// This class will not contain each and every possible way, 
	/// but different ways might be added in the future.
	/// 
	/// The source structure will be called the view model,
	/// the dest structure the entity.
	/// </summary>
	internal static class CollectionConversionHelper
	{
		public static IList<Entity> ConvertCollection_CompareSetsOfIDs_IDsToDeleteContains(
			IList<ViewModel> viewModels,
			IList<Entity> entities,
			IRepository repository)
		{
			var newEntities = new List<Entity>();

			foreach (ViewModel viewModel in viewModels)
			{
				Entity entity = viewModel.ToEntity(repository);

				bool isNew = viewModel.ID == 0;
				if (isNew)
				{
					newEntities.Add(entity);
				}
			}

			// Delete
			ISet<int> entityIDs = new HashSet<int>(entities.Select(x => x.ID)
														   .Where(x => x != 0));

			ISet<int> viewModelIDs = new HashSet<int>(viewModels.Select(x => x.ID)
																.Where(x => x != 0));

			ISet<int> idsToDelete = new HashSet<int>(entityIDs.Except(viewModelIDs));

			foreach (Entity entity in entities.ToArray())
			{
				if (idsToDelete.Contains(entity.ID))
				{
					entity.UnlinkRelatedEntities();
					repository.Delete(entity);
				}
			}

			return newEntities;
		}

		/// <summary>
		/// Benefit: no extra 'contains' check.
		/// </summary>
		public static IList<Entity> ConvertCollection_CompareSetsOfIDs_GetAndDelete(
			IList<ViewModel> viewModels,
			IList<Entity> existingEntities,
			IRepository repository)
		{
			var newEntities = new List<Entity>();

			foreach (ViewModel viewModel in viewModels)
			{
				Entity entity = viewModel.ToEntity(repository);

				bool isNew = viewModel.ID == 0;
				if (isNew)
				{
					newEntities.Add(entity);
				}
			}

			// Delete
			ISet<int> existingEntityIDs = new HashSet<int>(existingEntities.Select(x => x.ID)
																		   .Where(x => x != 0));

			ISet<int> viewModelIDs = new HashSet<int>(viewModels.Select(x => x.ID)
																.Where(x => x != 0));

			ISet<int> idsToDelete = new HashSet<int>(existingEntityIDs.Except(viewModelIDs));

			foreach (int idToDelete in idsToDelete)
			{
				Entity entityToDelete = repository.Get(idToDelete);
				entityToDelete.UnlinkRelatedEntities();
				repository.Delete(entityToDelete);
			}

			return newEntities;
		}

		/// <summary> 
		/// Benefit: You get to explicitly see which entities are new and which are not.
		/// Downside: You either have code for conversion of single entities in two places, or more ToEntity overloads or a weird way where you seem to retrieving the same entity twice.
		/// </summary>
		public static IList<Entity> ConvertCollection_MaintainListsOfSomeOperations_ExplicitTryGet(
			IList<ViewModel> viewModels,
			IList<Entity> existingEntities,
			IRepository repository)
		{
			var updatedEntities = new List<Entity>();
			var insertedEntities = new List<Entity>();

			foreach (ViewModel viewModel in viewModels)
			{
				Entity entity = repository.TryGet(viewModel.ID);
			    bool isNew = entity == null;

				entity = viewModel.ToEntity(repository);

			    if (isNew)
			    {
			        insertedEntities.Add(entity);
			    }
			    else
			    {
			        updatedEntities.Add(entity);
			    }
		    }

            // Delete
            IEnumerable<Entity> entitiesToDelete = existingEntities.Except(insertedEntities)
																   .Except(updatedEntities);

			foreach (Entity entityToDelete in entitiesToDelete)
			{
				entityToDelete.UnlinkRelatedEntities();
				repository.Delete(entityToDelete);
			}

			return insertedEntities;
		}

		/// <summary> 
		/// MaintainListsOfOperations_ExplicitTryGetInsertUpdate.
		/// Benefit: You keep everything in the entity realm, rather than working with IDs.
		/// Benefit: Complete control over the CRUD of the entities.
		/// Benefit: CRUD operations seem neatly symmetrical and clean.
		/// Benefit: You can report exactly which operation is executed onto which entity.
		/// Downside: You do not reuse a method that is the singular form of the conversion.
		/// Downside: Performance of comparing ID's might be better than comparing entities.
		/// Downside: You might be counting on instance integrity, 
		///		   while you might not have that depending on the persistence technology.
		///		   Identity integrity is more likely to be present than instance integrity.
		/// </summary>
		public static ConversionResult ConvertCollection(
			IList<ViewModel> viewModels,
			IList<Entity> existingEntities,
			IRepository repository)
		{
			var unmodifiedEntities = new List<Entity>();
			var updatedEntities = new List<Entity>();
			var insertedEntities = new List<Entity>();

			foreach (ViewModel viewModel in viewModels)
			{
				Entity entity = repository.TryGet(viewModel.ID);
				if (entity == null)
				{
					entity = repository.Create();
					entity.Name = viewModel.Name;
					insertedEntities.Add(entity);
				}
				else
				{
					if (entity.Name != viewModel.Name)
					{
						entity.Name = viewModel.Name;
						updatedEntities.Add(entity);
					}
					else
					{
						unmodifiedEntities.Add(entity);
					}
				}
			}

			// Delete
			IList<Entity> entitiesToDelete = existingEntities.Except(updatedEntities)
															 .Except(unmodifiedEntities)
															 .ToArray();

			foreach (Entity entityToDelete in entitiesToDelete)
			{
				entityToDelete.UnlinkRelatedEntities();
				repository.Delete(entityToDelete);
			}

			return new ConversionResult(
				unmodifiedEntities,
				insertedEntities,
				updatedEntities,
				entitiesToDelete);
		}
	}
}
