using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


namespace Com.Bit34Games.MakerPanels.Unity
{
    public class MakerField : MonoBehaviour
    {
        //  MEMBERS
        public float Height => _height;
        //      For Editor
        [SerializeField] private TMP_Text _label;
        [SerializeField] private float _height;
        [SerializeField] private Button _removeButton;


        //  METHODS
        protected void BaseInitialize(string label)
        {
            _label.text = label;
            _removeButton.gameObject.SetActive(false);
        }

        //  Showing the button is part of handing over the callback, not a
        //  second call that can be forgotten: BaseInitialize hid it, and a
        //  button with a callback and no way to click it is the one state
        //  that has no use.
        //
        //  Listeners are cleared first, so handing over a second callback
        //  replaces the first instead of removing the field twice on one
        //  click.
        public void SetRemoveButton(Action<MakerField> onRemoveClicked)
        {
            _removeButton.onClick.RemoveAllListeners();
            _removeButton.onClick.AddListener(() => onRemoveClicked(this));
            _removeButton.gameObject.SetActive(true);
        }
    }
}
