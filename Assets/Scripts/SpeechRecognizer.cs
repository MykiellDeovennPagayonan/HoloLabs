using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static SpeechRecognizerPlugin;

public class SpeechRecognizer : MonoBehaviour, ISpeechRecognizerPlugin
{
    [SerializeField] private Text uiText = null;

    private SpeechRecognizerPlugin plugin = null;

    private void Start() 
    {
        plugin = SpeechRecognizerPlugin.GetPlatformPluginVersion(this.gameObject.name);
    }

    public void StartListening()
    {
        plugin.StartListening(true, "es-ES");
    }

    public void StopListening()
    {
        plugin.StopListening();
    }

    public void OnResult(string recognizedResult)
    {
        char[] delimiterChars = { '~' };
        string[] result = recognizedResult.Split(delimiterChars);

        uiText.text = result[0];
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
}
