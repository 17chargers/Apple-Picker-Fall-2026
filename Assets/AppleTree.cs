using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppleTree : MonoBehaviour
{
    [Header("Inscribed")]
    
    public GameObject applePrefab;
    public float speed = 1f;
    public float leftAndRightEdge = 10f;
    public float changeDirChance = 0.1f;
    public float appleDropDelay = 1f;
    public GameObject poinsonApplePrefab;
    public float poisonChance = .16f;

    void Update() {
        Vector3 pos = transform.position;
        pos.x += speed * Time.deltaTime;
        transform.position = pos;

        if (pos.x < -leftAndRightEdge ) {
            speed = Mathf.Abs( speed );
        } else if ( pos.x > leftAndRightEdge ) {
            speed = -Mathf.Abs( speed );
        }
    }

    void FixedUpdate() {
        if (Random.value < changeDirChance ) {
            speed *= -1;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        Invoke( "DropApple", 2f );
    }

    void DropApple() {
        GameObject prefabToDrop = (Random.value<poisonChance) ? poinsonApplePrefab: applePrefab;
        GameObject apple = Instantiate<GameObject>( prefabToDrop);
        apple.transform.position = transform.position;
        Invoke( "DropApple", appleDropDelay);
    }
}
