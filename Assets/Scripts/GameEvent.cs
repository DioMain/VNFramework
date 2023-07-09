using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// ������� ����� ��� ScriptableEvent, MapEvent
/// </summary>
public class GameEvent
{
    [SerializeReference]
    public List<ActionBase> Actions = new List<ActionBase>();

    public List<ConditionBranchingAction> ConditionEvents = new List<ConditionBranchingAction>();

    public MonoBehaviour DispathObject;
    public ScriptableObject DispathScriptable;

    [SerializeField]
    private int index = 0;
    public int Index => index;

    public bool IsRun => coroutine != null;

    public event Action<GameEvent> OnStart;
    public event Action<GameEvent> OnEnd;

    [SerializeField]
    protected Coroutine coroutine = null;

    private bool indexChanged = false;

    public virtual void Run()
    {
        if (!IsRun)
        {
            coroutine = DispathObject.StartCoroutine(PipeLine());
        }
    }
    public virtual void Stop()
    {
        if (IsRun)
        {
            DispathObject.StopCoroutine(coroutine);
            coroutine = null;

            InvokeOnEnd();
        }
    }

    protected void InvokeOnStart() => OnStart?.Invoke(this);
    protected void InvokeOnEnd() => OnEnd?.Invoke(this);

    public void SetIndex(int index)
    {
        if (index < 0 || index >= Actions.Count)
        {
            Debug.LogWarning("�� ���������� ������ � EventPipeLineBase!");
            return;
        }

        this.index = index;
        indexChanged = true;
    }

    protected IEnumerator PipeLine()
    {
#if UNITY_EDITOR
        if (!MapManager.Instance.IngnoreLoading)
#endif
        {
            yield return new WaitWhile(() => !LoadingManager.GameIsReady);
        }
        

        OnStart?.Invoke(this);

        yield return null;

        int i = 0;
        foreach (var item in Actions)
        {
            item.MainEvent = this;
            item.ActionIndex = i;

            i++;
        }

        index = 0;
        while (index < Actions.Count)
        {
            yield return DispathObject.StartCoroutine(Actions[index].EventCorotine());

            if (!indexChanged)
                index++;
            else
                indexChanged = false;
        }

        coroutine = null;

        OnEnd?.Invoke(this);
    }
}
