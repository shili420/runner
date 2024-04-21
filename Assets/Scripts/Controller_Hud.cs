using UnityEngine;
using UnityEngine.UI;

public class Controller_Hud : MonoBehaviour
{
    public static bool gameOver = false;
    public Text distanceText;
    public Text gameOverText;
    private float distance = 0;

    //Distancia redondeada ("F0")
    void Start()
    {
        gameOver = false;
        distance = 0;
        distanceText.text = distance.ToString("F1");
        gameOverText.gameObject.SetActive(false);
    }


    //Distancia redondeada ("F0")
    void Update()
    {
        if (gameOver)
        {
            Time.timeScale = 0;
            gameOverText.text = "Game Over \n Total Distance: " + distance.ToString("F1");
            gameOverText.gameObject.SetActive(true);
        }
        else
        {
            distance += Time.deltaTime;
            distanceText.text = distance.ToString("F0");
        }
    }
}
