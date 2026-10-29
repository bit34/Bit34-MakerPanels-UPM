using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


namespace Com.Bit34Games.MakerPanels.Unity
{
    public class MakerDropdownOption : TMP_Dropdown.OptionData
    {
        public readonly int value;

        public MakerDropdownOption(string text, int value)
        {
            this.text = text;
            this.value = value;
        }
    }

    public class MakerDropdownField : MakerField
    {
        //  MEMBERS
        //      For Editor
        [SerializeField] private TMP_Dropdown _input;


        //  METHODS
        public void Initialize(string label, bool isReadonly, Action<int> onValueChange)
        {

            BaseInitialize(label);
            _input.interactable = !isReadonly;
            if (onValueChange != null)
            {
                _input.onValueChanged.AddListener((int index) => { onValueChange(((MakerDropdownOption)_input.options[index]).value); });
            }
        }

        public void SetOptions(string[] names, int[] values = null)
        {
            _input.ClearOptions();

            if (values == null)
            {
                values = new int[names.Length];
                for (int i = 0; i < values.Length; i++)
                {
                    values[i] = i;
                }
            }

            List<TMP_Dropdown.OptionData> options = new List<TMP_Dropdown.OptionData>(values.Length);
            for (int i = 0; i < names.Length; i++)
            {
                options.Add(new MakerDropdownOption(names[i], values[i]));
            }
            _input.AddOptions(options);
        }

        public void SetByValue(int value)
        {
            for (int i = 0; i < _input.options.Count; i++)
            {
                if (((MakerDropdownOption)_input.options[i]).value == value)
                {
                    _input.SetValueWithoutNotify(i);
                    return;
                }
            }
        }

        public void SetByName(string name)
        {
            for (int i = 0; i < _input.options.Count; i++)
            {
                if (((MakerDropdownOption)_input.options[i]).text == name)
                {
                    _input.SetValueWithoutNotify(i);
                    return;
                }
            }
        }

        public string GetName(int value)
        {
            for (int i = 0; i < _input.options.Count; i++)
            {
                if (((MakerDropdownOption)_input.options[i]).value == value)
                {
                    return ((MakerDropdownOption)_input.options[i]).text;
                }
            }
            return "";
        }
    }
}
