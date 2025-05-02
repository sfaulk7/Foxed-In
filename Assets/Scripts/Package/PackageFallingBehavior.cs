using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

public class PackageFallingBehavior : MonoBehaviour
{
    private bool _isFalling;
    Rigidbody PackageRigidBody;
    private float _extraGravity = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        _isFalling = true;
        PackageRigidBody = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        Vector3 CollidedsPosition = collision.gameObject.transform.position;
        if (CollidedsPosition.y < transform.position.y && CollidedsPosition.x == transform.position.x)
        {
            if (collision.gameObject.TryGetComponent(out PackageFallingBehavior PackageFallingBehavior))
            {
                _isFalling = false;
            }
        }
    }
    private void OnCollisionStay(Collision collision)
    {
        Vector3 CollidedsPosition = collision.gameObject.transform.position;
        if (CollidedsPosition.y < transform.position.y && CollidedsPosition.x == transform.position.x)
        {
            if (collision.gameObject.TryGetComponent(out PackageFallingBehavior PackageFallingBehavior))
            {
                _isFalling = false;
            }
        }
    }

    private void Update()
    {
        //Stop movement when no longer falling
        if (_isFalling == false)
        {
            PackageRigidBody.useGravity = false;
            PackageRigidBody.velocity = new Vector3(0, 0, 0);
        }

        //Make fall when above 0
        if (transform.position.y > 0)
        {
            _isFalling = true;
            PackageRigidBody.velocity += new Vector3(0, -_extraGravity, 0);
        }

        //Set falling to false when at or somehow below 0
        else if (transform.position.y <= 0)
        {
            _isFalling = false;
            //If below 0 set position to be 0
            if (transform.position.y < 0)
            {
                transform.position = new Vector3(transform.position.x, 0, transform.position.z);
            }
        }

        //If it should be falling enable gravity
        if (_isFalling == true)
        {
            PackageRigidBody.useGravity = true;
        }
    }
}
