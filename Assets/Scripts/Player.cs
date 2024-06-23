using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private PlayerInput _playerInput;
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private Engine _engine;
    private Transform _transform;

    private void Awake()
    {
        _transform = transform;
        _engine.Initialize(_rigidbody);
    }


}