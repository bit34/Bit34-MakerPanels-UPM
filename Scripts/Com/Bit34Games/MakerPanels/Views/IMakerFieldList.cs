using System;


namespace Com.Bit34Games.MakerPanels.Views
{
    public interface IMakerFieldList
    {
        //  MEMBERS
        int   FieldCount  { get; }
        float TotalHeight { get; }


        //  METHODS
        void HideFields();
        void ShowFields();
        int AddLabelField(string name);
        int AddButtonField(string name, Action onClick);
        int AddBoolField(string name, bool value, Action<bool> onValueChange);
        int AddIntField(string name, int value, bool isReadonly, Action<int> onValueChange);
        int AddInt2dField(string name, int xValue, int yValue, bool isReadonly, Action<int, int> onValueChange);
        int AddInt3dField(string name, int xValue, int yValue, int zValue, bool isReadonly, Action<int, int, int> onValueChange);
        int AddStringField(string name, string value, bool isReadonly, Action<string> onValueChange);
        int AddEnumField<TEnum>(string name, bool isReadonly, Action<TEnum> onValueChange) where TEnum : Enum;
        int AddDropdownField(string name, bool isReadonly, Action<int> onValueChange);
        void ShowFieldRemoveButton(int index);
        void ShowFieldRemoveButton(int index, Action onRemoveClicked);
        void SetBoolField(int index, bool value);
        void SetIntField(int index, int value);
        void SetInt2dField(int index, int xValue, int yValue);
        void SetInt3dField(int index, int xValue, int yValue, int zValue);
        void SetStringField(int index, string value);
        void SetEnumField<TEnum>(int index, TEnum value) where TEnum : Enum;
        void SetDropdownOptions(int index, string[] names, int[] values);
        void SetDropdownField(int index, int value);
        void SetDropdownField(int index, string name);
        string GetDropdownOptionName(int index, int value);
        void RemoveField(int index);
        void RemoveLastField();
    }
}
