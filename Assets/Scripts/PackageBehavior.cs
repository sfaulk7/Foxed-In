using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

public class PackageBehavior : MonoBehaviour
{
    [SerializeField]
    private float _startingfallSpeed;

    private float _currentFallSpeed;
    private bool _isFalling;
    private bool _shouldFall;
    private bool _invoked;

    private bool _packageAbove = false;
    private bool _packageBelow = false;
    private bool _packageToRight = false;
    private bool _packageToLeft = false;

    // Start is called before the first frame update
    void Start()
    {
        _currentFallSpeed = _startingfallSpeed;
        _isFalling = true;
        _shouldFall = true;
    }

    private void Update()
    {
        if (transform.position.y == 0)
        {
            _isFalling = false;
        }
    }

    void Fall()
    {
        transform.Translate(new Vector3(0, -5, 0));
    }

    void ShouldFallActivate()
    {
        _shouldFall = true;
        _invoked = false;
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.TryGetComponent(out PackageBehavior PackageBehavior))
        {
            //If colliding with a package below
            if (collision.gameObject.transform.position.y < transform.position.y)
            {
                //If colliding with a package directly below
                if (collision.gameObject.transform.position.x == transform.position.x)
                {
                    _packageBelow = true;
                    _isFalling = false;
                }
                //If there is no package directly below
                else
                {
                    _packageBelow = false;
                }
            }

            //If colliding with a package above
            if (collision.gameObject.transform.position.y > transform.position.y)
            {
                //If colliding with a package directly above
                if (collision.gameObject.transform.position.x == transform.position.x)
                {
                    _packageAbove = true;
                }
                //If there is no package directly Above
                else
                {
                    _packageAbove = false;
                }
            }

            //If colliding with a package to the left
            if (collision.gameObject.transform.position.x < transform.position.x - 1)
            {
                //If colliding with a package directly to the left
                if (collision.gameObject.transform.position.y == transform.position.y)
                {
                    _packageToLeft = true;
                }
                //If there is no package directly to the left
                else
                {
                    _packageToLeft = false;
                }
            }

            //If colliding with a package to the right
            if (collision.gameObject.transform.position.x > transform.position.x + 1)
            {
                //If colliding with a package directly to the right
                if (collision.gameObject.transform.position.y == transform.position.y)
                {
                    _packageToRight = true;
                }
                //If there is no package directly to the right
                else
                {
                    _packageToRight = false;
                }
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent(out TailSwipeClearer TailSwipeClearer))
        {
            //Hitting to the right
            if (collision.gameObject.transform.position.x < transform.position.x)
            {
                if (_packageToRight == false && transform.position.x <= 45)
                {
                    transform.Translate(new Vector3(5, 0, 0));
                }
            }
            //Hitting to the left
            else
            {
                if (_packageToLeft == false && transform.position.x >= 5)
                {
                    transform.Translate(new Vector3(-5, 0, 0));
                }
            }
        }
    }

    private void LateUpdate()
    { 
        if (_shouldFall)
        {
            //call the Fall funciton if all checks pass
            if (_isFalling)
            {
                Fall();
            }

            _shouldFall = false;

            //Start the countdown until 
            if (_shouldFall == false && _invoked == false)
            {
                Invoke(nameof(ShouldFallActivate), _currentFallSpeed);
                _invoked = true;
            }
        }
    }
}
