using Core.Pool;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using static MatrixData;

public class UpgradePlayerController : MonoBehaviour
{
    public PlayerData playerData;
    private int HpLevel;
    private int AttackSpeedLevel;
    private int PowerLevel;
    public Text Hp_text;
    public Text AttackSpeed_text;
    public Text Power_text;
    private float newHP;
    private float newatt;
    private float newPower;
    public GameObject Player;
    public Transform PosSpawn;
    public Text UnitText;
    private int unit;
    void Start()
    {
        SmartPool.Instance.Spawn(Player.gameObject, PosSpawn.position, PosSpawn.rotation);
        HpLevel = 0;
        AttackSpeedLevel = 0;
        PowerLevel = 0;
        UpgradeHP();
        UpgradeAttackSpeed();
        UpgradePower();
    }
    private void Update()
    {
        Hp_text.text = newHP.ToString();
        AttackSpeed_text.text = newatt.ToString();
        Power_text.text = newPower.ToString();
        unit = 1;
        UnitText.text = unit.ToString();
    }
    public void UpgradeHP()
    {
        HpLevel++;
        UpdateHP();
    }
    public void UpdateHP()
    {
        var upgradeRow = playerData.ContentContent.Data_Player
           .FirstOrDefault(row => row.HP_Level == HpLevel);
        if (upgradeRow != null)
        {
            newHP = float.Parse(upgradeRow.HP);
            PlayerController.instance.SetHP(newHP);
        }
        else
        {
            Debug.LogWarning($"Không tìm thấy HP_Level = {HpLevel}");
        }
    }

    public void UpgradeAttackSpeed()
    {
        AttackSpeedLevel++;
        UpdateAttSpeed();
    }
    public void UpdateAttSpeed()
    {
        var upgradeAtt = playerData.ContentContent.Data_Player.FirstOrDefault(row => row.Attack_Level == AttackSpeedLevel);
        if (upgradeAtt != null)
        {
            newatt = float.Parse(upgradeAtt.Attack_Speed);
            PlayerController.instance.SetAttackSpeed(newatt);
        }
    }

    public void UpgradePower()
    {
        PowerLevel++;
        UpdatePower();
    }
    public void UpdatePower()
    {
        var upgradePower = playerData.ContentContent.Data_Player.FirstOrDefault(row => row.PowerLevel == PowerLevel);
        if (upgradePower != null)
        {
            newPower = float.Parse(upgradePower.Attack_Speed);
            PlayerController.instance.SetPower(newPower);
        }
    }
    public void UnitPlayer()
    {
        float RandomX = Random.Range(-6.6f, 11);
        Vector3 RandomPos = new Vector3(RandomX, PosSpawn.transform.position.y, PosSpawn.transform.position.z);
        unit++;
        GameObject newPlayer = SmartPool.Instance.Spawn(Player.gameObject, RandomPos, PosSpawn.rotation);
        InitPlayerStats(newPlayer);
    }
    private void InitPlayerStats(GameObject playerObj)
    {
        PlayerController controller = playerObj.GetComponent<PlayerController>();
        if (controller == null) return;

        controller.SetHP(newHP);
        controller.SetAttackSpeed(newatt);
        controller.SetPower(newPower);
    }
}
