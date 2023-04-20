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

    private TextMeshProUGUI textComponent;
    private int currentLine;
    private bool typing = false;

    // Start is called before the first frame update
    void Start()
    {
        textComponent = GetComponentInChildren<TextMeshProUGUI>();
        Open();
    }

    void Open()
    {
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
        transform.LeanScale(Vector3.zero, openTime).setEaseInBounce().setOnComplete(() => {
            Destroy(gameObject);
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
