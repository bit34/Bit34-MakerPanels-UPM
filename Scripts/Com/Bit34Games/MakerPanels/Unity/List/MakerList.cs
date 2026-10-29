using System;
using System.Collections.Generic;
using UnityEngine;


namespace Com.Bit34Games.MakerPanels.Unity
{
    //  A list of selectable rows, and nothing more: the container, the rows,
    //  which one is selected, and the vertical layout.
    //
    //  Deliberately not a PresenterView, which is what separates it from
    //  MakerListView. A view is mediated and has to be opened by the
    //  presenter, so it can only be a screen or a panel; this can be dropped
    //  anywhere a list is wanted, including inside another view.
    //
    //  The one thing it needs from above it is a MakerPanelAssets to find,
    //  for the row prefab and the row height.
    public class MakerList : MonoBehaviour
    {
        //  MEMBERS
        public int SelectedItemId { get; private set; }
        //      For Editor
        //  The container is this list's own. The item prefab and its height
        //  live in MakerPanelAssets, where the height can sit beside the
        //  prefab it measures.
        [SerializeField] private RectTransform  _itemContainer;
        //      Private
        private MakerPanelAssets                _panelAssets;
        private Dictionary<int, MakerListItem>  _items;
        
        //  METHODS
        public void Initialize()
        {
            _panelAssets    = MakerPanelAssets.FindFor(this);
            _items          = new Dictionary<int, MakerListItem>();
            SelectedItemId  = -1;
        }

        public void AddItem(int itemId, string itemLabel, Action<int> onItemSelect)
        {
            GameObject    itemGO = Instantiate(_panelAssets.ListItemPrefab, _itemContainer);
            MakerListItem item   = itemGO.GetComponent<MakerListItem>();
            item.Initialize(itemId, itemLabel, onItemSelect);
            ((RectTransform)item.transform).anchoredPosition = new Vector2(0, -_panelAssets.ListItemHeight * _items.Count);
            _items.Add(itemId, item);
            _itemContainer.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, _panelAssets.ListItemHeight * _items.Count);
        }

        //  For a list whose labels can go stale without the item itself
        //  changing: a character's definition changes in place, and a plate
        //  or a character being renamed changes nothing else at all, so
        //  neither row is rebuilt and both would keep the old text.
        public void SetItemLabel(int itemId, string itemLabel)
        {
            _items[itemId].SetLabel(itemLabel);
        }

        public void SetSelectedItem(int itemId)
        {
            if (SelectedItemId != itemId)
            {
                if (SelectedItemId != -1)
                {
                    _items[SelectedItemId].SetSelection(false);
                }

                SelectedItemId = itemId;

                if (SelectedItemId != -1)
                {
                    _items[SelectedItemId].SetSelection(true);
                }
            }
        }

        public void RemoveItem(int itemId)
        {
            Destroy(_items[itemId].gameObject);
            _items.Remove(itemId);

            //  Removing what was selected leaves nothing selected.
            //
            //  SetSelectedItem cannot be left to do this afterwards: it does
            //  nothing at all when the id it is given is the one it already
            //  holds, so a SetSelectedItem(-1) arriving after this would be
            //  swallowed and the list would go on reporting a row that is
            //  gone.
            if (SelectedItemId == itemId)
            {
                SelectedItemId = -1;
            }

            int i = 0;
            foreach(MakerListItem item in _items.Values)
            {
                ((RectTransform)item.transform).anchoredPosition = new Vector2(0, -_panelAssets.ListItemHeight * i++);
            }
            _itemContainer.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, _panelAssets.ListItemHeight * _items.Count);
        }
    }
}