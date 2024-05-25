using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Random = UnityEngine.Random;

public class DiceManager : MonoBehaviour
{
    public static Action<int> DiceStopped;
    public static Action<int> PlayerCanMove;
    
    public GameObject dicePrefab;
    public MovementRecorder movementRecorder;

    private int _totalDiceCount;
    private int _stoppedDiceCount;
    private int _totalDiceResult;
    
    public List<int> targetedResult = new List<int>();
    
    [HideInInspector]
    public List<DiceData> diceDataList;

    public static readonly Dictionary<int, Vector3> RotationMatrix = new Dictionary<int, Vector3> // This matrix is for our dice's default rotation (2 is on top)
    {
        {1, new Vector3(270, 0, 0)},
        {2, new Vector3(0, 0, 0)},
        {3, new Vector3(0, 0, 270)},
        {4, new Vector3(0, 0, 90)},
        {5, new Vector3(180, 0, 0)},
        {6, new Vector3(90, 0, 0)}
    };
    
    private void Start()
    {
        DiceStopped += CanPlayerMove;
    }

    private void CanPlayerMove(int diceResult)
    {
        _stoppedDiceCount++;
        _totalDiceResult += diceResult;

        if (_stoppedDiceCount == _totalDiceCount)
        {
            PlayerCanMove?.Invoke(_totalDiceResult);

            _stoppedDiceCount = 0;
            _totalDiceResult = 0;
        }
    }

    private void SetDicesInformation()
    {
        targetedResult.Clear();
        
        _totalDiceCount = InputFieldManager.Instance.inputFields.Count;

        foreach (var inputField in InputFieldManager.Instance.inputFields)
        {
            targetedResult.Add(inputField.GetDiceValue());
        }
    }
    public void ThrowTheDice()
    {
        SetDicesInformation();
        GenerateDice(_totalDiceCount);

        //Generate list of dices, then put it into the simulation
        List<GameObject> diceList = new List<GameObject>();
        for (int i = 0; i < _totalDiceCount; i++)
        {
            diceList.Add(diceDataList[i].diceObject);
        }
        movementRecorder.StartSimulation(diceList);

        //Record the dice roll result
        for (int i = 0; i < _totalDiceCount; i++)
        {
            diceDataList[i].diceLogic.FindFaceResult();
        }

        //Reset and Alter the result FOR NOW, all 2
        movementRecorder.ResetToInitialState();
        for (int i = 0; i < targetedResult.Count; i++)
        {
            diceDataList[i].diceLogic.RotateDice(((int)targetedResult[i]));
        }

        movementRecorder.PlayRecording();
    }

    private void GenerateDice(int count)
    {
        //Object pooling. Only generate dices we need
        if(count > diceDataList.Count)
        {
            int diceToGenerate = count - diceDataList.Count;
            for (int i = 0; i < diceToGenerate; i++)
            {
                DiceData newDiceData = new DiceData(Instantiate(dicePrefab));
                diceDataList.Add(newDiceData);
            }
        }
        //Otherwise, just teleport the dice far away from the arena.
        //We can fetch them later if we want
        else if(count < diceDataList.Count)
        {
            int diceToDispose = diceDataList.Count - count;
            for (int i = diceDataList.Count - diceToDispose - 1;
                     i < diceDataList.Count; i++)
            {
                diceDataList[i].diceObject.transform.position = Vector3.down * 10000;
            }
        }

        //Set the position and rotation
        for (int i = 0; i < count; i++)
        {
            InitialState initial = SetInitialState();

            diceDataList[i].diceLogic.Reset();
            diceDataList[i].diceInteraction.Reset();
            diceDataList[i].diceObject.transform.position = initial.position;
            diceDataList[i].diceObject.transform.rotation = initial.rotation;
            diceDataList[i].rb.useGravity = true;
            diceDataList[i].rb.isKinematic = false;
            diceDataList[i].rb.velocity = initial.force;
            diceDataList[i].rb.AddTorque(initial.torque, ForceMode.VelocityChange);
        }
    }

    private InitialState SetInitialState()
    {
        int x, y, z;
        
        Vector3 position = transform.position;
        
        Quaternion rotation = Quaternion.identity;

        x = Random.Range(0, 5);
        y = Random.Range(0, 5);
        z = Random.Range(0, 5);
        Vector3 force = new Vector3(x, -y, z);

        x = Random.Range(0, 10);
        y = Random.Range(0, 10);
        z = Random.Range(0, 10);
        Vector3 torque = new Vector3(x, y, z);

        return new InitialState(position, rotation, force, torque);
    }

    [Serializable]

    public struct DiceData
    {
        public GameObject diceObject;
        public Rigidbody rb;
        public Dice diceLogic;
        public DiceInteraction diceInteraction;

        public DiceData(GameObject diceObject)
        {
            this.diceObject = diceObject;
            this.rb         = diceObject.GetComponent<Rigidbody>();
            this.diceLogic  = diceObject.transform.GetChild(0).GetComponent<Dice>();
            this.diceInteraction     = diceObject.GetComponent<DiceInteraction>();
            this.rb.maxAngularVelocity = 1000;
        }
    }

    [Serializable]
    public struct InitialState
    {
        public Vector3 position;
        public Quaternion rotation;
        public Vector3 force;
        public Vector3 torque;

        public InitialState(Vector3 position, Quaternion rotation,
                            Vector3 force, Vector3 torque)
        {
            this.position = position;
            this.rotation = rotation;
            this.force = force;
            this.torque = torque;
        }
    }

}

