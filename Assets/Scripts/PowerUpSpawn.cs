using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpSpawn : MonoBehaviour
{
    public Bounds bounds;
    public PowerUp[] powerUps;
    public Vector2 velocity;

    public float spawnCooldownMin;
    public float spawnCooldownMax;

    private float spawnCooldown;
    private float lastSpawn;

    private PowerUp lastPowerUp;
    
    void Start()
    {
        spawnCooldown = Random.Range(spawnCooldownMin, spawnCooldownMax);
        Vector3 position = transform.position;
        position.x = GameManager.instance.width / 2;
        transform.position = position;
    }

	void Update ()
    {
        if (!GameManager.instance.gameOver)
        {
            if (Time.time >= lastSpawn + spawnCooldown)
            {
                PowerUp powerUp = powerUps[Random.Range(0, powerUps.Length)];
                Vector3 position = new Vector3(
                          Random.Range(bounds.min.x, bounds.max.x),
                          Random.Range(bounds.min.y, bounds.max.y),
                          Random.Range(bounds.min.z, bounds.max.z)
                          );
                GameObject gameObject = Instantiate(powerUp.gameObject, transform.position + position, Quaternion.identity);
                Rigidbody2D rigidbody = gameObject.GetComponent<Rigidbody2D>();
                rigidbody.velocity = velocity;

                spawnCooldown = Random.Range(spawnCooldownMin, spawnCooldownMax);
                lastSpawn = Time.time;
                lastPowerUp = powerUp;
            }
        }
	}
}
