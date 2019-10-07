using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCamera : MonoBehaviour
{
    public Transform player;
    public Vector3 startPoint = new Vector3(0, 0, -10);
           
    // Start is called before the first frame update
    void Start()
    {
        transform.position = startPoint;
    }

    // Update is called once per frame
    void Update()
    {
        CheckPlayerRightMovenment();

        CheckThatPlayerWasRespawn();
    }
    private void CheckPlayerRightMovenment()
    {
        if (player.position.x >= startPoint.x)
        {
            transform.position = new Vector3(player.position.x,
                                             transform.position.y,
                                             transform.position.z);
        }
    }
    private void CheckThatPlayerWasRespawn()
    {
        if (player.position.x < startPoint.x)
        {
            transform.position = startPoint;
        }
    }
}
