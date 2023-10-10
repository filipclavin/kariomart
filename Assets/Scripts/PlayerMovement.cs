using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private InputActionAsset _playerActions;
    private InputAction _accelerateAction;
    private InputAction _turnAction;
    private InputAction _respawnAction;

    private Rigidbody _rb;

    [SerializeField] private float _speed;
    [SerializeField] private float _turnSpeed;
    
    private bool _isGrounded = true;
    private Vector3 _lastGroundedPos;
    private Quaternion _lastGroundedRot;

    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _accelerateAction = _playerActions.FindActionMap("Car").FindAction("Accelerate");
        _turnAction = _playerActions.FindActionMap("Car").FindAction("Turn");
        _respawnAction = _playerActions.FindActionMap("Car").FindAction("Respawn");

        _accelerateAction.Enable();
        _turnAction.Enable();
        _respawnAction.Enable();

        _respawnAction.performed += ctx =>
        {
            transform.position = _lastGroundedPos;
            transform.rotation = _lastGroundedRot;
            _rb.angularVelocity = Vector3.zero;
            _rb.velocity = Vector3.zero;
        };
    }

    private void Update()
    {
        if (!_isGrounded)
        {
            return;
        }

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
        float forwardScalar = Mathf.RoundToInt(Vector3.Dot(transform.forward, _rb.velocity));
        if (forwardScalar == 0)
        {
            return;
        }

        forwardScalar /= Mathf.Abs(forwardScalar);
        
        if (forwardScalar == 0)
        {
            return;
        }

        _rb.AddTorque(_turnSpeed * dir * forwardScalar * Vector3.up);
    }

    private void OnCollisionEnter(Collision collision)
    {
        string cName = collision.gameObject.name;

        if (cName.Contains("Barrier"))
        {
            _rb.angularVelocity = Vector3.zero;
            _rb.velocity *= 0.5f;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        _isGrounded = false;
    }

    private void OnCollisionStay(Collision collision)
    {
        if (!_isGrounded && transform.up.y > 0)
        {
             _isGrounded = true;
            _lastGroundedPos = transform.position;
            _lastGroundedRot = transform.rotation;
        }
    }

    private void OnDestroy()
    {
        _accelerateAction.Disable();
        _turnAction.Disable();
        _respawnAction.Disable();
    }
}
