using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using TMPro;
using Newtonsoft.Json;


public class QuestionLoader : MonoBehaviour
{

    // path to the json file
    string questionPath =
    "Assets/Scripts/Playground/Reading&Serializing JSON/questions.json";

    // collection of questions
    QuestionCollection questionCollection;
    TextMeshProUGUI words;
    [SerializeField] bool shouldFormat = false;

    void Awake()
    {
        words = FindObjectOfType<TextMeshProUGUI>();
    }

    [ContextMenu("Load Questions")]
    void LoadQuestions()
    {
        // reads the file with path "questionPath" and closes it automatically
        // after its done 
        using (StreamReader reader = new StreamReader(questionPath))
        {
            // extract and store the entire file in a string
            string json = reader.ReadToEnd();

            // deserialize that string data into our question collection object
            questionCollection = JsonUtility.FromJson<QuestionCollection>(json);
        }

        print("Questions loaded: " + questionCollection.questions.Length);
        words.text = questionCollection.ToString();
    }

    [ContextMenu("Write Sample Questions")]
    void WriteSampleQuestions()
    {
        GenerateSampleQuestions();

        // writes data to the file with path "question path"
        using (StreamWriter writer = new StreamWriter(questionPath))
        {
            // gets the data back in the form of a string and writes it
            // back to the json file
            string json = JsonUtility.ToJson(questionCollection, shouldFormat);
            // string json = JsonConvert.SerializeObject(questionCollection, Formatting.Indented);
            writer.Write(json);
        }
    }

    void GenerateSampleQuestions()
    {
        // create new questions and store them into question collection
        Question[] newQuestions = new Question[2] {
            new Question() { text = "what month is my birthday?", answer = "september" },
            new Question() { text = "how are you?", answer = "fine, thanks :D" }
        };

        questionCollection = new QuestionCollection() {
            questions = newQuestions, collectionName = "samples"
        };
    }
}
