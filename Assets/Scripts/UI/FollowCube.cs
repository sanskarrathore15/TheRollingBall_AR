using UnityEngine;

public class FollowCube : MonoBehaviour
{
    public Transform cube; // Assign the cube in the inspector
    public float heightOffset = 0.2f; // Adjust height above the cube

    void Update()
    {
        if (cube != null)
        {
            transform.position =  new Vector3(0, heightOffset, 0);
        }
    }
}
