using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;

public enum ShapeState
{
<<<<<<< Updated upstream
    Waiting, Shifting, Merging
=======
    Waiting, Shifting
>>>>>>> Stashed changes
}

public abstract class Shape : MonoBehaviour, IPointerDownHandler
{
<<<<<<< Updated upstream
    private const float TimeShiftDown = 0.07f;
    private const float TimeRefillShiftDown = 0.08f;
    private const float TimeBounce = 0.06f;
    private const float BounceAmount = 0.1f;

    public ShapeData _shapeData;
    public ShapeState _shapeState;

    public int _row;
    public int _col;

    private Sequence _shiftDownSequence;

    protected SpriteRenderer _shapeSpriteRenderer;
=======
    public ShapeData _shapeData;
    public ShapeState _shapeState;

    private const float TimeShiftDown = 0.15f;
    private const float TimeRefillShiftDown = 0.15f;
    private const float TimeBounce = 0.1f;
    private const float BounceAmount = 0.05f;

    public int row;
    public int col;

    private SpriteRenderer _shapeSpriteRenderer;
>>>>>>> Stashed changes

    void Awake()
    {
        _shapeSpriteRenderer = GetComponent<SpriteRenderer>();
    }

<<<<<<< Updated upstream
    public abstract void OnPointerDown(PointerEventData eventData);

    public virtual void SetShapeData(ShapeData shapeData, int row, int col)
    {
        this._row = row;
        this._col = col;
        _shapeData = shapeData;
        _shapeSpriteRenderer.sprite = shapeData.Sprite;
        _shapeSpriteRenderer.sortingOrder = row + 2;
=======
    public void OnPointerDown(PointerEventData eventData)
    {
        CheckAdjacentShapes(true);
        BoardManager.Instance.HandleShiftDown();
    }

    public void SetShapeData(ShapeData shapeData, int row, int col)
    {
        this.row = row;
        this.col = col;
        _shapeData = shapeData;
>>>>>>> Stashed changes
    }

    public void CheckAdjacentShapes(bool isThisClickedShape)
    {
        int rows = BoardManager.Instance.GetRowCount();
        int columns = BoardManager.Instance.GetColumnCount();

        if (isThisClickedShape)
            BoardManager.Instance.AddShapeToAdjacentShapes(this);

<<<<<<< Updated upstream
        _CheckAdjacentShapes(_row, _col + 1, columns, false);
        _CheckAdjacentShapes(_row, _col - 1, columns, false);
        _CheckAdjacentShapes(_row + 1, _col, rows, true);
        _CheckAdjacentShapes(_row - 1, _col, rows, true);
    }

    private void _CheckAdjacentShapes(int row, int col, int constraint, bool isRowChanging)
    {
        Shape[,] shapeMatrix = BoardManager.Instance.GetShapeMatrix();
        int temp;

        if (isRowChanging)
            temp = row;
        else
            temp = col;

        if (temp < constraint && temp >= 0)
        {
            if (shapeMatrix[row, col] != null && !BoardManager.Instance.IsShapeCheckedBefore(shapeMatrix[row, col].GetComponent<Shape>()) &&
                shapeMatrix[row, col].GetComponent<Shape>()._shapeData.ShapeType == _shapeData.ShapeType)
            {
                BoardManager.Instance.AddShapeToAdjacentShapes(shapeMatrix[row, col].GetComponent<Shape>());
                shapeMatrix[row, col].GetComponent<Shape>().CheckAdjacentShapes(false);
=======
        _CheckAdjacentShapes(row, col + 1, columns, false);
        _CheckAdjacentShapes(row, col - 1, columns, false);
        _CheckAdjacentShapes(row + 1, col, rows, true);
        _CheckAdjacentShapes(row - 1, col, rows, true);
    }

    private void _CheckAdjacentShapes(int r, int c, int constraint, bool isRowChanging)
    {
        GameObject[,] shapeMatrix = BoardManager.Instance.GetShapeMatrix();
        int temp;

        if (isRowChanging)
            temp = r;
        else
            temp = c;

        if (temp < constraint && temp >= 0)
        {
            if (shapeMatrix[r, c] != null && !BoardManager.Instance.IsShapeCheckedBefore(shapeMatrix[r, c].GetComponent<Shape>()) &&
                shapeMatrix[r, c].GetComponent<Shape>()._shapeData.ShapeType == _shapeData.ShapeType)
            {
                BoardManager.Instance.AddShapeToAdjacentShapes(shapeMatrix[r, c].GetComponent<Shape>());
                shapeMatrix[r, c].GetComponent<Shape>().CheckAdjacentShapes(false);
>>>>>>> Stashed changes
            }
        }
    }

    public void ShiftDown(bool forRefill = false)
    {
        int rowToShift;

        if (forRefill)
        {
            rowToShift = FindEmptyRow(BoardManager.Instance.GetRowCount() - 1);
            HandleShiftDownForRefill(rowToShift);
        }
        else
        {
<<<<<<< Updated upstream
            rowToShift = FindEmptyRow(_row);
=======
            rowToShift = FindEmptyRow(row);
>>>>>>> Stashed changes
            HandleShiftDown(rowToShift);
        }
    }

    private int FindEmptyRow(int rowIndex)
    {
<<<<<<< Updated upstream
        Shape[,] shapeMatrix = BoardManager.Instance.GetShapeMatrix();
=======
        GameObject[,] shapeMatrix = BoardManager.Instance.GetShapeMatrix();
>>>>>>> Stashed changes
        int rowToShift = -1;

        for (int i = rowIndex; i >= 0; i--)
        {
<<<<<<< Updated upstream
            if (shapeMatrix[i, _col] == null)
=======
            if (shapeMatrix[i, col] == null)
>>>>>>> Stashed changes
                rowToShift = i;
        }

        return rowToShift;
    }

    private void HandleShiftDown(int rowToShift)
    {
        if (rowToShift != -1)
        {
<<<<<<< Updated upstream
            Shape[,] shapeMatrix = BoardManager.Instance.GetShapeMatrix();

            shapeMatrix[rowToShift, _col] = this;
            shapeMatrix[_row, _col] = null;
=======
            GameObject[,] shapeMatrix = BoardManager.Instance.GetShapeMatrix();

            shapeMatrix[rowToShift, col] = this.gameObject;
            shapeMatrix[row, col] = null;
>>>>>>> Stashed changes
            Shift(rowToShift, TimeShiftDown);
        }
    }

    private void HandleShiftDownForRefill(int rowToShift)
    {
        if (rowToShift != -1)
        {
<<<<<<< Updated upstream
            Shape[,] shapeMatrix = BoardManager.Instance.GetShapeMatrix();

            shapeMatrix[rowToShift, _col] = this;
=======
            GameObject[,] shapeMatrix = BoardManager.Instance.GetShapeMatrix();

            shapeMatrix[rowToShift, col] = this.gameObject;
>>>>>>> Stashed changes
            Shift(rowToShift, TimeRefillShiftDown);
        }
    }

    private void Shift(int rowToShift, float shiftDownTime)
    {
<<<<<<< Updated upstream
        if(_shapeState == ShapeState.Shifting)
        {
            _shiftDownSequence.Kill();
            _row = FindCurrentRow();
        }
        else
        {
            _shapeState = ShapeState.Shifting;
        }

        _shiftDownSequence = DOTween.Sequence();

        Vector2 offset = _shapeSpriteRenderer.bounds.size;

        float posToShift = offset.y * rowToShift;

        _shiftDownSequence.Append(transform.DOLocalMoveY(posToShift, shiftDownTime * (_row - rowToShift)).SetEase(Ease.InQuad)).OnComplete(() =>
        {
            BounceShape(transform.position.y + BounceAmount);
            _shapeState = ShapeState.Waiting;
        });
     
        
        _row = rowToShift;
        _shapeSpriteRenderer.sortingOrder = _row + 1;
    }

    private int FindCurrentRow()
    {
        int currentRow;
        Vector2 offset = _shapeSpriteRenderer.bounds.size;
        currentRow = Mathf.RoundToInt(transform.localPosition.y / offset.y);
        return currentRow;
=======
        Vector2 offset = _shapeSpriteRenderer.bounds.size;

        Vector3 posToShift = transform.position;
        posToShift.y -= offset.y * (row - rowToShift);

        transform.DOMove(posToShift, shiftDownTime * (row - rowToShift)).SetEase(Ease.InQuad).OnComplete(() =>
        {
            BounceShape(transform.position.y + BounceAmount);
        });

        row = rowToShift;
>>>>>>> Stashed changes
    }

    private void BounceShape(float pos)
    {
        transform.DOMoveY(pos, TimeBounce).SetEase(Ease.OutQuad).SetLoops(2, LoopType.Yoyo);
    }

    public abstract void Explode();
<<<<<<< Updated upstream

    public abstract void Merge();
=======
>>>>>>> Stashed changes
}
