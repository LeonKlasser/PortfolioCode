using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flipper : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = 90f;

    public IEnumerator FlipLeft()
    {
        Quaternion startRot = transform.rotation;
        float a = 180;

        while (a >= 0)
        {
            transform.rotation = Quaternion.Euler(0, a, 0);
            a -= rotationSpeed * Time.deltaTime;
            yield return null;
        }
        transform.rotation = Quaternion.Euler(0, 0, 0);

    }

    public IEnumerator FlipRight()
    {
        Quaternion startRot = transform.rotation;
        float a = 1;

        while (a <= 180)
        {
            transform.rotation = Quaternion.Euler(0, a, 0);
            a += rotationSpeed * Time.deltaTime;
            yield return null;
        }
        transform.rotation = Quaternion.Euler(0, 180, 0);

    }
}


