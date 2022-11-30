using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundLoop : MonoBehaviour
{

    // 배경가로길이
    private float width;
    public float spawnPoint = 0;
    public float endPoint = 0;


    private void Awake()
    {
        //BoxCollider2D backgroundCollider = GetComponent<BoxCollider2D>();
        //width = backgroundCollider.size.x;
    }
   

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x <= endPoint)
        {
            Reposition();
        }
    }
    private void Reposition()
    {
        //Vector2 offset = new Vector2(width * 2f+spawnPoint, 0);
        Vector3 offset = new Vector3(spawnPoint,25, 0);
        transform.position = offset;
    }
}
