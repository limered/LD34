using UnityEngine;
using System.Collections;

public class LineScript : MonoBehaviour
{
    public int currentDrop = 0;

    public bool Tick()
    {
        return NextDrop();
    }

    public void Collect() {
        currentDrop = 0;
        TurnOffDrops();
    }

    private bool StartDrop()
    {
        var prop = Random.value;
        if (prop < 0.5f)
        {
            currentDrop++;
            return RenderCurrentDrop();
        }
        return false;
    }

    private void TurnOffDrops()
    {
        var drops = GetComponentsInChildren<DropScript>();
        for(int i = 0; i < drops.Length; i++)
        {
            drops[i].NoActive();
        }
    }

    private bool NextDrop()
    {
        if (currentDrop == 0)
            return StartDrop();
        else
        {
            currentDrop++;
            return RenderCurrentDrop();
        }
    }

    private bool RenderCurrentDrop()
    {
        TurnOffDrops();
        if (currentDrop == 6)
        {
            currentDrop = 0;
            return true;
        }
        var drops = GetComponentsInChildren<DropScript>();
        drops[currentDrop - 1].Active();
        return false;
    }
}