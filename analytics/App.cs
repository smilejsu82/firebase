using Firebase.Analytics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class App : MonoBehaviour
{
    [SerializeField]
    private Button btnGuestLogin;
    [SerializeField]
    private Button btnKakaoLogin;
    [SerializeField]
    private Button btnLevelClear;
    [SerializeField]
    private Text txtLevelClear;
    [SerializeField]
    private Dropdown dropdown;
    [SerializeField]
    private Button btnPostScore;
    [SerializeField]
    private Text txtScore;

    private string[] arr = { "A", "B", "C" };
    private int targetLevelIdx = 0;

    // Start is called before the first frame update
    void Start()
    {
        dropdown.onValueChanged.AddListener((index) =>
        {
            this.targetLevelIdx = index;
        });

        Firebase.Analytics.FirebaseAnalytics.LogEvent(Firebase.Analytics.FirebaseAnalytics.EventAppOpen);

        this.btnGuestLogin.onClick.AddListener(() =>
        {
            Firebase.Analytics.FirebaseAnalytics.LogEvent(AnalyticsConstants.EVENT_GUEST_LOGIN);
        });

        this.btnKakaoLogin.onClick.AddListener(() =>
        {
            Firebase.Analytics.FirebaseAnalytics.LogEvent("EventKakaoLogin");
        });

        this.btnLevelClear.onClick.AddListener(() =>
        {
            var randSec = Random.Range(10, 61);
            var millisec = System.TimeSpan.FromSeconds(randSec).TotalMilliseconds;
            this.txtLevelClear.text = string.Format("{0} {1}", this.arr[targetLevelIdx], millisec.ToString());
            Firebase.Analytics.FirebaseAnalytics.LogEvent(AnalyticsConstants.EVENT_LEVEL_CLEAR, this.arr[targetLevelIdx], millisec);
        });

        this.btnPostScore.onClick.AddListener(() =>
        {
            var randScore = Random.Range(100, 1001);
            this.txtScore.text = randScore.ToString();
            Parameter[] parameters = { new Parameter(FirebaseAnalytics.ParameterScore, randScore) };
            Firebase.Analytics.FirebaseAnalytics.LogEvent(Firebase.Analytics.FirebaseAnalytics.EventPostScore, parameters);
        });
    }

    // Update is called once per frame
    void Update()
    {

    }
}
