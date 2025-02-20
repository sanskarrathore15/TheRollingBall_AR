using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BallAttraction : MonoBehaviour
{
    public Transform[] surfaces; // List of surfaces (cube, slab, etc.)
    public Transform[] checkpoints; // Checkpoint positions
    public float attractionForce = 5f; // Reduced for smoother attraction
    public float smoothness = 0.1f;
    public float tiltSensitivity = 0f;
    public float gravityScale = 0.04f;
    public float attractionThreshold = 0.05f; // Prevents small jitter corrections
    public Rigidbody rb;

    private Transform currentSurface;
    private Vector3 closestPoint;
    private bool isBallActive = false;
    private int lastCheckpointIndex = -1; // Tracks last checkpoint
    public TextMeshProUGUI checkpointText;

    public Button playButton;

    public CountdownTimer timer;

    public AudioSource audioSource;
    public AudioSource audioSourceCP;

    public GameObject snow;

    void Start()
    {
        UpdateUI();
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        rb.useGravity = false;
        rb.isKinematic = true;
        rb.transform.position = checkpoints[lastCheckpointIndex+1].position;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            ActivateBall();
        }
    }

    void FixedUpdate()
    {
        if (!isBallActive) return;

        currentSurface = GetClosestSurface();

        if (currentSurface != null)
        {
            Collider surfaceCollider = currentSurface.GetComponent<Collider>();
            if (surfaceCollider != null)
            {
                closestPoint = surfaceCollider.ClosestPoint(transform.position);
            }
            else
            {
                closestPoint = currentSurface.position;
            }

            Vector3 attractionDirection = closestPoint - transform.position;
            float distance = attractionDirection.magnitude;

            // 3. Only apply force if outside threshold to prevent jitter
            if (distance > attractionThreshold)
            {
                Vector3 attractionForceVector = attractionDirection.normalized * Mathf.Lerp(0, attractionForce, distance);
                rb.velocity = Vector3.Lerp(rb.velocity, attractionForceVector, smoothness);
            }
        }

        Vector3 customGravity = new Vector3(0, -9.81f * gravityScale, 0);
        rb.AddForce(customGravity, ForceMode.Acceleration);
    }

    private Transform GetClosestSurface()
    {
        Transform closest = null;
        float minDistance = float.MaxValue;

        foreach (Transform surface in surfaces)
        {
            float distance = Vector3.Distance(transform.position, surface.position);
            if (distance < minDistance)
            {
                minDistance = distance;
                closest = surface;
            }
        }
        return closest;
    }

    public void ActivateBall()
    {
        isBallActive = true;
        rb.isKinematic = false;
        if(!timer.isRunning)
{       timer.isRunning = true;
        timer.StartCoroutine(timer.StartCountdown()); }
        playButton.gameObject.SetActive(false);
    }

    public void Reset()
    {
        // Reset Rigidbody properties
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        rb.useGravity = false;
        rb.isKinematic = true;
        timer.isRunning = false;

        // Move to last checkpoint
        rb.transform.position = checkpoints[lastCheckpointIndex].position;
        Debug.Log("ball resetted");

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Checkpoint"))
        {
            UpdateCheckpoint(other.transform);
        }
          
    }

    public void PlayMusic()
    {
        audioSource.Play();
    }
 

    private void UpdateCheckpoint(Transform checkpoint)
    {
        for (int i = 0; i < checkpoints.Length; i++)
        {
            if (checkpoints[i] == checkpoint)
            {
                lastCheckpointIndex = i;
                audioSourceCP.Play();
                UpdateUI();
                Debug.Log("Checkpoint " + i + " reached!");

                if (i == 7)
                {
                    snow.SetActive(true);
                    ParticleSystem ps = snow.GetComponent<ParticleSystem>();
                    ps.Play();
                }

                break;

               
            }

            
        }
    }

    public void UpdateUI()
    {
       // livesText.text = "Lives: " + lives;
        checkpointText.text = "Checkpoint: " + (lastCheckpointIndex)+"/7"; // +1 for user-friendly display
    }
}
