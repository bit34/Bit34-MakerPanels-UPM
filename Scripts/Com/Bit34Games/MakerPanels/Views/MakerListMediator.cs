using Com.Bit34Games.Presenter.Views;


namespace Com.Bit34Games.MakerPanels.Views
{
    public abstract class MakerListMediator : BasePresenterMediator,
                                              IMakerListMediator
    {
        //  MEMBERS
        //      View
        private IMakerListView _baseView;


        //  METHODS
        protected void InitializeBase(IMakerListView baseView)
        {
            _baseView = baseView;
        }

        #region IMakerListMediator implementation

        //  Abstract, not a virtual that throws: this class cannot know what
        //  selecting an item means, so the compiler should ask the subclass
        //  for it rather than a NotImplementedException finding out at run
        //  time. GridMakerGridListMediator already overrides it.
        public abstract void OnViewItemSelected(int itemId);

        #endregion
    }
}
