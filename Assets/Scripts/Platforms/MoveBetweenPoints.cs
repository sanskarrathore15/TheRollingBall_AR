using System.Collections;
using UnityEngine;

public class MoveBetweenPoints : MonoBehaviour
{
    [Header("Movement Settings")]
    public Vector3 startLocalPosition;   // The initial local position of the slab
    public Vector3 destinationLocal;     // The target local position of the slab
    public float moveDuration = 2f;      // Time taken for smooth movement
    public float interval = 1f;          // Pause time at each position

    private void Start()
    {
        // Set the initial local position
        transform.localPosition = startLocalPosition;
        StartCoroutine(MoveRoutine());
    }

    IEnumerator MoveRoutine()
    {
        while (true)
        {
            yield return StartCoroutine(SmoothMove(destinationLocal)); // Move to destination
            yield return new WaitForSeconds(interval);
            yield return StartCoroutine(SmoothMove(startLocalPosition)); // Move back to start
            yield return new WaitForSeconds(interval);
        }
    }

    IEnumerator SmoothMove(Vector3 targetLocalPosition)
    {
        Vector3 initialPosition = transform.localPosition;
        float elapsedTime = 0f;

        while (elapsedTime < moveDuration)
        {
            transform.localPosition = Vector3.Lerp(initialPosition, targetLocalPosition, elapsedTime / moveDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.localPosition = targetLocalPosition; // Ensure exact position
    }
}
