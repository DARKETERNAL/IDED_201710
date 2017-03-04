using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[System.Serializable]
public class InputEvent : UnityEvent<string>
{ }

[RequireComponent(typeof(GameManager))]
public class GameInputController : MonoBehaviour
{
    [SerializeField]
    private Text displayText;

    [SerializeField]
    private Text guessedText;

    private InputEvent onInputReceived;
    private GameManager gameManager;

    private bool inputEnabled = true;

    public void ValidateWord()
    {
        bool guessedWordCorrectly = gameManager.ValidateWord(guessedText.text);

        if (guessedWordCorrectly)
        {
            Debug.Log("Correct!");
        }
        else
        {
            Debug.Log("Incorrect");
        }
    }

    public void EnableInputProcessing(bool val)
    {
        this.enabled = val;
    }

    // Use this for initialization
    private void Start()
    {
        gameManager = GetComponent<GameManager>();

        onInputReceived = new InputEvent();
        onInputReceived.AddListener(OnInputReceived);
    }

    private void OnInputReceived(string input)
    {
        if (!string.IsNullOrEmpty(input))
        {
            Debug.Log(string.Format("Received input [{0}]. Processing", input));
            gameManager.ProcessInput(input);

            if (displayText != null)
            {
                displayText.text = gameManager.HintWord;
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