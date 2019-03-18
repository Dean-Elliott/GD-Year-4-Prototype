using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Spawner : MonoBehaviour
{
    public GameObject left;
    public GameObject right;
    public GameObject targetPrefab;
    public GameObject activeTarget;
    float timeOfLastTargetGot = 0;
    float timeToGetLastTarget;
    float averagetime;
    List<float> times = new List<float>();
    public TextMeshProUGUI lastTimeText;
    public TextMeshProUGUI averageTimeText;
    public TextMeshProUGUI worsTimesText;

    // Start is called before the first frame update
    void Start()
    {
        newTarget();
    }

    public void newTarget()
    {
        timeToGetLastTarget = Time.time - timeOfLastTargetGot;
        timeOfLastTargetGot = Time.time;

        times.Add(timeToGetLastTarget);

        float totalTime = 0;
        float worstTime = 0;
        for (int i = 1; i < times.Count; i++)
        {
            totalTime += times[i];
            print(times[i] + "    " + i);
            if(times[i] > worstTime)
            {
                worstTime = times[i];
            }
        }
        lastTimeText.text = "last time: " + timeToGetLastTarget.ToString();
        averageTimeText.text = "average time: " + (totalTime / (times.Count-1)).ToString();
        worsTimesText.text = "worst time: " + worstTime.ToString();

        if (activeTarget != null)
        {
            activeTarget.GetComponent<SimpleTarget>().TriggerExplosion();
        }

        Destroy(activeTarget);
        Vector2 randomSpawnLocation = new Vector2(Random.Range(left.transform.position.x, right.transform.position.x), Random.Range(left.transform.position.y, right.transform.position.y));
        activeTarget = Instantiate(targetPrefab, randomSpawnLocation, Quaternion.identity);
        activeTarget.GetComponent<SimpleTarget>().mySpawner = this;
    }
}
