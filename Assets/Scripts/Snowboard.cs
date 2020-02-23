using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snowboard : MonoBehaviour
{
    public Transform target;
    public float y_offset;

    // public AudioClip fallingSound;
    // public AudioClip skiSound;
    // public AudioClip landingSound;
    // public AudioSource audioSource;
    // public Rigidbody rb;

    void Start()
    {
        // audioSource = GetComponent<AudioSource>();
        // rb = GetComponent<Rigidbody>();
    }
    
    
    void FixedUpdate()
    {
        Vector3 desiredPosition = target.position - new Vector3(0, y_offset, 0);
        // Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, .001f);
        transform.position = desiredPosition;
        transform.rotation = target.rotation;

        RaycastHit hit;
        Quaternion rotat = transform.rotation;
        if (Physics.SphereCast(transform.position, 0.1f, -transform.up, out hit, 1)) {
            rotat = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(Vector3.Cross(transform.right, hit.normal), hit.normal), Time.deltaTime * 5.0f);
        }
        rotat.y = target.transform.rotation.y;

        target.rotation = rotat;

    }
    
    // public bool isGrounded = true;

    // void OnCollisionEnter(Collision col)
    // {
        
    //     if (col.gameObject.CompareTag("Terrain"))
    //     {
    //         audioSource.Stop();
    //         audioSource.PlayOneShot(landingSound, 0.7F);
    //         isGrounded = true;
    //         audioSource.PlayOneShot(skiSound, 0.7F);
    //     }
        
    // }



    // void OnCollisionExit(Collision col) 
    // {
    //     if (col.gameObject.CompareTag("Terrain"))
    //     {
    //         audioSource.Stop();
    //         isGrounded = false;
    //         audioSource.PlayOneShot(fallingSound, 0.7F);
    //     }

    // }

}
