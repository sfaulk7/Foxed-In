using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float _acceleration = 5.0f;

    [SerializeField]
    private float _maxSpeed = 20.0f;

    [SerializeField]
    private float _jumpPower = 10.0f;

    [SerializeField]
    private GameObject _tailSwipeHitbox;

    private bool _facingRight = true;

    private Rigidbody _rigidbody;
    private float _moveInput;

    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        _moveInput = Input.GetAxisRaw("Horizontal");
        if (_moveInput == 1)
        {
            _facingRight = true;
        }
        else if (_moveInput == -1)
        {
            _facingRight = false;
        }

        //W key
        if (Input.GetKeyDown(KeyCode.W))
        {
            Jump();
        }
        //Left Click
        if (Input.GetMouseButtonDown(0))
        {
            TailSwipe();
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 deltaMovement = new Vector3();
        deltaMovement.x = _moveInput * _acceleration;
        _rigidbody.AddForce(deltaMovement * _acceleration * Time.fixedDeltaTime, ForceMode.VelocityChange);

        Vector3 newVelocity = _rigidbody.velocity;
        newVelocity.x = Mathf.Clamp(newVelocity.x, -_maxSpeed, _maxSpeed);
        _rigidbody.velocity = newVelocity;
    }

    void Jump()
    {
        _rigidbody.AddForce(Vector3.up * _jumpPower, ForceMode.Impulse);
    }

    void TailSwipe()
    {
        Vector3 tailSwipePosition;
        if (_facingRight == true)
        {
            tailSwipePosition = transform.position + (transform.right);
        }
        else
        {
            tailSwipePosition = transform.position - (transform.right);
        }
        GameObject tailSwipeHitbox = Instantiate(_tailSwipeHitbox, tailSwipePosition, Quaternion.identity);
    }

    void Bite()
    {
        
    }
}