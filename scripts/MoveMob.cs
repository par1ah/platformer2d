using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class MoveMob : monster
{
    [SerializeField]
    private float speed = 2.0F;
    private bullet bullet;

    private Vector3 direction;
    private SpriteRenderer sprite;

    protected override void Awake()
    {
        bullet = Resources.Load<bullet>("knife");
        sprite = GetComponentInChildren<SpriteRenderer>();
    }

    protected override void Start()
    {
        direction = transform.right; //перемещает GameObject с учетом его поворота
    }
    protected override void Update()
    {
        Move();
    }

    protected override void OnTriggerEnter2D(Collider2D collider)
    {
        Unit unit = collider.GetComponent<Unit>();
        if (unit && unit is mycontrol)
        {
            if (Mathf.Abs(unit.transform.position.x - transform.position.x) < 0.7F) Damage(); //проверка получения урона сбоку
            else unit.Damage(); //если прыгаем он умирает
        }
    }

    private void Move()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position + transform.up * 0.5F + transform.right*direction.x * 0.9F, 0.1F);
        //проверка препятствий

        if (colliders.Length > 0 && colliders.All(x => !x.GetComponent<mycontrol>())) //
        {
            direction *= -1F;
            sprite.flipX = direction.x < 0.0f; //разворот 
        }


        transform.position = Vector3.MoveTowards(transform.position, transform.position + direction, speed * Time.deltaTime); //передвижение моба вправо
    }
}
