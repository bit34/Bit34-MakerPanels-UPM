using Com.Bit34Games.Presenter.Views;


namespace Com.Bit34Games.MakerPanels.Views
{
    public abstract class MakerFieldListMediator : BasePresenterMediator,
                                                   IMakerFieldListMediator
    {
        //  MEMBERS
        //      View
        private IMakerFieldListView _baseView;


        //  METHODS
        protected void InitializeBase(IMakerFieldListView baseView)
        {
            _baseView = baseView;
            _baseView.FieldList.HideFields();
        }
        
    }
}
