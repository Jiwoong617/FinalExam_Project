using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundLoop : MonoBehaviour
{

    // ��氡�α���
    public float endPoint = 0;
    void Update()
    {
        transform.Translate(Vector3.left * 10 * Time.deltaTime);

        if (transform.position.x <= endPoint)
            transform.position = new Vector3(245, 15, 1);
    }
}
