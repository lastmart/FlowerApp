﻿namespace FlowerApp.Domain.Models.RecommendationModels;

public class Survey
{
    public string UserId { get; set; }
    public List<SurveyAnswer> Answers { get; set; } = new();
}