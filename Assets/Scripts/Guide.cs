using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Guide : MonoBehaviour
{
    [SerializeField] private GameObject resetButton;
    [SerializeField] private Image helpImage;
    [SerializeField] private Image blackImage;

    [SerializeField] private Image levelImage;
    public bool tweenFreeze = false;

    private float alpha = 1;

    private void Start()
    {
        if (PuzzleManager.currentPuzzle == 0)
        {
            resetButton.SetActive(false);

            SetAlpha(1);
            StartCoroutine(showUI(helpImage));
            SetAlpha(0.5f);
            StartCoroutine(showUI(blackImage));
        }
        else
            resetButton.SetActive(true);
    }

    private void Update()
    {
        if (!tweenFreeze && Input.anyKey)
        {
            if (helpImage.enabled)
            {
                SetAlpha(1);
                StartCoroutine(hideUI(helpImage));
                SetAlpha(0.5f);
                StartCoroutine(hideUI(blackImage));
            }
            if (levelImage.enabled)
            {
                SetAlpha(0.8f);
                List<MaskableGraphic> maskableGraphics = new List<MaskableGraphic>();
                maskableGraphics.AddRange(levelImage.gameObject.GetComponentsInChildren<Image>());
                maskableGraphics.AddRange(levelImage.gameObject.GetComponentsInChildren<Text>());
                maskableGraphics.AddRange(levelImage.gameObject.GetComponentsInChildren<TMP_Text>());
                StartCoroutine(hideUI(maskableGraphics));
            }
        }
    }

    public void ResetLevel()
    {
        SceneManager.LoadScene(PuzzleManager.nameOfMainScene);
    }

    public void SetAlpha(float alpha = 1)
    {
        this.alpha = alpha;
    }

    public void ShowUI(GameObject gameObject)
    {
        List<MaskableGraphic> maskableGraphics = new List<MaskableGraphic>();
        maskableGraphics.AddRange(gameObject.GetComponentsInChildren<Image>(true));
        maskableGraphics.AddRange(gameObject.GetComponentsInChildren<Text>(true));
        maskableGraphics.AddRange(gameObject.GetComponentsInChildren<TMP_Text>(true));
        StartCoroutine(showUI(maskableGraphics));
    }

    public void HideUI(GameObject gameObject)
    {
        List<MaskableGraphic> maskableGraphics = new List<MaskableGraphic>();
        maskableGraphics.AddRange(gameObject.GetComponentsInChildren<Image>(true));
        maskableGraphics.AddRange(gameObject.GetComponentsInChildren<Text>(true));
        maskableGraphics.AddRange(gameObject.GetComponentsInChildren<TMP_Text>(true));
        StartCoroutine(hideUI(maskableGraphics));
    }

    IEnumerator showUI(List<MaskableGraphic> maskableGraphics)
    {
        tweenFreeze = true;

        float imageA = 0;
        float imageA_end = alpha;
        foreach (MaskableGraphic maskableGraphic in maskableGraphics)
        {
            maskableGraphic.gameObject.SetActive(true);
            maskableGraphic.enabled = true;
        }
        while (imageA <= imageA_end)
        {
            foreach (MaskableGraphic maskableGraphic in maskableGraphics)
            {
                maskableGraphic.color = new Color(maskableGraphic.color.r, maskableGraphic.color.g, maskableGraphic.color.b, imageA);
            }
            imageA += Time.deltaTime;
            yield return null;
        }
        foreach (MaskableGraphic maskableGraphic in maskableGraphics)
        {
            maskableGraphic.color = new Color(maskableGraphic.color.r, maskableGraphic.color.g, maskableGraphic.color.b, imageA_end);
        }

        tweenFreeze = false;
    }

    IEnumerator showUI(MaskableGraphic maskableGraphic)
    {
        tweenFreeze = true;

        float imageA = 0;
        float imageA_end = alpha;
        
        maskableGraphic.gameObject.SetActive(true);
        maskableGraphic.enabled = true;

        while (imageA <= imageA_end)
        {
            maskableGraphic.color = new Color(maskableGraphic.color.r, maskableGraphic.color.g, maskableGraphic.color.b, imageA);
            imageA += Time.deltaTime;
            yield return null;
        }

        maskableGraphic.color = new Color(maskableGraphic.color.r, maskableGraphic.color.g, maskableGraphic.color.b, imageA_end);

        tweenFreeze = false;
    }

    IEnumerator hideUI(List<MaskableGraphic> maskableGraphics)
    {
        tweenFreeze = true;

        float imageA_begin = alpha;
        
        while (imageA_begin >= 0)
        {
            foreach (MaskableGraphic maskableGraphic in maskableGraphics)
            {
                maskableGraphic.color = new Color(maskableGraphic.color.r, maskableGraphic.color.g, maskableGraphic.color.b, imageA_begin);
            }
            imageA_begin -= Time.deltaTime;
            yield return null;
        }
        foreach (MaskableGraphic maskableGraphic in maskableGraphics)
        {
            maskableGraphic.color = new Color(maskableGraphic.color.r, maskableGraphic.color.g, maskableGraphic.color.b, 0);
        }
        foreach (MaskableGraphic maskableGraphic in maskableGraphics)
        {
            maskableGraphic.gameObject.SetActive(false);
            maskableGraphic.enabled = false;
        }

        tweenFreeze = false;
    }

    IEnumerator hideUI(MaskableGraphic maskableGraphic)
    {
        tweenFreeze = true;

        float imageA_begin = alpha;

        while (imageA_begin >= 0)
        {
            maskableGraphic.color = new Color(maskableGraphic.color.r, maskableGraphic.color.g, maskableGraphic.color.b, imageA_begin);

            imageA_begin -= Time.deltaTime;
            yield return null;
        }
        maskableGraphic.color = new Color(maskableGraphic.color.r, maskableGraphic.color.g, maskableGraphic.color.b, 0);

        maskableGraphic.gameObject.SetActive(false);
        maskableGraphic.enabled = false;

        tweenFreeze = false;
    }
}
