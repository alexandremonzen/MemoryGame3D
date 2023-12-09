using UnityEngine;
using UnityEngine.InputSystem;

public sealed class RaycastMouse : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    private Vector3 _mousePositionVector;

    private PlayerInputActions _playerInputActions;
    private InputAction _mouseInputAction;

    private void Awake()
    {
        _playerInputActions = new PlayerInputActions();

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        if(!_camera)
            _camera = Camera.main;
    }

    private void OnEnable()
    {
        _playerInputActions.GeneralActionMap.Mouse.Enable();
        _playerInputActions.GeneralActionMap.MouseLeftClick.Enable();

        _mouseInputAction = _playerInputActions.GeneralActionMap.Mouse;
        _playerInputActions.GeneralActionMap.MouseLeftClick.performed += PerformedLeftClick;
    }

    private void OnDisable()
    {
        _playerInputActions.GeneralActionMap.Mouse.Disable();
        _playerInputActions.GeneralActionMap.MouseLeftClick.performed -= PerformedLeftClick;
        _playerInputActions.GeneralActionMap.MouseLeftClick.Disable();
    }

    private void PerformedLeftClick(InputAction.CallbackContext obj)
    {
        UpdateMousePosition();
        
        Ray ray = _camera.ScreenPointToRay(_mousePositionVector);
        if (!Physics.Raycast(ray, out RaycastHit hitInfo))
            return;
        
        IInteractableCursor interactableCursor = hitInfo.collider.gameObject.GetComponent<IInteractableCursor>();
        if (interactableCursor != null)
        {
            interactableCursor.InteractCursor();
        }
    }

    private void UpdateMousePosition()
    {
        _mousePositionVector = _mouseInputAction.ReadValue<Vector2>();
    }
}
