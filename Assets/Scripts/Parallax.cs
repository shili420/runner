using UnityEngine;

public class Parallax : MonoBehaviour
{
    public GameObject cam;
    private float length, startPos;
    public float parallaxEffect;
    private bool isMoving = true; //Parallax en funcionamiento 


    void Start()
    {
        startPos = transform.position.x;
        length = GetComponent<SpriteRenderer>().bounds.size.x;

      
    }
    void Update()
    {
        if (isMoving) //Que empiece moviendose
        {
            transform.position = new Vector3(transform.position.x - parallaxEffect, transform.position.y, transform.position.z);
            if (transform.localPosition.x < -20)
            {
                transform.localPosition = new Vector3(20, transform.localPosition.y, transform.localPosition.z);
            }
        }
    }

    public void StopParallax() //Metodo que llamo en el controlador para que hacer saber al parallax que el personaje esta muerto
    {
        isMoving = false;
    }
}
