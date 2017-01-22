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

    public int segments = 64;

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
        lineRenderer.numPositions = segments;

        Vector2[] points = new Vector2[segments];
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

            Vector2 point = new Vector2(x - 0.5f, y);
            points[p] = point;
            lineRenderer.SetPosition(p, point);      
        }

        /*
        Vector2[] vertices = new Vector2[segments * 2];
        for(int p = 0; p < points.Length - 1; p++)
        {
            Vector2 center = points[p];
            Vector2 normal = Quaternion.Euler(0f, 0f, -90f) * (points[p + 1] - center).normalized;
            vertices[p * 2] = center + normal * 1;
            vertices[p * 2 + 1] = center - normal * 1;
        }


        PolygonCollider2D polygonCollider = GetComponent<PolygonCollider2D>();
        polygonCollider.points = vertices;
        */
        EdgeCollider2D edgeCollider = GetComponent<EdgeCollider2D>();
        edgeCollider.points = points;
        /*
        EdgeCollider2D[] edgeColliders = GetComponents<EdgeCollider2D>();
        edgeColliders[0].offset = new Vector2(0, lineRenderer.widthMultiplier / 2 / transform.localScale.y);
        edgeColliders[0].points = points;
        edgeColliders[1].offset = new Vector2(0, -lineRenderer.widthMultiplier / 2 / transform.localScale.y);
        edgeColliders[1].points = points;
        */

        time += Time.deltaTime * speed;
    }

    void OnAudioFilterRead(float[] data, int channels)
    {

    }
}
