using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipMove : MonoBehaviour
{
    public ControlsObj controls;
    public Vector2 direction { get; private set; }
    [SerializeField]
    private float speed;


    private Rigidbody2D rb;
    public Animator spriteAnim;
    // Start is called before the first frame update
    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 temp_direction = Vector2.zero;
        if (Input.GetKey(controls.up))
        {
            temp_direction.y += 1;
        }
        if (Input.GetKey(controls.down))
        {
            temp_direction.y -= 1;
        }
        if (Input.GetKey(controls.left))
        {
            temp_direction.x -= 1;
        }
        if (Input.GetKey(controls.right))
        {
            temp_direction.x += 1;
        }

        direction = temp_direction;
    }

    private void FixedUpdate()
    {
        rb.MovePosition(this.rb.position + direction.normalized * speed * Time.deltaTime);
        spriteAnim.SetInteger("Direction", (int)direction.y);
    }
}
