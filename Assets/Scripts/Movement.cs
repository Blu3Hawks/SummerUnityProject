using UnityEngine;

public class Movement : MonoBehaviour
{
    public Vector3 pointB = new Vector3 (0f,10f, 0f);
    public float speed = 1.0f;

    private Vector3 pointA;
    private float elapsedTime = 0f;

    void Start()
    {
        pointA = transform.position;
    }

    void Update()
    {
        elapsedTime += Time.deltaTime;
        float t = elapsedTime / speed;

        transform.position = Vector3.Lerp (pointA, pointB, t);
    }
}
