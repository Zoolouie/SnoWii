using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAndOtherSounds : MonoBehaviour
{
    // Start is called before the first frame update
    public AudioClip fallingSound;
    public AudioClip skiSound;
    public AudioClip landingSound;
    public AudioSource audioSource;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    
    }

    public bool isGrounded;

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.CompareTag("Terrain"))
        {
            audioSource.Stop();
            audioSource.PlayOneShot(landingSound, 0.7F);
            isGrounded = true;
            audioSource.PlayOneShot(skiSound, 0.7F);
        }
    }



    void OnCollisionExit(Collision col) 
    {
        if (col.gameObject.CompareTag("Terrain"))
        {
            audioSource.Stop();
            isGrounded = false;
            audioSource.PlayOneShot(fallingSound, 0.7F);
        }

    }
}
