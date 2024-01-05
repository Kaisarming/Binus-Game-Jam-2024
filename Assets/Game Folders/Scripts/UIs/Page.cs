using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Page : MonoBehaviour
{
    public PageName namaPage;

    protected virtual void Start()
    {
        Button[] allButtons = GetComponentsInChildren<Button>(true);
        foreach (var item in allButtons)
        {
            item.onClick.AddListener(() => AudioManager.Instance.MainkanSuara("Click"));
        }
    }

    protected void ChangeScene(string namaScene)
    {
        SceneManager.LoadScene(namaScene);
    }
}
