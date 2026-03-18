using System.Collections.Generic;
using UnityEngine;

public class StatusManager
{
    private List<IStatusEffect> statusList;
    public void Initialize()
    {
        statusList = new List<IStatusEffect>();
    }

    public void AddStatus(IStatusEffect status)
    {
        statusList.Add(status);
        status.Enter();
    }

    public void RemoveStatus(IStatusEffect status)
    {
        status.Exit();
        statusList.Remove(status);
    }

    public void RemoveAllStatus()
    {
        foreach(var status in statusList)
        {
            status.Exit();
        }

        statusList.Clear();
    }

    public void Update()
    {
        for (int i = statusList.Count - 1; i >= 0; i--)
        {
            statusList[i].Update();
        }
    }
}
