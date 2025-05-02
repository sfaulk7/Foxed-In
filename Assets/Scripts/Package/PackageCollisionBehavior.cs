using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PackageCollisionBehavior : MonoBehaviour
{
    private bool _packageToRight = false;
    private bool _packageToLeft = false;

    [SerializeField]
    int _packagePointWorth;

    [SerializeField]
    int _packageMultiplierWorth;

    [SerializeField]
    int _packageHP = 5;
    private int _packageHPMaximum;

    [SerializeField]
    private Color WhenNoBoxes;

    [SerializeField]
    private Color WhenBoxOnLeft;

    [SerializeField]
    private Color WhenBoxOnRight;

    [SerializeField]
    private Color WhenBoxOnBoth;

    bool _wasDestroyed;
    bool _wasCleared;
    bool _scored;

    // Start is called before the first frame update
    void Start()
    {
        _packageHPMaximum = _packageHP;
    }

    public int PackagePointWorth()
    {
        return _packagePointWorth;
    }
    public int PackageMultiplierWorth()
    {
        return _packageMultiplierWorth;
    }
    public bool WasDestroyed()
    {
        return _wasDestroyed;
    }
    public bool WasCleared()
    {
        return _wasCleared;
    }
    public bool HasBeenScored()
    {
        return _scored;
    }
    public void SetScored(bool wasScored)
    {
        _scored = wasScored;
    }


    // Update is called once per frame
    void Update()
    {
        //If package is not in scene
        if (!transform.gameObject.activeInHierarchy)
        {
            _packageHP = _packageHPMaximum;
            if (_wasDestroyed == false)
            {
                _wasCleared = true;
            }
        }

        //If package is in scene
        if (transform.gameObject.activeInHierarchy)
        {
            //Set _wasDestryed and _wasCleared to false
            _wasDestroyed = false;
            _wasCleared = false;
            _scored = false;
        }

        //If the boxes HP ever hits 0
        if (_packageHP <= 0)
        {
            _wasDestroyed = true;

            //Send package back to pool
            transform.gameObject.SetActive(false);
            _packageHP = _packageHPMaximum;
        }

        //Left Cast
        if (Physics.Raycast(transform.position, new Vector3(-1, 0, 0), 5))
        {
            _packageToLeft = true;
        }
        else
        {
            _packageToLeft = false;
        }
        //Right Cast
        if (Physics.Raycast(transform.position, new Vector3(1, 0, 0), 5))
        {
            _packageToRight = true;
        }
        else
        {
            _packageToRight = false;
        }
        

        #region "DEBUGGING FOR PACKAGE ON PACKAGE COLLISION"
        ////If package on the left
        //if (_packageToLeft && !_packageToRight)
        //{
        //    GetComponent<MeshRenderer>().material.color = WhenBoxOnLeft;
        //}
        ////If package on the right
        //if (!_packageToLeft && _packageToRight)
        //{
        //    GetComponent<MeshRenderer>().material.color = WhenBoxOnRight;
        //}
        ////If no package
        //if (!_packageToLeft && !_packageToRight)
        //{
        //    GetComponent<MeshRenderer>().material.color = WhenNoBoxes;
        //}
        ////If package on both sides
        //if (_packageToLeft && _packageToRight)
        //{
        //    GetComponent<MeshRenderer>().material.color = WhenBoxOnBoth;
        //}
        #endregion
    }

    private void OnCollisionEnter(Collision collision)
    {
        //If colliding with the tailswipe hitbox
        if (collision.gameObject.TryGetComponent(out TailSwipeClearer TailSwipeClearer))
        {
            //Subtract health
            _packageHP--;

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
}
