using Assets.Code.Configs;
using Assets.Code.Controllers;
using Assets.Code.Models;
using Assets.Code.Views;
using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesConfigurator : MonoBehaviour
{

    [Header("Stalker AI")]
    [SerializeField] private AIConfig _stalkerAIConfig;
    [SerializeField] private LevelObjectView _stalkerAIView;
    [SerializeField] private Seeker _stalkerAISeeker;
    [SerializeField] private Transform _stalkerAITarget;


    private StalkerAI _stalkerAI;

    void Start()
    {
        _stalkerAI = new StalkerAI(
            _stalkerAIView, new StalkerAIModel(_stalkerAIConfig),
            _stalkerAISeeker, _stalkerAITarget);

        InvokeRepeating(nameof(RecalculateAIPath), 0.0f, 1.0f);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (null != _stalkerAI)
            _stalkerAI.FixedUpdate();
    }

    private void RecalculateAIPath()
    {
        _stalkerAI.RecalculatePath();
    }
}
