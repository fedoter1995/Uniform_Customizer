using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CustomizationMenu : MonoBehaviour
{
    [SerializeField]
    private List<CustomizationPage> _pages;
    [SerializeField]
    private List<GameObject> _temporaryObjects;



    private CustomizationPage currentPage;

    [SerializeField]
    private Button _button;


    private void Start()
    {
        foreach (GameObject obj in _temporaryObjects)
            obj.SetActive(true);

        foreach (CustomizationPage page in _pages)
            page.SetActive(false);

        _button.onClick.AddListener(NextPage);

        currentPage = _pages[0];

        currentPage.Initialize();

        currentPage.SetActive(true);
    }

    private void NextPage()
    {
        int i = _pages.FindIndex(page => page == currentPage);

        var data = new Dictionary<string, Dictionary<string, object>>();
        
        data.Add(currentPage.ToString(), currentPage.GetObjectData());

        GameManager.SaveObjectData(data);


        switch(i)
        {
            case 0:
                {
                    currentPage.OnExit();
                    currentPage = _pages[i + 1];
                    currentPage.OnEnter();
                    break;
                }
            case 1:
                {
                    currentPage.OnExit();
                    currentPage = _pages[i + 1];
                    currentPage.OnEnter();

                    foreach (GameObject obj in _temporaryObjects)
                        obj.SetActive(false);

                    break;
                }
            case 2:
                {
                    GameManager.CloseProgramm();
                    break;
                }
        }
          
    }
}
