using System;
using UnityEngine;

public class Spining : MonoBehaviour
{
    public float RotationSpeed = 45;

    public void Update()
    {
        transform.Rotate(Vector3.one * RotationSpeed * Time.deltaTime);
    }
}
