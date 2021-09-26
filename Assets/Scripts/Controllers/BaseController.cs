using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class BaseController : MonoBehaviour
{
    [SerializeField]
    protected GameObject _lookTarget;
    [SerializeField]
    protected Vector3 _destPos;
    protected Vector3 _endLookDir;
    [SerializeField]
    protected Define.State _state = Define.State.Idle;
    public virtual Define.State State
    {
        get { return _state; }
        set
        {
            if (!gameObject.IsValid()) return;
            _state = value;
            OnChangeState();
        }
    }

    private NavMeshAgent navAgent;
    public NavMeshAgent NavAgent
    {
        get; protected set;
    }
    Animator animator;
    public Animator Anim
    {
        get; protected set;
    }

    private void Awake()
    {
        Init();
    }
    public virtual void Init()
    {
        NavAgent = gameObject.GetOrAddComponent<NavMeshAgent>();
        Anim = gameObject.GetComponentInChildren<Animator>();
        if (!Anim)
            Anim = gameObject.GetOrAddComponent<Animator>();
    }
    void Update()
    {
        switch (State)
        {
            case Define.State.Moving:
                UpdateMoving();
                break;
            default:
                break;
        }
    }
  
    protected virtual void UpdateMoving()
    {
        if (!NavAgent.pathPending)
        {
            if (NavAgent.remainingDistance <= NavAgent.stoppingDistance)
            {
                if (!NavAgent.hasPath || navAgent.velocity.sqrMagnitude == 0f)
                {
                    MoveDoneCallback();
                    State = Define.State.Idle;
                }
            }
        }
    }

    public void SetDestination(Vector3 to, Vector3 EndLookDirection)
    {
        _destPos = to;
        if (NavAgent)
        {
            NavAgent.SetDestination(_destPos);
            this._endLookDir = EndLookDirection;
            State = Define.State.Moving;
        }
    }

    protected virtual void MoveDoneCallback()
    {
        NavAgent.ResetPath();
    }



    protected virtual void OnChangeState()
    {
        switch (_state)
        {
            case Define.State.Idle:
                Anim.CrossFade("WAIT", 0.1f);
                break;
            case Define.State.Moving:
                Anim.CrossFade("WALK", 0.1f);
                break;
            default:
                break;
        }
    }
}
