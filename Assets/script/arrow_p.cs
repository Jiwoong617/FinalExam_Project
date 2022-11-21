using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class arrow_p : MonoBehaviour
{
    public GameObject arrow;
    public Transform bow;
    public float power;


    void Start()
    {
        power = 10f;
    }

    void Update()
    {
        look_mouse();
        fire(); 
    }

    void look_mouse()
    {
        Vector2 mouse_pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 bow_point = new Vector2(mouse_pos.x-bow.position.x, mouse_pos.y-bow.position.y);

        bow.right = bow_point;
    }

    void fire()
    {
        if (Input.GetMouseButtonDown(0))
        {
            GameObject firearrow = Instantiate(arrow, bow.position, bow.rotation);
            firearrow.GetComponent<Rigidbody2D>().velocity = firearrow.transform.right * power;
        }
    }
}
