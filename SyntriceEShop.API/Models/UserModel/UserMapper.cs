using SyntriceEShop.API.Models.UserModel.DTO;

namespace SyntriceEShop.API.Models.UserModel;

public static class UserMapper
{
    public static GetUserResponse ToGetUserResponse(this User user)
    {
        return new GetUserResponse()
        {
            Id = user.Id,
            Username = user.Username
        };
    }
}