using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPhysics : MonoBehaviour
{
    [SerializeField] GameObject throwable = null;
    public float Power { get; set; } = 100.0f;
    private bool toss;
    private bool tossed;
    private bool reset;
    Transform objTransform = null;
    Quaternion objRot;
    void Start()
    {
        objTransform = throwable.transform;
        objRot = objTransform.rotation;
    }

    void Update()
    {
        Vector3 targetPosition = new Vector3(0.0f + Power * objRot.y, Power + Power * objRot.x, Power * 0.5f) - objTransform.position;
        if (!tossed)
        {
            tossed = true;
            throwable.GetComponent<Rigidbody>().isKinematic = false;
            throwable.GetComponent<Rigidbody>().AddForce(targetPosition);
            throwable.GetComponent<Rigidbody>().useGravity = true;
        }
        //if (throwable.transform.position.y <= 0.3f)
        //{
        //    reset = true;
        //}
        //if (reset)
        //{
        //    Reset();
        //}
    }
    public void Reset()
    {
        reset = false;
        tossed = false;
        Power = 100.0f;
        throwable.GetComponent<Rigidbody>().isKinematic = true;
        throwable.GetComponent<Rigidbody>().useGravity = false;
        objRot = objTransform.rotation = Quaternion.identity;
        objTransform.position = new Vector3(0.0f, 2.0f, 0.0f);
        throwable.GetComponent<Rigidbody>().MovePosition(objTransform.position);
        throwable.GetComponent<Rigidbody>().MoveRotation(objTransform.rotation);
    }
}
