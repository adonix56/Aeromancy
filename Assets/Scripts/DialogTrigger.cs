using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MyEvent : UnityEvent { }
public class DialogTrigger : MonoBehaviour
{
    private const string PLAYER_TAG = "Player";

    public enum PanelType
    {
        BigDialogPanel,
        MediumUpRightPanel,
        ThinUpRightPanel
    };
    public PanelType panelType;
    public string[] texts;
    public bool panelBlocksInput;
    public bool destroyOnTrigger = true;
    [SerializeField] private DialogController dialogController;
    public UnityEvent OnClose;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(PLAYER_TAG))
        {
            StartDialogue();
        }
    }

    public void StartDialogue() {
        DialogPanel newPanel = null;
        switch (panelType) {
            case PanelType.BigDialogPanel:
                newPanel = dialogController.OpenBaseDialogPanel(texts, panelBlocksInput);
                break;
            case PanelType.MediumUpRightPanel:
                newPanel = dialogController.OpenMediumUpRightPanel(texts, panelBlocksInput);
                break;
            case PanelType.ThinUpRightPanel:
                newPanel = dialogController.OpenThinUpRightPanel(texts, panelBlocksInput);
                break;
        }
        if (OnClose != null && newPanel != null) {
            newPanel.OnCloseEvent += OnClose.Invoke;
        }
        if (destroyOnTrigger)
            Destroy(gameObject);
    }
}
