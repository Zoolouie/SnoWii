using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SwitchLevel : MonoBehaviour
{
    // Start is called before the first frame update
    private float time = 5.0f;
    private float countdown = 5.0f;

    private bool isReset = false;

    private void Update () {
        if (isReset) {
            if (countdown < 0) {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); 
                countdown = time;
                isReset = false;
            }
            else {
                countdown -= Time.deltaTime;
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.name);
        if (other.name == "Player") {
            isReset = true;
        }
        
    }
}
