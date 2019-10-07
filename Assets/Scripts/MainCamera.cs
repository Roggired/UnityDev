using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCamera : MonoBehaviour
{
    public Transform player;
    public float startX, startY, startZ;
           
    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(startX, startY, startZ);
    }

    // Update is called once per frame
    void Update()
    {
        if (player.position.x >= startX)
        {
            transform.position = new Vector3(player.position.x,
                                             transform.position.y,
                                             transform.position.z);
        }
    }
}
