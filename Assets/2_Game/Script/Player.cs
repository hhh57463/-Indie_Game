using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerMoveDirection
{
    STOP,
    UP,
    DOWN,
    LEFT,
    RIGHT
}

public class Player : MonoBehaviour
{
    [SerializeField] GameObject PlayerGams = null;
    [SerializeField] GameObject BulletPrefab = null;
    [SerializeField] GameObject InhaleGams = null;

    Vector3 PlayerPos;

    float fMoveSpeed = 0.0f;
    float fFlashSkillCool = 0.0f;
    float fRotateDegree = 0.0f;
    float fBulletDelay = 0.0f;

    public PlayerMoveDirection PlayerDirect;

    bool bFlashSkill = false;
    bool bFlashUse = false;
    bool bMoveCtr = false;
    bool bSkillCheck = false;
    bool bBulletShootDelay = false;
    bool bInhaleOn = false;

    // Use this for initialization
    void Start()
    {
        fMoveSpeed = 2.0f;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Skill();
        PlayerRotate();
        ShootingBullet();
    }

    void Move()
    {
        if (Input.GetKey(KeyCode.W) && !bMoveCtr)
        {
            PlayerGams.transform.Translate(Vector3.up * fMoveSpeed * Time.deltaTime);
            PlayerDirect = PlayerMoveDirection.UP;
        }

        if (Input.GetKey(KeyCode.S) && !bMoveCtr)
        {
            PlayerGams.transform.Translate(Vector3.down * fMoveSpeed * Time.deltaTime);
            PlayerDirect = PlayerMoveDirection.DOWN;
        }

        if (Input.GetKey(KeyCode.A) && !bMoveCtr)
        {
            PlayerGams.transform.Translate(Vector3.left * fMoveSpeed * Time.deltaTime);
            PlayerDirect = PlayerMoveDirection.LEFT;
        }

        if (Input.GetKey(KeyCode.D) && !bMoveCtr)
        {
            PlayerGams.transform.Translate(Vector3.right * fMoveSpeed * Time.deltaTime);
            PlayerDirect = PlayerMoveDirection.RIGHT;
        }
    }

    void Skill()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift) && !bFlashUse)
        {
            PlayerPos = PlayerGams.transform.localPosition;
            bFlashSkill = true;
            bMoveCtr = true;
            fFlashSkillCool = Time.time;
        }
        if (bFlashUse)
        {
            bSkillCheck = true;
            if (Time.time > fFlashSkillCool + 5f)
            {
                bFlashSkill = false;
                bSkillCheck = false;
                bFlashUse = false;
            }
        }

        if (Input.GetKey(KeyCode.Space)) { InhaleGams.SetActive(true); bInhaleOn = true; }
        else { InhaleGams.SetActive(false); bInhaleOn = false; }

        if (bFlashSkill && !bSkillCheck)
        {
            switch (PlayerDirect)
            {
                case PlayerMoveDirection.STOP:
                    if (PlayerGams.transform.localPosition.y <= PlayerPos.y + 5f)
                    {
                        PlayerGams.transform.Translate(Vector3.up * fMoveSpeed * 20f * Time.deltaTime);
                    }
                    else
                    {
                        bFlashSkill = false;
                        PlayerDirect = PlayerMoveDirection.STOP;
                        bMoveCtr = false;
                        bFlashUse = true;
                    }
                    break;

                case PlayerMoveDirection.UP:
                    if (PlayerGams.transform.localPosition.y <= PlayerPos.y + 5f)
                    {
                        PlayerGams.transform.Translate(Vector3.up * fMoveSpeed * 20f * Time.deltaTime);
                    }
                    else
                    {
                        bFlashSkill = false;
                        PlayerDirect = PlayerMoveDirection.STOP;
                        bMoveCtr = false;
                        bFlashUse = true;
                    }
                    break;

                case PlayerMoveDirection.DOWN:
                    if (PlayerGams.transform.localPosition.y >= PlayerPos.y - 5f)
                    {
                        PlayerGams.transform.Translate(Vector3.down * fMoveSpeed * 20f * Time.deltaTime);
                    }
                    else
                    {
                        bFlashSkill = false;
                        PlayerDirect = PlayerMoveDirection.STOP;
                        bMoveCtr = false;
                        bFlashUse = true;
                    }
                    break;

                case PlayerMoveDirection.LEFT:
                    if (PlayerGams.transform.localPosition.x >= PlayerPos.x - 5f)
                    {
                        PlayerGams.transform.Translate(Vector3.left * fMoveSpeed * 20f * Time.deltaTime);
                    }
                    else
                    {
                        bFlashSkill = false;
                        PlayerDirect = PlayerMoveDirection.STOP;
                        bMoveCtr = false;
                        bFlashUse = true;
                    }
                    break;

                case PlayerMoveDirection.RIGHT:
                    if (PlayerGams.transform.localPosition.x <= PlayerPos.x + 5f)
                    {
                        PlayerGams.transform.Translate(Vector3.right * fMoveSpeed * 20f * Time.deltaTime);
                    }
                    else
                    {
                        bFlashSkill = false;
                        PlayerDirect = PlayerMoveDirection.STOP;
                        bMoveCtr = false;
                        bFlashUse = true;
                    }
                    break;
            }
        }
    }

    void PlayerRotate()
{
        Vector3 mPosition = Input.mousePosition;
        Vector3 oPosition = transform.position;

        mPosition.z = oPosition.z - Camera.main.transform.position.z;

        Vector3 target = Camera.main.ScreenToWorldPoint(mPosition);

        float dy = target.y - oPosition.y;
        float dx = target.x - oPosition.x;
        fRotateDegree = Mathf.Atan2(dy, dx) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.Euler(0f, 0f, fRotateDegree);
    }

    void ShootingBullet()
    {
        if (Input.GetMouseButton(0))
        {
            if (!bBulletShootDelay && !bInhaleOn)
            {
                fBulletDelay = Time.time;
                Instantiate(BulletPrefab, PlayerGams.transform.localPosition, Quaternion.Euler(0, 0, fRotateDegree - 90f));
                bBulletShootDelay = true;
            }
        }
        if (Time.time > fBulletDelay + 0.5f)                                //연사속도
        {
            bBulletShootDelay = false;
        }
    }

}
