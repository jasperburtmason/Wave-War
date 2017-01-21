using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Synthesiser : MonoBehaviour
{
    public float frequency = 440f;
    public float amplitude = 0.05f;

    private int sample;
    private int samples;

    void Awake()
    {
        samples = AudioSettings.outputSampleRate;
    }

    void Update()
    {
        samples = AudioSettings.outputSampleRate;
    }

    void OnAudioFilterRead(float[] data, int channels)
    {
        for (var i = 0; i < data.Length; i = i + channels)
        {
            data[i] = Mathf.Sin((float)sample / samples * frequency * 2 * Mathf.PI) * amplitude;
            if (channels == 2) data[i + 1] = data[i];
            sample++;
        }
    }
}
