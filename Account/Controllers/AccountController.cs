using Account.Models;
using Microsoft.AspNetCore.Mvc;
using Account.Services.Interfaces;

namespace Account.Controllers;

[ApiController]
[Route("[controller]")]
public class AccountController : ControllerBase {
    private readonly IStandardAccount _standardService;
    private readonly IMerchantAccount _merchantService;

    public AccountController(IStandardAccount standardService, IMerchantAccount merchantService) {
        _standardService = standardService;
        _merchantService = merchantService;
    }
    
    [Route("standard/create")]
    [HttpPost()]
    public ActionResult<StandardAccountModel> CreateStandardAccount(StandardAccount standardAccount) {
        var account = _standardService.Create(standardAccount);

        return account;
    }

    [Route("merchant/create")]
    [HttpPost()]
    public ActionResult<MerchantAccountModel> CreateMerchantAccount(MerchantAccount merchantAccount) {
        var account = _merchantService.Create(merchantAccount);

        return account;
    }
}
