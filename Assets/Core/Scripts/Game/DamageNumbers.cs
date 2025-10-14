using UnityEngine;
using DamageNumbersPro;

public class DamageNumbers : MonoBehaviour {

    public static DamageNumbers Instance;
    //Assign prefab in inspector.
    public DamageNumber Damage;
    public DamageNumber Heal;
    public DamageNumber Missed;
    public DamageNumber Buffs;
    public RectTransform rectParent;

    private Camera _camera;

    private void Awake()
    {
        _camera = Camera.main;
        if (Instance == null)
        {
            Instance = this;
            return;
        }
        Destroy(gameObject);
    }

    public void SpawnNumber(int value, Vector3 worldPosition, DamageType type = DamageType.Damage)
    {
        SpawnNumber(value.ToString(), worldPosition, type);
    }

    public void SpawnNumber(string value, Vector3 worldPosition, DamageType type = DamageType.Damage)
    {
        Vector3 screenPos = _camera.WorldToScreenPoint(worldPosition);

        if (screenPos.z > 0) // объект перед камерой
        {
            RectTransformUtility.ScreenPointToLocalPointInRectangle(rectParent, screenPos, _camera, out var canvasPos);
            var shower = type switch
            {
                DamageType.Damage => Damage,
                DamageType.Heal => Heal,
                DamageType.Missed => Missed,        
                DamageType.Buffs => Buffs,
                _ => Damage
            };
            var symbol = type switch
            {
                DamageType.Damage => "-",
                _ => ""
            };
            shower.SpawnGUI(rectParent, canvasPos, $"{symbol}{value}");
        }
    }

    public enum DamageType
    {
        Damage,
        Heal,
        Missed,
        Buffs
    }

}