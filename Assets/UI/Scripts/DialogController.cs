using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogController : MonoBehaviour
{
    public GameObject baseDialogPanel;
    public GameObject smallUpRightPanel;

    public void OpenBaseDialogPanel(string[] texts)
    {
        OpenPanel(baseDialogPanel, texts);
    }

    public void OpenSmallRightPanel(string[] texts)
    {
        OpenPanel(smallUpRightPanel, texts);
    }

    private void OpenPanel(GameObject panel, string[] texts)
    {
        GameObject newPanel =Instantiate(panel, transform);
        newPanel.GetComponent<DialogPanel>().texts = texts;
    }
}
