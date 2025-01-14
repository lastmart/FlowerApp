using System.Text.Json;
using AutoMapper;
using AppQuestion = FlowerApp.Domain.Models.RecommendationModels.SurveyQuestion;
using DbQuestion = FlowerApp.Data.DbModels.Surveys.SurveyQuestion;

namespace FlowerApp.Service.Common.Mappers;

public class QuestionProfile : Profile
{
    public QuestionProfile()
    {
        CreateMap<DbQuestion, AppQuestion>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Text, opt => opt.MapFrom(src => src.Text))
            .ForMember(dest => dest.Variants, opt => opt.MapFrom(src =>
                src.Variants.Split(';', StringSplitOptions.RemoveEmptyEntries)))
            .ForMember(dest => dest.QuestionType, opt => opt.MapFrom(src => src.QuestionType));
    }
}