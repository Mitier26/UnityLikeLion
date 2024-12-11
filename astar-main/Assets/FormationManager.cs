using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

// 대형 타입 정의
public enum FormationType : uint
{
    Square,
    Circle,
    Line,
    Triangle,
    Max
}

// 그룹 및 대형 관리자
public class FormationManager : MonoBehaviour
{
    [Header("Formation Settings")]
    [SerializeField] private FormationType currentFormation = FormationType.Square;
    [SerializeField] private float unitSpacing = 20f;

    private List<UnitObject> units = new List<UnitObject>();
    private Vector3 targetPosition;
    private bool isMoving = false;

    private GridPathfinding _HexGridPathfinding;
    [SerializeField] private LayerMask groundLayer;

    [SerializeField] private UnitObject unitPrefab;
    
    void Start()
    {
        _HexGridPathfinding = GetComponent<GridPathfinding>();
    }
    
    private HashSet<UnitObject> arrivedUnits = new HashSet<UnitObject>();
    
    public void OnUnitReachedDestination(UnitObject unit)
    {
        arrivedUnits.Add(unit);
        
        // 모든 유닛이 도착했는지 체크
        if (arrivedUnits.Count == units.Count)
        {
            AdjustFormation();
            arrivedUnits.Clear();
        }
    }

    private void AdjustFormation()
    {
        Vector3 center = GetGroupCenter();
        Vector3[] formationPositions = CalculateFormationPositions(center);
        
        // 각 유닛에게 최종 위치 할당
        for (int i = 0; i < units.Count; i++)
        {
            if (i < formationPositions.Length)
            {
                List<Vector3> finalPath = new List<Vector3> { formationPositions[i] };
                units[i].SetPath(finalPath);
            }
        }
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("Mouse Down");
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, groundLayer))
            {
                MoveGroup(hit.point);
            }
        }
        if (Input.GetMouseButtonDown(1))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, groundLayer))
            {
                var Node = _HexGridPathfinding.GetNodeFromWorldPosition(hit.point);
                if (Node != null)
                {
                    RegisterUnit(Instantiate(unitPrefab, Node.worldPosition, Quaternion.identity).GetComponent<UnitObject>());
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            ChangeFormation(currentFormation);
            currentFormation = (FormationType)(((int)currentFormation + 1 + (int)FormationType.Max) % (int)FormationType.Max);
        }
    }
    
    // 유닛 등록
    public void RegisterUnit(UnitObject unit)
    {
        if (!units.Contains(unit))
        {
            unit.formationManager = this;
            units.Add(unit);
            UpdateFormation();
        }
    }

    // 그룹 이동 명령
    public void MoveGroup(Vector3 target)
    {
        targetPosition = target;
        isMoving = true;
        UpdateGroupMovement();
    }

    // 대형 변경
    public void ChangeFormation(FormationType newFormation)
    {
        currentFormation = newFormation;
        UpdateFormation();
    }

    // 그룹 이동 업데이트
    private void UpdateGroupMovement()
    {
        Vector3 groupCenter = GetGroupCenter();
        List<Vector3> mainPath = _HexGridPathfinding.FindPath(groupCenter, targetPosition);

        if (mainPath.Count > 0)
        {
            Vector3[] formationPositions = CalculateFormationPositions(mainPath[mainPath.Count - 1]);
            
            for (int i = 0; i < units.Count; i++)
            {
                if (i < formationPositions.Length)
                {
                    List<Vector3> unitPath = _HexGridPathfinding.FindPath(
                        units[i].transform.position,
                        formationPositions[i]
                    );
                    units[i].SetPath(unitPath);
                }
            }
        }
    }

    // 대형 위치 계산
    private Vector3[] CalculateFormationPositions(Vector3 center)
    {
        switch (currentFormation)
        {
            case FormationType.Square:
                return CalculateSquareFormation(center);
            case FormationType.Circle:
                return CalculateCircleFormation(center);
            case FormationType.Line:
                return CalculateLineFormation(center);
            case FormationType.Triangle:
                return CalculateTriangleFormation(center);
            default:
                return new Vector3[0];
        }
    }

    // 그룹 중심점 계산
    private Vector3 GetGroupCenter()
    {
        if (units.Count == 0) return Vector3.zero;
        return units.Aggregate(Vector3.zero, (sum, unit) => sum + unit.transform.position) / units.Count;
    }

    // 대형별 위치 계산 메서드들
    private Vector3[] CalculateSquareFormation(Vector3 center)
    {
        List<Vector3> positions = new List<Vector3>();
        int unitCount = units.Count;
    
        // 열과 행 수 계산을 수정
        int cols = Mathf.CeilToInt(Mathf.Sqrt(unitCount));
        int rows = Mathf.CeilToInt((float)unitCount / cols);

        for (int i = 0; i < unitCount; i++)
        {
            int row = i / cols;
            int col = i % cols;
        
            // 오프셋 계산을 명확하게
            float xOffset = (col - (cols - 1) / 2f) * unitSpacing;
            float zOffset = (row - (rows - 1) / 2f) * unitSpacing;
        
            // 중심점에서 오프셋을 적용
            Vector3 position = center + new Vector3(xOffset, 0, zOffset);
            positions.Add(position);
        }

        return positions.ToArray();
    }

    private Vector3[] CalculateCircleFormation(Vector3 center)
    {
        List<Vector3> positions = new List<Vector3>();
        int unitCount = units.Count;
    
        // 반지름을 유닛 수에 따라 적절히 조정
        float radius = unitSpacing * unitCount / (2 * Mathf.PI);
        float angleStep = 360f / unitCount;

        for (int i = 0; i < unitCount; i++)
        {
            float angle = i * angleStep * Mathf.Deg2Rad;
            float xOffset = Mathf.Cos(angle) * radius;
            float zOffset = Mathf.Sin(angle) * radius;
        
            Vector3 position = center + new Vector3(xOffset, 0, zOffset);
            positions.Add(position);
        }

        return positions.ToArray();
    }

    private Vector3[] CalculateLineFormation(Vector3 center)
    {
        List<Vector3> positions = new List<Vector3>();
        int unitCount = units.Count;
        float startX = -(unitCount - 1) * unitSpacing / 2f;

        for (int i = 0; i < unitCount; i++)
        {
            float x = startX + (i * unitSpacing);
            positions.Add(center + new Vector3(x, 0, 0));
        }

        return positions.ToArray();
    }

    private Vector3[] CalculateTriangleFormation(Vector3 center)
    {
        List<Vector3> positions = new List<Vector3>();
        int unitCount = units.Count;
        int currentRow = 0;
        int unitsInCurrentRow = 1;
        int remainingUnits = unitCount;

        while (remainingUnits > 0)
        {
            for (int i = 0; i < unitsInCurrentRow && remainingUnits > 0; i++)
            {
                float x = (i - (unitsInCurrentRow - 1) / 2f) * unitSpacing;
                float z = -currentRow * unitSpacing;
                positions.Add(center + new Vector3(x, 0, z));
                remainingUnits--;
            }
            currentRow++;
            unitsInCurrentRow++;
        }

        return positions.ToArray();
    }

    // 현재 대형 업데이트
    private void UpdateFormation()
    {
        if (!isMoving)
        {
            Vector3 center = GetGroupCenter();
            Vector3[] positions = CalculateFormationPositions(center);
            
            for (int i = 0; i < units.Count; i++)
            {
                if (i < positions.Length)
                {
                    List<Vector3> path = _HexGridPathfinding.FindPath(
                        units[i].transform.position,
                        positions[i]
                    );
                    units[i].SetPath(path);
                }
            }
        }
    }
}