using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System;
using Unity.VisualScripting;
public class TutorialBehaviour : Window_Interface
{
    public RectTransform contentParent;
    public GameObject imagePrefab;
    public Sprite[] imageSprites;

    public float slideDuration = 0.5f;
    public Vector2 offScreenRightPos;
    public Vector2 offScreenLeftPos;
    public Vector2 centerScreenPos;
    [SerializeField]
    private GameObject m_gmPlaceSpawn;

    private RectTransform[] instantiatedImages;
    private int currentIndex = 0;
    public GameObject block;
    public Image[] pages;
    public Sprite active, inActive;
    public string[] titles;
    public string[] descriptions;
    public Text title;
    public Text description;
    GameObject[] iphones = new GameObject[4];
    void Start()
    {

        offScreenRightPos = new Vector2(Screen.width, 0);
        offScreenLeftPos = new Vector2(-Screen.width, 0);
        centerScreenPos = new Vector2(0, 0);


        instantiatedImages = new RectTransform[imageSprites.Length];

        for (int i = 0; i < imageSprites.Length; i++)
        {
            GameObject newImageObject = Instantiate(imagePrefab, contentParent);
            iphones[i] = newImageObject;
            RectTransform newImageRect = newImageObject.GetComponent<RectTransform>();
            instantiatedImages[i] = newImageRect;
            newImageObject.SetActive(true);

            Image imgComponent = newImageObject.GetComponent<Image>();
            imgComponent.sprite = imageSprites[i];


            if (i == currentIndex)
            {
                newImageRect.anchoredPosition = centerScreenPos;
            }
            else
            {
                newImageRect.localPosition = m_gmPlaceSpawn.transform.localPosition;
            }
        }
    }


    public void SlideNext()
    {
        if (currentIndex < 3)
        {
            block.SetActive(true);
            int nextIndex = (currentIndex + 1) % instantiatedImages.Length;


            instantiatedImages[currentIndex].DOLocalMoveX(-m_gmPlaceSpawn.transform.localPosition.x, slideDuration).SetEase(Ease.InOutQuad);


            //  instantiatedImages[nextIndex].anchoredPosition = offScreenRightPos;
            iphones[nextIndex].SetActive(true);
            instantiatedImages[nextIndex].DOAnchorPos(centerScreenPos, slideDuration).SetEase(Ease.InOutQuad).OnComplete(() =>
             {
                 iphones[currentIndex-1].SetActive(false);
                 block.SetActive(false);
             });

            currentIndex = nextIndex;

            changePages(currentIndex);
        }
        else
        {
            PlayerPrefs.SetInt("first", 1);
            Close();
        }

    }
    void changePages(int page)
    {
        title.text = titles[page];
        description.text = descriptions[page];
        for (int i = 0; i < pages.Length; i++)
        {
            pages[i].sprite = (i == page) ? active : inActive;
        }
    }



    public void SlidePrevious()
    {
        if (currentIndex > 0)
        {
            block.SetActive(true);
            int previousIndex = (currentIndex - 1 + instantiatedImages.Length) % instantiatedImages.Length;


            instantiatedImages[currentIndex].DOLocalMoveX(m_gmPlaceSpawn.transform.localPosition.x, slideDuration).SetEase(Ease.InOutQuad);

            iphones[previousIndex].SetActive(true);
           // instantiatedImages[previousIndex].anchoredPosition = offScreenLeftPos;
            instantiatedImages[previousIndex].DOAnchorPos(centerScreenPos, slideDuration).SetEase(Ease.InOutQuad).OnComplete(() =>
            {
                iphones[previousIndex+1].SetActive(false);
                block.SetActive(false);
            });
            currentIndex = previousIndex;
            changePages(currentIndex);
        }
    }
}
