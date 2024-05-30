using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class DiceInteraction : MonoBehaviour
{
    [Header("States")]
    public bool isContactWithFloor;
    public bool isContactWithDice;
    public bool isInSimulation = true;
    public bool isNotMoving = false;
    
    public AudioSource floorCollisionSound;
    public AudioSource diceCollisionSound;

    /// <summary>
    /// For object pooling system,
    /// we could reset the dice back and reuse it again
    /// </summary>
    public void Reset()
    {
        isContactWithFloor = false;
        isContactWithDice = false;
        isInSimulation = true;
        isNotMoving = false;
    }
    
    public void PlaySoundRollLow()
    {
        if (!floorCollisionSound.isPlaying)
            floorCollisionSound.Play();
    }

    public void PlaySoundRollHigh()
    {
        if (!diceCollisionSound.isPlaying)
            diceCollisionSound.Play();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Floor"))
        {
            isContactWithFloor = true;
        }

        if (collision.transform.CompareTag("Dice"))
        {
            isContactWithDice = true;
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.transform.CompareTag("Floor"))
        {
            isContactWithFloor = false;
        }

        if (collision.transform.CompareTag("Dice"))
        {
            isContactWithDice = false;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.transform.CompareTag("Floor"))
        {
            isContactWithFloor = false;
        }

        if (collision.transform.CompareTag("Dice"))
        {
            isContactWithDice = false;
        }
    }
}
