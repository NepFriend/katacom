using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMlAttack : PlayerAttack
{
    bool shootAgain;

    //��� ü������ ����Ѱ� Ȯ��
    bool modeChange;
    bool modeFire;

    int rollAttacklevel;

    // Start is called before the first frame update
    void Start()
    {
        playerMove = GetComponent<PlayerMove>();

        playerInput = GetComponent<PlayerInput>();

        PlInputAnimation = GetComponent<PlayerAnimation>();

        playerAttackDir = 0;
        stopActionOnOff = false;
        shoot = true;
        shootAgain = true;
        shootDirTime = 0;
        granadeOn = false;
        stopActionKind = 0;
        reloading = false;
        shootDelay = 1f;
        modeChange = true;
        modeFire = true;

        rollAttacklevel = 0;
    }

    // Update is called once per frame
    void Update()
    {
       // Debug.Log(shoot + "  " + shootAgain);

        //AttackDir();
        stopAction();
        attack();
        granade();

        //Debug.Log(modeChange + "  " + modeFire);
    }
    public override void AttackDir()
    {
        if (Input.GetKeyDown(KeyCode.Q) && playerAttackDir != 2)
        {
            playerAttackDir = 1;
            //shootDelay = 0.13f;
            PlInputAnimation.ShootReady(playerAttackDir);
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            playerAttackDir = 2;
            //shootDelay = 0.13f;
            PlInputAnimation.ShootReady(playerAttackDir);
        }

        if (Input.GetMouseButtonDown(1) && playerAttackDir != 2 && playerAttackDir != 1)
        {
            playerAttackDir = 3;
            //shootDelay = 0.13f;
            PlInputAnimation.ShootReady(playerAttackDir);
        }

        if (playerMove.jump && playerInput.lookDown && playerAttackDir == 0 && playerAttackDir != 4)
        {
            playerAttackDir = 4;
            //shootDelay = 0.13f;
            PlInputAnimation.ShootReady(playerAttackDir);
        }

        if (playerAttackDir > 0)
        {
            shootDirTime++;
        }

        if (Input.GetKeyUp(KeyCode.Q) && playerAttackDir == 1 || Input.GetKeyUp(KeyCode.W) && playerAttackDir == 2 || Input.GetMouseButtonUp(1) && playerAttackDir == 3 || playerAttackDir == 4 && !playerMove.jump || playerAttackDir == 4 && !playerInput.lookDown)
        {
            playerAttackDir = 0;
            shootDirTime = 0;
            PlInputAnimation.ShootReady(playerAttackDir);
        }


    }

    public override void attack()
    {



     

        if (modeChange)
        {
            if (Input.GetMouseButtonDown(0))
            {

                //Debug.Log(modeChange );

                if (!playerMove.run && playerMove.moveLevel != 4)
                {
                    if (shoot)
                    {
                        shootDelay = 1f;
                        StartCoroutine(PlayerWeakAttack());
                    }
                    else
                    {
                        shootAgain = false;
                    }


                }
                else if (playerMove.run)
                {
                    if (shoot)
                    {
                        StartCoroutine(PlayerWeakRunAttack());
                    }
                }
                else if (playerMove.moveLevel == 4)
                {
                    if (shoot)
                    {
                        StartCoroutine(PlayerWeakRollAttack());
                    }
                }




                Debug.Log(modeChange + "  " + modeFire);
            }

            if (Input.GetMouseButtonDown(1))
            {

                if (!playerMove.run && playerMove.moveLevel != 4)
                {
                    if (!modeChange && modeFire)
                    {
                        modeChange = true;
                        modeFire = false;

                        StartCoroutine(PlayerModeAttack());


                    }
                    else
                    {
                        if (shoot)
                        {
                            shootDelay = 1f;
                            StartCoroutine(PlayerStrongAttack());
                        }
                        else
                        {
                            shootAgain = false;
                        }
                    }


                }
                else if (playerMove.run)
                {
                    if (shoot)
                    {
                        StartCoroutine(PlayerStrongRunAttack());
                    }
                }
                else if (playerMove.moveLevel == 4)
                {
                    if (shoot)
                    {
                        StartCoroutine(PlayerStrongRollAttack());
                    }
                }


            }
        }
        else
        {
            if (Input.GetMouseButtonDown(1) || Input.GetMouseButtonDown(0))
            {
                if (shoot)
                {
                    shootDelay = 1f;
                    StartCoroutine(PlayerModeAttack());
                    modeFire = false;
                    modeChange = true;
                }



            }

        }


    }

    public override void stopAction()
    {
        if (!stopActionOnOff)
        {
            //����, �麴��, �Ӽ�����
            if (Input.GetKeyDown(KeyCode.R) && !reloading)
            {
                stopActionKind = 0;
                stopActionTimeLimit = 1.7f;
                reloading = true;
                StartCoroutine(reload());

            }

            //if (Input.GetKeyDown(KeyCode.V))
            //{
            //    stopActionKind = 1;
            //    stopActionTimeLimit = 0.8f;
            //    StartCoroutine(stopActionTime());
            //}

            if (Input.GetKeyDown(KeyCode.LeftControl))
            {
                //�и� �ʻ�� ������ �ѹ�
                if (modeChange && modeFire)
                {
                    modeChange = false;
                    PlInputAnimation.StopAction(0);
                    shootDelay = 0.5f;

                    Debug.Log(modeChange + "  " + modeFire);
                }

            }
        }

    }

    public override void granade()
    {
        //����ź�� Ư���ϰ� ����� ǥ��
        if (Input.GetKeyDown(KeyCode.G) && playerAttackDir == 0 && !stopActionOnOff && !granadeOn)
        {
            granadeOn = true;
            PlInputAnimation.granadeStart();
        }

        if (Input.GetKeyUp(KeyCode.G) && granadeOn)
        {
            granadeOn = false;
            PlInputAnimation.granadeEnd();
        }
    }

    IEnumerator PlayerWeakAttack() // �÷��̾� �����
    {
        // PlInputAnimation.DamagedAnime();

        shoot = false;
        PlInputAnimation.Shoot(0);


        yield return new WaitForSeconds(shootDelay);

        if (shootAgain)
        {
            shoot = true;
            PlInputAnimation.ShootReady(0);
        }
        else
        {
            
            StartCoroutine(PlayerWeakAttack());
            shootAgain = true;
        }
       
    }

    IEnumerator PlayerStrongAttack() // �÷��̾� ������
    {
        // PlInputAnimation.DamagedAnime();

        shoot = false;
        PlInputAnimation.Shoot(3);


        yield return new WaitForSeconds(shootDelay);

        if (shootAgain)
        {
            shoot = true;
            PlInputAnimation.ShootReady(0);
        }
        else
        {

            StartCoroutine(PlayerStrongAttack());
            shootAgain = true;
        }

    }

    IEnumerator PlayerWeakRollAttack() // �÷��̾� ����������
    {

        shoot = false;
        PlInputAnimation.Shoot(1);


        yield return new WaitForSeconds(0.6f);

        shoot = true;

    }

    IEnumerator PlayerStrongRollAttack() // �÷��̾� �����Ⱝ����
    {
        
        shoot = false;
        PlInputAnimation.Shoot(4);


        yield return new WaitForSeconds(1.3f);

        shoot = true;

    }

    IEnumerator PlayerWeakRunAttack() // �÷��̾� �޸��� �����
    {

        shoot = false;
        PlInputAnimation.Shoot(2);


        yield return new WaitForSeconds(0.9f);

        shoot = true;

    }

    IEnumerator PlayerStrongRunAttack() // �÷��̾� �޸��� ������
    {

        shoot = false;
        PlInputAnimation.Shoot(5);


        yield return new WaitForSeconds(1.4f);

        shoot = true;

    }

    IEnumerator PlayerJumpAttack() // �÷��̾� ��������
    {

        shoot = false;
        PlInputAnimation.Shoot(6);


        yield return new WaitForSeconds(shootDelay);


    }

    IEnumerator PlayerModeAttack() // �÷��̾� �޸��� �����
    {

        shoot = false;

        modeFire = false;
        PlInputAnimation.Shoot(7);


        yield return new WaitForSeconds(1.1f);

        shoot = true;

    }

    IEnumerator stopActionTime() // �÷��̾ �����ؾ� �ϴ� Ư�� �׼�
    {

        stopActionOnOff = true;
        PlInputAnimation.StopAction(stopActionKind);
        yield return new WaitForSeconds(stopActionTimeLimit);
        PlInputAnimation.StopActionDone();

        stopActionOnOff = false;
    }

    IEnumerator reload() // �÷��̾� ������
    { // ar ����

        reloading = true;
        PlInputAnimation.StopAction(0);
        yield return new WaitForSeconds(1.7f);
        PlInputAnimation.StopActionDone();

        reloading = false;

    }
}