using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Dice Data", menuName = "Scriptable Object/Dice Data")]
public class DiceData : ScriptableObject
{
    public List<FaceRelativeRotation> faceRelativeRotation;
    
    [System.Serializable]
    public struct FaceRelativeRotation
    {
        public string result;
        public List<Vector3> rotation;
    }
}