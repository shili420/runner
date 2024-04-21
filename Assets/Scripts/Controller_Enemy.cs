using UnityEngine;

public class Controller_Enemy : MonoBehaviour
{
    public static float enemyVelocity;
    private Rigidbody rb;
    public int damage = 1; //Danio que hace el enemigo cuando toca al jugador

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        rb.AddForce(new Vector3(-enemyVelocity, 0, 0), ForceMode.Force);
        OutOfBounds();
    }

    public void OutOfBounds()
    {
        if (this.transform.position.x <= -15)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter(Collider other) //Se activa el trigger del enemigo para hacer danio
    {
        if (other.CompareTag("Player"))
        {
            Controller_Player playerController = other.GetComponent<Controller_Player>();
            if (playerController != null)
            {
                playerController.TakeDamage(damage); // Llama al método del jugador para que reciba daño
            }
            Destroy(this.gameObject); // Destruye el enemigo después de causar daño al jugador
        }
    }
}
