using System;
using UnityEngine;

public class ProgressController
{
    private ProgressUI progressUI;

    private float duration;

    private float timer;

    public bool isRunning {  get; private set; }

    private Action onComplete;

    public ProgressController(ProgressUI progressUI)
    {
        this.progressUI = progressUI;
    }

    public void Begin(float duration, Action onComplete)
    {
        if (isRunning)
        {
            return;
        }

        this.duration = duration;
        this.onComplete = onComplete;

        timer = 0f;

        isRunning = true;

        progressUI.Show();
    }

    public void Tick()
    {
        if (!isRunning)
        {
            return;
        }

        timer += Time.deltaTime;

        float progress = timer / duration;

        progressUI.SetProgress(progress);

        if (timer >= duration)
        {
            Complete();
        }
    }

    public void Cancel()
    {
        isRunning = false;
        if (progressUI)
        {
            progressUI.Hide();
        }
    }

    private void Complete()
    {
        isRunning = false;

        progressUI.Hide();

        onComplete?.Invoke();
    }
}
