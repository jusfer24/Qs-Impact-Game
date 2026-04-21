using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [Header("Configuración de Movimiento")]
    public Transform player;
    public float detectionRadius = 4.0f;
    public float speed = 1.0f;

    [Header("Salud del Enemigo")]
    public int saludMaxima = 3;
    private int saludActual;
    private bool estaMuerto = false; // Controla si el enemigo ya fue derrotado

    private Rigidbody2D rb;
    private Animator animator;
    private Vector2 movement;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        // Iniciamos con la salud al máximo
        saludActual = saludMaxima;

        if (player == null)
        {
            GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
            if (playerObj != null) player = playerObj.transform;
        }
    }

    void Update()
    {
        // Si el enemigo está muerto o el jugador no existe, cancelamos todo el Update
        if (estaMuerto || player == null) return;

        // Calcular distancia hacia el jugador
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        if (distanceToPlayer < detectionRadius)
        {
            // Calcular la dirección (hacia dónde debe caminar)
            Vector2 direction = (player.position - transform.position).normalized;
            movement = new Vector2(direction.x, 0);

            // Lógica para invertir
            if (movement.x > 0)
            {
                transform.localScale = new Vector3(-1, 1, 1);
            }
            else if (movement.x < 0)
            {
                transform.localScale = new Vector3(1, 1, 1);
            }
        }
        else
        {
            movement = Vector2.zero;
        }
    }

    void FixedUpdate()
    {
        // Si está muerto, que no se mueva
        if (estaMuerto) return;

        // El movimiento físico (Rigidbody) DEBE ir en FixedUpdate para no dar tirones
        rb.MovePosition(rb.position + movement * speed * Time.fixedDeltaTime);
    }

    // LÓGICA DE DAÑO Y MUERTE ----------------------------------------------------

    // Este método se llama cuando una bala choca con el enemigo
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // IMPORTANTE: Asegúrate de que tu prefab de la bala tenga asignado el Tag "Bala"
        if (collision.CompareTag("Bala"))
        {
            RecibirDanio(1); // Le restamos 1 de vida por bala

            // Opcional: Destruir la bala al impactar para que no atraviese al enemigo
            Destroy(collision.gameObject);
        }
    }

    public void RecibirDanio(int danio)
    {
        if (estaMuerto) return; // Si ya está muerto, ignoramos nuevos daños

        saludActual -= danio;

        if (saludActual <= 0)
        {
            Morir();
        }
        else
        {
            // Si aún tiene vida, activamos la animación de daño
            animator.SetTrigger("ImpactBullet");
        }
    }

    private void Morir()
    {
        estaMuerto = true; // Cambiamos el estado
        movement = Vector2.zero; // Lo detenemos en seco

        // Activamos la animación de muerte en el Animator
        animator.SetTrigger("isDie");

        // Desactivamos las físicas y colisiones para que el jugador no choque con el cadáver
        GetComponent<Collider2D>().enabled = false;
        rb.simulated = false;

        // Destruimos el objeto del enemigo después de 1.5 segundos 
        // (Ajusta este número para que coincida con la duración de tu animación DieEnemie)
        Destroy(gameObject, 1.5f);
    }

    // ----------------------------------------------------------------------------

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Si está muerto, ya no le hace daño al jugador
        if (estaMuerto) return;

        if (collision.gameObject.CompareTag("Player"))
        {
            // Calculamos desde dónde viene el golpe para que el jugador sepa hacia dónde retroceder (knockback)
            Vector2 direccionDanio = (collision.transform.position - transform.position).normalized;

            // Verificamos de forma segura que el jugador tenga el script antes de causarle daño
            PlayerPlatformerController playerController = collision.gameObject.GetComponent<PlayerPlatformerController>();
            if (playerController != null)
            {
                playerController.RecibeDanio(direccionDanio, 1);
            }
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }
}