using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public enum PanelType
{
    Cretae,
    Join,
    Error
}
public class StatusPanel : MonoBehaviour
{
    PanelType type;
    public CanvasGroup CanvasGroup;
    public TextMeshProUGUI Title,Status;
    public Button CloseBTN,GoBTN;
    public TMP_InputField InputRoomName;

    public void Show(PanelType panelType, string message,bool showClose)
    {
        type = panelType;
        InputRoomName.text = "";
        switch (panelType)
        {
            case PanelType.Cretae:
                GoBTN.gameObject.SetActive(true);
                InputRoomName.gameObject.SetActive(true);
                Title.gameObject.SetActive(true);
                Status.gameObject.SetActive(true);
                Status.text = "";
                type = panelType;
                Title.text = message;
                CloseBTN.gameObject.SetActive(showClose);
                break;
            case PanelType.Join:
                GoBTN.gameObject.SetActive(true);
                InputRoomName.gameObject.SetActive(true);
                Title.gameObject.SetActive(true);
                type = panelType;
                Title.text = message;
                CloseBTN.gameObject.SetActive(showClose);
                Status.text = "";
                break;
            case PanelType.Error:
                GoBTN.gameObject.SetActive(false);
                InputRoomName.gameObject.SetActive(false);
                Status.gameObject.SetActive(false);
                Title.text = message;
                break;
        }
        CanvasGroup.alpha = 1;
        CanvasGroup.blocksRaycasts = true;
    }

    public void GoCheck()
    {
        GoBTN.interactable = !string.IsNullOrEmpty(InputRoomName.text);
    }

    public void SetStatus(string message)
    {
        Status.text = message;
    }

    public void ClosePanel()
    {
        CanvasGroup.blocksRaycasts = false;
        CanvasGroup.alpha = 0;
    }

    public void Go()
    {
        if (InputRoomName.text == "")
        {
            SetStatus("Need room name");
        }
        else
        {
            if (type == PanelType.Join)
            {
                Home.Instanst.Join(InputRoomName.text);
            }
            if (type == PanelType.Cretae)
            {
                Home.Instanst.Create(InputRoomName.text);
            }
        }
    }
}
