using UnityEngine;

public class BillboardUI : MonoBehaviour
{
    public Transform cube; // Assign the Cube in Inspector
    public float heightOffset = 0.5f; // UI height above the cube

    void LateUpdate()
    {
        if (cube == null) return;

        // Keep UI at a fixed height above the cube
        transform.position = new Vector3(cube.position.x, cube.position.y + heightOffset, cube.position.z);

        // Make UI always face the camera
        transform.rotation = Quaternion.LookRotation(transform.position - Camera.main.transform.position);
    }
}
