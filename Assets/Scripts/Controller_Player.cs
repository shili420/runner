using UnityEngine;
using System.Collections.Generic; //Libreria que use para hacer una lista de todos los fondos del parallax

public class Controller_Player : MonoBehaviour
{
    private Rigidbody rb;
    public float jumpForce = 10;
    private float initialSize;
    private int i = 0;
    private bool floored;
    private bool hasJumped = false;
    public int playerLives = 1; //Vidas con la que empieza un jugador
    public int extraLives = 0; //Vidas que gana el jugador

    public List<Parallax> allParallaxScripts = new List<Parallax>(); // Poder ingresar al codigo de parallax y crear una lista de todos los fondos


    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        initialSize = rb.transform.localScale.y;
    }

    void Update()
    {
        GetInput();
    }

    private void GetInput()
    {
        Jump();
        Duck();
    }

    private void Jump()
    {
        if (floored || !hasJumped) // Permitir el salto si está en el suelo o si no ha saltado todavía
        {
            if (Input.GetKeyDown(KeyCode.W))
            {
                rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z); // Restablecer la velocidad vertical
                rb.AddForce(new Vector3(0, jumpForce, 0), ForceMode.Impulse);
                if (!hasJumped && !floored) // Si no ha saltado antes y no está en el suelo
                {
                    hasJumped = true; // Marcar que ha realizado el primer salto
                }
            }
        }
    }

    private void Duck()
    {
        if (floored)
        {
            if (Input.GetKey(KeyCode.S))
            {
                if (i == 0)
                {
                    rb.transform.localScale = new Vector3(rb.transform.localScale.x, rb.transform.localScale.y / 2, rb.transform.localScale.z);
                    i++;
                }
            }
            else
            {
                if (rb.transform.localScale.y != initialSize)
                {
                    rb.transform.localScale = new Vector3(rb.transform.localScale.x, initialSize, rb.transform.localScale.z);
                    i = 0;
                }
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.S))
            {
                rb.AddForce(new Vector3(0, -jumpForce, 0), ForceMode.Impulse);
            }
        }
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            { 
                Destroy(this.gameObject);
                Controller_Hud.gameOver = true;
                // Detener el movimiento de todos los fondos
                foreach (Parallax parallaxScript in allParallaxScripts)
                {
                    parallaxScript.StopParallax(); //Llamo al metodo de parallax para detenerlo cuando colisiona con un enemigo
                }
            }

        }

        if (collision.gameObject.CompareTag("Floor"))
        {
            floored = true;
            hasJumped = false;
        }
    }

    public void TakeDamage(int damageAmount) //Metodo para recibir da;o llamado en el script del enemigo
    {
        if (extraLives > 0)
        {
            extraLives--; // Reducir una vida extra si el jugador tiene alguna
            Debug.Log("¡Has utilizado una vida extra! Vidas extras restantes: " + extraLives);
        }
        else
        {
            playerLives -= damageAmount; // Reduce las vidas del jugador según la cantidad de daño recibido
            Debug.Log("¡Has recibido " + damageAmount + " de daño! Vidas restantes: " + playerLives);

            if (playerLives <= 0)
            {
                Debug.Log("¡Game Over!");
                Destroy(gameObject); // Destruye el jugador si se queda sin vidas
                Controller_Hud.gameOver = true;
                // Detener el movimiento de todos los fondos
                foreach (Parallax parallaxScript in allParallaxScripts)
                {
                    parallaxScript.StopParallax(); //Llamo al metodo de parallax para detenerlo cuando colisiona con un enemigo
                }
            }
        }
    }

        private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Floor"))
        {
            floored = false;
        }
    }

    public void GainExtraLife()
    {
        extraLives++; // Incrementa las vidas extras del jugador
        Debug.Log("¡Has obtenido una vida extra! Vidas extras: " + extraLives);
    }

    
}
