using UnityEngine;
using UnityEngine.UI;
using static SpeechRecognizerPlugin;

public class SpeechRecognitionController : MonoBehaviour, ISpeechRecognizerPlugin
{
    private SpeechRecognizerPlugin plugin = null;
    public Text uiText;

    private void Start()
    {
        plugin = SpeechRecognizerPlugin.GetPlatformPluginVersion(this.gameObject.name);
    }

    // Implementing the ISpeechRecognizerPlugin interface

    public void OnResult(string recognizedResult)
    {
        // Split partial results by the delimiter and display them on the UI Text
        char[] delimiterChars = { '~' };
        string[] results = recognizedResult.Split(delimiterChars);

        uiText.text = "";
        for (int i = 0; i < results.Length; i++)
        {
            uiText.text += results[i] + ' ';
        }
    }

    public void OnError(string recognizedError)
    {
        ERROR error = (ERROR)int.Parse(recognizedError);
        switch (error)
        {
            case ERROR.UNKNOWN:
                Debug.Log("<b>ERROR: </b> Unknown");
                break;
            case ERROR.INVALID_LANGUAGE_FORMAT:
                Debug.Log("<b>ERROR: </b> Language format is not valid");
                break;
            default:
                break;
        }
    }

    // Functions for listening and stopping listening

    public void StartListening()
    {
        plugin.StartListening();
    }

    public void StopListening()
    {
        plugin.StopListening();
    }
}
