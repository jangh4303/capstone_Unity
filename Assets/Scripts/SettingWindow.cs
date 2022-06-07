using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingMenu : MonoBehaviour
{

    public GameObject settingWindow;

    bool exitKeyDown = false;


    void Update()
    {

        ShowSetting();
    }

    void ShowSetting()
    {

        exitKeyDown = Input.GetButton("Cancel");
        if (exitKeyDown)
            settingWindow.SetActive(true);

    }

}
