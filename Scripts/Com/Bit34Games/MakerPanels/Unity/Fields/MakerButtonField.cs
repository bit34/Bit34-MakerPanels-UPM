using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


namespace Com.Bit34Games.MakerPanels.Unity
{
    public class MakerButtonField : MakerField
    {
        //  MEMBERS
        //      For Editor
        [SerializeField] private Button _button;


        //  METHODS
        public void Initialize(string label, Action onClick)
        {
            BaseInitialize(label);
            _button.onClick.AddListener(() => { onClick(); });
        }

    }
}
