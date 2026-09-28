using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonApple : MonoBehaviour
{
    public float bottomY = -20f;

    // Update is called once per frame
    void Update()
    {
        if(transform.position.y < bottomY){
            Destroy(gameObject);
        }
    }
}
