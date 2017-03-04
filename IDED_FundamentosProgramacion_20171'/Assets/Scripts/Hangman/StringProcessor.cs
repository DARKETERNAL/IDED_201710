using System;
using UnityEngine;

public class StringProcessor
{
    private static StringProcessor instance;

    private string[] processedWords;
    private char[] hintWord;

    private string currentWord;

    public static StringProcessor Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new StringProcessor();
            }

            return instance;
        }
    }

    public char[] HintWord
    {
        get
        {
            return hintWord;
        }

        protected set
        {
            hintWord = value;
        }
    }

    public StringProcessor()
    {
        instance = this;
    }

    public void ProcessWords(TextAsset sourceAsset)
    {
        if (processedWords == null)
        {
            if (sourceAsset == null || sourceAsset.text == null || sourceAsset.text.Length == 0)
            {
                Debug.LogError("Can't process invalid source");
            }
            else
            {
                processedWords = sourceAsset.text.Split(Environment.NewLine.ToCharArray(), ' ');
            }
        }

        SelectWord();
    }

    public void SelectWord()
    {
        if (processedWords != null && processedWords.Length > 0)
        {
            currentWord = processedWords[UnityEngine.Random.Range(0, processedWords.Length)];
        }
        else
        {
            Debug.Log("Can't select a word from invalid collection");
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

    public void UpdateHintWord(string input)
    {
        if (HintWord == null)
        {
            HintWord = new char[currentWord.Length];
            HintWord.FillWithChar('*');
        }

        for (int i = 0; i < currentWord.Length; i++)
        {
            if (currentWord[i].Equals(input[0]))
            {
                HintWord[i] = input[0];
            }
        }
    }
}