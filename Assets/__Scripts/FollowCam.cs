using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class FollowCam : MonoBehaviour {
    static public GameObject targetObject;
    private Vector3 initialPosition;

    public GameObject ground;
    private Vector3 groundPosition;

    public float easing = 0.05f;

    void Start()
    {
        initialPosition = this.transform.position;
        groundPosition = ground.transform.position;
    }
    void FixedUpdate()
    {
        Vector3 targetPosition;

        if (targetObject != null &&
            targetObject.GetComponent<Rigidbody>().velocity.magnitude < 0.01)
            targetObject = null;

        if (targetObject == null)
            targetPosition = initialPosition;
        else
            targetPosition = targetObject.transform.position;

        Vector3 followPosition = Vector3.Lerp(this.transform.position, targetPosition, easing);

        this.transform.position = followPosition;

        this.GetComponent<Camera>().orthographicSize = followPosition.y - groundPosition.y;
    }
}
