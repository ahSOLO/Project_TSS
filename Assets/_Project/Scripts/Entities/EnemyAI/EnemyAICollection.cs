using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
[CreateAssetMenu(fileName = "Enemy AI", menuName = "Enemy AI", order = 51)]
public class EnemyAICollection : ScriptableObject
{
    public enum SelectedEnemyAI { ChaseAI, AimAtPlayerAI, FireWhenReadyAI, SweepAimAI };
    public List<SelectedEnemyAI> selectedEnemyAIs;
    public List<IEnemyAI> aIModifiers;

    private void OnEnable()
    {
        aIModifiers = new List<IEnemyAI>();

        foreach (var selectedEnemyAI in selectedEnemyAIs)
        {
            switch (selectedEnemyAI)
            {
                case SelectedEnemyAI.ChaseAI:
                    aIModifiers.Add(new ChaseAI());
                    break;
                case SelectedEnemyAI.AimAtPlayerAI:
                    aIModifiers.Add(new AimAtPlayerAI());
                    break;
                case SelectedEnemyAI.FireWhenReadyAI:
                    aIModifiers.Add(new FireWhenReadyAI());
                    break;
                case SelectedEnemyAI.SweepAimAI:
                    aIModifiers.Add(new SweepAimAI());
                    break;
                default:
                    break;
            }
        }
    }
}
