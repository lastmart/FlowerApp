// using AutoMapper;
// using FlowerApp.Data.DbModels.Surveys;
// using Question = FlowerApp.Domain.Models.RecommendationModels.Question;
//
// namespace FlowerApp.Service.Common.Mappers;
//
// public class QuestionProfile : Profile
// {
//     public QuestionProfile()
//     {
//         CreateMap<Survey, Question>()
//             .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
//             .ForMember(dest => dest.QuestionText, opt => opt.MapFrom(src => src.Question))
//             .ForMember(dest => dest.AnswerOptions, opt => opt.MapFrom(src => src.AnswerOptions))
//             .ForMember(dest => dest.AnswerOptions, opt => opt.MapFrom(src => src.AnswerOptions.Take(src.Answer).ToList()));
//     }
// }