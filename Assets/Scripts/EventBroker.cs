using System;

public class EventBroker 
{
    public static event Action PlayerPaddleHit;
    public static event Action KickStartAi;
    public static event Action AIPaddleHit;

    public static void CallPlayerPaddleHit()
    {
        if(PlayerPaddleHit != null)
        {
            PlayerPaddleHit();
        }
    }

    public static void CallKickStartAi()
    {
        if (KickStartAi != null)
            KickStartAi();
    }
    public static void callAIPaddleHit()
    {
        if(AIPaddleHit != null)
        {
            AIPaddleHit();
        }
    }

}
