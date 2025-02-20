using UnityEngine;

public class BallPhysics : MonoBehaviour
{
    public Transform cuboid; // The Vuforia multi-target cuboid
    public float gravityStrength = 9.8f;
    public float movementSpeed = 10f;
    public float edgeThreshold = 0.1f; // Distance to detect edge transitions
    public LayerMask planeLayer; // Add this to define the layer of your cuboid faces

    private Rigidbody ballRigidbody;
    private Vector3 currentPlaneNormal;
    private Transform currentPlane;

    void Start()
    {
        ballRigidbody = GetComponent<Rigidbody>();
        ballRigidbody.isKinematic = false;  // Make sure the Rigidbody is not Kinematic
        ballRigidbody.useGravity = false;  // Disable Unity's default gravity to handle custom gravity

        currentPlane = GetInitialPlane();
        currentPlaneNormal = currentPlane.up; // Assuming each face’s "up" is its normal
    }

    void FixedUpdate()
    {
        ApplyCustomGravity();
        HandleEdgeTransitions();
    }

    void ApplyCustomGravity()
    {
        // Calculate the direction to pull the ball toward the current plane
        Vector3 gravityDirection = -currentPlaneNormal; // Force is directed towards the plane
        Vector3 gravityForce = gravityDirection * gravityStrength;

        // Apply the gravity force to the ball
        ballRigidbody.AddForce(gravityForce, ForceMode.Acceleration);

        // Ensure the ball stays near the surface of the cuboid
        Vector3 projectedVelocity = Vector3.ProjectOnPlane(ballRigidbody.velocity, currentPlaneNormal);
        ballRigidbody.velocity = projectedVelocity;
    }

    void HandleEdgeTransitions()
    {
        RaycastHit hit;

        // Raycast to detect if the ball is still on the current plane
        if (Physics.Raycast(transform.position, -currentPlaneNormal, out hit, 1f, planeLayer))
        {
            if (hit.collider.transform != currentPlane)
            {
                // If the ball is on a new face, update the plane info
                currentPlane = hit.collider.transform;
                currentPlaneNormal = currentPlane.up;

                // Correct velocity to stay on the new plane
                ballRigidbody.velocity = Vector3.ProjectOnPlane(ballRigidbody.velocity, currentPlaneNormal);
            }
        }

        // Prevent the ball from falling off the edge of the cuboid face
        if (transform.position.y < currentPlane.position.y - edgeThreshold)
        {
            // Correct the position to keep it on the surface
            ballRigidbody.AddForce(Vector3.up * gravityStrength, ForceMode.Acceleration);
        }
    }

    Transform GetInitialPlane()
    {
        // Cast a ray downward at the start to find the initial plane
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit, 1f, planeLayer))
        {
            return hit.collider.transform;
        }
        return cuboid; // Default to the main cuboid if no plane is found
    }
}
