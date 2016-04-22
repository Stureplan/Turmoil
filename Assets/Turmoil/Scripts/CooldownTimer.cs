using UnityEngine;

public class CooldownTimer
{
    private float cooldownSeconds;
    private float lastAction;

    public CooldownTimer(float seconds)
    {
        cooldownSeconds = seconds;
    }

    public void UpdateTimer()
    {
        lastAction = Time.time;
    }

    public bool ActionReady()
    {
        bool actionReady = true;

        float sinceAction = Time.time - lastAction;
        actionReady = sinceAction > cooldownSeconds;

        return actionReady;
    }
}
