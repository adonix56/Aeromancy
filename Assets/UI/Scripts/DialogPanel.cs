using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using TMPro;

public class DialogPanel : MonoBehaviour
{
    public float openTime = 0.5f;
    public float characterWriteTime = 0.01f;
    public string[] texts;
    public bool blockInput = false;

    private TextMeshProUGUI textComponent;
    private int currentLine;
    private bool typing = false;

    public delegate void CloseEvent();
    public event CloseEvent OnCloseEvent;

    // Start is called before the first frame update
    void Start()
    {
        textComponent = GetComponentInChildren<TextMeshProUGUI>();
        Open();
        if (blockInput)
            CharacterManager.Instance.SetPlayable(false);
    }

    void Open()
    {
        DialogPanel[] oldPanels = transform.parent.GetComponentsInChildren<DialogPanel>();
        foreach(DialogPanel panel in oldPanels)
        {
            if(panel != this)
                panel.Close();
        }

        currentLine = 0;
        textComponent.text = "";
        Vector3 targetScale = transform.localScale;
        transform.localScale = Vector3.zero;
        transform.LeanScale(targetScale, openTime).setEaseOutBounce().setOnComplete(() => {
            TypeNextLine();
        });
    }

    void Close()
    {
        if (blockInput && !CharacterManager.Instance.InCutscene()) {
            CharacterManager.Instance.SetPlayable(true);
        }
        transform.LeanScale(Vector3.zero, openTime).setEaseInBounce().setOnComplete(() => {
            Destroy(gameObject);
            if(OnCloseEvent != null)
            {
                OnCloseEvent();
            }
        });
    }

    public void TypeNextLine()
    {
        if (!typing)
        {
            if (currentLine < texts.Length)
            {
                StartCoroutine(TypeLine());
            }
            else
            {
                Close();
            }
        }
    }

    IEnumerator TypeLine()
    {
        typing = true;
        textComponent.text = "";
        foreach (char c in texts[currentLine])
        {
            textComponent.text += c;
            yield return new WaitForSeconds(characterWriteTime);
        }
        currentLine++;
        typing = false;
    }

    //public void TypeLine()
    //{
    //    if(typing)
    //    {
    //        if (characterTimer > characterWriteTime)
    //        {
    //            textComponent.text += texts[currentLine][characterIndex];
    //            characterIndex++;

    //            if (characterIndex >= texts[currentLine].Length)
    //            {
    //                characterIndex = 0;
    //                currentLine++;
    //                typing = false;
    //            }
    //        }
    //        else
    //        {
    //            characterTimer += Time.deltaTime;
    //        }
    //    }
    //}
}
