using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Title : MonoBehaviour
{
    [HideInInspector] public bool isFinish = false;
    [SerializeField] private float moveSpeed;

    RectTransform rect;
    TMP_Text titleText;

    private void Awake()
    {
        rect = GetComponent<RectTransform>();
        titleText = GetComponent<TMP_Text>();
    }

    public void TextUp(float begin = 0, float end = 120)
    {
        StartCoroutine(Up(begin, end));
    }

    IEnumerator Up(float begin, float end)
    {
        float titleTextColorA = 0f;
        titleText.text = "Roll of The Dice";
        rect.localPosition = new Vector3(rect.localPosition.x, begin, rect.localPosition.z);
        while (rect.localPosition.y < end)
        {
            titleText.color = new Color(1, 1, 1, titleTextColorA);
            titleTextColorA += Time.deltaTime * 0.3f;
            if (titleTextColorA > 1)
                titleTextColorA = 1;
            rect.localPosition += Vector3.up * Time.deltaTime * moveSpeed;
            yield return null;
        }
        rect.localPosition = new Vector3(rect.localPosition.x, end, rect.localPosition.z);
        isFinish = true;
    }
}
