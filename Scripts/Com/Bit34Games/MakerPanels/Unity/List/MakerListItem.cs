using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


namespace Com.Bit34Games.MakerPanels.Unity
{
    public class MakerListItem : MonoBehaviour
    {
        //  MEMBERS
        //      For Editor
        [SerializeField] private TMP_Text _label;
        [SerializeField] private Button   _button;
        [SerializeField] private Image    _background;
        [SerializeField] private Color    _selectedColor;
        [SerializeField] private Color    _notSelectedColor;


        //  METHODS
        public void Initialize(int itemId, string itemLabel, Action<int> onSelect)
        {
            _label.text = itemLabel;
            _button.onClick.AddListener(() => { onSelect(itemId); });
            _background.color = _notSelectedColor;
        }

        public void SetLabel(string itemLabel)
        {
            _label.text = itemLabel;
        }

        public void SetSelection(bool state)
        {
            _background.color = (state) ? (_selectedColor) : (_notSelectedColor);
        }
    }
}
