using System;
using TMPro;
using UnityEngine;


namespace Com.Bit34Games.MakerPanels.Unity
{
    //  Three ints on one row. A position, in practice.
    //
    //  Two things worth stating, which the other fields now share and which
    //  this one had first:
    //
    //  It commits on end-edit, not on every keystroke. onValueChanged fires
    //  per character — typing "12" reports 1 and then 12 — which behind an
    //  undo stack is one journal entry per character.
    //
    //  And it parses defensively. Mid-typing a field can hold "", "-" or
    //  "+", none of which int.Parse accepts, and a field that was cleared
    //  holds "" at end-edit too; a value that is not a number reads as 0.
    public class MakerInt3dField : MakerField
    {
        //  MEMBERS
        //      For Editor
        [SerializeField] private TMP_InputField _inputX;
        [SerializeField] private TMP_InputField _inputY;
        [SerializeField] private TMP_InputField _inputZ;


        //  METHODS
        public void Initialize(string label, int xValue, int yValue, int zValue, bool isReadonly, Action<int, int, int> onValueChange)
        {
            BaseInitialize(label);

            Setup(_inputX, xValue, isReadonly);
            Setup(_inputY, yValue, isReadonly);
            Setup(_inputZ, zValue, isReadonly);

            if (onValueChange != null)
            {
                _inputX.onEndEdit.AddListener((string newValue) => { Report(onValueChange); });
                _inputY.onEndEdit.AddListener((string newValue) => { Report(onValueChange); });
                _inputZ.onEndEdit.AddListener((string newValue) => { Report(onValueChange); });
            }
        }

        public void SetValues(int xValue, int yValue, int zValue)
        {
            _inputX.SetTextWithoutNotify(xValue.ToString());
            _inputY.SetTextWithoutNotify(yValue.ToString());
            _inputZ.SetTextWithoutNotify(zValue.ToString());
        }

        private void Setup(TMP_InputField input, int value, bool isReadonly)
        {
            input.text        = value.ToString();
            input.readOnly    = isReadonly;
            input.contentType = TMP_InputField.ContentType.IntegerNumber;
        }

        private void Report(Action<int, int, int> onValueChange)
        {
            onValueChange(Read(_inputX), Read(_inputY), Read(_inputZ));
        }

        private int Read(TMP_InputField input)
        {
            if (int.TryParse(input.text, out int value))
            {
                return value;
            }

            return 0;
        }
    }
}
