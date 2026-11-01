namespace Ragkivio.Graphql.Types.Project;

public static class ProjectCreationMutation
{
    extension(IObjectTypeDescriptor<Mutation> descriptor)
    {
        public IObjectTypeDescriptor<Mutation> CreateProject()
        {
            return descriptor;
        }
    }
}