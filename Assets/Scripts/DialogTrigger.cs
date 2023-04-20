using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    public bool destroyOnTrigger;
    [SerializeField] private DialogController dialogController;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(PLAYER_TAG))
        {
            DialogPanel newPanel;
            switch(panelType)
            {
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
            Destroy(gameObject);
        }
    }
}
