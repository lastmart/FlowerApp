using AutoMapper;
using DbModels = FlowerApp.Domain.DbModels;
using RecommendationsModels =FlowerApp.Domain.ApplicationModels.RecommendationsModels;

namespace FlowerApp.Service.Common.Mappers;

public class QuestionProfile : Profile
{
    public QuestionProfile()
    {
        CreateMap<DbModels.Question, RecommendationsModels.Question>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.QuestionText, opt => opt.MapFrom(src => src.QuestionText))
            .ForMember(dest => dest.AnswerOptions, opt => opt.MapFrom(src => src.AnswerOptions))
            .ForMember(dest => dest.AnswerOptions, opt => opt.MapFrom(src => src.AnswerOptions.Take(src.AnswerSize).ToList()));
    }
}