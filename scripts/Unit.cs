using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    public virtual void Damage() //получение урона
    {
        Die();
    }
    protected virtual void Die()
    {
        Destroy(gameObject);
    }
}
