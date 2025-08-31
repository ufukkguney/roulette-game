using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VContainer;

public class GameSequenceManager
{
    #region Fields
    private List<Transform> spawnPoints;
    private List<CircleRow> activeRows;
    [Inject] private ItemManager itemManager;
    private WalletController walletController;
    #endregion

    #region Public API
    public void SetParams(List<Transform> spawnPoints, List<CircleRow> activeRows, WalletController walletController)
    {
        this.spawnPoints = spawnPoints;
        this.activeRows = activeRows;
        this.walletController = walletController;
    }

    public IEnumerator SelectAndPlaySequence(float delay, Action onSelectComplete = null)
    {
        int spawnCount = spawnPoints.Count;
        int repeatCount = UnityEngine.Random.Range(0, Constants.SelectRepeatCCount);
        CircleRow selectedRow = GetRandomUnselectedRow(ref repeatCount, spawnCount);
        walletController.AddItem(selectedRow.ItemType);

        yield return RunGlowSequence(spawnCount, repeatCount, selectedRow, delay);

        onSelectComplete?.Invoke();
    }

    public void Reset()
    {
        spawnPoints = null;
        activeRows = null;
        walletController = null;
    }
    #endregion

    #region Sequence Logic
    private IEnumerator RunGlowSequence(int spawnCount, int repeatCount, CircleRow selectedRow, float delay)
    {
        for (int i = 0; i <= repeatCount; i++)
        {
            int index = i % spawnCount;
            var glow = itemManager.GetCircleGlow(spawnPoints[index]);
            if (glow == null)
                continue;

            Action onGlowComplete = () => itemManager.ReturnCircleGlow(glow);

            if (i == repeatCount)
                HandleLastGlow(glow, spawnPoints[index], selectedRow, onGlowComplete);
            else
                glow.Select(spawnPoints[index], onGlowComplete);

            yield return new WaitForSeconds(delay);
        }
    }

    private CircleRow GetRandomUnselectedRow(ref int repeatCount, int spawnCount)
    {
        int selectedRowIndex = repeatCount % spawnCount;
        CircleRow selectedRow = activeRows[selectedRowIndex];
        if (selectedRow.IsSelected)
        {
            for (int i = 0; i < activeRows.Count; i++)
            {
                if (!activeRows[i].IsSelected)
                {
                    repeatCount = i;
                    return activeRows[i];
                }
            }
            return selectedRow;
        }
        return selectedRow;
    }
    #endregion

    #region Event & Utility
    private void HandleLastGlow(CircleGlow glow, Transform target, CircleRow selectedRow, Action onGlowComplete)
    {
        glow.MakeBlink(Constants.BlinkAmount, target, () =>
        {
            selectedRow.SelectRow();
            var args = new ItemEarnedEventArgs(
                itemManager.GetSpriteDependsOnType(selectedRow.ItemType),
                selectedRow.ItemType.ToString(),
                HasUnselectedItem()
            );
            EventManager.InvokeItemEarned(args);
            onGlowComplete();
        });
    }

    private bool HasUnselectedItem()
    {
        foreach (var row in activeRows)
        {
            if (!row.IsSelected)
                return true;
        }
        return false;
    }
    #endregion
}
