using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerBGSAction : ActionBase
{
    public enum OperationType
    {
        Set, Stop, Play
    }

    public OperationType Operation;

    public AudioClip Clip;

    public bool AutoPlay;

    public float Volume;

    public ManagerBGSAction() : base("ManagerBGS")
    {
        Operation = OperationType.Set;
        Clip = null;
        AutoPlay = true;
        Volume = 1;
    }

    public override IEnumerator EventCorotine()
    {
        switch (Operation)
        {
            case OperationType.Set:
                GameManager.Instance.Audio.BGSVolume = Volume;
                GameManager.Instance.Audio.SetBGS(Clip, AutoPlay);
                break;
            case OperationType.Stop:
                GameManager.Instance.Audio.StopBGS();
                break;
            case OperationType.Play:
                GameManager.Instance.Audio.PlayBGS();
                break;
        }

        yield return null;
    }

    public override string GetInfo()
    {
        switch (Operation)
        {
            case OperationType.Set:
                return $"����������, �����: {Clip.name}, ����������?: {AutoPlay}, ���������: {Volume}";
            case OperationType.Play:
                return $"������";
            case OperationType.Stop:
                return $"���������";
            default:
                return "NULL";
        }
    }

    public override string GetHeader()
    {
        return "���������� BGS";
    }
}
