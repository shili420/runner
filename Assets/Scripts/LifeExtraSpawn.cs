using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeExtraSpawn : MonoBehaviour
{
    public List<GameObject> powerup;
    public GameObject instantiatePos;
    public float respawningTimer;
    private float time = 0;

    void Start()
    {
        Controller_Extra.extraVelocity = 2;
    }

    void Update()
    {
        SpawnExtra();
        ChangeVelocity();
    }

    private void ChangeVelocity()
    {
        time += Time.deltaTime;
        Controller_Enemy.enemyVelocity = Mathf.SmoothStep(1f, 15f, time / 45f);
    }

    private void SpawnExtra()
    {
        respawningTimer -= Time.deltaTime;

        if (respawningTimer <= 0)
        {
            Instantiate(powerup[UnityEngine.Random.Range(0, powerup.Count)], instantiatePos.transform);
            respawningTimer = UnityEngine.Random.Range(2, 6);
        }
    }
}
