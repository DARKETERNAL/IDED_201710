using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private TextAsset source;

    private List<string> correctInputs;
    private char[] finalWord;

    public string HintWord
    {
        get
        {
            string result = string.Empty;

            if (StringProcessor.Instance != null)
            {
                result = new string(StringProcessor.Instance.HintWord);
            }

            return result;
        }
    }

    public bool ValidateWord(string guessedText)
    {
        bool result = false;

        if (StringProcessor.Instance != null)
        {
            result = StringProcessor.Instance.DecypheredWord(guessedText);
        }

        return result;
    }

    public void ProcessInput(string input)
    {
        if (correctInputs == null)
        {
            correctInputs = new List<string>();
        }

        if (correctInputs.Contains(input))
        {
            Debug.Log("Input already used");
        }
        else
        {
            if (StringProcessor.Instance != null)
            {
                bool wordHasLetter = StringProcessor.Instance.WordContainsLetter(input);

                if (wordHasLetter)
                {
                    correctInputs.Add(input);
                    StringProcessor.Instance.UpdateHintWord(input);
                }
                else
                {
                    Debug.Log("Incorrect input");
                }
            }
        }
    }

    // Use this for initialization
    private void Start()
    {
        if (StringProcessor.Instance != null)
        {
            StringProcessor.Instance.ProcessWords(source);
        }
    }
}