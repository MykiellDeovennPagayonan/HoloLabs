using TextSpeech;
using UnityEngine;
using UnityEngine.Android;
using UnityEngine.UI;

public class VoiceController : MonoBehaviour
{
    const string LANG_CODE = "en-US";

    public CommandsController CommandsController;

    private void Start()
    {
        Setup(LANG_CODE);

        SpeechToText.Instance.onResultCallback = OnFinalSpeechResult;
        TextToSpeech.Instance.onStartCallBack = OnSpeakStart;
        TextToSpeech.Instance.onDoneCallback = OnSpeakStop;
        CheckPermission();
    }

    void CheckPermission()
    {
        if (!Permission.HasUserAuthorizedPermission(Permission.Microphone))
        {
            Permission.RequestUserPermission(Permission.Microphone);
        }
    }

    public void StartSpeaking(string message)
    {
        TextToSpeech.Instance.StartSpeak(message);
    }

    public void StopSpeaking()
    {
        TextToSpeech.Instance.StopSpeak();
    }

    void OnSpeakStart()
    {
        Debug.Log("Started!....");
    }

    void OnSpeakStop()
    {
        Debug.Log("Stopped!....");
    }

    public void StartListening()
    {
        SpeechToText.Instance.StartRecording();
    }

    public void StopListening()
    {
        Debug.Log("Stopped!....");
        SpeechToText.Instance.StopRecording();
    }

    void OnFinalSpeechResult(string result)
    {
        CommandsController.RunCommand(result);
    }


    void OnPartialSpeechResult(string result)
    {
        
    }
    void Setup(string code)
    {
        TextToSpeech.Instance.Setting(code, 1, 1);
        SpeechToText.Instance.Setting(code);
    }
}
