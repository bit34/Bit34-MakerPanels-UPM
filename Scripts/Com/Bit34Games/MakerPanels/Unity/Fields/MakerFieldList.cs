using System;
using System.Collections.Generic;
using Com.Bit34Games.MakerPanels.Views;
using UnityEngine;

namespace Com.Bit34Games.MakerPanels.Unity
{
    //  A column of value fields, and nothing more: it makes them, stacks
    //  them by their own heights, and reaches them again by the index it
    //  handed back when each was added.
    //
    //  Deliberately not a PresenterView, which is what separates it from
    //  MakerFieldListView. A view is mediated and has to be opened by the
    //  presenter, so it can only be a screen or a panel; this can be
    //  dropped anywhere fields are wanted, including inside another view.
    //
    //  The one thing it needs from above it is a MakerPanelAssets to find,
    //  for the nine field prefabs.
    public class MakerFieldList : MonoBehaviour,
                                  IMakerFieldList
    {
        //  MEMBERS
        public int   FieldCount  => _fields.Count;
        //  What the fields add up to, for whoever has to be as tall as them:
        //  a list inside a row cannot leave its own height to a layout group
        //  it does not have.
        public float TotalHeight => _totalHeight;
        //      For Editor
        //  The transform stays: it is this list's own hierarchy. The field
        //  prefabs do not — the same nine serve every list, and each panel
        //  used to carry its own copy of the references.
        [SerializeField] private RectTransform _fieldTransform;
        //      Private
        private MakerPanelAssets           _panelAssets;
        private List<MakerField>           _fields;
        private float                      _totalHeight;


        //  METHODS
        public void Initialize()
        {
            _panelAssets  = MakerPanelAssets.FindFor(this);
            _fields       = new List<MakerField>();
            _totalHeight  = 0;
        }
        public void HideFields()
        {
            _fieldTransform.gameObject.SetActive(false);
        }

        public void ShowFields()
        {
            _fieldTransform.gameObject.SetActive(true);
        }

        //  A heading rather than a value. The panel assets carry its prefab
        //  like every other field's.
        public int AddLabelField(string name)
        {
            GameObject      fieldGO = Instantiate(_panelAssets.LabelFieldPrefab, _fieldTransform);
            MakerLabelField field   = fieldGO.GetComponent<MakerLabelField>();
            field.Initialize(name);
            return AddField(field);
        }

        public int AddButtonField(string name, Action onClick)
        {
            GameObject       fieldGO = Instantiate(_panelAssets.ButtonFieldPrefab, _fieldTransform);
            MakerButtonField field   = fieldGO.GetComponent<MakerButtonField>();
            field.Initialize(name, onClick);
            return AddField(field);
        }

        public int AddBoolField(string name, bool value, Action<bool> onValueChange)
        {
            GameObject     fieldGO = Instantiate(_panelAssets.BoolFieldPrefab, _fieldTransform);
            MakerBoolField field   = fieldGO.GetComponent<MakerBoolField>();
            field.Initialize(name, value, onValueChange);
            return AddField(field);
        }

        public int AddIntField(string name, int value, bool isReadonly, Action<int> onValueChange)
        {
            GameObject    fieldGO = Instantiate(_panelAssets.IntFieldPrefab, _fieldTransform);
            MakerIntField field   = fieldGO.GetComponent<MakerIntField>();
            field.Initialize(name, value, isReadonly, onValueChange);
            return AddField(field);
        }

        public int AddInt2dField(string name, int xValue, int yValue, bool isReadonly, Action<int, int> onValueChange)
        {
            GameObject      fieldGO = Instantiate(_panelAssets.Int2dFieldPrefab, _fieldTransform);
            MakerInt2dField field   = fieldGO.GetComponent<MakerInt2dField>();
            field.Initialize(name, xValue, yValue, isReadonly, onValueChange);
            return AddField(field);
        }

        public int AddInt3dField(string name, int xValue, int yValue, int zValue, bool isReadonly, Action<int, int, int> onValueChange)
        {
            GameObject      fieldGO = Instantiate(_panelAssets.Int3dFieldPrefab, _fieldTransform);
            MakerInt3dField field   = fieldGO.GetComponent<MakerInt3dField>();
            field.Initialize(name, xValue, yValue, zValue, isReadonly, onValueChange);
            return AddField(field);
        }

        public int AddStringField(string name, string value, bool isReadonly, Action<string> onValueChange)
        {
            GameObject       fieldGO = Instantiate(_panelAssets.StringFieldPrefab, _fieldTransform);
            MakerStringField field   = fieldGO.GetComponent<MakerStringField>();
            field.Initialize(name, value, isReadonly, onValueChange);
            return AddField(field);
        }

        public int AddEnumField<TEnum>(string name, bool isReadonly, Action<TEnum> onValueChange) where TEnum : Enum
        {
            GameObject     fieldGO = Instantiate(_panelAssets.EnumFieldPrefab, _fieldTransform);
            MakerEnumField field   = fieldGO.GetComponent<MakerEnumField>();
            field.Initialize<TEnum>(name, isReadonly, onValueChange);
            return AddField(field);
        }

        public int AddDropdownField(string name, bool isReadonly, Action<int> onValueChange)
        {
            GameObject         fieldGO = Instantiate(_panelAssets.DropdownFieldPrefab, _fieldTransform);
            MakerDropdownField field   = fieldGO.GetComponent<MakerDropdownField>();
            field.Initialize(name, isReadonly, onValueChange);
            return AddField(field);
        }

        //  The field is asked where it is when the click comes, rather than
        //  the index being captured here: removing anything above it shifts
        //  it down, and a captured index would then name a different field
        //  — or none, once the list is shorter than it.
        //
        //  Which is what the MakerField the callback carries is for.
        public void ShowFieldRemoveButton(int index)
        {
            _fields[index].SetRemoveButton((MakerField field) => { RemoveField(_fields.IndexOf(field)); });
        }

        //  For a list whose fields stand for something outside it. Taking a
        //  behaviour's reaction off is a command and the panel is rebuilt
        //  from what that leaves, so a list that deleted its own field would
        //  be answering for a model it cannot see.
        public void ShowFieldRemoveButton(int index, Action onRemoveClicked)
        {
            _fields[index].SetRemoveButton((MakerField field) => { onRemoveClicked(); });
        }

        public void SetBoolField(int index, bool value)
        {
            ((MakerBoolField)_fields[index]).SetValue(value);
        }

        public void SetIntField(int index, int value)
        {
            ((MakerIntField)_fields[index]).SetValue(value);
        }

        public void SetInt2dField(int index, int xValue, int yValue)
        {
            ((MakerInt2dField)_fields[index]).SetValues(xValue, yValue);
        }

        public void SetInt3dField(int index, int xValue, int yValue, int zValue)
        {
            ((MakerInt3dField)_fields[index]).SetValues(xValue, yValue, zValue);
        }

        public void SetStringField(int index, string value)
        {
            ((MakerStringField)_fields[index]).SetValue(value);
        }

        public void SetEnumField<TEnum>(int index, TEnum value) where TEnum : Enum
        {
            ((MakerEnumField)_fields[index]).SetValue<TEnum>(value);
        }

        public void SetDropdownOptions(int index, string[] names, int[] values)
        {
            ((MakerDropdownField)_fields[index]).SetOptions(names, values);
        }

        public void SetDropdownField(int index, int value)
        {
            ((MakerDropdownField)_fields[index]).SetByValue(value);
        }

        public void SetDropdownField(int index, string name)
        {
            ((MakerDropdownField)_fields[index]).SetByName(name);
        }

        public string GetDropdownOptionName(int index, int value)
        {
            return ((MakerDropdownField)_fields[index]).GetName(value);
        }

        public void RemoveField(int index)
        {
            Destroy(_fields[index].gameObject);
            _fields.RemoveAt(index);
            Relayout();
        }

        public void RemoveLastField()
        {
            int lastFieldIndex = _fields.Count - 1;
            Destroy(_fields[lastFieldIndex].gameObject);
            _fields.RemoveAt(lastFieldIndex);
            Relayout();
        }

        //  Returns the index the Set methods above take: a field's place in
        //  the column is its name for the rest of the panel's life, which is
        //  why every Add hands one back and the property list views keep
        //  them in fields.
        private int AddField(MakerField field)
        {
            ((RectTransform)field.transform).anchoredPosition = new Vector2(0, -_totalHeight);
            _totalHeight += field.Height;
            _fields.Add(field);
            Resize();
            return _fields.Count - 1;
        }

        //  Every field placed again from the top, because a removal moves
        //  everything under it. The one body both removals share: they
        //  differ in which field goes, not in what is left.
        private void Relayout()
        {
            _totalHeight = 0;
            for (int i = 0; i < _fields.Count; i++)
            {
                MakerField field = _fields[i];
                ((RectTransform)field.transform).anchoredPosition = new Vector2(0, -_totalHeight);
                _totalHeight += field.Height;
            }

            Resize();
        }

        //  The transform the fields hang from is a scroll view's content, so
        //  it has to be told how tall they came to — otherwise there is
        //  nothing to scroll and the fields past the bottom cannot be
        //  reached. MakerList sizes its own container for the same reason.
        private void Resize()
        {
            _fieldTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, _totalHeight);
        }
    }
}