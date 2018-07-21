/**
* Copyright 2015 IBM Corp. All Rights Reserved.
*
* Licensed under the Apache License, Version 2.0 (the "License");
* you may not use this file except in compliance with the License.
* You may obtain a copy of the License at
*
*      http://www.apache.org/licenses/LICENSE-2.0
*
* Unless required by applicable law or agreed to in writing, software
* distributed under the License is distributed on an "AS IS" BASIS,
* WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
* See the License for the specific language governing permissions and
* limitations under the License.
*
*/

using UnityEngine;
using System.Collections;
using IBM.Watson.DeveloperCloud.Logging;
using IBM.Watson.DeveloperCloud.Services.SpeechToText.v1;
using IBM.Watson.DeveloperCloud.Utilities;
using IBM.Watson.DeveloperCloud.DataTypes;
using System.Collections.Generic;
using UnityEngine.UI;
using IBM.Watson.DeveloperCloud.Services.Conversation.v1;
using FullSerializer;
using IBM.Watson.DeveloperCloud.Connection;

public class SpeechSandboxStreaming
{ 
    
    private SpeechToText _speechToText;

    private string stt_username = "ccba4445-fcde-45cb-b1ff-74595ccfc527";
    private string stt_password = "fizC05WCHwgA";
    // Change stt_url if different from below
    private string stt_url = "https://stream.watsonplatform.net/speech-to-text/api";
     
    public delegate void Callback(string clip);
    public Callback callback;
    void Start()
    {
        LogSystem.InstallDefaultReactors();

        //  Create credential and instantiate service
        Credentials stt_credentials = new Credentials(stt_username, stt_password, stt_url);

        _speechToText = new SpeechToText(stt_credentials);
        
    }
    public void speechToText(Callback recureFunction, AudioClip voice) {
        this.callback = recureFunction;
        _speechToText.Recognize(Success, OnFail,voice);
    }
    public void Success(SpeechRecognitionEvent events, Dictionary<string, object> customData = null) {
        if (events != null && events.results.Length > 0) {
            foreach (var res in events.results) {
                foreach (var alt in res.alternatives) {
                    if (res.final && alt.confidence > 0) {
                        string text = alt.transcript;
                        callback(text);
                    }
                }
            }
        }
    }
    private void OnFail(RESTConnector.Error error, Dictionary<string, object> customData) {
        Log.Error("ExampleSpeechToText.OnFail()", "Error received: {0}", error.ToString());
    }
}
