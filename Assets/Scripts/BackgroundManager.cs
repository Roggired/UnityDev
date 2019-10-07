using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundManager : MonoBehaviour
{
    public Transform mainCamera;
    public Transform background3, background2, background1;
    public Vector3 background3StartPoint = new Vector3(-19.2f, 0, 0);
    public float maxRangeBetweenMainCameraAndLastBackground = 35.2f;
    public float rangeBetweenNextBackgrounds = 19.2f;

    // Start is called before the first frame update
    void Start()
    {
        SetBackgroundsToStartPositions();
    }
    private void SetBackgroundsToStartPositions()
    {
        background3.position = background3StartPoint;

        Vector3 point = new Vector3(background3StartPoint.x + rangeBetweenNextBackgrounds,
                                    background3StartPoint.y,
                                    background3StartPoint.z);
        background2.position = point;
        point = new Vector3(point.x + rangeBetweenNextBackgrounds,
                            point.y,
                            point.z);
        background1.position = point;
    }

    // Update is called once per frame
    void Update()
    {
        float mainCameraX = mainCamera.position.x;
        float lastBackgroundX = background3.position.x;

        if (mainCameraX - lastBackgroundX > maxRangeBetweenMainCameraAndLastBackground)
        {
            StepBackgrounds();
        }

        if (mainCameraX == 0)
        {
            SetBackgroundsToStartPositions();
        }
    }
    private void StepBackgrounds()
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
