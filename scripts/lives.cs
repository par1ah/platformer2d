using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lives : MonoBehaviour
{
    private Transform[] hearts = new Transform[5];
    private mycontrol gg;

    private void Awake()
    {
        gg = FindObjectOfType<mycontrol>();
        for (int i = 0; i < hearts.Length; i++)
        {
            hearts[i] = transform.GetChild(i); //получение объектов
        }
    }


    public void Refresh()
    {
        for (int i=0; i<hearts.Length; i++)
        {
            if (i < gg.Lives) hearts[i].gameObject.SetActive(true); //активны только те, которые есть
            else hearts[i].gameObject.SetActive(false);
        }
    }
}
