using Com.Bit34Games.MakerPanels.Views;
using Com.Bit34Games.Presenter.Unity;
using UnityEngine;


namespace Com.Bit34Games.MakerPanels.Unity
{
    //  A MakerFieldList as a panel: the list itself does the fields, and
    //  this is what the presenter can open and a mediator can be bound to.
    //  Whoever holds the view reaches the fields through FieldList.
    public class MakerFieldListView : PresenterView,
                                      IMakerFieldListView
    {
        //  MEMBERS
        public IMakerFieldList FieldList => _fieldList;
        //      For Editor
        [SerializeField] private MakerFieldList _fieldList;
        //      Private
        private IMakerFieldListMediator _baseMediator;


        //  METHODS
        protected void InitializeBase(IMakerFieldListMediator baseMediator)
        {
            _baseMediator = baseMediator;
            _fieldList.Initialize();
        }

    }
}
