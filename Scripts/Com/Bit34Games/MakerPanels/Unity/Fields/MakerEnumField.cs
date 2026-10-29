using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


namespace Com.Bit34Games.MakerPanels.Unity
{
    public class MakerEnumOption : TMP_Dropdown.OptionData
    {
        public readonly int value;

        public MakerEnumOption(string text, int value)
        {
            this.text = text;
            this.value = value;
        }
    }

    public class MakerEnumField : MakerField
    {
        //  MEMBERS
        //      For Editor
        [SerializeField] private TMP_Dropdown _input;


        //  METHODS
        public void Initialize<TEnum>(string label, bool isReadonly, Action<TEnum> onValueChange) where TEnum : Enum
        {
            string[] names = Enum.GetNames(typeof(TEnum));
            TEnum[] values = (TEnum[])Enum.GetValues(typeof(TEnum));
            List<TMP_Dropdown.OptionData> options = new List<TMP_Dropdown.OptionData>(values.Length);
            for (int i = 0; i < names.Length; i++)
            {
                options.Add(new MakerEnumOption(names[i], (int)(object)values[i]));
            }

            BaseInitialize(label);
            _input.AddOptions(options);
            _input.interactable = !isReadonly;
            if (onValueChange != null)
            {
                _input.onValueChanged.AddListener((int index) => { onValueChange(values[index]); });
            }
        }

        public void SetValue<TEnum>(TEnum value)
        {
            int intValue = (int)(object)value;
            for (int i = 0; i < _input.options.Count; i++)
            {
                if (((MakerEnumOption)_input.options[i]).value == intValue)
                {
                    _input.SetValueWithoutNotify(i);
                    return;
                }
            }
        }
    }
}
