namespace Com.Bit34Games.MakerPanels.Unity
{
    //  A row that is only its label: a heading inside a field list, or a
    //  line of text where the others take a value. It has nothing to read
    //  back, which is why it has no Set of its own.
    public class MakerLabelField : MakerField
    {
        //  METHODS
        public void Initialize(string label)
        {
            BaseInitialize(label);
        }
    }
}
