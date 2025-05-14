using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillCardController : MonoBehaviour
{
    private LevelUpUI levelUpUI;
    private PlayerStatHendler playerStatHandler;
    private WeaponStat weaponStat;

    private void Start()
    {
        levelUpUI = GameManager.Instance.levelUpUI;
        playerStatHandler = GameManager.Instance.player.Stat;
        weaponStat = GameManager.Instance.weaponStat;
    }

    public void ChangeAttackDanmageLevel()
    {
        levelUpUI.ChangeColor(this.gameObject, weaponStat.UpgradeDamage_Count);
        levelUpUI.InvisibleCard();
        GameManager.Instance.AttackDamageUp();
        Time.timeScale = 1f;
    }

    public void ChangePlayerSpeedLevel()
    {
        levelUpUI.ChangeColor(this.gameObject, playerStatHandler.UpgradePlayerSpeed_Count);
        levelUpUI.InvisibleCard();
        GameManager.Instance.PlayerSpeedUp();
        Time.timeScale = 1f;
    }

    public void ChangeAttackDelayLevel()
    {
        levelUpUI.ChangeColor(this.gameObject, playerStatHandler.UpgradeAttackDelay_Count);
        levelUpUI.InvisibleCard();
        GameManager.Instance.AttackDelayUp();
        Time.timeScale = 1f;
    }

    public void ChangeBulletNumLevel()
    {
        levelUpUI.ChangeColor(this.gameObject, weaponStat.UpgradeBulletNum_Count);
        levelUpUI.InvisibleCard();
        GameManager.Instance.BulletNumUp();
        Time.timeScale = 1f;
    }

    public void ChangeMaxHpLevel()
    {
        levelUpUI.ChangeColor(this.gameObject, playerStatHandler.UpgradeMaxHp_Count);
        levelUpUI.InvisibleCard();
        GameManager.Instance.MaxHpUp();
        Time.timeScale = 1f;
    }

}
