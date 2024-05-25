using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Dice : MonoBehaviour
{
    [Header("References")]
    public FaceData[] faceDatas;

    public int defaultFaceResult = -1;
    public bool sentResult = false;
    
    public void Reset()
    {
        defaultFaceResult = -1;
        sentResult = false;
    }
    
    public void RotateDice(int alteredFaceResult)
    {
        Debug.Log(defaultFaceResult);
        Vector3 rotationFromIdentity = DiceManager.RotationMatrix[defaultFaceResult];

        float inverseX = rotationFromIdentity.x == 0f ? 0f : 360f - rotationFromIdentity.x;
        float inverseY = rotationFromIdentity.y == 0f ? 0f : 360f - rotationFromIdentity.y;
        float inverseZ = rotationFromIdentity.z == 0f ? 0f : 360f - rotationFromIdentity.z;


        Vector3 inverseRotationToDefault = new Vector3(inverseX, inverseY, inverseZ);
        
        // First change it to default rotation
        transform.Rotate(inverseRotationToDefault,Space.Self);

        Vector3 rotationFromMatrix = DiceManager.RotationMatrix[alteredFaceResult];
            
        // Then rotate according to default's rotation matrix
        transform.Rotate(rotationFromMatrix, Space.Self);
    }
    
    public int FindFaceResult()
    {
        int maxIndex = 0;
        for (int i = 1; i < faceDatas.Length; i++)
        {
            if (faceDatas[maxIndex].faceDetector.transform.position.y <
                faceDatas[i].faceDetector.transform.position.y)
            {
                maxIndex = i;
            }
        }
        defaultFaceResult = faceDatas[maxIndex].result;

        return faceDatas[maxIndex].result;
    }
}

[Serializable]
public struct FaceData
{
    public GameObject faceDetector;
    public int result;
}