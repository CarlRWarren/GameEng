using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Reticle : MonoBehaviour
{    
    [SerializeField] GameObject ball;
    public Pointer pointer;
    public SpriteRenderer circleRender;

    public Sprite sprite;
    public Camera camera = null;
    private void Awake()
    {
        pointer.OnPointerUpdate += UpdateSprite;
        camera = Camera.main;
    }
    void Update()
    {
        transform.LookAt(camera.gameObject.transform);
    }
    private void OnDestroy()
    {
        pointer.OnPointerUpdate -= UpdateSprite;
    }
    private void UpdateSprite(Vector3 point, GameObject hitObject)
    {
        ball.transform.position = point;

        transform.position = point;
        if (hitObject)
        {
            circleRender.sprite = sprite;
        }
    }
}
