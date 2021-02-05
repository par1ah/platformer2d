using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class moveBlock : MonoBehaviour
{
    [SerializeField]
    private float speed = 2.0F;

    private Vector3 direction;
    private SpriteRenderer sprite;
    protected void Awake()
    {
        sprite = GetComponentInChildren<SpriteRenderer>();
    }

    protected void Start()
    {
        direction = transform.up;
    }
    protected void Update()
    {
        Move();
    }
    private void Move()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position + transform.up * 0.5F + transform.up * direction.y * 0.9F, 0.1F);
        //проверка препятствий

        if (colliders.Length > 0 && colliders.All(x => !x.GetComponent<mycontrol>())) //
        {
            direction *= -1F;
            sprite.flipX = direction.x < 0.0f; //разворот 
        }


        transform.position = Vector3.MoveTowards(transform.position, transform.position + direction, speed * Time.deltaTime); //передвижение моба вправо
    }
}
