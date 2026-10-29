using System;
using TMPro;
using UnityEngine;


namespace Com.Bit34Games.MakerPanels.Unity
{
    //  Two ints on one row. A cell, in practice.
    //
    //  Commits on end-edit and parses defensively, like the other two number
    //  fields — MakerInt3dField says why at length.
    public class MakerInt2dField : MakerField
    {
        //  MEMBERS
        //      For Editor
        [SerializeField] private TMP_InputField _inputX;
        [SerializeField] private TMP_InputField _inputY;


        //  METHODS
        public void Initialize(string label, int xValue, int yValue, bool isReadonly, Action<int, int> onValueChange)
        {
            BaseInitialize(label);

            Setup(_inputX, xValue, isReadonly);
            Setup(_inputY, yValue, isReadonly);

            if (onValueChange != null)
            {
                _inputX.onEndEdit.AddListener((string newValue) => { Report(onValueChange); });
                _inputY.onEndEdit.AddListener((string newValue) => { Report(onValueChange); });
            }
        }

        public void SetValues(int xValue, int yValue)
        {
            _inputX.SetTextWithoutNotify(xValue.ToString());
            _inputY.SetTextWithoutNotify(yValue.ToString());
        }

        private void Setup(TMP_InputField input, int value, bool isReadonly)
        {
            input.text        = value.ToString();
            input.readOnly    = isReadonly;
            input.contentType = TMP_InputField.ContentType.IntegerNumber;
        }

        private void Report(Action<int, int> onValueChange)
        {
            onValueChange(Read(_inputX), Read(_inputY));
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
