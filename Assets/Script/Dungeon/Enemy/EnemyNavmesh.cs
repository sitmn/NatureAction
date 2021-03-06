using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UniRx;

public class EnemyNavmesh : MonoBehaviour
{
    public NavMeshAgent _navmeshAgent;
    private Animator _animator;

    [SerializeField]
    private Transform _targetTr;

    // Start is called before the first frame update
    void Awake()
    {
        _navmeshAgent = GetComponent<NavMeshAgent>();
        _animator = GetComponent<Animator>();
    }

    private void Start()
    {
        //会話イベントストリーム（NavmeshAgent停止用）
        EventManager._eventFlag.Subscribe((x) =>
        {
            if (x == true)
            {
                _navmeshAgent.Stop(true);
            }
            else
            {
                _navmeshAgent.Resume();
            }
        }).AddTo(this);
    }

    // Update is called once per frame
    void Update()
    {
        _navmeshAgent.SetDestination(_targetTr.position);
        _animator.SetFloat("MoveSpeed", _navmeshAgent.velocity.sqrMagnitude);

        

        //移動中以外はキャラの方を向く
        if(_navmeshAgent.updateRotation == false)
        {
            Vector3 direction = _targetTr.position - transform.position;
            Quaternion toRotation = Quaternion.FromToRotation(transform.forward, direction);
            transform.rotation = Quaternion.Lerp(transform.rotation, transform.rotation * toRotation, ConstValue._enemyrotateSpeed * Time.deltaTime);
        }
    }
}
