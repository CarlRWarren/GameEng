using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BottleLauncher : MonoBehaviour
{
    [SerializeField] GameObject m_bottle = null;
    public float Power { get; set; }

    void Update()
    {
        Quaternion bottleRot = m_bottle.transform.rotation;
        
        if (Input.GetKeyDown(KeyCode.A))
        {
            Debug.Log("Enter a");

            //look and aim left
            while (Input.GetKeyDown(KeyCode.A)&&bottleRot.y <= 45.0f)
            {
                Debug.Log("Enter a loop");

                //aim right
                bottleRot.y += 0.2f;
            }
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            Debug.Log("Enter d");

            //look and aim right
            while (Input.GetKeyDown(KeyCode.D) && bottleRot.y >= -45.0f)
            {
                //aim left
                Debug.Log("Enter d loop");

                bottleRot.y -= 0.2f;
            }
        }
        else if (Input.GetKeyDown(KeyCode.W))
        {
            Debug.Log("Enter w");

            //aim up
            while (Input.GetKeyDown(KeyCode.W) && bottleRot.x <= 45.0f)
            {
                //increment arc
                Debug.Log("Enter w loop");

                bottleRot.x += 0.2f;
            }
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            Debug.Log("Enter s");

            //aim down
            while (Input.GetKeyDown(KeyCode.S) && bottleRot.x >= -45.0f)
            {
                Debug.Log("Enter s loop");

                bottleRot.x -= 0.2f;
            }
        }
        else if (Input.GetKeyDown(KeyCode.Space)==true)
        {
            Debug.Log("Entered space");

            while (Input.GetKeyDown(KeyCode.Space)==true)
            {
                Debug.Log("Entered loop");

                //increment/decriment power
                float powDif = 0.3f;
                if (Power >= 6.0f)
                {
                    powDif *= -1.0f;
                }
                else if (Power <= 0.0f)
                {
                    powDif *= -1.0f;
                }
                else
                {
                    Power += powDif;
                }
                if (Input.GetKeyUp(KeyCode.Space))
                {
                    break;
                }
            }
            //throw
            Debug.Log(Power.ToString());
        }
        if (m_bottle.transform.position.y <= -1.0f)
        {
            Reset();
        }
    }

    private void Reset()
    {
        m_bottle.transform.position = new Vector3(0.0f, 0.0f, 0.62f);
    }
}
