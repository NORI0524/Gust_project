using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using SoundMan = Singleton<SoundManager>;

//お客さんの満足度クラス
public class SatisfactionLevel
{
    public const int MinSatisfyValue = 0;
    public const int MaxSatisfyValue = 100;
    public const int AddSatisfyValue = 5;
    public const int SubSatisfyValue = 5;

    public int satisfyValue;

    BitFlag state = new BitFlag();

    Timer delay = new Timer(1);

    class State
    {
        public const uint Full = 1 << 0;
    }

    public SatisfactionLevel()
    {
        satisfyValue = MinSatisfyValue;
        state.FoldALLBit();
        delay.Start();
        delay.EnabledLoop();
    }

    public void AddSatisfy()
    {
        delay.Update();
        if (!delay.IsFinish()) return;

        //天かすの数によって満足度が溜まりにくくなる
        var koromoFac = GameObject.Find("KoromoManager").GetComponent<SpawnFactory>();

        if (satisfyValue < MaxSatisfyValue) GameDirector.totalSatisfyValue += AddSatisfyValue - koromoFac.GetCurrentNum();

        satisfyValue = Mathf.Clamp(satisfyValue + AddSatisfyValue, MinSatisfyValue, MaxSatisfyValue);

        //満足度が最高のとき
        if(satisfyValue == MaxSatisfyValue)
        {
            if(!IsFull()) SoundMan.Instance.PlaySE("happy");
            state.AddBit(State.Full);
        }
    }

    public void SubSatisfy()
    {
        delay.Update();
        if (!delay.IsFinish()) return;

        if (satisfyValue > MinSatisfyValue) GameDirector.totalSatisfyValue -= SubSatisfyValue;

        satisfyValue = Mathf.Clamp(satisfyValue - SubSatisfyValue, MinSatisfyValue, MaxSatisfyValue);

        state.FoldBit(State.Full);
    }

    public bool IsFull() { return state.CheckBit(State.Full); }
    public int GetSatisfyValue() { return satisfyValue; }
}
