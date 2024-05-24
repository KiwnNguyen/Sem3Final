using AccpSem3.Models.Repository;
using Newtonsoft.Json;
using Sem3Final.Models.ModelsView;
using Sem3Final.Models.ModelsView.ModelJoin;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sem3Final.Models.Linear
{
    public class LinearRegressionModel
    {
        private double[] coefficients;

        public struct TrainingResult
        {
            public double[] Coefficients;
            public int Score;
        }

        public TrainingResult Train(IEnumerable<QuestionJoin> trainingData, List<string> answers)
        {
            List<QuestionJoin> trainingDataList = trainingData.ToList();
            int numFeatures = trainingDataList[0].examination.title.Length;

            int numInstances = trainingDataList.Count;

            double[][] x = new double[numInstances][];
            double[] y = new double[numInstances];
            int score = 0;
            int score1 = 0;
            int score2 = 0;
            int totalScore = 0;
            // Chuyển đổi dữ liệu huấn luyện thành ma trận đặc trưng x và vector giá trị y
            foreach (string listanswers in answers)
            {
                for (int i = 0; i < numInstances; i++)
                {
                    x[i] = new double[numFeatures];
                    for (int j = 0; j < numFeatures; j++)
                    {
                        if (j < trainingDataList[i].examination.title.Length)
                        {
                            x[i][j] = (double)trainingDataList[i].examination.title[j];// Chuyển đổi ký tự thành giá trị số
                        }
                        else
                        {
                            string t = "";
                        }
                    }
                    y[i] = trainingDataList[i].answer.Is_correct1 ? 1.0 : 0.0; // Gán giá trị đúng/sai cho vector giá trị y
                                                                               // Kiểm tra đáp án đúng/sai
                    if (trainingDataList[i].answer.title == listanswers)
                    {
                        if (trainingDataList[i].category.id == 2)
                        {
                            if (trainingDataList[i].answer.is_correct == 1)
                            {
                                score++;
                            }
                        }
                        if (trainingDataList[i].category.id == 3)
                        {
                            if (trainingDataList[i].answer.is_correct == 1)
                            {
                                score1++;
                            }
                        }
                        if (trainingDataList[i].category.id == 4)
                        {
                            if (trainingDataList[i].answer.is_correct == 1)
                            {
                                score2++;
                            }
                        }

                    }
                }
            }

            HttpContext.Current.Session["ScorePhanI"] = score;
            HttpContext.Current.Session["ScorePhanII"] = score1;
            HttpContext.Current.Session["ScorePhanIII"] = score2;
            totalScore = score + score1 + score2;

            string account = HttpContext.Current.Session["AccountNameCadidate"] as string;
            IEnumerable<ScoreResultCadi> values = AnswerRepositories.Instance.GetResultCadi(account);
            string questions = null;
            string answersJson = "[";
            foreach (ScoreResultCadi value in values)
            {
                string question = value.question.title;
                string answer = value.answer.title;
                if (questions == null)
                {
                    questions = question;
                }
                // Thêm giá trị answer vào chuỗi JSON
                answersJson += "{";
                answersJson += "\"question\": \"" + question + "\",";
                answersJson += "\"answer\": \"" + answer + "\"";
                answersJson += "},";
            }
            // Xóa dấu "," cuối cùng trong chuỗi JSON
            if (answersJson.EndsWith(","))
            {
                answersJson = answersJson.Remove(answersJson.Length - 1);
            }
            answersJson += "]";

            // submit Answer of Candidate

            List<Dictionary<string, string>> answerList = new List<Dictionary<string, string>>();
            foreach (string answer in answers)
            {
                Dictionary<string, string> answerDict = new Dictionary<string, string>();
                answerDict.Add("answer", answer);
                answerList.Add(answerDict);
            }

            string answerJsonOfCandidate = JsonConvert.SerializeObject(answerList);


            if (values != null && answerJsonOfCandidate != null)
            {
                List<CadidateView> caivi = CandidateRepositories.Instance.GetById(account);
                int id = 0;
                foreach (CadidateView te in caivi)
                {
                    id = te.id;
                }
                CandidateRepositories.Instance.UpdateCadi(id, totalScore, answersJson, answerJsonOfCandidate);
            }
            // Huấn luyện mô hình hồi quy tuyến tính
            //OrdinaryLeastSquares ols = new OrdinaryLeastSquares();
            //MultipleLinearRegression regression = ols.Learn(x, y);
            // Lấy các hệ số hồi quy từ mô hình
            //coefficients = regression.Weights;
            HttpContext.Current.Session["ScoreCadi"] = totalScore;
            return new TrainingResult {  Score = score };
        }
    }
}