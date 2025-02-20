using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotateSlabs : MonoBehaviour
{

    public float rotationAngle = 90f; // Rotation angle per step
    public float interval = 2f; // Time interval between rotations
    public float rotationDuration = 1f; // Time taken for smooth rotation

    private void Start()
    {
        StartCoroutine(RotateRoutine());
    }

    IEnumerator RotateRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(interval);
            yield return StartCoroutine(SmoothRotate());
        }
    }

    IEnumerator SmoothRotate()
    {
        Quaternion startRotation = transform.localRotation;
        Quaternion endRotation = startRotation * Quaternion.Euler(0, 0, rotationAngle);
        float elapsedTime = 0f;

        while (elapsedTime < rotationDuration)
        {
            transform.localRotation = Quaternion.Lerp(startRotation, endRotation, elapsedTime / rotationDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.localRotation = endRotation; // Ensure it ends at exact rotation
    }

}


