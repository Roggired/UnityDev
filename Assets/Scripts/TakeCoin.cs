using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TakeCoin : MonoBehaviour
{
    public Text coinScore;
    public RectTransform coinOnScorePlate;

    public float animationMovingSpeed = 0.25f;
    public float animationScalingSpeed = 0.05f;

    private bool played = false;
    private bool scaled = false;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (played)
        {
            Vector3 coinOnScorePlatePosition = coinOnScorePlate.TransformPoint(coinOnScorePlate.transform.position);
            coinOnScorePlatePosition.z = transform.position.z;

            Vector3 target = coinOnScorePlatePosition - transform.position;

            transform.Translate(target * Time.deltaTime * animationMovingSpeed);

            if (!scaled)
            {
                StartCoroutine(ScaleCoroutine());
                scaled = true;
            }

            Destroy(gameObject, 3f);
        }
    }
    private void AddScore()
    {
        coinScore.text = System.Convert.ToInt32(coinScore.text) + 1 + "";
    }
    private IEnumerator ScaleCoroutine()
    {
        Scale(1.2f);
        while (transform.localScale.x > 0.5f)
        {
            Scale(animationScalingSpeed);
            yield return new WaitForSeconds(0.2f);
        }
        AddScore();
    }
    private void Scale(float factor)
    {
        Vector3 foo = new Vector3(transform.localScale.x,
                                  transform.localScale.y,
                                  transform.localScale.z);
        foo.Scale(new Vector3(factor,
                              factor,
                              factor));
        transform.localScale = foo;
    }

    public void Play()
    {
        played = true;
    }
}
