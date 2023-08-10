using MediatR;

namespace DevFreela.Application.Projects.Commands.FinishProject;

public class FinishProjectCommand : IRequest<Unit>
{
    public FinishProjectCommand(int id)
    {
        Id = id;
    }

    public int Id { get; private set; }
}