﻿using Domain.Contracts;
using Microsoft.AspNetCore.Identity;

namespace Domain.App.Identity;

public class AppRole : IdentityRole<Guid>, IDomainEntityId
{
    
}