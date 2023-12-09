using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(MemoryCardsPlacement))]
public sealed class MemoryCardsPlacementEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        GUILayout.Space(20);

        MemoryCardsPlacement memoryCardsPlacement = (MemoryCardsPlacement)target;

        if (GUILayout.Button("Create Cards"))
        {
            memoryCardsPlacement.CreateMemoryCardsInEditor();
        }
    }
}