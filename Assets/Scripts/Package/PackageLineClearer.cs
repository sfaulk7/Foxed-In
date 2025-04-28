using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PackageLineClearer : MonoBehaviour
{
    [SerializeField]
    private GameObject _package;

    private Vector3 _rayStart;
    private float _rayDistance;
    private int _packagesHit;
    private bool rowComplete;

    // Start is called before the first frame update
    void Start()
    {
        _rayStart = new Vector3(-20, 0, 5);
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit[] hits;
        hits = Physics.RaycastAll(_rayStart, new Vector3(1, 0, 0), 90);

        if (hits.Length >= 11)
        {
            for (int i = 0; i < hits.Length; i++)
            {
                RaycastHit hit = hits[i];

                if (hit.transform.TryGetComponent(out PackageCollisionBehavior PackageCollisionBehavior))
                {
                    if (_packagesHit == 11)
                    {
                        Destroy(hit.transform.gameObject);
                        rowComplete = true;
                    }
                    else
                    {
                        _packagesHit++;
                    }
                }
            }
            if (_packagesHit < 11)
            {
                _packagesHit = 0;
            }
            else if (_packagesHit >= 11 && rowComplete)
            {
                _packagesHit = 0;
                rowComplete = false;
            }
        }
    }
}
