# Bit34 MakerPanels

The UI parts an in-game level editor is built from: a panel of labelled
fields, and a list of things to pick from. Both are Unity views a consumer
subclasses; neither knows what it is editing.

## A panel is a field list

A property panel **is** a field list, it does not own one. A panel view
derives from `MakerFieldListView` and calls `AddInt2dField` on itself, and
anything it needs beside its fields goes **into** the list, as a field — a
button is a field, a dropdown is a field.

The shape this replaces is a panel view holding a `[SerializeField]`
reference to a separate field list: two types, two prefabs and a layer of
forwarding methods for every panel, with nothing gained. The collapse into
one type is deliberate.

## What is in it

- **`MakerFieldListView`** — `AddBoolField`, `AddButtonField`,
  `AddDropdownField`, `AddEnumField`, `AddIntField`, `AddInt2dField`,
  `AddInt3dField`, `AddStringField`, each returning the field's index, and a
  `Set…` for each kind whose value can change afterwards. The list lays
  itself out and reports its height.
- **`MakerListView`** and `MakerListItem` — the pick-one-of-many half:
  add, select, relabel, clear.
- **`MakerFieldListMediator`** and `MakerListMediator` — mediator bases with
  the plumbing done and the decisions abstract. `OnViewItemSelected` is
  abstract rather than a virtual that throws, because a base class cannot
  know what selecting an item means and the compiler should be the one to
  ask.
- **`MakerPanelAssets`** — one scene component holding the eight field
  prefabs and the list item prefab. A view finds it with
  `GetComponentInParent`, inactive panels included, so the prefabs are filled
  in once per maker screen instead of once per panel.

## Wiring

None of its own. The package binds no views and has no `AddBindings`: a
consumer subclasses the two views and the two mediator bases and binds those
pairs, because the mediator is where the meaning is.

## Dependencies

`com.bit34games.presenter` for the view and mediator bases (which brings
`com.bit34games.director` and `com.bit34games.injector`).

Everything in it derives from a Unity class, so all of it lives in
`MakerPanels.Unity`, with the view and mediator interfaces in
`MakerPanels.Views`.
