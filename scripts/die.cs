using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class die : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collider)
    {
        Unit unit = collider.GetComponent<Unit>();

        if (unit && unit is mycontrol) //я получаю дамаг
        {
            //public GameObject respawn;
            // other.transform.position = respawn.transform.position;
            unit.Damage();
        }

    }
}
