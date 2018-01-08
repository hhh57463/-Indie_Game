using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerMoveDirection
{
    STOP,
    UP,
    DOWN,
    LEFT,
    RIGHT,
    UPLEFT,
    LEFTDOWN,
    RIGHTDOWN,
    UPRIGHT
}

public class Player : MonoBehaviour
{
    [SerializeField] GameObject PlayerGams = null;
    [SerializeField] GameObject BulletPrefab = null;
    [SerializeField] GameObject InhaleGams = null;

    [SerializeField] Skills SkillUISc = null;

    SpriteRenderer PlayerSr = null;

    [SerializeField]
    Rigidbody2D PlayerRd = null;

    [SerializeField] Material PlayerTailMat = null;

    Vector3 PlayerPos;

    public float fMoveSpeed = 0.0f;
    float fFlashSkillCool = 0.0f;
    float fRotateDegree = 0.0f;
    float fBulletDelay = 0.0f;

    public PlayerMoveDirection PlayerDirect;

    public int nFlashCool = 0;

    public bool bFlashUse = false;
    bool bFlashSkill = false;
    bool bMoveCtr = false;
    bool bSkillCheck = false;
    bool bBulletShootDelay = false;
    bool bInhaleOn = false;

    // Use this for initialization
    void Start()
    {
        fMoveSpeed = 2.0f;
        nFlashCool = 5;
        PlayerSr = GetComponent<SpriteRenderer>();
        PlayerRd = GameObject.Find("PlayerTrans").GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Skill();
        PlayerRotate();
        ShootingBullet();
        PlayerColor();
    }
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    void Move()
    {
        if (!bMoveCtr)
        {
            if (PlayerDirect == PlayerMoveDirection.STOP)
            {
                if ((Input.GetKey(KeyCode.W)) || (Input.GetKey(KeyCode.S)) || (Input.GetKey(KeyCode.A)) || (Input.GetKey(KeyCode.D)))
                {
                    fMoveSpeed = 2.0f;
                }
            }
            //if (PlayerDirect != PlayerMoveDirection.STOP)
            //{
                //if ((Input.GetKeyUp(KeyCode.W)) || (Input.GetKeyUp(KeyCode.S)) || (Input.GetKeyUp(KeyCode.A)) || (Input.GetKeyUp(KeyCode.D)))
                //{
                //    //fMoveSpeed = 2.0f;
                //    PlayerRd.AddForce(Vector3.zero);
                //}
            //}
            if (Input.GetKey(KeyCode.W))
            {
                //PlayerGams.transform.Translate(Vector3.up * fMoveSpeed * Time.deltaTime);
                PlayerRd.AddForce(Vector3.up * fMoveSpeed);
                PlayerDirect = PlayerMoveDirection.UP;
            }

            if (Input.GetKey(KeyCode.S))
            {
                PlayerGams.transform.Translate(Vector3.down * fMoveSpeed * Time.deltaTime);
                PlayerDirect = PlayerMoveDirection.DOWN;
            }

            if (Input.GetKey(KeyCode.A))
            {
                PlayerGams.transform.Translate(Vector3.left * fMoveSpeed * Time.deltaTime);
                PlayerDirect = PlayerMoveDirection.LEFT;
            }

            if (Input.GetKey(KeyCode.D))
            {
                PlayerGams.transform.Translate(Vector3.right * fMoveSpeed * Time.deltaTime);
                PlayerDirect = PlayerMoveDirection.RIGHT;
            }

            if (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.A))
            {
                PlayerDirect = PlayerMoveDirection.UPLEFT;
            }

            if (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.D))
            {
                PlayerDirect = PlayerMoveDirection.UPRIGHT;
            }

            if (Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.A))
            {
                PlayerDirect = PlayerMoveDirection.LEFTDOWN;
            }

            if (Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.D))
            {
                PlayerDirect = PlayerMoveDirection.RIGHTDOWN;
            }
        }
    }

    void Skill()
    {
        if (PlayerDirect != PlayerMoveDirection.STOP)
        {
            if (Input.GetKeyDown(KeyCode.LeftShift) && !bFlashUse)
            {
                PlayerPos = PlayerGams.transform.localPosition;
                bFlashSkill = true;
                bMoveCtr = true;
                fFlashSkillCool = Time.time;
                SkillUISc.FlashCoolText.enabled = true;
                SkillUISc.FlashImg.fillAmount = 0;
                StartCoroutine(FlashCount());
            }
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
                //case PlayerMoveDirection.STOP:
                //    if (PlayerGams.transform.localPosition.y <= PlayerPos.y + 5f)
                //    {
                //        PlayerGams.transform.Translate(Vector3.up * fMoveSpeed * 20f * Time.deltaTime);
                //    }
                //    else
                //    {
                //        bFlashSkill = false;
                //        PlayerDirect = PlayerMoveDirection.STOP;
                //        bMoveCtr = false;
                //        bFlashUse = true;
                //    }
                //    break;

                case PlayerMoveDirection.UP:
                    if (PlayerGams.transform.localPosition.y < 26f)
                    {
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
                    }
                    if (PlayerGams.transform.localPosition.y > 26f)
                    {
                        if (PlayerGams.transform.localPosition.y < 31.9f)
                        {
                            PlayerGams.transform.Translate(Vector3.up * fMoveSpeed * 20f * Time.deltaTime);
                        }
                        else
                        {
                            fMoveSpeed = 0;
                            bFlashSkill = false;
                            PlayerDirect = PlayerMoveDirection.STOP;
                            bMoveCtr = false;
                            bFlashUse = true;
                        }
                    }
                    if (PlayerGams.transform.localPosition.y >= 32f)
                    {
                        bFlashSkill = false;
                        PlayerDirect = PlayerMoveDirection.STOP;
                        bMoveCtr = false;
                        bFlashUse = true;
                    }
                    break;

                case PlayerMoveDirection.DOWN:
                    if (PlayerGams.transform.localPosition.y > -26f)
                    {
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
                    }

                    if (PlayerGams.transform.localPosition.y < -26f)
                    {
                        if (PlayerGams.transform.localPosition.y > -31.9f)
                        {
                            PlayerGams.transform.Translate(Vector3.down * fMoveSpeed * 20f * Time.deltaTime);
                        }
                        else
                        {
                            fMoveSpeed = 0;
                            bFlashSkill = false;
                            PlayerDirect = PlayerMoveDirection.STOP;
                            bMoveCtr = false;
                            bFlashUse = true;
                        }
                    }

                    if (PlayerGams.transform.localPosition.y <= -31.9f)
                    {
                        bFlashSkill = false;
                        PlayerDirect = PlayerMoveDirection.STOP;
                        bMoveCtr = false;
                        bFlashUse = true;
                    }
                    break;

                case PlayerMoveDirection.LEFT:
                    if (PlayerGams.transform.localPosition.x > -26f)
                    {
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
                    }
                    if (PlayerGams.transform.localPosition.x < -26f)
                    {
                        if (PlayerGams.transform.localPosition.x > -31.9f)
                        {
                            PlayerGams.transform.Translate(Vector3.left * fMoveSpeed * 20f * Time.deltaTime);
                        }
                        else
                        {
                            fMoveSpeed = 0;
                            bFlashSkill = false;
                            PlayerDirect = PlayerMoveDirection.STOP;
                            bMoveCtr = false;
                            bFlashUse = true;
                        }
                    }

                    if (PlayerGams.transform.localPosition.y <= -31.9f)
                    {
                        bFlashSkill = false;
                        PlayerDirect = PlayerMoveDirection.STOP;
                        bMoveCtr = false;
                        bFlashUse = true;
                    }
                    break;

                case PlayerMoveDirection.RIGHT:
                    if (PlayerGams.transform.localPosition.x < 26f)
                    {
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
                    }

                    if (PlayerGams.transform.localPosition.x > 26f)
                    {
                        if (PlayerGams.transform.localPosition.x < 31.9f)
                        {
                            PlayerGams.transform.Translate(Vector3.right * fMoveSpeed * 20f * Time.deltaTime);
                        }
                        else
                        {
                            fMoveSpeed = 0;
                            bFlashSkill = false;
                            PlayerDirect = PlayerMoveDirection.STOP;
                            bMoveCtr = false;
                            bFlashUse = true;
                        }
                    }

                    if (PlayerGams.transform.localPosition.y >= 31.9f)
                    {
                        bFlashSkill = false;
                        PlayerDirect = PlayerMoveDirection.STOP;
                        bMoveCtr = false;
                        bFlashUse = true;
                    }

                    break;

                case PlayerMoveDirection.UPLEFT:
                    if ((PlayerGams.transform.localPosition.y < 10f) && (PlayerGams.transform.localPosition.x > -21f))
                    {
                        if ((PlayerGams.transform.localPosition.y <= PlayerPos.y + 5f) && (PlayerGams.transform.localPosition.x >= PlayerPos.x - 5f))
                        {
                            PlayerGams.transform.Translate(Vector3.up * fMoveSpeed * 20f * Time.deltaTime);
                            PlayerGams.transform.Translate(Vector3.left * fMoveSpeed * 20f * Time.deltaTime);
                        }
                        else
                        {
                            bFlashSkill = false;
                            PlayerDirect = PlayerMoveDirection.STOP;
                            bMoveCtr = false;
                            bFlashUse = true;
                        }
                    }

                    if ((PlayerGams.transform.localPosition.y > 10f) && (PlayerGams.transform.localPosition.x < -21f))
                    {
                        if ((PlayerGams.transform.localPosition.y < 15.9f) && (PlayerGams.transform.localPosition.x > -26.9f))
                        {
                            PlayerGams.transform.Translate(Vector3.up * fMoveSpeed * 20f * Time.deltaTime);
                            PlayerGams.transform.Translate(Vector3.left * fMoveSpeed * 20f * Time.deltaTime);
                        }
                        else
                        {
                            fMoveSpeed = 0;
                            bFlashSkill = false;
                            PlayerDirect = PlayerMoveDirection.STOP;
                            bMoveCtr = false;
                            bFlashUse = true;
                        }
                    }
                    if ((PlayerGams.transform.localPosition.y >= 16f) && (PlayerGams.transform.localPosition.y <= -26.9f))
                    {
                        bFlashSkill = false;
                        PlayerDirect = PlayerMoveDirection.STOP;
                        bMoveCtr = false;
                        bFlashUse = true;
                    }

                    break;

                case PlayerMoveDirection.UPRIGHT:
                    if ((PlayerGams.transform.localPosition.y <= PlayerPos.y + 5f) && (PlayerGams.transform.localPosition.x <= PlayerPos.x + 5f))
                    {
                        PlayerGams.transform.Translate(Vector3.up * fMoveSpeed * 20f * Time.deltaTime);
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

                case PlayerMoveDirection.LEFTDOWN:
                    if ((PlayerGams.transform.localPosition.x >= PlayerPos.x - 5f) && (PlayerGams.transform.localPosition.y >= PlayerPos.y - 5f))
                    {
                        PlayerGams.transform.Translate(Vector3.down * fMoveSpeed * 20f * Time.deltaTime);
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

                case PlayerMoveDirection.RIGHTDOWN:
                    if ((PlayerGams.transform.localPosition.x <= PlayerPos.x + 5f) && (PlayerGams.transform.localPosition.y >= PlayerPos.y - 5f))
                    {
                        PlayerGams.transform.Translate(Vector3.down * fMoveSpeed * 20f * Time.deltaTime);
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
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

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

    void PlayerColor()                              //빨 파 노 보 초
    {

        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            PlayerSr.color = new Color(255f, 255f, 255f, 255f);
            PlayerTailMat.color = new Color(255f, 255f, 255f, 255f);
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            PlayerSr.color = new Color(255f, 0f, 0f, 255f);
            PlayerTailMat.color = new Color(255f, 0f, 0f, 255f);
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            PlayerSr.color = new Color(0f, 0f, 255f, 255f);
            PlayerTailMat.color = new Color(0f, 0f, 255f, 255f);
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            PlayerSr.color = new Color(255f, 255f, 0f, 255f);
            PlayerTailMat.color = new Color(255f, 255f, 0f, 255f);
        }

        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            PlayerSr.color = new Color(255f, 0f, 255f, 255f);
            PlayerTailMat.color = new Color(255f, 0f, 255f, 255f);
        }

        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            PlayerSr.color = new Color(0f, 255f, 0f, 255f);
            PlayerTailMat.color = new Color(0f, 255f, 0f, 255f);
        }

    }

    IEnumerator FlashCount()
    {
        yield return new WaitForSeconds(1f);
        nFlashCool--;
        if (nFlashCool > 0)
            StartCoroutine(FlashCount());
        else
        {
            nFlashCool = 5;
            SkillUISc.FlashCoolText.enabled = false;
        }
    }

}
