using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 1.0f;
    public GameObject powerupIndicator;

    private bool hasPowerup;
    public bool HasPowerup
    {
        get
        {
            return hasPowerup;
        }
        set
        {
            hasPowerup = value;
            onPowerupChange(value);
        }
    }

    private Rigidbody playerRb;
    private GameObject focalPoint;
    private float powerupStrength = 15.0f;

    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        focalPoint = GameObject.Find("Focal Point");
    }

    // Update is called once per frame
    void Update()
    {
        float forwardInput = Input.GetAxis("Vertical");
        playerRb.AddForce(focalPoint.transform.forward * speed * forwardInput);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Powerup"))
        {
            HasPowerup = true;
            Destroy(other.gameObject);

            StartCoroutine("ExpirePowerup");
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy") && HasPowerup)
        {
            Rigidbody enemyRb = collision.gameObject.GetComponent<Rigidbody>();
            Vector3 awayFromPlayer = collision.gameObject.transform.position - transform.position;

            enemyRb.AddForce(awayFromPlayer * powerupStrength, ForceMode.Impulse);
            Debug.Log("Collided with: " + collision.gameObject.name + " with powerup set to: " + HasPowerup);
        }
    }

    // This method creates a new thread
    private IEnumerator ExpirePowerup()
    {
        yield return new WaitForSeconds(7);
        HasPowerup = false;
    }

    private void onPowerupChange(bool hasPowerup)
    {
        Debug.Log("powerup changed");
        powerupIndicator.SetActive(hasPowerup);
    }
}
