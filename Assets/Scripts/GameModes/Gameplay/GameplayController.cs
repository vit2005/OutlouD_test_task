using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class GameplayController : MonoBehaviour
{
    [SerializeField] GameObject prefab;
    [SerializeField] Transform spawnPoint;
    [SerializeField] CountdownTimer timer;
    [SerializeField] List<Texture> textures;
    [SerializeField] Button shuffleButton;

    [Inject] private TableGenerator _tableGenerator;
    [Inject] private AppControllerStorage _appControllerStorage;

    private List<GameObject> _cardsInstances = new List<GameObject>();
    private List<Card> _cards = new List<Card>();
    private Card _openedCard;
    private bool _openingLocked = true;

    private const float ANIMATION_DURATION = 2f;
    private const float CARDS_TIME_MULTIPLIER = 5f;

    public void InitTable(int count)
    {
        timer.Init(count * CARDS_TIME_MULTIPLIER);
        var points = _tableGenerator.GenerateSpawnPoints(count);
        points.Shuffle();
        int code = 0;
        for (int i = 0; i < points.Count; i++)
        {
            var instance = Instantiate(prefab, spawnPoint);
            instance.transform.localPosition = points[i];
            _cardsInstances.Add(instance);
            var card = instance.GetComponent<Card>();
            code = i / 2;
            card.Init(code, textures[code]);
            card.Open();
            _cards.Add(card);
        }

        shuffleButton.interactable = true;
        StartCoroutine(CloseAll(_cards, () => StartGame()));
    }

    private void StartGame()
    {
        timer.StartTimer();
        timer.timeOut += Defeat;
    }

    private IEnumerator CloseAll(List<Card> cardsToClose, Action onFinished = null, float? matchTime = null)
    {
        yield return new WaitForSeconds(ANIMATION_DURATION);
        foreach (var card in cardsToClose) { card.Close(); }
        onFinished?.Invoke();
        _openingLocked = false;
    }

    public void OnClick(Vector2 pointerPos)
    {
        if (_openingLocked) return;
        var ray = AppController.Instance.Camera.ScreenPointToRay(pointerPos);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            var card = hit.transform.GetComponentInParent<Card>();
            if (card == null) return;
            if (card.isOpen) return;
            card.Open();
            if (_openedCard == null)
            {
                _openedCard = card;
                return;
            }
            if (_openedCard.code != card.code)
            {
                StartCoroutine(CloseTwoCards(_openedCard, card));
            }
            else
            {
                _openedCard = null;
                CheckVictory();
            }
        }
    }

    private IEnumerator CloseTwoCards(Card card1, Card card2)
    {
        _openingLocked = true;
        yield return new WaitForSeconds(ANIMATION_DURATION);
        card1.Close();
        card2.Close();
        _openedCard = null;
        _openingLocked = false;
    }

    public void Shuffle()
    {
        shuffleButton.interactable = false;
        _openingLocked = true;
        List<Card> cards = _cards.Where(x => !x.isOpen).ToList();
        List<Vector3> positions = cards.Select(x => x.transform.position).ToList();
        positions.Shuffle();
        for (int i = 0; i < cards.Count; i++)
        {
            cards[i].transform.position = positions[i];
            cards[i].Open();
        }
        StartCoroutine(CloseAll(cards));
    }

    private void CheckVictory()
    {
        if (_cards.All(x => x.isOpen))
        {
            _openingLocked = true;
            StartCoroutine(VictoryScenario());
        }
    }

    private void Defeat()
    {
        StartCoroutine(DefeatScenario());
    }

    private IEnumerator VictoryScenario()
    {
        timer.StopTimer();
        _appControllerStorage.SaveLevel(timer.remainingTime);
        yield return new WaitForSeconds(1f);
        GravityFallCards();
        yield return new WaitForSeconds(2f);
        AppController.Instance.modes.SetGameMode(GameModeId.MENU);
    }

    private IEnumerator DefeatScenario()
    {
        yield return new WaitForSeconds(1f);
        GravityFallCards();
        yield return new WaitForSeconds(2f);
        AppController.Instance.modes.SetGameMode(GameModeId.MENU);
    }

    private void GravityFallCards()
    {
        foreach (var card in _cardsInstances)
        {
            var rb = card.GetComponent<Rigidbody>();
            rb.isKinematic = false;
            rb.AddExplosionForce(100f, spawnPoint.position, 10f);
        }
    }

    public void OnExit()
    {
        timer.StopTimer();
        timer.timeOut -= Defeat;
        for (int i = _cardsInstances.Count - 1; i >= 0; i--)
        {
            GameObject.Destroy(_cardsInstances[i]);
        }
        _cardsInstances.Clear();
        _cards.Clear();

    }
}
