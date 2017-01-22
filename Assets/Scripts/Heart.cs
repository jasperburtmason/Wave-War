using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heart : MonoBehaviour
{
    public int player;

    [HideInInspector]
    public int health;
    public int healthMin = 0;
    public int healthMax = 100;

    public int damageAmount = 20;
    public ParticleSystem damageParticleSystem;

    [HideInInspector]
    public float rate;
    public float rateMin = 0f;
    public float rateMax = 1f;

    public float rateGain = 0.1f;
    public float rateLoss = 0.1f;

    public float sizeMin = 1f;
    public float sizeMax = 4f;

    public float movementSpeed = 1f;

    public Color colour;

    public Wave[] waves;
    public float waveSpeed;

	[HideInInspector]
	public bool poweredUp=false;

    new private Rigidbody2D rigidbody;
    
    void Start()
    {
        SetHealth(healthMax);
        rate = (rateMax - rateMin) / 2;
        rigidbody = GetComponent <Rigidbody2D>();
    }

	void Update()
    {
        if ((player == 0 && Input.GetButtonDown("Jump") || player == 1 && Input.GetButtonDown("Jump-2")))
        {
            rate = Mathf.Clamp(rate + (rateGain), rateMin, rateMax);
        }
        else
        {
            rate = Mathf.Clamp(rate - (rateLoss * Time.deltaTime), rateMin, rateMax);
        }


        float hr = (rate - rateMin) / rateMax;
        foreach (Wave wave in waves)
        {
            wave.targetSpeed = hr * waveSpeed;
        }
        transform.localScale = new Vector3(sizeMin, sizeMin) + new Vector3(sizeMax - sizeMin, sizeMax - sizeMin) * hr;

        ParticleSystem.MainModule main = damageParticleSystem.main;
        main.startSize = 1f / 100f * transform.lossyScale.magnitude;

        Move();
	}

	void Move()
    {
        float x = (player == 0) ? Input.GetAxis("Horizontal") : Input.GetAxis("Horizontal-2");
        float y = (player == 0) ? Input.GetAxis("Vertical") :  Input.GetAxis("Vertical-2");
		Vector3 movement = new Vector3(x, y);
        rigidbody.velocity = movement * movementSpeed; 
	}

	void OnCollisionEnter2D(Collision2D collision)
    {
		if (collision.gameObject.tag == "Waves" && !poweredUp)
        {
            SetHealth(Mathf.Clamp(health - damageAmount, healthMin, healthMax));

            damageParticleSystem.Play();

            if (health <= healthMin || health > healthMax)
            {
                GameManager.instance.GameOver(this);
            }
        }
        //bounce
	}

    void SetHealth(int health)
    {
		
		this.health = health;
		float h = (float)(health - healthMin) / healthMax;

		Color colour = this.colour;
		colour.r *= h;
		colour.g *= h;
		colour.b *= h;

		SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer> ();
		//spriteRenderer.color = colour;

		ParticleSystem.MainModule main = damageParticleSystem.main;
		main.startColor = colour;

    }
}
