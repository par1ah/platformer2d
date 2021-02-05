using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class money : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collider)
    {
        mycontrol gg = collider.GetComponent<mycontrol>();
        if (gg) {  //если мы берем монету объект уничтожается
            Destroy(gameObject);
        }
    }
}
