using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace myBehaviourTreeTest
{
    // ���ϰ�
    public enum enStatus
    {
        Invalid,
        Success,
        Failure,
        Running,
        Aborted,
    };

    //� ������� ���� �˷���
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

        //���³� ���� �Ұ�
        public bool IsTerminated() { return _enMyStatus == enStatus.Success | _enMyStatus == enStatus.Failure; }
        
        public bool IsRunning() { return _enMyStatus == enStatus.Running; }

        //�θ� ����
        public void SetParent(BehaviourTreeTest btNewParent) { _btParent = btNewParent; }

        //�θ� ����
        public BehaviourTreeTest GetParent() { return _btParent; }

        //�������ͽ� ����
        public enStatus GetStatus() { return _enMyStatus; }

        //�������ͽ� ����
        public void SetStatus(enStatus enNewStatus) { _enMyStatus = enNewStatus; }

        //��� ���� 
        public enNodeType GetNodeType() { return _enMyNodeType; }

        //��� ����
        public void SetNodeType(enNodeType enNewType) { _enMyNodeType = enNewType; }

        //�ε��� ����
        public int GetIndex() { return _iIndex; }

        //�ε��� ����
        public void SetIndex(int iIndex) { _iIndex = iIndex; }

        //���½�Ű��
        virtual public void Reset() { _enMyStatus = enStatus.Invalid; }

        //�ʱ�ȭ?
        public virtual void Initialize() { }

        //����?
        public virtual enStatus Update()
        {
            return enStatus.Success;
        }

        //������
        public virtual void Terminate() { }


        //���� �����
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





