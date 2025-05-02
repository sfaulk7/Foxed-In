using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _scoreText;

    [SerializeField]
    private float _startingScore = 0.0f;

    private float _currentScore = 0.0f;
    GameObject[] _boxes;

    // Start is called before the first frame update
    void Start()
    {
        _currentScore = _startingScore;
        _scoreText.text = _currentScore.ToString("0");
    }

    void DestroyScoring(float score)
    {
        _currentScore += score / 5;
    }

    void LineScoring(List<GameObject> line)
    {
        float currentBoxWorth;
        float finalPoints = 0.0f;
        float totalPoints = 0.0f;
        float totalMultiplier = 0.0f;

        float brownBoxCount = 0.0f;
        float greenBoxCount = 0.0f;
        float blueBoxCount = 0.0f;
        float goldBoxCount = 0.0f;

        for (int i = 0; i < line.Count; i++)
        {
            currentBoxWorth = ObjectPool.SharedInstance.pooledObjects[i].GetComponent<PackageCollisionBehavior>().PackagePointWorth();
            //Brown Box
            if (currentBoxWorth == 10)
            {
                brownBoxCount++;
                totalPoints += 10;
            }
            //Green Box
            else if (currentBoxWorth == 25)
            {
                greenBoxCount++;
                totalPoints += 25;
            }
            //Blue Box
            else if (currentBoxWorth == 50)
            {
                blueBoxCount++;
                totalPoints += 50;
            }
            //Gold Box
            else if (currentBoxWorth == 100)
            {
                goldBoxCount++;
                totalPoints += 100;
            }
        }
        float differentBoxes = 0.0f;
        float multHold = 0;
        if (brownBoxCount > 0)
        {
            differentBoxes++;
            multHold = Mathf.Pow(2, brownBoxCount);
            totalMultiplier += multHold;
        }
        if (greenBoxCount > 0)
        {
            differentBoxes++;
            multHold = Mathf.Pow(2, greenBoxCount);
            totalMultiplier += multHold;
        }
        if (blueBoxCount > 0)
        {
            differentBoxes++;
            multHold = Mathf.Pow(2, blueBoxCount);
            totalMultiplier += multHold;
        }
        if (goldBoxCount > 0)
        {
            differentBoxes++;
            multHold = Mathf.Pow(2, goldBoxCount);
            totalMultiplier += multHold;
        }
        totalMultiplier = totalMultiplier / differentBoxes;
        finalPoints = totalPoints * totalMultiplier;

        _currentScore += finalPoints;
    }

    private void LateUpdate()
    {
        List<GameObject> _boxes = new List<GameObject>();
        for (int i = 0; i < ObjectPool.SharedInstance.pooledObjects.Count; i++)
        {
            //If the box has been scored continue
            if (ObjectPool.SharedInstance.pooledObjects[i].GetComponent<PackageCollisionBehavior>().HasBeenScored())
            {
                continue;
            }
            if (!ObjectPool.SharedInstance.pooledObjects[i].activeInHierarchy)
            {
                //If the box was destroyed add 20% of its value to score
                if (ObjectPool.SharedInstance.pooledObjects[i].GetComponent<PackageCollisionBehavior>().WasDestroyed())
                {
                    DestroyScoring(ObjectPool.SharedInstance.pooledObjects[i].GetComponent<PackageCollisionBehavior>().PackagePointWorth());
                    ObjectPool.SharedInstance.pooledObjects[i].GetComponent<PackageCollisionBehavior>().SetScored(true);
                }
                //If the box was lined up add to the current lined list
                if (ObjectPool.SharedInstance.pooledObjects[i].GetComponent<PackageCollisionBehavior>().WasCleared())
                {
                    _boxes.Add(ObjectPool.SharedInstance.pooledObjects[i]);
                }
            }
        }
        if (_boxes.Count >= 10)
        {
            LineScoring(_boxes);
        }

        _scoreText.text = _currentScore.ToString("0");
    }
}
