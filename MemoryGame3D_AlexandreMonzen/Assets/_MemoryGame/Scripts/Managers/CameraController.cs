using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class CameraController : MonoBehaviour
{
    [SerializeField] private List<GameObject> _cameras;

    private GameController _gameController;

    private void Awake()
    {
        _gameController = FindObjectOfType<GameController>();
    }

    private void OnEnable()
    {
        _gameController.GameWasStarted += () => ActiveCamera(1);
        _gameController.SendToMenuWasRequested += () => ActiveCamera(0);
    }

    private void OnDisable()
    {
        _gameController.GameWasStarted -= () => ActiveCamera(1);
        _gameController.SendToMenuWasRequested -= () => ActiveCamera(0);
    }

    private void ActiveCamera(int index)
    {
        foreach (GameObject camera in _cameras)
        {
            camera.SetActive(false);
        }

        _cameras[index].SetActive(true);
    }

}
