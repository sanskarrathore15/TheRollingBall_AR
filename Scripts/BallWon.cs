using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BallWon : MonoBehaviour
{
    public GameObject WinnerUI;
    //public GameObject snow;
    public Rigidbody rb;
    public Transform won;

    public TextMeshProUGUI finalTimeText2;

    public CountdownTimer countdownTimer;

    public GameObject timerCount;
    // Start is called before the first frame update


    // Update is called once per frame
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            countdownTimer.isRunning = false;
            timerCount.gameObject.SetActive(false);
            finalTimeText2.text = countdownTimer.GetRemainingTime()+" sec";
            WinnerUI.SetActive(true);
            /*snow.SetActive(true);
            ParticleSystem ps = snow.GetComponent<ParticleSystem>();
            ps.Play();*/
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            rb.useGravity = false;
            rb.isKinematic = true;
            rb.transform.position = won.position;
        }
    }

    

   
        
       
}
