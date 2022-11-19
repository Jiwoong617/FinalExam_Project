using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow_pointer : MonoBehaviour
{
    private float angle;
    private Vector2 target;
    private Vector2 mouse;
    public int clicked = 0;
    // Start is called before the first frame update
    void Start()
    {
        target = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        //mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //angle = Mathf.Atan2(mouse.y - target.y, mouse.x - target.x) * Mathf.Rad2Deg;
        //this.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        if (Input.GetMouseButtonUp(0))
        {
            clicked = 1;
        }
        if (clicked == 0)
        {
            mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            angle = Mathf.Atan2(mouse.y - target.y, mouse.x - target.x) * Mathf.Rad2Deg;
            this.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        }
    }
}
