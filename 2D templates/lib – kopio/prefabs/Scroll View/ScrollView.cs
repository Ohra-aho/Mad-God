using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ScrollView : MonoBehaviour
{
    //public List<GameObject> content;

    //Instansiates content
    public void DisplayContent(List<GameObject> content)
    {
        ClearScroll();
        GameObject scroll = transform.GetChild(0).gameObject;
        for(int i = 0; i < content.Count; i++)
        {
            Instantiate(content[i], scroll.transform);
        }
        AdjustScrollSize();

    }

    public void DisplayItems(List<Item> content, GameObject prefab)
    {
        ClearScroll();
        GameObject scroll = transform.GetChild(0).gameObject;
        for (int i = 0; i < content.Count; i++)
        {
            GameObject new_item = Instantiate(prefab, scroll.transform);
            new_item.name = content[i].name;
        }
        AdjustScrollSize();
    }

    //Changes scrolls size according to amount of content
    public void AdjustScrollSize()
    {
        //Get info
        float scrollViewHeight = GetComponent<RectTransform>().sizeDelta.y;
        float cellSizeY = transform.GetChild(0).GetComponent<GridLayoutGroup>().cellSize.y;
        float cellSizeX = transform.GetChild(0).GetComponent<GridLayoutGroup>().cellSize.x;
        float cellSpacingY = transform.GetChild(0).GetComponent<GridLayoutGroup>().spacing.y;
        float cellSpacingX = transform.GetChild(0).GetComponent<GridLayoutGroup>().spacing.x;
        RectTransform contentTransform = transform.GetChild(0).GetComponent<RectTransform>();
        int contentCount = transform.GetChild(0).childCount;

        //Calculate required scrolls size
        float x = GetComponent<RectTransform>().sizeDelta.x / (cellSizeX + cellSpacingX);

        float requiredScrollHeight
            = (float)(Math.Ceiling(contentCount / x) * (cellSizeY+cellSpacingY))+cellSizeY;

        //Make size andjustments
        contentTransform.sizeDelta = new Vector2(
                contentTransform.sizeDelta.x,
                requiredScrollHeight
            );

        //Calculate how much scroll needs to be moved so it starts from the top
        contentTransform.localPosition = new Vector2(0, -(requiredScrollHeight / 2));

        //If scroll size is smaller than container, make it as tall as container
        if(contentTransform.sizeDelta.y < scrollViewHeight)
        {
            contentTransform.sizeDelta
            = new Vector2(
                contentTransform.sizeDelta.x,
                scrollViewHeight
            );
        }
    }

    public void ClearScroll()
    {
        for(int i = transform.GetChild(0).childCount-1; i >= 0; i--)
        {
            DestroyImmediate(transform.GetChild(0).GetChild(i).gameObject);
        }
    }
}
