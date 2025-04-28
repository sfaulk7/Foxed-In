using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityManager : MonoBehaviour
{
    [SerializeField]
    GameObject _package;

    [SerializeField]
    private float _gravityIncreaseTimer = 1.0f;

    [SerializeField]
    private float _startingGravity = 1.0f;

    [HideInInspector]
    public float currentGravity = 0.0f;

    Rigidbody _packageRigidbody;


    // Start is called before the first frame update
    void Start()
    {
        _packageRigidbody = _package.GetComponent<Rigidbody>();
        currentGravity = _startingGravity;
        Invoke(nameof(IncreaseGravity), _gravityIncreaseTimer);
    }

    public float GetCurrentGravity()
    {
        return currentGravity;
    }

    void IncreaseGravity()
    {
        currentGravity += .1f;
        Invoke(nameof(IncreaseGravity), _gravityIncreaseTimer);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
