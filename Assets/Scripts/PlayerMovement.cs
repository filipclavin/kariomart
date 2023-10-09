using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private InputActionAsset _playerActions;
    private InputAction _accelerateAction;
    private InputAction _turnAction;

    private Rigidbody _rb;

    [SerializeField] private float _speed;
    [SerializeField] private float _turnSpeed;


    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _accelerateAction = _playerActions.FindActionMap("Car").FindAction("Accelerate");
        _turnAction = _playerActions.FindActionMap("Car").FindAction("Turn");

        _accelerateAction.Enable();
        _turnAction.Enable();
    }

    private void Update()
    {
        float accDir = _accelerateAction.ReadValue<float>();

        if (accDir != 0)
        {
            Accelerate(accDir);
        }

        float turnDir = _turnAction.ReadValue<float>();
        if (turnDir != 0)
        {
            Turn(turnDir);
        }
    }

    private void Accelerate(float dir)
    {
        _rb.AddForce(_speed * dir * transform.forward);
    }

    private void Turn(float dir)
    {
        _rb.AddTorque(_turnSpeed * (dir < 0 ? Vector3.down : Vector3.up));
    }

    private void OnDestroy()
    {
        _accelerateAction.Disable();
        _turnAction.Disable();
    }
}
