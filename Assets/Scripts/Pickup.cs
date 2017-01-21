using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    public enum Type { SineWave, TriangleWave, SquareWave, Shield }
    public Type type = Type.SineWave;

    void OnTriggerEnter2D(Collider2D collider)
    {
        Heart heart = collider.GetComponent<Heart>();
        Heart other = null;
        if (heart)
        {
            switch(type)
            {
                case Type.SineWave:
                    other = GameManager.instance.players[heart.player];
                    foreach(Wave wave in other.waves)
                    {
                        wave.function = Wave.Function.Sine;
                    }
                    break;

                case Type.TriangleWave:
                    other = GameManager.instance.players[heart.player];
                    foreach (Wave wave in other.waves)
                    {
                        wave.function = Wave.Function.Triangle;
                    }
                    break;

                case Type.SquareWave:
                    other = GameManager.instance.players[heart.player];
                    foreach (Wave wave in other.waves)
                    {
                        wave.function = Wave.Function.Square;
                    }
                    break;
            }
            Destroy(gameObject);
        }
    }
}
