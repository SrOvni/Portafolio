using System.Threading;
using System;
using System.Collections;
using TMPro;
using UnityEngine.UI;
using UnityEngine;
using R3;

public class ReactiveExample : MonoBehaviour
{
    [SerializeField] TMP_Text counterText;
    IDisposable subscription;
    CancellationTokenSource cts;
    [SerializeField] private Button cancelButton;
    [SerializeField] private Button coinButton;

    Subject<Unit> _clickSubject = new();

    private void Awake()
    {
        counterText.text = "0";
        cts = new CancellationTokenSource();
        cancelButton.onClick.AddListener(() => cts.Cancel());
    }

    private void Start()
    {
        coinButton.onClick.AsObservable(cts.Token).Subscribe(_ =>
        {
            Debug.Log("Hola mundo");
        });
    }

    void Oestroy()
    {
        subscription?.Dispose();
        cts?.Dispose();
    }


}

public static class ButtonClickedEventExtensions
{
    public static Observable<Unit> AsObservable(this Button.ButtonClickedEvent onClickEvent, CancellationToken cancellationToken)
    {
        var subject = new Subject<Unit>();
        cancellationToken.Register(() =>
        {
            onClickEvent.RemoveListener(OnClick);
            subject.OnCompleted();
            subject.Dispose();
        });


        void OnClick()
        {
            subject.OnNext(default);
        }

        return subject.AsObservable();
    }
}