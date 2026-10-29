# Change Log

## [0.1.0] - 2026-10-29

First release. The package was written inside a Unity project and comes out as
it stood there; this entry says what it is rather than what changed.

### Added
 - **`MakerFieldListView`** — a property panel, which *is* a field list rather than owning one. `AddLabelField`, `AddButtonField`, `AddBoolField`, `AddIntField`, `AddInt2dField`, `AddInt3dField`, `AddStringField`, `AddEnumField<TEnum>` and `AddDropdownField`, each returning the field's index, with a `Set…` for every kind whose value can change afterwards. The list lays itself out and reports its height.
 - `ShowFieldRemoveButton`, `RemoveField` and `RemoveLastField` — for a panel whose fields come and go, such as one row per authored rule.
 - `HideFields` / `ShowFields` — a panel with nothing selected shows nothing.
 - **`MakerListView`** and `MakerListItem` — the pick-one-of-many half: add, select, relabel, remove, clear. The selection callback is per item.
 - **`MakerFieldListMediator`** and `MakerListMediator` — mediator bases with the plumbing done and the decisions abstract. `OnViewItemSelected` is abstract rather than a virtual that throws, because a base class cannot know what selecting an item means and the compiler should be the one to ask.
 - **`MakerPanelAssets`** — one scene component holding the field prefabs and the list item prefab. A view finds it with `GetComponentInParent`, inactive panels included, so the prefabs are filled in once per maker screen rather than once per panel. It walks up the hierarchy and throws when there is none, which is why it reaches a panel inside a popup too.
 - Fifteen prefabs, one per field kind plus the list parts.

### Notes
 - No bindings of its own and no `AddBindings`: a consumer subclasses the two views and the two mediator bases and binds those pairs, because the mediator is where the meaning is.
 - Everything here derives from a Unity class, so there is no assembly to compile and the package ships its C# as plain source.
 - Depends on `com.bit34games.presenter` for the view and mediator bases, and through it on `com.bit34games.director` and `com.bit34games.injector`.
