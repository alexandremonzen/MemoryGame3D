using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(SetTextureArray2D))]
public class MemoryCardEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        SetTextureArray2D setTextureArray2D = (SetTextureArray2D)target;

        EditorGUI.BeginChangeCheck();

        if (EditorGUI.EndChangeCheck())
            SetValidSliceIndexValue(setTextureArray2D, true);

        if (GUILayout.Button("Preview Texture"))
        {
            SetValidSliceIndexValue(setTextureArray2D, false);
            setTextureArray2D.SetSliceFromTextureArray();
        }
    }

    private void SetValidSliceIndexValue(SetTextureArray2D setTextureArray2D, bool warning)
    {
        setTextureArray2D.SliceIndex = Mathf.Clamp(setTextureArray2D.SliceIndex, 0, setTextureArray2D.TextureArray.depth - 1);
        if (warning) Debug.LogWarning($"{setTextureArray2D} - Slice index out of bounds, setting to a valid value");
    }
}