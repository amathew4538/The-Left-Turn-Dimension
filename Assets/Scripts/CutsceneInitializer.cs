using UnityEngine;
using UnityEngine.Playables;

public class CutsceneInitializer : MonoBehaviour
{
    public PlayableDirector director;

    void Awake()
    {
        if (director != null)
        {
            director.time = 0;
            director.Evaluate();
        }
    }
}