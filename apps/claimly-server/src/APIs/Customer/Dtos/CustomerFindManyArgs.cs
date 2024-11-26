using Claimly.APIs.Common;
using Claimly.Infrastructure.Models;
using Microsoft.AspNetCore.Mvc;

namespace Claimly.APIs.Dtos;

[BindProperties(SupportsGet = true)]
public class CustomerFindManyArgs : FindManyInput<Customer, CustomerWhereInput> { }
