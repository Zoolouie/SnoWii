using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TPCharacterController : MonoBehaviour
{
    public float Speed;
    // Start is called before the first frame update
    void Start()
    {
        PlayerMovement();
    }

    void PlayerMovement()
    {
        Debug.Log("Test");
        float hor = Input.GetAxis("Horizontal");
        float ver = Input.GetAxis("Vertical");
        Vector3 playerMovement = new Vector3(hor, 0f, ver) * Speed * Time.deltaTime;
        transform.Translate(playerMovement, Space.Self);
    }
}
