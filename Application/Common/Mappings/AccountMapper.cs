using Application.Common.Interfaces.Mappings;
using Application.Entities;
using Application.Features;
using Application.Features.Accounts;
using Riok.Mapperly.Abstractions;

namespace Application.Common.Mappings;

[Mapper]
public partial class AccountMapper : IAccountMapper
{
    public partial Account FromRequest(CreateAccount.Request request);

    public partial Account FromRequest(UpdateAccount.Request request);
}
