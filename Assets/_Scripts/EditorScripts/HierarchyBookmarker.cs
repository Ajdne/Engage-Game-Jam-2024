using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

/// <summary>
/// Put this script on an empty object in the scene.
/// It allows you to assign objects in the hierarchy to bookmarks.
/// </summary>
public class HierarchyBookmarker : MonoBehaviour
{
    public List<GameObject> Bookmarks = new List<GameObject>();
    public List<Color> ButtonColors = new List<Color>();
}

#if UNITY_EDITOR
[CustomEditor(typeof(HierarchyBookmarker)), CanEditMultipleObjects, ExecuteInEditMode]
public class HierarchyBookmarkerInspector : Editor
{
    HierarchyBookmarker SelectedHierarchyBookmarker;

    public override void OnInspectorGUI()
    {
        SelectedHierarchyBookmarker = (HierarchyBookmarker)target;

        base.OnInspectorGUI();

        GUILayout.Space(10f);

        GUILayout.Label("SELECT A BOOKMARK");

        ElementButtons();
    }

    private void ElementButtons()
    {
        if (SelectedHierarchyBookmarker.Bookmarks.Count < 1) return;

        int colorCounter = 0;

        for (int i = 0; i < SelectedHierarchyBookmarker.Bookmarks.Count; i++)
        {
            SwitchButtonColor(colorCounter);
            colorCounter++;

            if (colorCounter >= SelectedHierarchyBookmarker.ButtonColors.Count)
            {
                colorCounter = 0;
            }

            ElementButton(i);
        }

        GUI.color = Color.white;
    }

    private void ElementButton(int elementIndex)
    {
        bool elementButton = GUILayout.Button(SelectedHierarchyBookmarker.Bookmarks[elementIndex].name);

        if(elementButton)
        {
            Selection.activeObject = SelectedHierarchyBookmarker.Bookmarks[elementIndex];
        }
    }

    private void SwitchButtonColor(int colorIndex)
    {
        if (SelectedHierarchyBookmarker.ButtonColors.Count < 1) return;
        GUI.color = SelectedHierarchyBookmarker.ButtonColors[colorIndex];
    }
}
#endif
