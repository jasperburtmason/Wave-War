using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heart : MonoBehaviour
{
    public int player;

    [HideInInspector]
    public float heartRate;
    public float heartRateMin = 0f;
    public float heartRateMax = 1f;

    public float heartRateGain = 0.1f;
    public float heartRateLoss = 0.1f;

    public float sizeMin = 1f;
    public float sizeMax = 4f;

    public float heartMovementSpeed = 1f;

    public Wave[] waves;
	
	new private Rigidbody2D rigidbody;
    
    void Start()
    {
        heartRate = (heartRateMax - heartRateMin) / 2;
        rigidbody = GetComponent <Rigidbody2D>();
    }

	void Update()
    {
        heartRate = Mathf.Clamp(heartRate - (heartRateLoss * Time.deltaTime), heartRateMin, heartRateMax);
        if ((player == 1 && Input.GetButtonDown("Jump") || player == 2 && Input.GetButtonDown("Jump-2")))
        {
            heartRate += heartRateGain;
        }

        float t = (heartRate - heartRateMin) / heartRateMax;
        foreach (Wave wave in waves)
        {
            wave.targetScale = t;
        }
        transform.localScale = new Vector3(sizeMin, sizeMin) + new Vector3(sizeMax - sizeMin, sizeMax - sizeMin) * t;

        Move();
	}

	void Move()
    {
        float y = (player == 1) ? Input.GetAxis("Vertical") : (player == 2) ? Input.GetAxis("Vertical-2") : 0f;
		Vector2 movement = new Vector2(0, y);
		rigidbody.MovePosition(rigidbody.position + movement * heartMovementSpeed * Time.deltaTime);
	}

	void OnCollisionEnter2D(Collision2D collision)
    {
		if (collision.gameObject.tag == "Waves")
        {
            GameManager.instance.GameOver(this);
            gameObject.SetActive(false);
        }
	}
}
