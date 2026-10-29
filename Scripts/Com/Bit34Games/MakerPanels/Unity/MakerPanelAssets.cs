using UnityEngine;


namespace Com.Bit34Games.MakerPanels.Unity
{
    //  The prefabs the panels build themselves out of, filled in by hand in
    //  the Inspector — once, for the whole project.
    //
    //  Read by the two panel views and by nothing else: every member below
    //  is a prefab, so this is view data from end to end. The views find it
    //  up the hierarchy, the way GridsView finds IGridSceneManager and
    //  MyGridComponent finds TileAssets — Unity to Unity, no interface in
    //  between, because there is no other side to serve.
    //
    //  It was briefly bound and injected through the mediators, which made
    //  couriers of them and handed the whole Views layer a scene component
    //  it never reads. A panel used to carry these nine references itself,
    //  which meant every new panel repeated nine assignments — and a panel
    //  prefab is made by copying another one, so that is nine chances to
    //  copy the wrong thing. The character panels cost exactly that.
    //
    //  Named slots rather than an indexed array, which is where this differs
    //  from TileAssets: a game's tile types are open-ended and numbered, but
    //  the set of field kinds is fixed and closed, so the Inspector should
    //  not be asking anyone to keep an order straight.
    public class MakerPanelAssets : MonoBehaviour
    {
        //  MEMBERS
        public GameObject LabelFieldPrefab   => Require(_labelFieldPrefab,   nameof(_labelFieldPrefab));
        public GameObject ButtonFieldPrefab   => Require(_buttonFieldPrefab,   nameof(_buttonFieldPrefab));
        public GameObject BoolFieldPrefab     => Require(_boolFieldPrefab,     nameof(_boolFieldPrefab));
        public GameObject IntFieldPrefab      => Require(_intFieldPrefab,      nameof(_intFieldPrefab));
        public GameObject Int2dFieldPrefab    => Require(_int2dFieldPrefab,    nameof(_int2dFieldPrefab));
        public GameObject Int3dFieldPrefab    => Require(_int3dFieldPrefab,    nameof(_int3dFieldPrefab));
        public GameObject StringFieldPrefab   => Require(_stringFieldPrefab,   nameof(_stringFieldPrefab));
        public GameObject EnumFieldPrefab     => Require(_enumFieldPrefab,     nameof(_enumFieldPrefab));
        public GameObject DropdownFieldPrefab => Require(_dropdownFieldPrefab, nameof(_dropdownFieldPrefab));
        public GameObject ListItemPrefab      => Require(_listItemPrefab,      nameof(_listItemPrefab));
        public float      ListItemHeight      => _listItemHeight;
        //      For Editor
        [Header("Fields")]
        [SerializeField] private GameObject _labelFieldPrefab;
        [SerializeField] private GameObject _buttonFieldPrefab;
        [SerializeField] private GameObject _boolFieldPrefab;
        [SerializeField] private GameObject _intFieldPrefab;
        [SerializeField] private GameObject _int2dFieldPrefab;
        [SerializeField] private GameObject _int3dFieldPrefab;
        [SerializeField] private GameObject _stringFieldPrefab;
        [SerializeField] private GameObject _enumFieldPrefab;
        [SerializeField] private GameObject _dropdownFieldPrefab;
        [Header("List")]
        [SerializeField] private GameObject _listItemPrefab;
        //  Beside the prefab it describes, rather than on each list: every
        //  item comes from the one prefab above, so a per-panel height was
        //  never a choice — only a number that had to agree with it in four
        //  places.
        [SerializeField] private float _listItemHeight = 40;


        //  METHODS
        //  How a panel finds its table: up its own hierarchy, which is where
        //  the consumer put it. Inactive included on purpose — the maker
        //  screen builds every panel and then switches off the ones whose
        //  tool is not in hand, and a panel initialised on the wrong side of
        //  that would otherwise get a silent null.
        //
        //  Which object the consumer picks decides what can hold a panel. A
        //  popup is not under the screen — the presenter parents it under
        //  its own container — so a popup that builds fields needs this
        //  above both, and that is why it goes on the object the presenter
        //  itself sits under rather than on the maker screen.
        public static MakerPanelAssets FindFor(Component panel)
        {
            MakerPanelAssets assets = panel.GetComponentInParent<MakerPanelAssets>(true);
            if (assets == null)
            {
                throw new System.Exception("No MakerPanelAssets above " + panel.name + ". " +
                                           "A maker panel reads its field and row prefabs from one, " +
                                           "so the consumer puts the component on an object the panels sit under.");
            }

            return assets;
        }

        //  Said plainly here rather than as a null reference three frames
        //  deeper, the way MyGridSceneManager answers a missing asset table.
        //  The list is filled in by hand, so a forgotten slot is the first
        //  thing that happens.
        private GameObject Require(GameObject prefab, string fieldName)
        {
            if (prefab == null)
            {
                throw new System.Exception("MakerPanelAssets." + fieldName + " is empty, " +
                                           "so that field cannot be built.");
            }

            return prefab;
        }
    }
}
