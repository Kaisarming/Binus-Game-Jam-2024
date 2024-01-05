using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasManager : MonoBehaviour
{
    [SerializeField] protected Page[] allPages;

    private void Start()
    {
        allPages = GetComponentsInChildren<Page>(true);
    }

    private void SetPage(PageName nama)
    {
        foreach (var item in allPages)
        {
            item.gameObject.SetActive(false);
        }

        Page findPage = Array.Find(allPages, p => p.namaPage == nama);
        if(findPage != null)
        {
            findPage.gameObject.SetActive(true);
        }
    }
}
