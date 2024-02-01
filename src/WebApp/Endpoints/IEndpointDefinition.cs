namespace DevTalk.WebApp.Endpoints;

public interface IEndpointDefinition
{
    void AddEndpoints(IEndpointRouteBuilder builder);
}