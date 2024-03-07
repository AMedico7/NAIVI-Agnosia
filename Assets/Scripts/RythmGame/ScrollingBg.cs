using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollingBg : MonoBehaviour
{

    public float speed = .2f;
    private MeshRenderer _renderer;

    private float time;
    private float offset;

    private float initialOffset;
    private float initialTime;

    void Start()
    {
        _renderer = GetComponent<MeshRenderer>();
        initialOffset = _renderer.material.mainTextureOffset.x;
        initialTime = 0f;

    }   

    // Update is called once per frame
    void Update()
    {   
        time = Time.time - initialTime;
        offset = time * speed;

        _renderer.material.mainTextureOffset = new Vector2(initialOffset + offset, 0f);
    }

    public void SetSpeed(float newSpeed)
    {

        if (Mathf.Sign(speed) != Mathf.Sign(newSpeed))
        {
            ChangeDirection();
        }

        speed = newSpeed;
    }

    public void ChangeDirection()
    {
        initialTime = time;
        initialOffset = offset;
    }
}
