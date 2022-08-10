using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class MovementSystem : MonoBehaviour
{
    private Rigidbody _playerRigidbody;
    [SerializeField]
    private float _moveSpeed;

    void Start()
    {
        Init();
    }

    void Init()
    {
        _playerRigidbody = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {

        var movementDirection =
            new Vector3(DynamicJoystick.Instance.Horizontal, 0, DynamicJoystick.Instance.Vertical);
        movementDirection.Normalize();

        _playerRigidbody.velocity = new Vector3(DynamicJoystick.Instance.Horizontal * _moveSpeed, _playerRigidbody.velocity.y,
            DynamicJoystick.Instance.Vertical * _moveSpeed);

        if (movementDirection != Vector3.zero)
        {
            var toRotation = Quaternion.LookRotation(movementDirection, Vector3.up);
            transform.rotation =
                Quaternion.RotateTowards(transform.rotation, toRotation, Time.deltaTime);

        }
    }
}
