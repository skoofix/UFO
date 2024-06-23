using UnityEngine;

public class Engine : MonoBehaviour
{
    [SerializeField] private LayerMask _layerMask;
    [SerializeField] private float _spherecastRadius;
    [SerializeField] private float _maxDistance;

    [SerializeField] private float _maxForce;
    [SerializeField] private float _damping;

    private Rigidbody _rigidbody;
    private Transform _transform;
    private float _springSpeed;
    private float _lastDistance;

    [SerializeField] private float _altitude;

    public void Initialize(Rigidbody rigidbody)
    {
        _transform = transform;
        _rigidbody = rigidbody;
    }

    private void FixedUpdate()
    {
        if (_rigidbody == null)
            return;

        var forward = transform.forward;

        Lift(forward);
    }

    private void Lift(Vector3 forward)
    {
        if (Physics.SphereCast(_transform.position, _spherecastRadius, forward, out RaycastHit hitInfo, _maxDistance, _layerMask, QueryTriggerInteraction.Ignore))
        {
            var distance = hitInfo.distance;

            _springSpeed = (distance - _lastDistance) * Time.fixedTime;
            _springSpeed = Mathf.Max(_springSpeed, 0);
            _lastDistance = distance;
            
            var minForceHeight = _altitude + 1f;
            var maxForceHeight = _altitude - 1f;
            distance = Mathf.Clamp(distance, maxForceHeight, minForceHeight);

            var forceFactor = distance.Remap(maxForceHeight, minForceHeight, _maxForce, 0);

            _rigidbody.AddForce(-forward * Mathf.Max(forceFactor - _springSpeed * _maxForce * _damping, 0), ForceMode.Force);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;

        var startPoint = transform.position;
        var endPoint = transform.position + transform.forward * _maxDistance;

        Gizmos.DrawWireCube(startPoint, Vector3.one * 0.2f);
        Gizmos.DrawLine(startPoint, endPoint);
        Gizmos.DrawSphere(endPoint, _spherecastRadius);
    }
}