using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// YOU must implement the Slingshot

public class Slingshot : MonoBehaviour
{



    public GameObject Launcher;

    private bool isAiming;
    private float sphereRadius;

    public GameObject prefabBall;
    public GameObject activeBall;
    public float speedMultiplier = 10.0f;


    void Start()
    {
        Launcher.SetActive(false);
        isAiming = false;
        sphereRadius = this.GetComponent<SphereCollider>().radius;
        activeBall = null;
    }
    void OnMouseEnter()
    {
        Launcher.SetActive(true);
    }
    void OnMouseExit()
    {
        Launcher.SetActive(false);
    }
    void OnMouseDown()
    {
        isAiming = true;
        activeBall = Instantiate(prefabBall) as GameObject;
        activeBall.transform.position = Launcher.transform.position;
        activeBall.GetComponent<Rigidbody>().isKinematic = true;
    }

    void Update()
    {
        if (!isAiming)
            return;

        Vector3 mousePositionScreen = Input.mousePosition;
        mousePositionScreen.z = -Camera.main.transform.position.z;
        Vector3 mousePositionworld = Camera.main.ScreenToWorldPoint(mousePositionScreen);
        Vector3 dragVector = mousePositionworld - Launcher.transform.position;

        if (dragVector.magnitude > sphereRadius)
        {
            dragVector.Normalize();
            dragVector *= sphereRadius;
        }

        activeBall.transform.position = Launcher.transform.position + dragVector;

        if (Input.GetMouseButtonUp(0))
        {
            isAiming = false;
            Rigidbody rb = activeBall.GetComponent<Rigidbody>();
            rb.isKinematic = false;
            rb.velocity = -dragVector * speedMultiplier;

        }
    }
}