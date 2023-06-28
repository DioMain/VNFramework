using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public abstract class TextWriterBase : MonoBehaviour, IGameInited
{
    [Header("Common options:")]

    [Tooltip("Text source")]
    [SerializeField]
    protected TextMeshProUGUI UITextMeshPro;

    [TextArea]
    [Multiline(5)]
    public string Text;

    [Tooltip("1 / Speed = letter delay (letters per secound)")]
    [Range(1f, 100f)]
    public float Speed = 1f;
    [Range(1f, 100f)]
    public float defaultSpeed = 22.4f;

    public bool Clear = true;
    public bool WaitPress = true;

    [Header("Common information:")]
    [SerializeField]
    private bool isSkiped = false;
    [SerializeField]
    private bool isWaiting = false;
    public bool IsWaiting => isWaiting;

    private Coroutine writeCoroutine = null;
    public bool IsWrite => writeCoroutine != null;
    
    public abstract void OnSpecialSituation(string text, TextWriterSS situation);
    public abstract void OnEveryLetter(char letter);
    public abstract void OnWaitStart();
    public abstract void OnWaitEnd();
    public abstract void OnWriteStart();
    public abstract void OnWriteEnd();

    public void Write(string text, bool clear = true, bool wait = true, float speed = 0)
    {
        if (!IsWrite)
        {
            Clear = clear;
            WaitPress = wait;
            Speed = speed <= 0 ? defaultSpeed : speed;
            Text = text;
            writeCoroutine = StartCoroutine(WriteCoroutine());
            StartCoroutine(SkipCorotine());
        }
    }
    public void WriteStop()
    {
        if (IsWrite)
        {
            StopCoroutine(writeCoroutine);
            writeCoroutine = null;
            OnWriteEnd();
        }
    }

    private string GetVariable(char type, string key)
    {
        switch (type)
        {
            case 'b':
                if (!GameManager.Instance.Data.BoolValues.ContainsKey(key))
                    return "NULL";
                else
                    return GameManager.Instance.Data.BoolValues[key].ToString();
            case 'i':
                if (!GameManager.Instance.Data.IntValues.ContainsKey(key))
                    return "NULL";
                else
                    return GameManager.Instance.Data.IntValues[key].ToString();
            case 's':
                if (!GameManager.Instance.Data.StringValues.ContainsKey(key))
                    return "NULL";
                else
                    return GameManager.Instance.Data.StringValues[key];
            case 'f':
                if (!GameManager.Instance.Data.FloatValues.ContainsKey(key))
                    return "NULL";
                else
                    return GameManager.Instance.Data.FloatValues[key].ToString();
            default:
                return "NULL";
        }
    }

    private IEnumerator WriteCoroutine()
    {
        bool spaceFlag = false, tagFlag = false, keyFlag = false, ingoreFlag = false;

        float delay = 1f / Speed;

        string buffer = string.Empty;

        if (Clear)
            UITextMeshPro.text = string.Empty;

        OnWriteStart();

        for (int i = 0; i < Text.Length; i++)
        {
            char cur = Text[i];

            if (!spaceFlag && !tagFlag)
            {
                if (cur == '/')
                    keyFlag = true;

                if (cur == '\\')
                    ingoreFlag = true;

                if (cur == ' ')
                    spaceFlag = true;

                if (cur == '<')
                    tagFlag = true;
            }


            if (!spaceFlag && !tagFlag && !keyFlag && !ingoreFlag)
            {
                UITextMeshPro.text += cur;

                OnEveryLetter(cur);

                if (!isSkiped)
                    yield return new WaitForSeconds(delay);
            }
            else if (spaceFlag)
            {
                buffer += cur;

                if (i < Text.Length - 1 && Text[i + 1] != ' ')
                {
                    OnSpecialSituation(buffer, TextWriterSS.Space);

                    UITextMeshPro.text += buffer;
                    buffer = string.Empty;

                    spaceFlag = false;
                }
            }
            else if (tagFlag)
            {
                buffer += cur;

                if (cur == '>')
                {
                    OnSpecialSituation(buffer, TextWriterSS.Tag);

                    UITextMeshPro.text += buffer;
                    buffer = string.Empty;

                    tagFlag = false;
                }
            }
            else if (ingoreFlag)
            {
                buffer += $"\\{Text[i + 1]}";

                OnSpecialSituation(buffer, TextWriterSS.Ingore);

                UITextMeshPro.text += Text[i + 1];
                buffer = string.Empty;

                ingoreFlag = false;

                i++;
            }
            else if (keyFlag)
            {
                buffer += $"/{Text[i + 1]}";

                OnSpecialSituation(buffer, TextWriterSS.Key);

                switch (Text[i + 1])
                {
                    // SET PAUSE
                    case 'p':
                        isWaiting = true;
                        OnWaitStart();
                        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Z));
                        OnWaitEnd();
                        isWaiting = false;
                        isSkiped = false;
                        break;
                    // DELAY 0.1f sec
                    case '.':
                        yield return new WaitForSeconds(0.1f);
                        break;
                    // DELAY 0.25f sec
                    case '|':
                        yield return new WaitForSeconds(0.25f);
                        break;
                    // DELAY 0.5f sec
                    case 'd':
                        yield return new WaitForSeconds(0.5f);
                        break;
                    // DELAY 1f sec
                    case 'D':
                        yield return new WaitForSeconds(1f);
                        break;
                    // FOR OUTPUT VARIABLE (/vs[strname])
                    case 'v':
                        if (Text[i + 3] != '[')
                            break;

                        char type = Text[i + 2];
                        string key = "";

                        int offset = 4;

                        while (i + offset < Text.Length - 1 && Text[i + offset] != ']' && Text[i + offset] != '\0')
                        {
                            key += Text[i + offset];
                            offset++;
                        }

                        string value = GetVariable(type, key);

                        Text = Text.Remove(i, offset + 1);
                        Text = Text.Insert(i, value);

                        i -= 2;
                        
                        break;
                    default:
                        UITextMeshPro.text += Text[i + 1];
                        break;
                }

                buffer = string.Empty;
                keyFlag = false;

                i++;
            }

            yield return null;
        }

        if (WaitPress)
        {
            isWaiting = true;
            OnWaitStart();
            yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Z) || Input.GetMouseButtonDown(0));
            OnWaitEnd();
            isWaiting = false;
        }

        writeCoroutine = null;
        isSkiped = false;

        OnWriteEnd();
    }

    private IEnumerator SkipCorotine()
    {
        while (IsWrite)
        {
            yield return null;

            if (Input.GetKeyDown(KeyCode.C))
                isSkiped = true;
        }
    }

    public abstract void Init();
}

public enum TextWriterSS
{
    Unknown = -1, Space, Tag, Ingore, Key
}
