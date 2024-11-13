using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FloatingTextManager : MonoBehaviour
{
    public static FloatingTextManager instance;
    public GameObject textPrefebs;

    private void Awake()
    {
        instance = this;
    }

    public void Show(string text, Vector3 worldPos)
    {
        Vector2 screenPos = Camera.main.WorldToScreenPoint(worldPos);

        GameObject textObj = Instantiate(textPrefebs, transform);
        textObj.transform.position = screenPos;

        TextMeshProUGUI temp = textObj.GetComponent<TextMeshProUGUI>();

        if (temp != null)
        {
            temp.text = text;

            StartCoroutine(AnimateText(textObj));
        }

       }

    private IEnumerator AnimateText(GameObject textobj)
    {
        float duration = 1f;
        float timer = 0f;

        Vector3 startpos = textobj.transform.position;
        TextMeshProUGUI temp = textobj.GetComponent<TextMeshProUGUI>();

        while (timer < duration)
        {
            timer += Time.deltaTime;
            float progress = timer / duration;

            textobj.transform.position = startpos + Vector3.up * (progress * 50f);

            if (temp != null )
            {
                temp.alpha = 1-progress;
            }
            yield return null;
        }
        Destroy(textobj );
    }
}