using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class arrow_rigid : MonoBehaviour
{
    void Update()
    {
        transform.right = GetComponent<Rigidbody2D>().velocity;
    }
}
