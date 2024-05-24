using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Dice : MonoBehaviour
{
    [Header("References")]
    public DiceData diceData;
    public GameObject[] faceDetectors;
    
    public int defaultFaceResult = -1;

    private Dictionary<int, int> faceResults = new Dictionary<int, int>();

    private void Awake()
    {
        faceResults.Add(2, 0);
        faceResults.Add(5, 1);
        faceResults.Add(4, 2);
        faceResults.Add(3, 3);
        faceResults.Add(6, 4);
        faceResults.Add(1, 5);
    }
    
    public void Reset()
    {
        defaultFaceResult = -1;
    }
    
    public void RotateDice(int alteredFaceResult)
    {
        Debug.Log(defaultFaceResult);
        
        Vector3 rotationFromMatrix =
            diceData.faceRelativeRotation[defaultFaceResult].rotation[0];
        transform.Rotate(rotationFromMatrix, Space.Self);


        if (alteredFaceResult != 2)
        {
            rotationFromMatrix =
                diceData.faceRelativeRotation[0].rotation[faceResults[alteredFaceResult]];
            
            transform.Rotate(rotationFromMatrix, Space.Self);

        }

    }
    
    public void FindFaceResult()
    {
        int maxIndex = 0;
        for (int i = 1; i < faceDetectors.Length; i++)
        {
            if (faceDetectors[maxIndex].transform.position.y <
                faceDetectors[i].transform.position.y)
            {
                maxIndex = i;
            }
        }
        defaultFaceResult = maxIndex;
    }
}