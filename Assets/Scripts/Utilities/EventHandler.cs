using System;

public static class EventHandler
{
    public static event Action SetSkillDone;
    public static void CallSetSkillDone()
    {
        SetSkillDone?.Invoke();
    }

    public static event Action SetInitSkillObjDone;
    public static void CallSSetInitSkillObjDone()
    {
        SetInitSkillObjDone?.Invoke();
    }
}
