using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class EndAction : ActionBase
{
    public EndAction() : base("End")
    {

    }

    public override IEnumerator EventCorotine()
    {
        yield return this;

        GameManager.Instance.Loading.LoadMainMenuScene();
    }

    public override string GetInfo()
    {
        return "";
    }

    public override string GetHeader()
    {
        return "Завершить игру";
    }
}