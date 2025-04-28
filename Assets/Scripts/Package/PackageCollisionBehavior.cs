using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PackageCollisionBehavior : MonoBehaviour
{
    private bool _packageToRight = false;
    private bool _packageToLeft = false;

    [SerializeField]
    private Color WhenNoBoxes;

    [SerializeField]
    private Color WhenBoxOnLeft;

    [SerializeField]
    private Color WhenBoxOnRight;

    [SerializeField]
    private Color WhenBoxOnBoth;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
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
