using Application.Entities;
using Application.Features;
using Application.Features.Accounts;

namespace Application.Common.Interfaces.Mappings;

public interface IAccountMapper
{
    Account FromRequest(CreateAccount.Request request);

    Account FromRequest(UpdateAccount.Request request);
}
