using JJ.Framework.Business;

namespace JJ.Demos.Misc
{
    /// <summary>
    /// Not finished. Trying something out.
    /// </summary>
    internal abstract class ActionHandler<TViewModel, TEntity>
        where TViewModel : ViewModelBase
    {
        protected TViewModel Action(TViewModel userInput)
        {
            userInput.RefreshCounter++;

            userInput.Successful = false;

            TEntity entity = ToEntity(userInput);

            IResult result = Business(userInput, entity);

            TViewModel viewModel = ToViewModel(entity);

            if (result != null)
            {
                viewModel.Successful = result.Successful;
                viewModel.Messages = result.Messages;
            }
            viewModel.RefreshCounter = userInput.RefreshCounter;

            NonPersisted(viewModel);

            return viewModel;
        }

        protected virtual TEntity ToEntity(TViewModel userInput) => default;

        protected virtual IResult Business(TViewModel userInput, TEntity entity) => null;

        protected abstract TViewModel ToViewModel(TEntity entity);

        protected virtual void NonPersisted(TViewModel viewModel)
        { }
    }
}
