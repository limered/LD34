using UnityEngine;
using System.Collections;

public class LineScript : MonoBehaviour
{
    public int currentDrop = 0;

    public void Tick()
    {
        NextDrop();
    }

    public void Collect() {
        currentDrop = 0;
        TurnOffDrops();
    }

    private void StartDrop()
    {
        var prop = Random.value;
        if (prop < 0.5f)
        {
            currentDrop++;
            RenderCurrentDrop();
        }
    }

    private void TurnOffDrops()
    {
        var drops = GetComponentsInChildren<DropScript>();
        for(int i = 0; i < drops.Length; i++)
        {
            drops[i].NoActive();
        }
    }

    private void NextDrop()
    {
        if (currentDrop == 0)
            StartDrop();
        else
        {
            currentDrop++;
            RenderCurrentDrop();
        }
    }

    private void RenderCurrentDrop()
    {
        TurnOffDrops();
        if (currentDrop == 6)
        {
            currentDrop = 0;
            return;
        }
        var drops = GetComponentsInChildren<DropScript>();
        drops[currentDrop - 1].Active();
    }
}