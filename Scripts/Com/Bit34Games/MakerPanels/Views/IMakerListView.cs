namespace Com.Bit34Games.MakerPanels.Views
{
    public interface IMakerListView
    {
        //  METHODS
        void AddItem(int itemId, string itemLabel);
        void SetItemLabel(int itemId, string itemLabel);
        void SetSelectedItem(int itemId);
        void RemoveItem(int itemId);
    }
}
