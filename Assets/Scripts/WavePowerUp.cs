using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WavePowerUp : PowerUp
{
    public Wave.Function function = Wave.Function.Sine;

    public override IEnumerator Activate(Heart heart)
    {
        Heart other = GameManager.instance.players[heart.player];
        foreach (Wave wave in other.waves)
        {
            wave.function = function;
        }

        yield return StartCoroutine(base.Activate(heart));

        foreach (Wave wave in other.waves)
        {
            wave.function = Wave.Function.Sine;
        }
    }
}
