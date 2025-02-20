using UnityEngine;

public class Checkpoint : MonoBehaviour
{
 
   
    public int checkpointIndex;
    public BallDead ballDeadScript; // Assign the BallDead script in the Inspector

    public GameObject snow;

   /* private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log(checkpointIndex);
           // UpdateCheckpoint(other.transform);
            ballDeadScript.UpdateUI(); // Update checkpoint in BallDead script
        }
    }*/

    private void UpdateCheckpoint(Transform checkpoint)
    {
        if (checkpointIndex == 7)
        {
            snow.SetActive(true);
            ParticleSystem ps = snow.GetComponent<ParticleSystem>();
            ps.Play();
        }
    }
}
