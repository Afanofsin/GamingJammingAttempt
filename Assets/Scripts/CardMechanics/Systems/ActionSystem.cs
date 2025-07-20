using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ActionSystem : MonoBehaviour
{
    public static ActionSystem Instance;
    private List<GameAction> reactions = null;
    public bool isPerforming { get; private set; } = false;
    private static Dictionary<Type, List<Action<GameAction>>> preSubs = new();
    private static Dictionary<Type, List<Action<GameAction>>> postSubs = new();
    private static Dictionary<Type, Func<GameAction, IEnumerator>> performers = new();
    private static Dictionary<Delegate, Action<GameAction>> reactionWrappers = new();
    public void Perform(GameAction action, System.Action OnPerformFinished = null)
    {
        if (isPerforming) return;
        isPerforming = true;
        StartCoroutine(Flow(action, () =>
        {
            isPerforming = false;
            OnPerformFinished?.Invoke();
        }));
    }

    public void AddReaction(GameAction gameAction)
    {
        reactions?.Add(gameAction);
    }

    private IEnumerator Flow(GameAction action, Action OnFlowFinished = null)
    {
        reactions = action.PreReactions;
        PerformSubscribers(action, preSubs);
        yield return PerformReactions();

        reactions = action.PerformReactions;
        yield return PerformPerformer(action);
        yield return PerformReactions();

        reactions = action.PostReactions;
        PerformSubscribers(action, postSubs);
        yield return PerformReactions();

        OnFlowFinished?.Invoke();

    }

    private void PerformSubscribers(GameAction action, Dictionary<Type, List<Action<GameAction>>> subs)
    {
        Type type = action.GetType();
        if (subs.ContainsKey(type))
        {
            foreach (var sub in subs[type].ToList())
            {
                sub(action);
            }
        }
    }

    private IEnumerator PerformReactions()
    {
        foreach(var reaction in reactions)
        {
            yield return Flow(reaction);
        }
    }

    private IEnumerator PerformPerformer(GameAction action)
    {
        Type type = action.GetType();
        if(performers.ContainsKey(type))
        {
            yield return performers[type](action);
        }
    }

    public static void AttachPerformer<T>(Func<T, IEnumerator> performer) where T: GameAction
    {
        Type type = typeof(T);
        IEnumerator wrappedPerformer(GameAction action) => performer((T)action);
        if(performers.ContainsKey(type)) performers[type] =  wrappedPerformer;
        else performers.Add(type, wrappedPerformer);
    }

    public static void DetachPerformer<T>() where T: GameAction
    {
        Type type = typeof(T);
        if(performers.ContainsKey(type)) performers.Remove(type);
    }

    public static void SubscribeReaction<T>(Action<T> reaction, ReactionTiming timing) where T : GameAction
    {
        if (reactionWrappers.ContainsKey(reaction)) return;

        Dictionary<Type, List<Action<GameAction>>> subs = timing == ReactionTiming.PRE ? preSubs : postSubs;
        void wrappedReaction(GameAction action) => reaction((T)action);
        reactionWrappers[reaction] = wrappedReaction;
        if (subs.ContainsKey(typeof(T)))
        {
            subs[typeof(T)].Add(wrappedReaction);
        }
        else
        {
            subs.Add(typeof(T), new());
            subs[typeof(T)].Add(wrappedReaction);
        }

    }

    public static void UnsubscribeReaction<T>(Action<T> reaction, ReactionTiming timing) where T : GameAction
    {
        if (!reactionWrappers.TryGetValue(reaction, out Action<GameAction> wrappedReaction))
        {
            return;
        }

        Dictionary<Type, List<Action<GameAction>>> subs = timing == ReactionTiming.PRE ? preSubs : postSubs;
        if(subs.ContainsKey(typeof(T)))
        {
            subs[typeof(T)].Remove(wrappedReaction);
        }
        reactionWrappers.Remove(reaction);
    }
    private void Awake()
    {
        if(Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

}

public enum ReactionTiming
{
    PRE,
    POST
}
