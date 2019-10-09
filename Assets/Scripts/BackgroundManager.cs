using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundManager : MonoBehaviour
{
    public Transform mainCamera;
    public Vector3 mainCameraStartPoint = new Vector3(-10, 0, -10);
    public GameObject backgroundPrefab;
    public Transform[] backgrounds;
    public float delayBetweenDestroyingOldBacks = 2f;
    private Transform[] newBackgrounds;
    public Vector3 background3StartPoint = new Vector3(-19.2f, 0, 0);
    public float maxRangeBetweenMainCameraAndLastBackground = 35.2f;
    public float rangeBetweenNextBackgrounds = 19.2f;

    private bool wasSteped = false;

    // Start is called before the first frame update
    void Start()
    {
        SetBackgroundsToStartPositions(backgrounds);
    }
    private void SetBackgroundsToStartPositions(Transform[] backgrounds)
    {
        backgrounds[0].position = background3StartPoint;

        Vector3 point = new Vector3(background3StartPoint.x + rangeBetweenNextBackgrounds,
                                    background3StartPoint.y,
                                    background3StartPoint.z);
        backgrounds[1].position = point;
        point = new Vector3(point.x + rangeBetweenNextBackgrounds,
                            point.y,
                            point.z);
        backgrounds[2].position = point;
        wasSteped = false;
    }

    // Update is called once per frame
    void Update()
    {
        float mainCameraX = mainCamera.position.x;
        float lastBackgroundX = backgrounds[0].position.x;

        if (mainCameraX - lastBackgroundX > maxRangeBetweenMainCameraAndLastBackground)
        {
            if (!wasSteped)
            {

                newBackgrounds = CreateNewBackgrounds();
                SetBackgroundsToStartPositions(newBackgrounds);
            }
            StepBackgrounds();
        }

        if (mainCameraX == mainCameraStartPoint.x && wasSteped)
        {
            SetBackgroundsToStartPositions(newBackgrounds);
            DeleteOldBackgrounds(newBackgrounds);
        }
    }
    private void StepBackgrounds()
    {
        backgrounds[0].position = new Vector3(backgrounds[2].position.x + rangeBetweenNextBackgrounds,
                                              backgrounds[0].position.y,
                                              backgrounds[0].position.z);

        Transform temp = backgrounds[0];
        backgrounds[0] = backgrounds[1];
        backgrounds[1] = backgrounds[2];
        backgrounds[2] = temp;

        wasSteped = true;
    }
    private Transform[] CreateNewBackgrounds()
    {
        Transform[] array = new Transform[3];
        array[0] = Instantiate(backgroundPrefab).transform;
        array[1] = Instantiate(backgroundPrefab).transform;
        array[2] = Instantiate(backgroundPrefab).transform;

        return array;
    }
    private void DeleteOldBackgrounds(Transform[] newBackgrounds)
    {
        Transform[] forDestroying = { backgrounds[0], backgrounds[1], backgrounds[2] };
        Destroy(forDestroying[0].gameObject, delayBetweenDestroyingOldBacks);
        Destroy(forDestroying[1].gameObject, delayBetweenDestroyingOldBacks);
        Destroy(forDestroying[2].gameObject, delayBetweenDestroyingOldBacks);
        backgrounds[0] = newBackgrounds[0];
        backgrounds[1] = newBackgrounds[1];
        backgrounds[2] = newBackgrounds[2];
    }
}
