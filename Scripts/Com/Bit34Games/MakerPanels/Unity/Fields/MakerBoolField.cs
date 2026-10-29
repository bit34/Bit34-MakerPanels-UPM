using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


namespace Com.Bit34Games.MakerPanels.Unity
{
    public class MakerBoolField : MakerField
    {
        //  MEMBERS
        //      For Editor
        [SerializeField] private Toggle _input;


        //  METHODS
        public void Initialize(string label, bool value, Action<bool> onValueChange)
        {
            BaseInitialize(label);

            _input.isOn = value;
            if (onValueChange != null)
            {
                _input.onValueChanged.AddListener((bool newValue) => { onValueChange(newValue); });
            }
        }

        public void SetValue(bool value)
        {
            _input.SetIsOnWithoutNotify(value);
        }
    }
}
