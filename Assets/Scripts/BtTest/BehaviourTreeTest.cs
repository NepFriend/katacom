using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace myBehaviourTreeTest
{
    // 리턴값
    public enum enStatus
    {
        Invalid,
        Success,
        Failure,
        Running,
        Aborted,
    };

    //어떤 노드인지 역할 알려줌
    public enum enNodeType
    {
        Root,
        Selector,
        Sequence,
        Paraller,
        Decorator,
        Condition,
        Action,
    };

    public class BehaviourTreeTest 
    {
        private enStatus _enMyStatus;
        private enNodeType _enMyNodeType;
        private int _iIndex;
        private BehaviourTreeTest _btParent;

        //끝냈나 보는 불값
        public bool IsTerminated() { return _enMyStatus == enStatus.Success | _enMyStatus == enStatus.Failure; }
        
        public bool IsRunning() { return _enMyStatus == enStatus.Running; }

        //부모 설정
        public void SetParent(BehaviourTreeTest btNewParent) { _btParent = btNewParent; }

        //부모 결정
        public BehaviourTreeTest GetParent() { return _btParent; }

        //스테이터스 설정
        public enStatus GetStatus() { return _enMyStatus; }

        //스테이터스 결정
        public void SetStatus(enStatus enNewStatus) { _enMyStatus = enNewStatus; }

        //노드 설정 
        public enNodeType GetNodeType() { return _enMyNodeType; }

        //노드 결정
        public void SetNodeType(enNodeType enNewType) { _enMyNodeType = enNewType; }

        //인덱스 설정
        public int GetIndex() { return _iIndex; }

        //인덱스 결정
        public void SetIndex(int iIndex) { _iIndex = iIndex; }

        //리셋시키기
        virtual public void Reset() { _enMyStatus = enStatus.Invalid; }

        //초기화?
        public virtual void Initialize() { }

        //실행?
        public virtual enStatus Update()
        {
            return enStatus.Success;
        }

        //끝내기
        public virtual void Terminate() { }


        //종합 실행기
        public virtual enStatus Tick()
        {
            if (_enMyStatus == enStatus.Invalid)
            {
                Initialize();
                _enMyStatus = enStatus.Running;
            }

            _enMyStatus = Update();

            if (_enMyStatus != enStatus.Running)
            {
                Terminate();
            }
            return _enMyStatus;
        }
    }

}





