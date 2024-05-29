using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class CameraShake : MonoBehaviour
{ 
        [SerializeField] private float duration = 0.6f;
        [SerializeField] private AnimationCurve curve;

        private void Awake()
        { 
                GameActions.FirstDiceTouchedTheFloor += ShakeCamera;
        }

        private void ShakeCamera()
        { 
                StartCoroutine(CameraShakeCoroutine());
        }

        private IEnumerator CameraShakeCoroutine()
        {
                Vector3 startPosition = transform.position;

                float passedTime = 0f;

                while (passedTime < duration)
                {
                        float strength = curve.Evaluate(passedTime / duration);
                        transform.position = startPosition + (Random.insideUnitSphere * strength);
                        passedTime += Time.deltaTime;
                        yield return null;
                }

                transform.position = startPosition;
        }
}
