using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

/// <summary>
/// Инструмент для рандомного размещения префабов (деревья, камни и т.д.) с контролем
/// минимального расстояния между экземплярами и радиусом внутренней зоны, где объекты не спавнятся.
/// </summary>
public class PrefabScatterTool : MonoBehaviour
{
    [InfoBox("Область размещения: центр + размер (по X,Z). Можно указать внутренний радиус, где спавн запрещён.")]
    [BoxGroup("Базовые настройки")]
    public GameObject[] prefabs;

    [BoxGroup("Базовые настройки")]
    public int spawnCount = 100;

    [BoxGroup("Базовые настройки")]
    public Vector2 areaSize = new Vector2(50f, 50f);

    [BoxGroup("Базовые настройки")]
    public Vector3 areaCenter = Vector3.zero;

    [BoxGroup("Ограничения")] [Min(0f)]
    public float minDistance = 2f;

    [BoxGroup("Ограничения")] [Min(0f)]
    public float innerNoSpawnRadius = 5f;

    [BoxGroup("Ограничения")] [Min(1)]
    public int maxAttemptsPerObject = 30;

    [BoxGroup("Размещение на поверхности")]
    public bool alignToGround = true;

    [BoxGroup("Размещение на поверхности")]
    public float raycastHeight = 50f;

    [BoxGroup("Размещение на поверхности")]
    public LayerMask groundLayer = ~0;

    [BoxGroup("Опции результата")]
    public Transform parent;

    [BoxGroup("Опции результата")]
    public bool clearBeforeSpawn = false;

    [BoxGroup("Вариации")] public Vector2 scaleRange = new Vector2(1f, 1f);
    [BoxGroup("Вариации")] public Vector2 rotationYRange = new Vector2(0f, 360f);

    private readonly List<Vector3> _placedPositions = new List<Vector3>();

    [Button(ButtonSizes.Large, Name = "Scatter Prefabs")]
    private void Scatter()
    {
        if (prefabs == null || prefabs.Length == 0)
        {
            Debug.LogWarning("PrefabScatterTool: нет префабов в списке (prefabs).");
            return;
        }

        if (parent == null)
        {
            var go = new GameObject("ScatteredPrefabs");
            parent = go.transform;
            parent.SetParent(this.transform, true);
        }

        if (clearBeforeSpawn)
        {
            ClearChildren();
        }

        _placedPositions.Clear();

        int spawned = 0;

        for (int i = 0; i < spawnCount; i++)
        {
            bool placed = false;

            for (int attempt = 0; attempt < maxAttemptsPerObject; attempt++)
            {
                Vector3 candidate = SamplePositionInArea();

                // проверка на внутренний радиус
                Vector2 flat = new Vector2(candidate.x - (areaCenter.x + transform.position.x), candidate.z - (areaCenter.z + transform.position.z));
                if (flat.magnitude < innerNoSpawnRadius)
                    continue;

                if (alignToGround)
                {
                    if (!ProjectToGround(candidate, out Vector3 grounded))
                    {
                        continue;
                    }

                    candidate = grounded;
                }

                if (IsFarFromOthers(candidate))
                {
                    var prefab = prefabs[Random.Range(0, prefabs.Length)];
                    if (prefab == null) continue;

                    var instance = Instantiate(prefab, candidate, Quaternion.Euler(0f, Random.Range(rotationYRange.x, rotationYRange.y), 0f), parent);

                    float s = Random.Range(scaleRange.x, scaleRange.y);
                    instance.transform.localScale = instance.transform.localScale * s;

                    _placedPositions.Add(candidate);
                    spawned++;
                    placed = true;
                    break;
                }
            }
        }

        Debug.Log($"PrefabScatterTool: попытки={spawnCount}, успешно размещено={spawned}.");
    }

    [Button(ButtonSizes.Small, Name = "Clear Children")]
    private void ClearChildren()
    {
        if (parent == null) return;

        for (int i = parent.childCount - 1; i >= 0; i--)
        {
#if UNITY_EDITOR
            DestroyImmediate(parent.GetChild(i).gameObject);
#else
            Destroy(parent.GetChild(i).gameObject);
#endif
        }
    }

    private Vector3 SamplePositionInArea()
    {
        float halfX = areaSize.x * 0.5f;
        float halfZ = areaSize.y * 0.5f;

        float x = Random.Range(-halfX, halfX) + areaCenter.x + transform.position.x;
        float z = Random.Range(-halfZ, halfZ) + areaCenter.z + transform.position.z;

        float y = areaCenter.y + raycastHeight + transform.position.y;

        return new Vector3(x, y, z);
    }

    private bool ProjectToGround(Vector3 fromAbove, out Vector3 groundedPos)
    {
        Ray ray = new Ray(fromAbove, Vector3.down);
        if (Physics.Raycast(ray, out RaycastHit hit, raycastHeight * 2f, groundLayer.value))
        {
            groundedPos = hit.point;
            return true;
        }

        groundedPos = Vector3.zero;
        return false;
    }

    private bool IsFarFromOthers(Vector3 pos)
    {
        float minDistSqr = minDistance * minDistance;
        for (int i = 0; i < _placedPositions.Count; i++)
        {
            if ((_placedPositions[i] - pos).sqrMagnitude < minDistSqr) return false;
        }

        return true;
    }
}