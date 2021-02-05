using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingMob : monster
{
    [SerializeField]
    private float time = 3.0F;//частота стрельбы
    [SerializeField]
    private Color bulletColor = Color.white;

    private bullet bullet;
    private SpriteRenderer sprite;

    protected override void Awake() 
    {
        sprite = GetComponentInChildren<SpriteRenderer>();
        bullet = Resources.Load<bullet>("knife");
    }

    protected override void Start() 
    {
        InvokeRepeating("Shoot", time, time); //gосле первого вызова эта функция повторяется каждые несколько секунд
    }

    private void Shoot() //стрельба
    {
        Vector3 position = transform.position;
        position.y += 0.25F;
        bullet newBullet = Instantiate(bullet, position, bullet.transform.rotation) as bullet;

        newBullet.Parent = gameObject;
        newBullet.Direction = -newBullet.transform.right; //стрельбы влево
        newBullet.Color = bulletColor;

    }

    protected  override void OnTriggerEnter2D(Collider2D collider)
    {
        Unit unit = collider.GetComponent<Unit>();
        if (bullet && unit is mycontrol)
        {
            if (Mathf.Abs(unit.transform.position.x - transform.position.x) < 0.7F) Damage();
            else unit.Damage();
        }
    }
}
