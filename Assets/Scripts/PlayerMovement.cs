using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private InputActionAsset _playerActions;
    private InputAction _accelerateAction;
    private InputAction _turnAction;

    private Rigidbody _rb;

    [SerializeField] private float _accelerationForce;
    [SerializeField] private float _maxVelocity;
    [SerializeField] private float _turnForce;
    [SerializeField] private float _maxAngularVelocity;
    [SerializeField] private int _boostDuration;

    private bool _isBoosting;
    private CancellationTokenSource _boostTokenSource;

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
        _rb.AddForce(Time.deltaTime * _accelerationForce * dir * transform.forward);

        if (!_isBoosting && _rb.velocity.magnitude > _maxVelocity)
        {
            _rb.velocity = _maxVelocity * _rb.velocity.normalized;
        }
    }

    private void Turn(float dir)
    {
        float forwardScalar = Mathf.RoundToInt(Vector3.Dot(transform.forward, _rb.velocity));
        if (forwardScalar == 0)
        {
            return;
        }

        forwardScalar /= Mathf.Abs(forwardScalar);

        _rb.AddTorque(Time.deltaTime * _turnForce * dir * forwardScalar * Vector3.up);

        if (_rb.angularVelocity.magnitude > _maxAngularVelocity)
        {
            _rb.angularVelocity = _maxAngularVelocity * _rb.angularVelocity.normalized;
        }
    }

    public async void Boost()
    {
        _isBoosting = true;

        if (_boostTokenSource != null)
        {
            _boostTokenSource.Cancel();
            _boostTokenSource.Dispose();
        }

        _boostTokenSource = new();

        try
        {
            await Task.Delay(_boostDuration, _boostTokenSource.Token);
            _isBoosting = false;
        }
        catch { }
    }

    private void OnDestroy()
    {
        _accelerateAction?.Disable();
        _turnAction?.Disable();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (_rb != null)
        {
            _rb.constraints = RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
        }
    }
}
