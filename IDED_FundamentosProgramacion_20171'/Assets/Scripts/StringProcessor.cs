using UnityEngine;

public class StringProcessor : MonoBehaviour
{
    private static StringProcessor instance;

    [SerializeField]
    private TextAsset source;

    private string[] processedWords;

    private string currentWord;

    public static StringProcessor Instance
    {
        get
        {
            return instance;
        }
    }

    public void SelectWord()
    {
        if (processedWords != null && processedWords.Length > 0)
        {
            currentWord = processedWords[Random.Range(0, processedWords.Length)];
        }
    }

    public bool WordContainsLetter(string inputChar)
    {
        bool result = currentWord.Contains(inputChar);
        return result;
    }

    public bool DecypheredWord(string compareWord)
    {
        return compareWord.Equals(currentWord);
    }
}