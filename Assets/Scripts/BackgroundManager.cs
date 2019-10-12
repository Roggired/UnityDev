using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundManager : MonoBehaviour
{
    public Transform mainCamera;
    public Vector3 mainCameraStartPoint = new Vector3(-10, 0, -10);
    public GameObject backgroundPrefab;
    public Transform[] backgrounds;
    public float delayBetweenDestroyingOldBacks = 1f;
    private Transform[] newBackgrounds;
    public Vector3 background3StartPoint = new Vector3(-19.2f, 0, 0);
    public float maxRangeBetweenMainCameraAndLastBackground = 35.2f;
    public float rangeBetweenNextBackgrounds = 19.2f;

    private bool steped = false;
    void Start()
    {
        SetBackgroundsToStartPositions(backgrounds);
        newBackgrounds = CreateNewBackgrounds();
    }
    private void SetBackgroundsToStartPositions(Transform[] backgrounds)
    {
        Vector3 point = new Vector3(background3StartPoint.x,
                                    background3StartPoint.y,
                                    background3StartPoint.z);
        backgrounds[0].position = point;


        point = new Vector3(background3StartPoint.x + rangeBetweenNextBackgrounds,
                            background3StartPoint.y,
                            background3StartPoint.z);
        backgrounds[1].position = point;


        point = new Vector3(point.x + rangeBetweenNextBackgrounds,
                            point.y,
                            point.z);
        backgrounds[2].position = point;
    }

    void Update()
    {
        float mainCameraX = mainCamera.position.x;
        float lastBackgroundX = backgrounds[0].position.x;

        if (mainCameraX - lastBackgroundX > maxRangeBetweenMainCameraAndLastBackground)
        {
            StepBackgrounds();
            steped = true;
        }

        if (mainCameraX == mainCameraStartPoint.x && steped)
        {
            DeleteOldBackgrounds();
            steped = false;
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
    }
    private Transform[] CreateNewBackgrounds()
    {
        Transform[] array = new Transform[3];
        array[0] = Instantiate(backgroundPrefab).transform;
        array[1] = Instantiate(backgroundPrefab).transform;
        array[2] = Instantiate(backgroundPrefab).transform;

        for (int i = 0; i < array.Length; i++)
        {
            array[i].transform.SetParent(transform);
        }
        SetBackgroundsToStartPositions(array);
        HideBackgrounds(array);

        return array;
    }
    private void HideBackgrounds(Transform[] backgrounds)
    {
        for (int i = 0; i < backgrounds.Length; i++)
        {
            Vector3 newPosition = new Vector3(backgrounds[i].transform.position.x,
                                              backgrounds[i].transform.position.y,
                                              backgrounds[i].transform.position.z + 2);
            backgrounds[i].transform.position = newPosition;
        }
    }
    private void DeleteOldBackgrounds()
    {
        Transform[] forDestroying = { backgrounds[0], backgrounds[1], backgrounds[2] };
        Destroy(forDestroying[0].gameObject, delayBetweenDestroyingOldBacks);
        Destroy(forDestroying[1].gameObject, delayBetweenDestroyingOldBacks);
        Destroy(forDestroying[2].gameObject, delayBetweenDestroyingOldBacks);
        backgrounds = newBackgrounds;

        for (int i = 0; i < backgrounds.Length; i++)
        {
            Vector3 newPosition = new Vector3(backgrounds[i].transform.position.x,
                                              backgrounds[i].transform.position.y,
                                              backgrounds[i].transform.position.z - 2);
            backgrounds[i].transform.position = newPosition;
        }
    }
}
