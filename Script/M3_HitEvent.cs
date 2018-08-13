using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class M3_HitEvent : MonoBehaviour
{
    //WordManager wm;
    AudioClip recording;
    AudioSource audioSource;
    TextSandboxStreaming tts;
    SpeechSandboxStreaming stt;

    public Text Word_English, Word_Hangul;
    public Image Word_Image;
    private float startRecordingTime;
    private string spelling;
    private int spellingIdx = 0;
    bool ChkGetkeyDown = false;


    void Start()
    {
        tts = new TextSandboxStreaming();
        stt = new SpeechSandboxStreaming();
        audioSource = GetComponent<AudioSource>();
        WordManager.GetInstance.ReadTxt("test.txt");
    }

    public void ThrowWordInBoard(string word)
    {
        //Add Animation
        spelling = word;
        PrtBoard(word, Word_English, Word_Hangul, Word_Image);
        StartCoroutine(WaitForFstSentence());
    }
    IEnumerator WaitForFstSentence()
    {
        yield return new WaitForSeconds(1.5f);
        SpeakingWaston("Repeat after me and find the alphabat");
        StartCoroutine(WaitForSentence());
    }
    IEnumerator WaitForSentence()
    {
        yield return new WaitForSeconds(4.0f);
        SpeakingWaston(spelling);
        StartCoroutine(WaitForWord());
    }
    IEnumerator WaitForWord()
    {
        yield return new WaitForSeconds(2.0f);
        SpeakingWaston(spelling);
        StartCoroutine(WaitForSecWord());
    }
    IEnumerator WaitForSecWord()
    {
        yield return new WaitForSeconds(1.0f);
        SpeakingWaston("Please read the word");
        StartCoroutine(WaitForSpeaking());
    }
    IEnumerator WaitForSpeaking() {
        yield return new WaitForSeconds(0.5f);
        SpeechMyVoice();
    }


    public void SpeechMyVoice()
    {
        if (!ChkGetkeyDown)
        {
            touchdown();
            StartCoroutine(WaitForSpeech());
            ChkGetkeyDown = true;
        }
    }
    IEnumerator WaitForSpeech()
    {
        yield return new WaitForSeconds(3.0f);
        touchUp();
    }

    public void ThrowAlphabat(string word)
    {
        if (spelling[spellingIdx].Equals(word.ToCharArray()[0]) && spelling.Length >= spellingIdx)
        {
            Word_English.text = Word_English.text + word.ToCharArray()[0];

            spellingIdx++;
            //맞을떄 애니메이션
            Debug.Log("Hiting Animation");
        }
        else if (!spelling[spellingIdx].Equals(word))
        {
            //틀릴때 애니메이션
            Debug.Log("!spelling[spellingIdx].Equals(word)");
        }
        else
        {
            Debug.Log("WrongFunction ThrowAlphabat()");
        }


        if (spelling.Equals(Word_English.text))
        {
            SpeakingWaston("Good Job!!  Find the other words");

            PrtBoard("", Word_English, Word_Hangul, Word_Image);
            WordManager.GetInstance.setCount();
            WordManager.GetInstance.ChkBoardInWord = false;
            spellingIdx = 0;
            spelling = null;
        }
    }

    #region Function
    public void PrtBoard(string word, Text english, Text hangul, Image img)
    {
        //english.text = word;
        try
        {
            hangul.text = WordManager.GetInstance.getDictionary(word); //  wm.getDictionary(word);
            Sprite newSprite = Resources.Load<Sprite>(word);
            img.sprite = newSprite;
        }
        catch (System.Exception)
        {
            hangul.text = null;
            english.text = null;
            Sprite newSprite = Resources.Load<Sprite>("WhiteBoard");
            img.sprite = newSprite;
        }
    }


    private void SpeakingWaston(string word)
    {
        tts.TextToSpeech(AudioClip, word);
    }

    public void touchUp()
    {
        //End the recording when the mouse comes back up, then play it
        Microphone.End("");

        //Trim the audioclip by the length of the recording

        AudioClip(recording);
        stt.speechToText(new SpeechSandboxStreaming.Callback(Example), recording);
        //stt.speechToText(new Callback(Example), recording);
    }

    public void Example(string clip)
    {
        Debug.Log("TEst:=" + clip);
        //write to code
    }

    public void touchdown()
    {
        //Get the max frequency of a microphone, if it's less than 44100 record at the max frequency, else record at 44100
        int minFreq;
        int maxFreq;
        int freq = 44100;
        Microphone.GetDeviceCaps("", out minFreq, out maxFreq);
        if (maxFreq < 44100)
            freq = maxFreq;

        //Start the recording, the length of 300 gives it a cap of 5 minutes
        recording = Microphone.Start("", false, 5, 44100);
        startRecordingTime = Time.time;
    }


    private void AudioClip(AudioClip clip)
    {
        if (Application.isPlaying && clip != null)
        {
            GameObject audioObject = new GameObject("AudioObject");
            AudioSource source = audioObject.AddComponent<AudioSource>();
            source.spatialBlend = 0.0f;
            source.loop = false;
            source.clip = clip;
            source.Play();

            Destroy(audioObject, clip.length);
        }
    }
    #endregion
}