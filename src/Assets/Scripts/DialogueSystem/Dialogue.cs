using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public struct TextBlock
{
    [SerializeField, TextArea] public string text;
    [SerializeField] public UnityEvent events;
}

public class Dialogue : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textMesh;
    [SerializeField] private List<TextBlock> textBlocks;
    [SerializeField] private float characterDelay;

    private bool _writing = true;
    private int _nextIndex = 0;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(NextTextblock());
    }

    private IEnumerator NextTextblock()
    {
        if (_nextIndex < textBlocks.Count)
        {
            _writing = true;
            textMesh.text = "";
            char[] charArray = textBlocks[_nextIndex].text.ToCharArray();
            for (int i = 0; i < charArray.Length; i++)
            {
                textMesh.text += charArray[i];
                yield return new WaitForSeconds(characterDelay);
            }

            textBlocks[_nextIndex].events.Invoke();

            _nextIndex++;
            _writing = false;
        }
    }

    public void ProceedDialogue()
    {
        if (!_writing)
        {
            StartCoroutine(NextTextblock());
        }
    }
}
