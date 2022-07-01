using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScalaPingPong : MonoBehaviour
{
    public float speed = 0.5f;
    public float duration = 1.5f;
    public bool repeatable;
    public Vector3 maxScale;
    public Vector3 minScale;

    private void OnEnable()
    {
        StartCoroutine(AnimarScala());
    }

    private void OnDisable()
    {
        StopCoroutine(AnimarScala());
    }
    IEnumerator AnimarScala()
    {        
        while(repeatable)
        {
            yield return RepeatLeap(minScale, maxScale, duration);
            yield return RepeatLeap(maxScale, minScale, duration);
        }
    }
    public IEnumerator RepeatLeap(Vector3 a, Vector3 b, float time)
    {
        float i = 0.0f;
        float rate = (1.0f / time) * speed;
        while(i < 1.0f)
        {
            i += Time.deltaTime * rate;
            transform.localScale = Vector3.Lerp(a, b, i);
            yield return null;
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    
}
