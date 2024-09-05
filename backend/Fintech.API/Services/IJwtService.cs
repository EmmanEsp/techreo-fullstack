namespace Fintech.API.Services;

public interface IJwtService {
    string GenerateJwtToken(Guid customerId);
}
