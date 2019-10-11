using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenusButtonMovingAnimation : MonoBehaviour
{
    public float animationSpeed = 1f;

    private bool played = false;
    private Vector3 targetPoint;
    
    void Update()
    {
        if (played)
        {
            Vector3 frameTarget = targetPoint - GetComponent<RectTransform>().localPosition;
            Vector3 newLocalPosition = new Vector3(GetComponent<RectTransform>().localPosition.x + frameTarget.x * Time.deltaTime * animationSpeed,
                                                   GetComponent<RectTransform>().localPosition.y + frameTarget.y * Time.deltaTime * animationSpeed,
                                                   GetComponent<RectTransform>().localPosition.z + frameTarget.z * Time.deltaTime * animationSpeed);
            GetComponent<RectTransform>().localPosition = newLocalPosition;
        }
    }
    
    public void Play(Vector3 targetPoint)
    {
        played = true;
        this.targetPoint = targetPoint;
    }
}
