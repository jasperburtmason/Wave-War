using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    public float duration = 5f;

    private bool active;

    void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.tag == "Kill Box")
        {
            Destroy(gameObject);
        }
        else
        {
            Heart heart = collider.GetComponent<Heart>();
            if (!active && heart)
            {
                StartCoroutine(Activate(heart));
            }
        }  
    }

    public virtual IEnumerator Activate(Heart heart)
    {
        active = true;
        GetComponent<Collider2D>().enabled = false;
        GetComponent<Renderer>().enabled = false;

        yield return new WaitForSeconds(duration);

        Destroy(gameObject);
    }
}
