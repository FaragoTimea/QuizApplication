using AutoMapper;
using QuizApplication.Data;

namespace QuizApplication.DTO
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {

            CreateMap<Quiz, QuizDTO>();
            CreateMap<QuizDTO, Quiz>();

            CreateMap<Question, QuestionDTO>()
                .ForMember(dto => dto.Type,
                opt => opt.MapFrom(entity => entity.QuestionType.ToString()));
            CreateMap<QuestionDTO, Question>()
                .ForMember(entity => entity.QuestionType,
                opt => opt.MapFrom(dto => dto.Type));

            CreateMap<AnswerOption, AnswerOptionDTO>();
            CreateMap<AnswerOptionDTO, AnswerOption>();
        }
    }
}
