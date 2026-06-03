using Application.Categories.Dtos;
using MediatR;

namespace Application.Categories.Commands;

public class GetCategoriesCommand : IRequest<List<CategoryDto>>
{
}