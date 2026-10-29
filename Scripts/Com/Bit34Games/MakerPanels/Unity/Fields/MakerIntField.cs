using System;
using TMPro;
using UnityEngine;


namespace Com.Bit34Games.MakerPanels.Unity
{
    //  One int on a row.
    //
    //  It commits on end-edit rather than on every keystroke, and parses
    //  defensively — see MakerInt3dField, which says why at length.
    public class MakerIntField : MakerField
    {
        //  MEMBERS
        //      For Editor
        [SerializeField] private TMP_InputField _input;


        //  METHODS
        public void Initialize(string label, int value, bool isReadonly, Action<int> onValueChange)
        {
            BaseInitialize(label);

            _input.text = value.ToString();
            _input.readOnly = isReadonly;
            _input.contentType = TMP_InputField.ContentType.IntegerNumber;
            if (onValueChange != null)
            {
                _input.onEndEdit.AddListener((string newValue) =>
                {
                    onValueChange(int.TryParse(_input.text, out int parsed) ? parsed : 0);
                });
            }
        }

        public void SetValue(int value)
        {
            _input.SetTextWithoutNotify(value.ToString());
        }
    }
}
