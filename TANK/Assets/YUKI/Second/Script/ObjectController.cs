using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class ObjectController : MonoBehaviour
{
    [SerializeField]
    private Vector3 m_target;

    private NavMeshAgent m_navAgent = null;

    private Vector3 TargetPos;

    private void Awake()
    {
        m_navAgent = GetComponent<NavMeshAgent>();
        TargetPos = GetComponent<FromtCheck>().getTargetPosition;
    }

    private void Start()
    {
    }

    private void Update()
    {
        TargetPos = GetComponent<FromtCheck>().getTargetPosition;

        Move();
    }

    private void Move()
    {
        m_target = TargetPos;

        if (this.GetComponent<FromtCheck>().instanceflg == true)
        {
            m_navAgent.destination = m_target;
            this.GetComponent<FromtCheck>().instanceflg = false;
        }
    }

} // class ObjectController