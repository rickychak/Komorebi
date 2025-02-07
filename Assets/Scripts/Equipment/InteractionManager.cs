using UnityEngine;
using System.Collections.Generic;

public class InteractionManager : MonoBehaviour 
{
    private static InteractionManager instance;
    public static InteractionManager Instance => instance;

    [SerializeField] private float checkInterval = 0.2f;
    [SerializeField] private float interactionDistance = 3f;
    
    private Transform playerTransform;
    private float checkTimer;
    private readonly List<IInteractable> items = new List<IInteractable>();
    private readonly InteractionManagerLogic logic;

    public InteractionManager()
    {
        logic = new InteractionManagerLogic();
    }

    private void Awake()
    {
        if (instance == null) instance = this;
    }

    private void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    public void RegisterItem(IInteractable item)
    {
        logic.RegisterItem(items, item);
    }

    public void UnregisterItem(IInteractable item)
    {
        logic.UnregisterItem(items, item);
    }

    private void Update()
    {
        checkTimer += Time.deltaTime;
        if (checkTimer >= checkInterval)
        {
            checkTimer = 0f;
            logic.CheckInteractions(items, playerTransform.position, interactionDistance);
        }
    }
} 