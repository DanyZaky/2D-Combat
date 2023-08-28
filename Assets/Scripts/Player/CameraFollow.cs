using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public float smoothSpeed = 0.125f;
    public Vector3 offset;

    public float minXLimit = -10f;  // Batas minimal nilai x
    public float maxXLimit = 10f;   // Batas maksimal nilai x

    private void LateUpdate()
    {
        Vector3 desiredPosition = new Vector3(target.position.x + offset.x, transform.position.y, transform.position.z) + offset;

        // Batasi nilai x antara minXLimit dan maxXLimit
        float clampedX = Mathf.Clamp(desiredPosition.x, minXLimit, maxXLimit);

        Vector3 smoothedPosition = Vector3.Lerp(transform.position, new Vector3(clampedX, desiredPosition.y, desiredPosition.z), smoothSpeed * Time.deltaTime);
        transform.position = smoothedPosition;
    }
}
