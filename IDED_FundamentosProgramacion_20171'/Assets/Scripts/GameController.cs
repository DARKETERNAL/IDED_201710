using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class InputEvent : UnityEvent<string>
{ }

public class GameController : MonoBehaviour
{
    private InputEvent onInputReceived;

    // Use this for initialization
    private void Start()
    {
        onInputReceived = new InputEvent();
        onInputReceived.AddListener(OnInputReceived);
    }

    private void OnInputReceived(string input)
    {
        if (!string.IsNullOrEmpty(input))
        {
            Debug.Log(string.Format("Received input [{0}]. Processing", input));

            if (StringProcessor.Instance != null)
            {
                bool wordHasLetter = StringProcessor.Instance.WordContainsLetter(input);

                if (wordHasLetter)
                {
                }
                else
                {

                }
            }
        }
    }

    private void OnDestroy()
    {
        onInputReceived.RemoveAllListeners();
    }

    // Update is called once per frame
    private void Update()
    {
        string inputString = (string)Input.inputString;

        if (onInputReceived != null)
        {
            onInputReceived.Invoke(inputString);
        }
    }
}