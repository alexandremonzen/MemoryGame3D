using System.Collections;

using UnityEngine;

public sealed class RotateAroundAxis : MonoBehaviour
{
    [Header("Rotation Axis")]
    [SerializeField] private Vector3 _rotationVector = new Vector3(0f, 1f, 0f);
    [SerializeField] private float _speed;

    private void OnEnable()
    {
        StartCoroutine(Rotate());
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }

    private IEnumerator Rotate()
    {
        while(true)
        {
            transform.Rotate(_rotationVector * _speed * Time.deltaTime);
            yield return null;
        }
    }
}