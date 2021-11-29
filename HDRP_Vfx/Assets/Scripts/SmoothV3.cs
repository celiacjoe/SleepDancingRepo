using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmoothV3 : MonoBehaviour
{
    public Transform target;
    public GameObject GO;
    public float smoothTime = 0.3F;
    private Vector3 velocity = Vector3.zero;
    public float velocity2;
    float yVelocity = 0.0f;

    void Update()
    {
        // Define a target position above and behind the target transform
        Vector3 targetPosition = target.position;
        //  Vector3 newPosition = Mathf.SmoothDamp(GO.transform.position, target.position, yVelocity , smoothTime);
        //  GO.transform.position = new Vector3(newPosition.x,newPosition.y,newPosition.z);

        // Smoothly move the camera towards that target position
        GO.transform.position = Vector3.SmoothDamp(GO.transform.position, targetPosition, ref velocity, smoothTime);
    }
}
