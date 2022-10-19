using AutoMapper;
using InterviewTask.Api.Models.Dtos;
using InterviewTask.Api.Models.Entities;

namespace InterviewTask.Api;

public class InterviewTaskMapperConfiguration : Profile
{
    public InterviewTaskMapperConfiguration()
    {
        // To handle EFCore change detector issue, we need to disable creating new ids.
        CreateMap<TodoDto, Todo>()
        .ForMember(dest => dest.TodoId, opt => opt.Ignore());

        CreateMap<Todo,TodoDto>();
    }
}