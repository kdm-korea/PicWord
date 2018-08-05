using UnityEngine;

public class Calculator : MonoBehaviour {

    public float GetX(float degree, float radius)
    {
        return Mathf.Cos(degree * Mathf.Deg2Rad) * radius;
    }

    public float GetY(float degree, float radius)
    {
        return Mathf.Sin(degree * Mathf.Deg2Rad) * radius;
    }

    public Vector3 GetPosition(float degree, float radius, float height)
    {
        float x = GetX(degree, radius);
        float z = GetY(degree, radius);

        return new Vector3(x, height, z);
    }
}