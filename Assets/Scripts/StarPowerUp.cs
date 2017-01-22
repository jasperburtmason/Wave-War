using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarPowerUp : PowerUp
{
    public override IEnumerator Activate(Heart heart)
    {
        heart.GetComponent<Animator>().SetBool("Star Power Up", true);
		heart.poweredUp = true;
        yield return StartCoroutine(base.Activate(heart));
        heart.GetComponent<Animator>().SetBool("Star Power Up", false);
		heart.poweredUp = false;
    }
}
