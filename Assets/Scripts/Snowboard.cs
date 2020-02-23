using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snowboard : MonoBehaviour
{
    public Transform target;
    public float y_offset;
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
}
