using UnityEngine;
using System.Collections;
using IBM.Watson.DeveloperCloud.Logging;
using IBM.Watson.DeveloperCloud.Services.TextToSpeech.v1;
using IBM.Watson.DeveloperCloud.Utilities;
using IBM.Watson.DeveloperCloud.DataTypes;
using System.Collections.Generic;
using UnityEngine.UI;
using IBM.Watson.DeveloperCloud.Services.Conversation.v1;
using FullSerializer;
using IBM.Watson.DeveloperCloud.Connection;

public class TextSandboxStreaming {

    public delegate void SuccessCallback<AudioClip>(AudioClip clip);
    SuccessCallback<AudioClip> clipFunction;
    
    private TextToSpeech _textToSpeech;

    private string _textToSpeechUrl = "https://stream.watsonplatform.net/text-to-speech/api";
    private string _username = "9f5a83dd-9397-48a1-9b83-c8ce6ebb28bf";
    private string _password = "vIqJsyqErUSi";

    AudioClip voice;
    public TextSandboxStreaming() {

        LogSystem.InstallDefaultReactors();
        Credentials tts_credentials = new Credentials(_username, _password, _textToSpeechUrl);
        _textToSpeech = new TextToSpeech(tts_credentials);
    }


    public void TextToSpeech(SuccessCallback<AudioClip> successCallback,string text) {
        clipFunction = successCallback;
        _textToSpeech.ToSpeech(HandleToSpeechCallback, OnFail, text);
    }
    

    private void OnFail(RESTConnector.Error error, Dictionary<string, object> customData) {
        Log.Error("ExampleSpeechToText.OnFail()", "Error received: {0}", error.ToString());
    }

    void HandleToSpeechCallback(AudioClip clip, Dictionary<string, object> customData = null) {
        clipFunction(clip);
    }
    
}
