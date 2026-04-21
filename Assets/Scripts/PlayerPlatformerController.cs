using Unity.VisualScripting.Dependencies.NCalc;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerPlatformerController : MonoBehaviour
{
    public int vida = 5;
    public Animator animator;
    [Header("Configuracion de Movimiento")]
    public float moveSpeed = 8f;   
    public float jumpForce = 15f;
    public float fuerzaRebote = 15f;



    [Header("Configuracion del Suelo (GroundCheck)")]
    public Transform groundCheck;  
    public float groundCheckRadius = 0.2f; 
    public LayerMask groundLayer;  

    private Rigidbody2D rb;
    private float horizontalInput;
    private bool isGrounded;
    public bool isDiying;
    public bool recibiendoDano;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (!isDiying)
        {
            horizontalInput = Input.GetAxisRaw("Horizontal");
            isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

            animator.SetBool("onground", isGrounded);
            animator.SetFloat("Movement", horizontalInput * moveSpeed);
            animator.SetBool("recibeDano", recibiendoDano);

            if (horizontalInput < 0)
            {
                transform.localScale = new Vector3(-1, 1, 1);
            }
            if (horizontalInput > 0)
            {
                transform.localScale = new Vector3(1, 1, 1);
            }

            Vector3 posicion = transform.position;
            if (!recibiendoDano)
            {
                transform.position = new Vector3(posicion.x, posicion.y, posicion.z);
            }


            if (Input.GetButtonDown("Jump") && isGrounded && !recibiendoDano)
            {
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
            }
            

        }
        

    }

    void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(horizontalInput * moveSpeed, rb.linearVelocity.y);
    }

    private void OnDrawGizmosSelected()
    {
        if (groundCheck != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
        }
    }

    public void RecibeDanio(Vector2 direccion, int cantDanio)
    {
        if (!recibiendoDano)
        {
            recibiendoDano = true;
            vida -= cantDanio;
            Vector2 rebote = new Vector2(transform.position.x - direccion.x, 0.2f).normalized;
            rb.AddForce(rebote * fuerzaRebote, ForceMode2D.Impulse);
            //if (vida <= 0)
            //{
            //    muerto = true;
            //}
            //if (!muerto)
            //{
                
            //}
        }
    }

    public void DesactivaDanio()
    {
        recibiendoDano = false;
        rb.linearVelocity = Vector2.zero;
    }
}