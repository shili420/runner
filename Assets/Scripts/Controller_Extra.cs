using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller_Extra : MonoBehaviour
{
    public static float extraVelocity;
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        rb.AddForce(new Vector3(-extraVelocity, 0, 0), ForceMode.Force);
        OutOfBounds();
    }

    public void OutOfBounds()
    {
        if (this.transform.position.x <= -15)
        {
            Destroy(this.gameObject);
        }
    }
    private void OnTriggerEnter(Collider other) //Si el prefab toca al jugador se activa el trigger para ganar la vida
    {
        if (other.CompareTag("Player")) 
        {
            Controller_Player playerController = other.GetComponent<Controller_Player>();
            if (playerController != null)
            {
                playerController.GainExtraLife(); // Llama al método del jugador para otorgar una vida extra
                Destroy(gameObject); // Destruye el prefab de vida extra una vez que toco al jugador
            }
        }
    }
}
