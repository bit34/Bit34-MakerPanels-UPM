using System;
using TMPro;
using UnityEngine;


namespace Com.Bit34Games.MakerPanels.Unity
{
    //  One string on a row.
    //
    //  It commits on end-edit rather than on every keystroke, which matters
    //  more here than for the number fields: typing a name reports every
    //  prefix of it, and the empty string in between. Behind an undo stack
    //  that is one entry per character, and for a field whose value is a key
    //  somewhere it is a stream of values that were never meant.
    public class MakerStringField : MakerField
    {
        //  MEMBERS
        //      For Editor
        [SerializeField] private TMP_InputField _input;


        //  METHODS
        public void Initialize(string label, string value, bool isReadonly, Action<string> onValueChange)
        {
            BaseInitialize(label);
            _input.text = value;
            _input.readOnly = isReadonly;
            if (onValueChange != null)
            {
                _input.onEndEdit.AddListener((string newValue) => { onValueChange(newValue); });
            }
        }

        public void SetValue(string value)
        {
            _input.SetTextWithoutNotify(value);
        }
    }
}
