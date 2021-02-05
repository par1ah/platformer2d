using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mycontrol : Unit
{
    [SerializeField]
    private float speed = 4F;
    [SerializeField]
    private int lives = 5;
    public int Lives 
    {
        get { return lives; }
        set
        {
            if (value < 5) lives = value; //устанавливаем значение хп
            livesBar.Refresh();
        }
    }

    private lives livesBar;
    
    [SerializeField]
    private float jumpForce = 900;

    private Rigidbody2D rb; //ссылка на компонент
    private Animator animator; //ссылка на аниматор
    private SpriteRenderer sprite; //ссылка на спрайт

    private bool isGrounded;
    public Transform groundCheck;
    public float CheckRadius;
    public LayerMask whatIsGround;

    private int extraJump;
    public int extraJumpValue;

    private bullet bullet; 

    private CharState State
    {
        get { return (CharState)animator.GetInteger("State"); }
        set { animator.SetInteger("State", (int)value); }
    }

    private void Awake() //получение ссылок
    {
        rb = GetComponent<Rigidbody2D>(); 
        animator = GetComponent<Animator>();
        sprite = GetComponentInChildren<SpriteRenderer>();
        extraJump = extraJumpValue;

        livesBar = FindObjectOfType<lives>();

        bullet = Resources.Load<bullet>("knife"); //ссылка на префаб
    }

    private void Update() //логика управления
    {
        if (isGrounded) State = CharState.Animation;
        if (Input.GetButton("Horizontal")) Run();
        if (Input.GetKeyDown(KeyCode.Space)) Jump();
        if (Input.GetKeyDown(KeyCode.E)) Shoot();

    }
    private void Run() //передвижение по забиндженным клавишам, флип картинки
    {
        Vector3 direction = transform.right * Input.GetAxis("Horizontal");
        transform.position = Vector3.MoveTowards(transform.position, transform.position + direction, speed * Time.deltaTime);
        sprite.flipX = direction.x < 0.0f;
        if (isGrounded) State = CharState.walk;
    }
    private void Jump() //реализация ограниченного числа прыжков с проверкой изграунд
    {
        if (!isGrounded) State = CharState.jump;
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, CheckRadius, whatIsGround);
        if (isGrounded == true)
        {
            extraJump = extraJumpValue;
        }
        if (Input.GetKeyDown(KeyCode.Space) && extraJump > 0)
        {
            rb.AddForce(Vector2.up * jumpForce);
            extraJump--;
        }
        else if (Input.GetKeyDown(KeyCode.Space) && extraJump == 0 && isGrounded == true)
        {
            rb.velocity = Vector2.up * jumpForce;
        }
    }
    private void Shoot() 
    {
        Vector3 position = transform.position;
        position.y += -0.2f; //откуда относительно персонажа выстреливают пули
        bullet newBullet = Instantiate(bullet, position, bullet.transform.rotation) as bullet; //создание

        newBullet.Direction = newBullet.transform.right * (sprite.flipX ? -1.0F : 1.0F); //выстреливает по той же траектории, в которую мы повернуты
        newBullet.Parent = gameObject; //мы выпускаем снаряд
    }

    public GameObject respawn;

    public override void Damage()
    {
        Unit unit = GetComponent<Unit>();

        Lives--;
            rb.velocity = Vector3.zero;
        rb.AddForce(transform.up * 10.0F, ForceMode2D.Impulse); //подкидывает нас при получении урона
        Debug.Log(lives);
        if ((Lives <= 0) && unit is mycontrol) //смерть
        { 
            transform.position = respawn.transform.position;
            lives = 5;
            Lives = 5;
        }
    }


    private void OnTriggerEnter2D(Collider2D collider)
    {
        bullet bullet = collider.gameObject.GetComponent<bullet>();
        Unit unit = collider.gameObject.GetComponent<Unit>();
        if (bullet && bullet.Parent != gameObject) Damage();
        money hp = collider.gameObject.GetComponent<money>();
        if (hp && Lives<=4) { lives++; Lives++; } //восстановление хп
    }
}

public enum CharState
{
    Animation,
    walk,
    jump
}
