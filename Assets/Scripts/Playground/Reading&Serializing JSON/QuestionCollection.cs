using System.Collections;
using System.Collections.Generic;
using System;
using System.Text;
using UnityEngine;

[Serializable]
public class QuestionCollection
{
    public Question[] questions;
    public string collectionName;

    public override string ToString()
    {
        string result = "QUESTIONS\n";
        StringBuilder sb = new StringBuilder();
        foreach (Question question in questions)
        {
            sb.Append(string.Format($"Question: {question.text}\nAnswer: {question.answer}\n"));
        }

        return result + sb.ToString();
    }
}
