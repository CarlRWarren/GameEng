using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GrabObject : MonoBehaviour
{
    public GameObject objectGrabed = null;
    public bool isCollided = false;
    public bool isGrabing = false;
    public Vector3 throwForce = Vector3.zero;
    public float timer = 0.0f;

    private void Update()
    {
        if (isCollided)
        {
            if (OVRInput.Get(OVRInput.Axis1D.PrimaryIndexTrigger) > 0.5f)
            {
                isGrabing = true;
            }
        }
        if (OVRInput.Get(OVRInput.Axis1D.PrimaryIndexTrigger) < 0.5f)
        {
            LetGo();
            isGrabing = false;
        }
        if (isGrabing)
        {
            objectGrabed.transform.position = this.transform.position;
        }
        this.GetComponent<Collider>().isTrigger = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (objectGrabed == null && !isGrabing)
        {
            isCollided = true;
            objectGrabed = other.gameObject;
            objectGrabed.GetComponent<Rigidbody>().useGravity = false;
        }
    }

    void LetGo()
    {
        isCollided = false;
        objectGrabed.GetComponent<Rigidbody>().useGravity = true;
        objectGrabed.GetComponent<Rigidbody>().velocity = throwForce;
        objectGrabed = null;
    }
}
