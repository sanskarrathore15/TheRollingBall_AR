using System.Collections;
using UnityEngine;

public class MoveSlabs : MonoBehaviour
{
    [Header("Movement Settings")]
    public float centerPositionX = 0f;  // Set the center position of the slab
    public float moveOffset = 0.0144f;  // Distance to move left or right
    public float interval = 2f;         // Time interval between movements
    public float moveDuration = 1f;     // Time taken for smooth movement

    private int direction = -1; // Start moving left first

    private void Start()
    {
        // Set the initial position based on the given center X position
        transform.localPosition = new Vector3(centerPositionX, transform.localPosition.y, transform.localPosition.z);
        StartCoroutine(MoveRoutine());
    }

    IEnumerator MoveRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(interval);
            yield return StartCoroutine(SmoothMove(centerPositionX + (direction * moveOffset))); // Move left or right
            yield return new WaitForSeconds(interval);
            yield return StartCoroutine(SmoothMove(centerPositionX)); // Return to center

            direction *= -1; // Alternate direction
        }
    }

    IEnumerator SmoothMove(float targetX)
    {
        Vector3 startPosition = transform.localPosition;
        Vector3 endPosition = new Vector3(targetX, startPosition.y, startPosition.z);
        float elapsedTime = 0f;

        while (elapsedTime < moveDuration)
        {
            transform.localPosition = Vector3.Lerp(startPosition, endPosition, elapsedTime / moveDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.localPosition = endPosition; // Ensure exact position
    }
}
