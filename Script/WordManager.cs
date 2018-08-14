using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;

public class WordManager
{
    private static WordManager wordManager = null;
    private static readonly object padlock = new object();

    private WordManager()
    {
    }

    public static WordManager GetInstance {
        get {
            lock (padlock)
            {
                if (wordManager == null)
                {
                    wordManager = new WordManager();
                }
                return wordManager;
            }
        }
    }
    
    public bool ChkBoardInWord = false;
    private static int wordCount = 0;
    private static Dictionary<string, string> wordCollect = new Dictionary<string, string>();


    #region wordCollect Management

    public void ReadTxt(string fileName)
    {
        StreamReader sr = new StreamReader(fileName, System.Text.Encoding.Default);
        try
        {
            string[] word;
            while (!sr.EndOfStream)
            {
                word = sr.ReadLine().Split('\t');
                setDictionary(word[0], word[1]);
                word = null;
            }
        }
        catch (Exception e)
        {
            throw e;
        }
        sr.Close();
    }
    #endregion

    public string getDictionary(string english)
    {
        string hangul = wordCollect[english].ToString();
        return hangul;
    }

    public void setDictionary(string english, string hangul)
    {
        wordCollect.Add(english, hangul);
    }

    public int getCount()
    {
        return wordCount;
    }

    public void setCount()
    {
        wordCount++;
    }
}