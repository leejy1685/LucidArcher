using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField] RoomSpawner roomSpawner;
    [SerializeField] UIManager UIManager;
    public LevelUpUI levelUpUI;
    public PlayerStatHendler playerStatHendler;
    public WeaponStat weaponStat;
    public GameObject player;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;

    }

    void Start()
    {
        roomSpawner.Init();
        weaponStat = player.GetComponentInChildren<WeaponStat>(true);
    }

    void FixedUpdate()
    {
        if (playerStatHendler.MaxEXP == playerStatHendler.EXP)
        {
            UIManager.PlayerLevelUp();
        }
    }

    public void AttackDamageUp()
    {
        weaponStat.UpgradeDamage();
        UIManager.ChangeState(UIState.Game);
    }

    public void AttackDelayUp()
    {
        playerStatHendler.UpgradeAttackDelay();
        UIManager.ChangeState(UIState.Game);
    }

    public void MaxHpUp()
    {
        playerStatHendler.UpgradeMaxHP();
        UIManager.ChangeState(UIState.Game);
    }

    public void PlayerSpeedUp()
    {
        playerStatHendler.UpgradePlayerSpeed();
        UIManager.ChangeState(UIState.Game);
    }

    public void BulletNumUp()
    {
        weaponStat.UpgradeBulletNum();
        UIManager.ChangeState(UIState.Game);
    }

}
