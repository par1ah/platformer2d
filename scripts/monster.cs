using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class monster : Unit
{
    protected virtual void Awake() { }
    protected virtual void Start() { }
    protected virtual void Update() { }

    protected virtual void OnTriggerEnter2D(Collider2D collider)
    {
        bullet bullet = collider.GetComponent<bullet>();
        if (bullet)
        {
            Damage();
        }

        Unit unit = collider.GetComponent<Unit>();

        if(unit && unit is mycontrol)
        {
            unit.Damage();
        }
    }
}
