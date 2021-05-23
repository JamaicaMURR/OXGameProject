using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pop : MonoBehaviour
{
    public IndependentClocks indieClocks;

    public Text textField;

    public float evaporatingSpeed;
    public float fadingTime;

    public void BeginEvaporating()
    {
        StartCoroutine("Evaporate");
    }

    IEnumerator Evaporate()
    {
        float expiredTime = 0;

        while(expiredTime < fadingTime)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y + evaporatingSpeed * indieClocks.DeltaTime, transform.position.z);
            textField.color = new Color(textField.color.r, textField.color.g, textField.color.b, 1 - (expiredTime / fadingTime));

            expiredTime += indieClocks.DeltaTime;

            yield return new WaitForEndOfFrame();
        }

        Destroy(gameObject);
        yield break;
    }
}
