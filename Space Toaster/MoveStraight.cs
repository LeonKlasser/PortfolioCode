using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveStraight : MonoBehaviour
{
    [SerializeField] float speed;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(this.gameObject, 20f);
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.rotation.y == 0){
            transform.Translate(new Vector3(-speed * Time.deltaTime, 0, 0));
        } else {
            transform.Translate(new Vector3(0, 0, speed * Time.deltaTime));
        }
    }
}
