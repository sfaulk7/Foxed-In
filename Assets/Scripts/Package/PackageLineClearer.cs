using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PackageLineClearer : MonoBehaviour
{
    [SerializeField]
    private GameObject _package;

    [SerializeField]
    PackageSpawner _packageSpawner;

    private Vector3 _rayStart;
    private float _rayDistance;
    private int _packagesHit;
    private int _packageGridWidth;
    private bool rowComplete;

    // Start is called before the first frame update
    void Start()
    {
        _packageGridWidth = _packageSpawner.GridWidth();
        _rayStart = new Vector3(-20, 0, 5);
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit[] hits;
        hits = Physics.RaycastAll(_rayStart, new Vector3(1, 0, 0), 90);

        //If the amount of objects the Raycast hits is more or equal to the GridWidth
        if (hits.Length >= _packageGridWidth)
        {
            //Check all the objects hit
            for (int i = 0; i < hits.Length; i++)
            {
                RaycastHit hit = hits[i];

                //If the current checked object is a Package
                if (hit.transform.TryGetComponent(out PackageCollisionBehavior PackageCollisionBehavior))
                {
                    //If the amount of packages hit by the raycast is equal to the GridWidth
                    if (_packagesHit == _packageGridWidth)
                    {
                        hit.transform.gameObject.SetActive(false);
                        rowComplete = true;
                    }
                    //Otherwise increase how many packages have been hit by raycast
                    else
                    {
                        _packagesHit++;
                    }
                }
            }
            //If the full check of all hit object by the raycast returned less
            //packages being hit than the gridwidth
            if (_packagesHit < _packageGridWidth)
            {
                //Reset the package counter
                _packagesHit = 0;
            }
            //If the full check of all hit object by the raycast returned more
            //packages being hit than the gridwidth AND the row has been completed/cleared
            else if (_packagesHit >= _packageGridWidth && rowComplete)
            {
                //Reset the package counter and set rowComplete to be false
                _packagesHit = 0;
                rowComplete = false;
            }
        }
    }
}
