using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wave : MonoBehaviour
{
    public enum Function { Sine, Triangle, Square };
    public Function function;
    public float frequency;
    public float amplitude;

    public float startSpeed = 0f;
    public float targetSpeed = 1f;
    public float speedSmoothness = 1f;

    public int pointCount = 64;

    private float speed;
    private float time;

    void Start()
    {
        speed = startSpeed;
        transform.localScale = new Vector3(GameManager.instance.width, transform.localScale.y, transform.localScale.z);
    }

	void Update ()
    {
        speed = Mathf.Lerp(speed, targetSpeed, Time.deltaTime * speedSmoothness);

        LineRenderer lineRenderer = GetComponent<LineRenderer>();
        EdgeCollider2D edgeCollider = GetComponent<EdgeCollider2D>();
        lineRenderer.numPositions = pointCount;

        Vector2[] points = new Vector2[pointCount];
        for(int p = 0; p < points.Length; p++)
        {
            float x = (float)p / (points.Length - 1);
            float y = 0;

            float t = (time + x) * frequency;
            switch (function)
            {
                case Function.Sine:
                    y = Mathf.Sin(t * 180f * Mathf.Deg2Rad);
                    break;
                case Function.Triangle:
                    y = Mathf.PingPong(t * 180f * Mathf.Deg2Rad, Mathf.PI) / Mathf.PI * 2f - 1f;
                    break;
                case Function.Square:
                    y = Mathf.Sign(Mathf.Sin(t * 180f * Mathf.Deg2Rad));
                    break;
            }
            y *= amplitude * speed; 

            Vector3 point = new Vector2(x - 0.5f, y);
            points[p] = point;
            lineRenderer.SetPosition(p, point);      
        }

        time += Time.deltaTime * speed;

        edgeCollider.points = points;
    }

    void OnAudioFilterRead(float[] data, int channels)
    {

    }
}
