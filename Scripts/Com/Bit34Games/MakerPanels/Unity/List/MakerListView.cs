using Com.Bit34Games.MakerPanels.Views;
using Com.Bit34Games.Presenter.Unity;
using UnityEngine;
using UnityEngine.UI;


namespace Com.Bit34Games.MakerPanels.Unity
{
    //  A MakerList as a panel: the list itself does the listing, and this
    //  adds what only a mediated panel can have — a mediator to report a
    //  selection to, and the buttons that act on whatever is selected.
    public class MakerListView : PresenterView,
                                 IMakerListView
    {
        //  MEMBERS
        //      For Editor
        [SerializeField] private MakerList _list;
        [SerializeField] private Button[]  _selectionRelatedButtons;
        private IMakerListMediator         _baseMediator;


        //  METHODS
        protected void InitializeBase(IMakerListMediator baseMediator)
        {
            _baseMediator = baseMediator;
            _list.Initialize();
            SetSelectionRelatedButtons(false);
        }

        public void AddItem(int itemId, string itemLabel)
        {
            _list.AddItem(itemId, itemLabel, _baseMediator.OnViewItemSelected);
        }

        public void SetItemLabel(int itemId, string itemLabel)
        {
            _list.SetItemLabel(itemId, itemLabel);
        }

        public void SetSelectedItem(int itemId)
        {
            _list.SetSelectedItem(itemId);
            SetSelectionRelatedButtons(_list.SelectedItemId != -1);
        }
        
        public void RemoveItem(int itemId)
        {
            //  Asked before the list is told, because the list clears its
            //  own selection while removing and the answer would be -1 by
            //  the time it returns.
            //
            //  And it has to be asked at all rather than left to a later
            //  SetSelectedItem(-1): that call is swallowed when the list
            //  already holds -1, which it now does. Callers that deselect
            //  before removing never saw this; clearing a whole list does.
            if (_list.SelectedItemId == itemId)
            {
                SetSelectionRelatedButtons(false);
            }
            _list.RemoveItem(itemId);
        }

        //  The buttons a list offers for whatever is selected — remove it,
        //  rename it, whatever the consumer wires in. Off while nothing is.
        private void SetSelectionRelatedButtons(bool isInteractable)
        {
            for (int i = 0; i < _selectionRelatedButtons.Length; i++)
            {
                _selectionRelatedButtons[i].interactable = isInteractable;
            }
        }

    }
}
