using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fallingBlock : MonoBehaviour
{
    Rigidbody2D rb;
    Vector2 mposition;
    bool back;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        mposition = transform.position;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name.Equals("gg") && back == false)//когда объект с именем прикасается - платформа падает
        {
            Invoke("FallPlatform", 1f);
        }
    }
    void FallPlatform()
    {
        rb.isKinematic = false;
        Invoke("BackBlock", 3f);
    }

    void BackBlock()
    {
        rb.velocity = Vector2.zero;
        rb.isKinematic = true;
        back = true;
    }
    void Update()
    {
        if (back == true)
        {
            transform.position = Vector2.MoveTowards(transform.position, mposition, 3f * Time.deltaTime); //если падает, то двигается из текущего положения в ранее
        }
        if (transform.position.y == mposition.y)
        {
            back = false;
        }
    }
}
