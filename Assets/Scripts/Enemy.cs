using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed = 3.0f;
    private Rigidbody enemyRb;
    private GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        enemyRb = GetComponent<Rigidbody>();
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        MoveTowardPlayer();
        DestroyOnFall();
    }

    private void MoveTowardPlayer()
    {
        Vector3 direction = player.transform.position - transform.position;
        enemyRb.AddForce(direction.normalized * speed);
    }

    private void DestroyOnFall()
    {
        if (transform.position.y < - 10)
        {
            Destroy(gameObject);
        }
    }
}
