using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class QuestionManager : MonoBehaviour
{
    public static QuestionManager Instance;

    [Header("UI")]
    public GameObject questionPanel;
    public TextMeshProUGUI questionText;

    private Question currentQuestion;

    [System.Serializable]
    public class Question
    {
        public string text;
        public bool answer;
    }

    [Header("Preguntas de Ciberseguridad")]
    public Question[] questions = new Question[5];

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

        questionPanel.SetActive(false);
    }

    public void ShowRandomQuestion()
    {
        int index = Random.Range(0, questions.Length);
        currentQuestion = questions[index];

        questionText.text = currentQuestion.text;
        questionPanel.SetActive(true);

        Time.timeScale = 0f; 
    }

    public void AnswerTrue()
    {
        CheckAnswer(true);
    }

    public void AnswerFalse()
    {
        CheckAnswer(false);
    }

    void CheckAnswer(bool playerAnswer)
    {
        questionPanel.SetActive(false);
        Time.timeScale = 1f;

        if (playerAnswer == currentQuestion.answer)
        {
            Debug.Log("✔ Respuesta correcta!");
            RoundManager.Instance.ShowFinalVictory(); 
        }
        else
        {
            Debug.Log("❌ Respuesta incorrecta. Reiniciando...");
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
