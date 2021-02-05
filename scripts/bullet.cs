using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour
{
    public GameObject parent; //кто выпустил снаряд
    public GameObject Parent { set { parent = value; }
        get { return parent; } } //получаем родителя

    private float speed = 10.0f;
    private Vector3 direction;
    public Vector3 Direction { set { direction = value; } } //устанавливаем направление

    private SpriteRenderer sprite;

    public Color Color
    {
        set { sprite.color = value; } //передает цвет
    }

    private void Awake() //получение ссылки
    {
        sprite = GetComponentInChildren<SpriteRenderer>();
    }

    private void Start() //через какое время исчезает
    {
        Destroy(gameObject, 1.5F);
    }

    private void Update() 
    {
        transform.position = Vector3.MoveTowards(transform.position, transform.position + direction, speed * Time.deltaTime);
        sprite.flipX = direction.x < 0.0f;
    }
    private void OnTriggerEnter2D(Collider2D collider)
    {
        Unit unit = collider.GetComponent<Unit>(); 
        if (unit && unit.gameObject != parent) //тот кто выпускает снаряд не получает урон
        {

            if (!(unit is MoveMob)) unit.Damage(); //если монстр двигается, то пулей не убивается
            Destroy(gameObject);
        }
    }
}
