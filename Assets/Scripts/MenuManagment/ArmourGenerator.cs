using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class ArmourGenerator : MonoBehaviour
{
    [Range(0,360)]
    public float angleBreadth;
    public float offsetfromCenter;
    public float thickness;
    public int iterations; //more iterations means more box collider 2Ds will make up the arc, more accurate but more expensive
    public GameObject armourPrefab;
    public GameObject armourParent;

    GameObject[] armourChunks;
    int iterationLastFrame;

    void Update()
    {
        if (armourChunks != null)
        {
            for (int i = 0; i < armourChunks.Length; i++)
            {
                DestroyImmediate(armourChunks[i]);
            }
        }
        armourChunks = new GameObject[iterations];


        for (int i = 0; i < iterations; i++)
        {
            float angleToByIncrementEachIteration = angleBreadth / (iterations - 1);
            float angleIncrement = i * angleToByIncrementEachIteration - (angleBreadth / 2);
            Vector2 instanceArmourChunkVector = (Vector2)(Quaternion.Euler(0, 0, angleIncrement) * Vector2.up);
            Debug.DrawLine(transform.position, new Vector2 (transform.position.x + instanceArmourChunkVector.x, transform.position.y + instanceArmourChunkVector.y), Color.magenta);
            float arcRadius =  2.0f * Mathf.PI * (offsetfromCenter + (thickness / 2.0f)) * (angleBreadth / 360.0f);
            float chunkWidth = (arcRadius / (iterations - 1));
            armourChunks[i] = Instantiate(armourPrefab, instanceArmourChunkVector * offsetfromCenter, Quaternion.LookRotation(Vector3.forward, instanceArmourChunkVector));
            armourChunks[i].transform.localScale = new Vector2(chunkWidth, thickness);
            armourChunks[i].transform.parent = armourParent.transform;
        }
    }
}
