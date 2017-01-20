using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Wave Object")]
public class WaveObject : ScriptableObject
{
    public enum Function { Sine, Square, Triangle};
    public Function function = Function.Sine;
    public float frequency = 1f;
    public float amplitude = 1f;
}
