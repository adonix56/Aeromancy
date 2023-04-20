using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogController : MonoBehaviour
{
    public GameObject baseDialogPanel;
    public GameObject mediumUpRightPanel;
    public GameObject thinUpRightPanel;

    public DialogPanel OpenBaseDialogPanel(string[] texts, bool blockInput = false)
    {
        return OpenPanel(baseDialogPanel, texts, blockInput);
    }

    public DialogPanel OpenMediumUpRightPanel(string[] texts, bool blockInput = false)
    {
        return OpenPanel(mediumUpRightPanel, texts, blockInput);
    }

    public DialogPanel OpenThinUpRightPanel(string[] texts, bool blockInput = false)
    {
        return OpenPanel(thinUpRightPanel, texts, blockInput);
    }

    private DialogPanel OpenPanel(GameObject panel, string[] texts, bool blockInput = false)
    {
        GameObject newPanel = Instantiate(panel, transform);
        newPanel.GetComponent<DialogPanel>().texts = texts;
        newPanel.GetComponent<DialogPanel>().blockInput = blockInput;
        return newPanel.GetComponent<DialogPanel>();
    }
}
