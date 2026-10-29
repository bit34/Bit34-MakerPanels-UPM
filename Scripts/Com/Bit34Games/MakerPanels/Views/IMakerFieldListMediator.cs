namespace Com.Bit34Games.MakerPanels.Views
{
    //  Deliberately empty, and worth saying why: it is the type a panel's own
    //  mediator interface derives from, so that InitializeBase has something
    //  to take.
    //
    //  The field prefabs do not belong here. They briefly did — as a
    //  MakerPanelAssets property — which gave every consumer of this
    //  interface a dependency on a scene component that none of them reads.
    //  The view gets them itself.
    public interface IMakerFieldListMediator
    {
    }
}
