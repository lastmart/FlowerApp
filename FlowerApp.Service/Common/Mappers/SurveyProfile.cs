using AutoMapper;
using DtoSurvey = FlowerApp.DTOs.Common.Surveys.Survey;
using AppSurvey = FlowerApp.Domain.Models.RecommendationModels.Survey;
using DbSurvey = FlowerApp.Data.DbModels.Surveys.Survey;
using DtoAnswer = FlowerApp.DTOs.Common.Surveys.SurveyAnswer;
using AppAnswer = FlowerApp.Domain.Models.RecommendationModels.SurveyAnswer;
using DbAnswer = FlowerApp.Data.DbModels.Surveys.SurveyAnswer;

namespace FlowerApp.Service.Common.Mappers;

public class SurveyProfile : Profile
{
    public SurveyProfile()
    {
        CreateMap<DtoSurvey, AppSurvey>()
            .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId))
            .ForMember(dest => dest.Answers, opt => opt.MapFrom(src => src.Answers))
            .ReverseMap();

        CreateMap<DtoAnswer, AppAnswer>()
            .ForMember(dest => dest.QuestionId, opt => opt.MapFrom(src => src.QuestionId))
            .ForMember(dest => dest.QuestionMask, opt => opt.MapFrom(src => src.QuestionsMask))
            .ReverseMap();

        CreateMap<AppSurvey, DbSurvey>()
            .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId))
            .ForMember(dest => dest.Answers, opt => opt.MapFrom(src => src.Answers))
            .ReverseMap();

        CreateMap<AppAnswer, DbAnswer>()
            .ForMember(dest => dest.QuestionId, opt => opt.MapFrom(src => src.QuestionId))
            .ForMember(dest => dest.QuestionsMask, opt => opt.MapFrom(src => string.Join(';', src.QuestionMask)));

        CreateMap<DbAnswer, AppAnswer>()
            .ForMember(dest => dest.QuestionId, opt => opt.MapFrom(src => src.QuestionId))
            .ForMember(
                dest => dest.QuestionMask,
                opt => opt.MapFrom(src => src.QuestionsMask.Split(';', StringSplitOptions.RemoveEmptyEntries))
            );
    }
}