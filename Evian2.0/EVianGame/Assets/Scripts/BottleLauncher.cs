using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BottleLauncher : MonoBehaviour
{
    [SerializeField] GameObject m_bottle = null;
    [SerializeField] Slider power = null;
    [SerializeField] TextMeshProUGUI time = null;
    [SerializeField] TextMeshProUGUI m_score = null;
    public float Power { get; set; } = 100.0f;

    private bool flip = true;
    private bool toss;
    private bool tossed;
    private bool reset;
    Transform bottleTransform = null;
    Quaternion bottleRot;
    float horizontal = 0.0f;
    float timer = 45.0f;

    private void Start()
    {
        bottleTransform = m_bottle.transform;
        bottleRot = bottleTransform.rotation;
        power.minValue = 100.0f;
        power.maxValue = 600.0f;
    }

    void Update()
    {
        timer -= Time.deltaTime;
        if (timer > 0)
        {
            time.text = (int)timer+"s remaining";
            if (Input.GetKey(KeyCode.A))
            {
                //look and aim left
                if (!Input.GetKeyUp(KeyCode.A) && bottleRot.y >= -45.0f)
                {
                    //aim right
                    bottleRot.y -= 0.4f * Time.deltaTime;
                }
            }
            else if (Input.GetKey(KeyCode.D))
            {
                //look and aim right
                if (!Input.GetKeyUp(KeyCode.D) && bottleRot.y <= 60.0f)
                {
                    //aim left
                    bottleRot.y += 0.4f * Time.deltaTime;
                }
            }
            else if (Input.GetKeyDown(KeyCode.W))
            {
                //aim up
                if (!Input.GetKeyUp(KeyCode.W) && bottleTransform.rotation.x >= -60.0f)
                {
                    //increment arc
                    bottleRot.x -= 0.2f;
                }
            }
            else if (Input.GetKeyDown(KeyCode.S))
            {
                //aim down
                if (!Input.GetKeyUp(KeyCode.S) && bottleTransform.rotation.x <= 60.0f)
                {
                    bottleRot.x += 0.2f;
                }
            }
            if (Input.GetKey(KeyCode.Space) == true)
            {
                toss = true;

                if (Input.GetKeyUp(KeyCode.Space) == false)
                {
                    power.value = Power;
                    //increment/decriment power
                    float powDif = 10.0f;
                    if (Power >= 600.0f)
                    {
                        flip = false;
                    }
                    else if (Power < 99.0f)
                    {
                        Power = 100.0f;
                        flip = true;
                    }
                    if (flip) Power += powDif;
                    else Power -= powDif;
                }

                //throw
            }
            Vector3 targetPosition = new Vector3(0.0f + Power * bottleRot.y, Power + Power * bottleRot.x, Power * 0.5f) - bottleTransform.position;
            GameObject arrow = transform.GetChild(2).gameObject;
            arrow.transform.rotation = new Quaternion(arrow.transform.rotation.x, bottleRot.y, arrow.transform.rotation.z,arrow.transform.rotation.w);
            if (toss && Input.GetKey(KeyCode.Space) == false && !tossed)
            {
                tossed = true;
                m_bottle.GetComponent<Rigidbody>().isKinematic = false;
                m_bottle.GetComponent<Rigidbody>().AddForce(targetPosition);
                m_bottle.GetComponent<Rigidbody>().useGravity = true;
                toss = false;
            }
            if (m_bottle.transform.position.y <= 0.3f)
            {
                reset = true;
            }
            if (reset)
            {
                Reset();
            }
        }
        else
        {
            PlayerPrefs.SetInt("Score", int.Parse(m_score.text));            
            SceneManager.LoadScene("MainMenu");
        }
    }

    public void Reset()
    {
        reset = false;
        tossed = false;
        Power = 100.0f;
        m_bottle.GetComponent<Rigidbody>().isKinematic = true;
        m_bottle.GetComponent<Rigidbody>().useGravity = false;
        bottleRot = bottleTransform.rotation = Quaternion.identity;
        bottleTransform.position = new Vector3(0.0f, 2.65f, -8.95f);
        m_bottle.GetComponent<Rigidbody>().MovePosition(bottleTransform.position);
        m_bottle.GetComponent<Rigidbody>().MoveRotation(bottleTransform.rotation);
    }
}
