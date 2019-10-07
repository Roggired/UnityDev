using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundManager : MonoBehaviour
{
    public Transform mainCamera;
    public Transform background3, background2, background1;
    public float maxRangeBetweenMainCameraAndLastBackground = 35.2f;
    public float rangeBetweenNextBackgrounds = 19.2f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        float mainCameraX = mainCamera.position.x;
        float lastBackgroundX = background3.position.x;

        if (mainCameraX - lastBackgroundX > maxRangeBetweenMainCameraAndLastBackground)
        {
            background3.position = new Vector3(background1.position.x + rangeBetweenNextBackgrounds,
                                               background3.position.y,
                                               background3.position.z);

            Transform temp = background3;
            background3 = background2;
            background2 = background1;
            background1 = temp;
        }
    }
}
