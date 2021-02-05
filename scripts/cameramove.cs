
using UnityEngine;

public class cameramove : MonoBehaviour
{
    public GameObject player;
    [SerializeField]
    private float speed = 2.0F;
    [SerializeField]
    private Transform target;

    private void Awake()
    {
        if (!target) { target = FindObjectOfType<mycontrol>().transform; } //проверка таргета и поиск его
    }
    void Update()
    {
        Vector3 position = target.position;
        position.z = -10.0F; //чтобы камера не ломалась а работала в 2d

        transform.position = Vector3.Lerp(transform.position, position, speed * Time.deltaTime); 
    }
}
