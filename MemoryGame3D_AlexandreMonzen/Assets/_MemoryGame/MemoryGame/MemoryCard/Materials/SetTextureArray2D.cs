using UnityEngine;

public sealed class SetTextureArray2D : MonoBehaviour
{
    [SerializeField] private Texture2DArray _textureArray;
    [SerializeField] private int _sliceIndex;
    [SerializeField] private Material _sharedMaterial;
    [SerializeField] private bool _previewTextureOnValidate = true;
    private Renderer _renderer;

    public int SliceIndex { get => _sliceIndex; set => _sliceIndex = value; }
    public Texture2DArray TextureArray { get => _textureArray; }

    private void Awake()
    {
        SetSliceFromTextureArray();
    }

    private void OnValidate()
    {
        if (_previewTextureOnValidate)
            SetSliceFromTextureArray();
    }

    private Texture2D GetSliceFromTextureArray(Texture2DArray textureArray, int sliceIndex)
    {
        int width = textureArray.width;
        int height = textureArray.height;

        Texture2D slicedTexture = new Texture2D(width, height);
        Color32[] pixels = textureArray.GetPixels32(sliceIndex, 0);
        slicedTexture.SetPixels32(pixels);

        slicedTexture.Apply();

        return slicedTexture;
    }

    public void SetSliceFromTextureArray(int sliceIndex = -1)
    {
        if (sliceIndex > -1)
            _sliceIndex = sliceIndex;

        if (_sliceIndex < 0 && _sliceIndex > _textureArray.depth)
        {
            Debug.LogError("Index out of bounds, please check the depth value from the 2D Array Texture");
            return;
        }

        if (_sharedMaterial)
        {
            _sharedMaterial.SetTexture("_BaseMap", GetSliceFromTextureArray(_textureArray, _sliceIndex));
            return;
        }

        _renderer = GetComponent<Renderer>();
        Material tempMaterial = new Material(_renderer.sharedMaterial);
        tempMaterial.SetTexture("_BaseMap", GetSliceFromTextureArray(_textureArray, _sliceIndex));
        _renderer.sharedMaterial = tempMaterial;
    }

}

